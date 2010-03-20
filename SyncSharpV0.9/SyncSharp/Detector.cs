using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SyncSharp.Storage;
using SyncSharp.DataModel;
using System.IO;

namespace SyncSharp.Business
{
	public class Detector
	{
		public CustomDictionary<string, string, FileUnit> sCleanFiles;
		public CustomDictionary<string, string, FileUnit> sDirtyFiles;
		public CustomDictionary<string, string, FileUnit> tCleanFiles;
		public CustomDictionary<string, string, FileUnit> tDirtyFiles;
		public CustomDictionary<string, string, FileUnit> sDirtyDirs;
		public CustomDictionary<string, string, FileUnit> sCleanDirs;
		public CustomDictionary<string, string, FileUnit> tDirtyDirs;
		public CustomDictionary<string, string, FileUnit> tCleanDirs;
		public FileList sourceList;
		public FileList destList;
		private SyncTask task;
		private long srcDirtySize, tgtDirtySize;
		private bool openFileDetected;
		private CustomDictionary<string, string, FileUnit> sMetaData, tMetaData;

		public long SrcDirtySize
		{
			get { return srcDirtySize; }
			set { srcDirtySize = value; }
		}
		public long TgtDirtySize
		{
			get { return tgtDirtySize; }
			set { tgtDirtySize = value; }
		}
		public bool OpenFileDetected
		{
			get { return openFileDetected; }
			set { openFileDetected = value; }
		}

		public Detector(String machineID, SyncTask syncTask)
		{
			sCleanFiles = new CustomDictionary<string, string, FileUnit>();
			sDirtyFiles = new CustomDictionary<string, string, FileUnit>();
			tCleanFiles = new CustomDictionary<string, string, FileUnit>();
			tDirtyFiles = new CustomDictionary<string, string, FileUnit>();
			sDirtyDirs = new CustomDictionary<string, string, FileUnit>();
			sCleanDirs = new CustomDictionary<string, string, FileUnit>();
			tDirtyDirs = new CustomDictionary<string, string, FileUnit>();
			tCleanDirs = new CustomDictionary<string, string, FileUnit>();
			task = syncTask;
			sMetaData = SyncMetaData.ReadMetaData(@".\Profiles\" + machineID + @"\" + syncTask.Name + "src.meta");
            //tMetaData = SyncMetaData.ReadMetaData(@".\Profiles\" + machineID + @"\" + syncTask.Name + "src.meta");
            tMetaData = SyncMetaData.ReadMetaData(@".\Profiles\" + machineID + @"\" + syncTask.Name + "tgt.meta");
			srcDirtySize = 0;
			tgtDirtySize = 0;
		}

		public bool isSynchronized()
		{
			return (sDirtyFiles.Primary.Count == 0 && tDirtyFiles.Primary.Count == 0 && sDirtyDirs.Primary.Count == 0 &&
							tDirtyDirs.Primary.Count == 0);
		}

		public bool metaDataExists()
		{
			return (sMetaData != null && tMetaData != null);
		}

		public void compareFolders()
		{
			int sRevPathLen = task.Source.Length;
			int tRevPathLen = task.Target.Length;

			List<FileUnit> srcFiles = new List<FileUnit>();
			Stack<string> stack = new Stack<string>();
			stack.Push(task.Source);
			getCurrentSrcInfo(srcFiles, stack);

			List<FileUnit> destFiles = new List<FileUnit>();
			stack.Push(task.Target);
			getCurrentTgtInfo(destFiles, stack);

			compareSrcFileUnits(sRevPathLen, srcFiles);
			compareTgtFileUnits(tRevPathLen, destFiles);

			addSrcDeletionToList();
			addTgtDeletionToList();

			createFileLists();
		}

		private void getCurrentSrcInfo(List<FileUnit> srcFiles, Stack<string> stack)
		{
			while (stack.Count > 0)
			{
				string folder = stack.Pop();
				foreach (string fileName in Directory.GetFiles(folder))
				{
					try
					{
						FileStream stream = File.OpenRead(fileName);
						stream.Close();
                        if (!task.Filters.isFileExcluded(new FileInfo(fileName)))
                            srcFiles.Add(new FileUnit(fileName));
					}
					catch
					{
						openFileDetected = true;
						break;
					}
				}

				foreach (string folderName in Directory.GetDirectories(folder))
				{
					if (!task.Filters.isSourceDirExcluded(folderName))
					{
						stack.Push(folderName);
						srcFiles.Add(new FileUnit(folderName));
					}
				}
			}
		}

		private void getCurrentTgtInfo(List<FileUnit> destFiles, Stack<string> stack)
		{
			while (stack.Count > 0)
			{
				string folder = stack.Pop();
				foreach (string fileName in Directory.GetFiles(folder))
				{
					try
					{
						FileStream stream = File.OpenRead(fileName);
						stream.Close();
                        if (!task.Filters.isFileExcluded(new FileInfo(fileName)))
							destFiles.Add(new FileUnit(fileName));
					}
					catch
					{
						openFileDetected = true;
						break;
					}
				}

				foreach (string folderName in Directory.GetDirectories(folder))
				{
					if (!task.Filters.isTargetDirExcluded(folderName))
					{
						stack.Push(folderName);
						destFiles.Add(new FileUnit(folderName));
					}
				}
			}
		}

		private void compareSrcFileUnits(int sRevPathLen, List<FileUnit> srcFiles)
		{
			foreach (FileUnit u in srcFiles)
			{
				String folderRelativePath = u.AbsolutePath.Substring(sRevPathLen);
				if (u.IsDirectory)
				{
					compareSrcDirs(u, folderRelativePath);
				}
				else
				{
					String relativePath = u.AbsolutePath.Substring(sRevPathLen);
					compareSrcFiles(u, relativePath);
				}
			}
		}

		private void compareSrcDirs(FileUnit u, String folderRelativePath)
		{
			if (metaDataExists())
			{
				if (sMetaData.Primary.ContainsKey(folderRelativePath))
				{
					sCleanDirs.add(folderRelativePath, u);
					sMetaData.removeByPrimary(folderRelativePath);
				}
				else
				{
					sDirtyDirs.add(folderRelativePath, "C-" + folderRelativePath, u);
				}
			}
			else
			{
				sDirtyDirs.add(folderRelativePath, "C-" + folderRelativePath, u);
			}
		}

		private void compareSrcFiles(FileUnit u, String relativePath)
		{
			if (metaDataExists())
			{
				if (sMetaData.Primary.ContainsKey(relativePath))
				{
					if ((u.LastWriteTime - sMetaData.getByPrimary(relativePath).LastWriteTime).Duration().TotalSeconds <= task.Settings.IgnoreTimeChange)
					{
						sCleanFiles.add(relativePath, sMetaData.PriSub[relativePath], u);
						sMetaData.removeByPrimary(relativePath);
					}
					else
					{
						srcDirtySize += u.Size;
						sDirtyFiles.add(relativePath, "M-" + sMetaData.PriSub[relativePath], u);
						sMetaData.removeByPrimary(relativePath);
					}
				}
				else
				{
					srcDirtySize += u.Size;
					u.Hash = "C-" + MyUtility.computeMyHash(u);
					sDirtyFiles.add(relativePath, u.Hash, u);
				}
			}
			else
			{
				srcDirtySize += u.Size;
				u.Hash = "C-" + MyUtility.computeMyHash(u);
				sDirtyFiles.add(relativePath, u.Hash, u);
			}
		}

		private void compareTgtFileUnits(int tRevPathLen, List<FileUnit> destFiles)
		{
			foreach (FileUnit u in destFiles)
			{
				String folderRelativePath = u.AbsolutePath.Substring(tRevPathLen);
				if (u.IsDirectory)
				{
					compareTgtDirs(u, folderRelativePath);
				}
				else
				{
					String relativePath = u.AbsolutePath.Substring(tRevPathLen);
					compareTgtFiles(u, relativePath);
				}
			}
		}

		private void compareTgtDirs(FileUnit u, String folderRelativePath)
		{
			if (metaDataExists())
			{
				if (tMetaData.Primary.ContainsKey(folderRelativePath))
				{
					tCleanDirs.add(folderRelativePath, u);
					tMetaData.removeByPrimary(folderRelativePath);
				}
				else
				{
					tDirtyDirs.add(folderRelativePath, "C-" + folderRelativePath, u);
				}
			}
			else
			{
				tDirtyDirs.add(folderRelativePath, "C-" + folderRelativePath, u);
			}
		}

		private void compareTgtFiles(FileUnit u, String relativePath)
		{
			if (metaDataExists())
			{
				if (tMetaData.Primary.ContainsKey(relativePath))
				{
					if ((u.LastWriteTime - tMetaData.getByPrimary(relativePath).LastWriteTime).Duration().TotalSeconds <= task.Settings.IgnoreTimeChange)
					{
						tCleanFiles.add(relativePath, tMetaData.PriSub[relativePath], u);
						tMetaData.removeByPrimary(relativePath);
					}
					else
					{
						tgtDirtySize += u.Size;
						tDirtyFiles.add(relativePath, "M-" + tMetaData.PriSub[relativePath], u);
						tMetaData.removeByPrimary(relativePath);
					}
				}
				else
				{
					tgtDirtySize += u.Size;
					u.Hash = "C-" + MyUtility.computeMyHash(u);
					tDirtyFiles.add(relativePath, u.Hash, u);
				}
			}
			else
			{
				tgtDirtySize += u.Size;
				u.Hash = "C-" + MyUtility.computeMyHash(u);
				tDirtyFiles.add(relativePath, u.Hash, u);
			}
		}

		private void addSrcDeletionToList()
		{
			if (metaDataExists())
			{
				foreach (var item in sMetaData.Primary)
				{
					if (item.Value.IsDirectory)
					{
						sDirtyDirs.add(item.Key, "D-" + item.Key, item.Value);
					}
					else
					{
						sDirtyFiles.add(item.Key, "D-" + sMetaData.PriSub[item.Key], item.Value);
					}
				}
			}
		}

		private void addTgtDeletionToList()
		{
			if (metaDataExists())
			{
				foreach (var item in tMetaData.Primary)
				{
					if (item.Value.IsDirectory)
					{
						tDirtyDirs.add(item.Key, "D-" + item.Key, item.Value);
					}
					else
					{
						tDirtyFiles.add(item.Key, "D-" + tMetaData.PriSub[item.Key], item.Value);
					}
				}
			}
		}

		private void createFileLists()
		{
			sourceList = new FileList(sCleanFiles, sDirtyFiles, sDirtyDirs, sCleanDirs);
			destList = new FileList(tCleanFiles, tDirtyFiles, tDirtyDirs, tCleanDirs);
		}

		public FileList getSrcList()
		{
			return sourceList;
		}

		public FileList getDestList()
		{
			return destList;
		}
	}
}