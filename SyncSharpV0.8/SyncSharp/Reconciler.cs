using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SyncSharp.DataModel;
using SyncSharp.Storage;
using Microsoft.VisualBasic.FileIO;

namespace SyncSharp.Business
{

	public class SyncSummary
	{
		public int iSrcFileCopy;
		public int iSrcFileDelete;
		public int iSrcFileRename;
		public int iSrcFileOverwrite;
		public int iSrcFolderCreate;
		public int iSrcFolderDelete;

		public int iTgtFileCopy;
		public int iTgtFileDelete;
		public int iTgtFileRename;
		public int iTgtFileOverwrite;
		public int iTgtFolderCreate;
		public int iTgtFolderDelete;

		public DateTime startTime;
		public DateTime endTime;
		public String logFile;

		public SyncSummary()
		{
			iSrcFileCopy = iSrcFileDelete = iSrcFileRename = iSrcFileOverwrite = 0;
			iSrcFolderCreate = iSrcFolderDelete = 0;
			iTgtFileCopy = iTgtFileDelete = iTgtFileRename = iTgtFileOverwrite = 0;
			iTgtFolderCreate = iTgtFolderDelete = 0;
		}
	}

	public class FolderDiffLists
	{
		private List<FileUnit> singleSourceFilesList; // Copy Src to Tgt
		private List<FileUnit> singleTargetFilesList; // Copy Tgt to Src
		private List<FileUnit> newSourceFilesList;	// Copy Src To Tgt
		private List<FileUnit> newTargetFilesList; 	// Copy Tgt to Src
		private List<FileUnit> deleteSourceFilesList;	// Del Src
		private List<FileUnit> deleteTargetFilesList;	// Del Tgt
		private List<FileUnit> keepBothFilesList;		// Copy to both destination
		private List<FileUnit> unChangedFilesList;

		public FolderDiffLists()
		{
			singleSourceFilesList = new List<FileUnit>();
			singleTargetFilesList = new List<FileUnit>();
			newSourceFilesList = new List<FileUnit>();
			newTargetFilesList = new List<FileUnit>();
			deleteSourceFilesList = new List<FileUnit>();
			deleteTargetFilesList = new List<FileUnit>();
			keepBothFilesList = new List<FileUnit>();
			unChangedFilesList = new List<FileUnit>();
		}

		public List<FileUnit> KeepBothFilesList
		{
			get { return keepBothFilesList; }
		}

		public List<FileUnit> UnChangedFilesList
		{
			get { return unChangedFilesList; }
		}

		public List<FileUnit> SingleSourceFilesList
		{
			get { return singleSourceFilesList; }
		}

		public List<FileUnit> SingleTargetFilesList
		{
			get { return singleTargetFilesList; }
		}

		public List<FileUnit> NewSourceFilesList
		{
			get { return newSourceFilesList; }
		}

		public List<FileUnit> NewTargetFilesList
		{
			get { return newTargetFilesList; }
		}

		public List<FileUnit> DeleteSourceFilesList
		{
			get { return deleteSourceFilesList; }
		}

		public List<FileUnit> DeleteTargetFilesList
		{
			get { return deleteTargetFilesList; }
		}
	}

	public class Reconciler
	{
		FileList _srcList;
		FileList _tgtList;
		String _srcPath;
		String _tgtPath;
		String _taskName;
		TaskSettings _taskSettings;
		SyncSummary _summary;

		// Data Structure for Sync without metadata
		public CustomDictionary<string, string, FileUnit> _updatedList;

		// DataStructure for Sync with metadata
		CustomDictionary<string, string, FileUnit> _srcDirtyFilesList;
		CustomDictionary<string, string, FileUnit> _srcCleanFilesList;
		CustomDictionary<string, string, FileUnit> _srcDirtyFoldersList;
		CustomDictionary<string, string, FileUnit> _srcCleanFoldersList;
		CustomDictionary<string, string, FileUnit> _tgtDirtyFilesList;
		CustomDictionary<string, string, FileUnit> _tgtCleanFilesList;
		CustomDictionary<string, string, FileUnit> _tgtDirtyFoldersList;
		CustomDictionary<string, string, FileUnit> _tgtCleanFoldersList;

		// Data Structure for "rename" operation
		CustomDictionary<string, string, FileUnit> _srcRenameList;
		CustomDictionary<string, string, FileUnit> _tgtRenameList;

		public Reconciler(FileList srcList, FileList tgtList, SyncTask task, String id)
		{
			_srcList = srcList;
			_tgtList = tgtList;
			_taskSettings = task.Settings;
			_srcPath = task.Source;
			_tgtPath = task.Target;
			_taskName = task.Name;
			_updatedList = new CustomDictionary<string, string, FileUnit>();
			_srcRenameList = new CustomDictionary<string, string, FileUnit>();
			_tgtRenameList = new CustomDictionary<string, string, FileUnit>();

      Logger.CloseLog();
			Logger.CreateLog(@".\Profiles\" + id + @"\" + task.Name + ".log");
			_summary = new SyncSummary();
			_summary.logFile = _taskName + ".log";
		}

		public void BackupSource(CustomDictionary<string, string, FileUnit> srcList)
		{
			_summary.startTime = DateTime.Now;

			foreach (var myRecord in srcList.Primary)
			{
				String relativePath = myRecord.Key;
				String srcFile; String tgtFile;
				if (relativePath[0] == '\\')
				{ srcFile = _srcPath + relativePath; tgtFile = _tgtPath + relativePath; }
				else
				{
					srcFile = _srcPath + "\\" + relativePath;
					tgtFile = _tgtPath + "\\" + relativePath;
				}
				if (Directory.Exists(srcFile))
				{
					if (!Directory.Exists(tgtFile))
					{
						Directory.CreateDirectory(tgtFile);
						_summary.iTgtFolderCreate++;
						Logger.WriteEntry("FOLDER ACTION - Create " + tgtFile);
					}
				}
				else
				{
					if (!File.Exists(tgtFile))
					{
						checkAndCreateFolder(tgtFile);
						File.Copy(srcFile, tgtFile);
						_summary.iSrcFileCopy++;
						Logger.WriteEntry("FILE ACTION - Copy " + srcFile + " to " + tgtFile);
					}
					else
					{
						FileInfo srcFileInfo = new FileInfo(srcFile);
						FileInfo tgtFileInfo = new FileInfo(tgtFile);

						if (srcFileInfo.LastWriteTimeUtc != tgtFileInfo.LastWriteTimeUtc)
						{
							File.Copy(srcFile, tgtFile, true);
							_summary.iSrcFileCopy++; _summary.iTgtFileOverwrite++;
							Logger.WriteEntry("FILE ACTION - Copy " + srcFile + " to " + tgtFile);

						}
					}
				}
			}
			_summary.endTime = DateTime.Now;
			Logger.CloseLog();
		}

		public void PreviewWithoutMetaData()
		{
		}

		public FolderDiffLists PreviewWithMetaData()
		{
			FolderDiffLists previewLists = new FolderDiffLists();

			// Get the list for source and target.
			_srcDirtyFilesList = _srcList.DirtyFiles; _srcCleanFilesList = _srcList.CleanFiles;
			_srcDirtyFoldersList = _srcList.DirtyDirs; _srcCleanFoldersList = _srcList.CleanDirs;
			_tgtDirtyFilesList = _tgtList.DirtyFiles; _tgtCleanFilesList = _tgtList.CleanFiles;
			_tgtDirtyFoldersList = _tgtList.DirtyDirs; _tgtCleanFoldersList = _tgtList.CleanDirs;

			FileUnit srcFile;
			FileUnit tgtFile;

			// Check "RENAME" operation.
			foreach (var mySrcRecord in _srcDirtyFilesList.PriSub)
			{
				String relativePath = mySrcRecord.Key;
				String srcSecondKey = mySrcRecord.Value;
				String srcFlag = "" + srcSecondKey[0];
				String srcHashCode = srcSecondKey.Substring(2);

				if (srcFlag.Equals("C"))
				{
					if (_srcDirtyFilesList.containsSecKey("D-" + srcHashCode))
					{
						if (!_srcRenameList.containsPriKey(relativePath))
						{
							List<String> delFiles = _srcDirtyFilesList.getBySecondary("D-" + srcHashCode);
							foreach (String myFile in delFiles)
							{
								if (!_srcRenameList.containsSecKey(myFile))
								{
									_srcRenameList.add(relativePath, myFile, _srcDirtyFilesList.getByPrimary(relativePath));
									break;
								}
							}
						}
					}
				}
				else if (srcFlag.Equals("D"))
				{
					if (_srcDirtyFilesList.containsSecKey("C-" + srcHashCode))
					{
						if (!_srcRenameList.containsSecKey(relativePath))
						{
							List<String> createFiles = _srcDirtyFilesList.getBySecondary("C-" + relativePath);
							foreach (String myFile in createFiles)
							{
								if (!_srcRenameList.containsPriKey(myFile))
								{
									_srcRenameList.add(myFile, relativePath, _srcDirtyFilesList.getByPrimary(myFile));
									break;
								}
							}
						}
					}
				}
			}
			foreach (var myTgtRecord in _tgtDirtyFilesList.PriSub)
			{
				String relativePath = myTgtRecord.Key;
				String tgtSecondKey = myTgtRecord.Value;
				String tgtFlag = "" + tgtSecondKey[0];
				String tgtHashCode = tgtSecondKey.Substring(2);

				if (tgtFlag.Equals("C"))
				{
					if (_tgtDirtyFilesList.containsSecKey("D-" + tgtHashCode))
					{
						if (!_tgtRenameList.containsPriKey(relativePath))
						{
							List<String> delFiles = _tgtDirtyFilesList.getBySecondary("D-" + tgtHashCode);
							foreach (String myFile in delFiles)
							{
								if (!_tgtRenameList.containsSecKey(myFile))
								{
									_tgtRenameList.add(relativePath, myFile, _tgtDirtyFilesList.getByPrimary(relativePath));
									break;
								}
							}
						}
					}
				}
				else if (tgtFlag.Equals("D"))
				{
					if (_tgtDirtyFilesList.containsSecKey("C-" + tgtHashCode))
					{
						if (!_tgtRenameList.containsSecKey(relativePath))
						{
							List<String> createFiles = _tgtDirtyFilesList.getBySecondary("C-" + relativePath);
							foreach (String myFile in createFiles)
							{
								if (!_tgtRenameList.containsPriKey(myFile))
								{
									_tgtRenameList.add(myFile, relativePath, _tgtDirtyFilesList.getByPrimary(myFile));
									break;
								}
							}
						}
					}
				}
			}

			foreach (var mySrcRecord in _srcDirtyFilesList.PriSub)
			{
				String relativePath = mySrcRecord.Key;

				String srcSecondKey = mySrcRecord.Value;
				String srcFlag = "" + srcSecondKey[0];
				String srcHashCode = srcSecondKey.Substring(2);
				srcFile = _srcDirtyFilesList.getByPrimary(relativePath);

				if (_tgtCleanFilesList.containsPriKey(relativePath))
				{
					if (srcFlag.Equals("M"))
					{
						srcFile.Match = new FileUnit(_tgtPath + relativePath);
						String hashCode = MyUtility.computeMyHash(srcFile);
						srcFile.Hash = hashCode;
						previewLists.NewSourceFilesList.Add(srcFile);
					}
					if (srcFlag.Equals("D"))
					{
						if (!_srcRenameList.containsSecKey(relativePath))
						{
							previewLists.DeleteTargetFilesList.Add(new FileUnit(_tgtPath + relativePath));
						}
					}
				}
				else if (!_tgtDirtyFilesList.containsPriKey(relativePath))
				{
					if (srcFlag.Equals("C"))
					{
						if (!_srcRenameList.containsPriKey(relativePath))
						{
							String hashCode = MyUtility.computeMyHash(srcFile);
							srcFile.Hash = hashCode;
							previewLists.SingleSourceFilesList.Add(srcFile);
						}
						else
						{
							String delRelativePath = _srcRenameList.PriSub[relativePath];
							if (_tgtCleanFilesList.containsPriKey(delRelativePath))
							{
								FileUnit fileMeta = _srcDirtyFilesList.getByPrimary(relativePath);
								_updatedList.add(relativePath, fileMeta.Hash, fileMeta);
								checkAndCreateFolder(_tgtPath + relativePath);
								File.Move(_tgtPath + delRelativePath, _tgtPath + relativePath);
								_tgtCleanFilesList.removeByPrimary(delRelativePath);
							}
							else if (_tgtRenameList.containsSecKey(delRelativePath) && !_tgtRenameList.containsPriKey(relativePath))
							{
								List<String> lstFiles = _tgtRenameList.SubPri[delRelativePath];
								String createFilePath = lstFiles[0];
								tgtFile = _tgtRenameList.getByPrimary(createFilePath);
								int iSlash = relativePath.LastIndexOf('\\');

								String srcFilePath = "";
								String tgtFilePath = "";
								if (iSlash > 0)
								{
									srcFilePath = relativePath.Substring(0, iSlash);
									tgtFilePath = createFilePath.Substring(0, iSlash);
								}
								previewSyncAction(srcFile, tgtFile, srcFlag, "C", _srcPath + srcFilePath, _tgtPath + tgtFilePath, previewLists);
								_tgtDirtyFilesList.removeByPrimary(createFilePath);
							}
						}
					}
				}
				else if (_tgtDirtyFilesList.containsPriKey(relativePath))
				{
					string tgtSecondKey = _tgtDirtyFilesList.PriSub[relativePath];
					String tgtFlag = "" + tgtSecondKey[0];
					String tgtHashCode = tgtSecondKey.Substring(2);
					tgtFile = _tgtDirtyFilesList.getByPrimary(relativePath);

					if (srcFlag.Equals("D") && _srcRenameList.containsSecKey(relativePath))
						continue;
					if (tgtFlag.Equals("D") && _tgtRenameList.containsSecKey(relativePath))
					{
						List<String> lstFiles = _tgtRenameList.SubPri[relativePath];
						String createFilePath = lstFiles[0];
						tgtFlag = "C"; tgtFile = _tgtRenameList.getByPrimary(createFilePath);

						int iSlash = relativePath.LastIndexOf('\\');
						String srcFilePath = "";
						String tgtFilePath = "";
						if (iSlash > 0)
						{
							srcFilePath = relativePath.Substring(0, iSlash);
							tgtFilePath = createFilePath.Substring(0, iSlash);
						}
						previewSyncAction(srcFile, tgtFile, srcFlag, tgtFlag, _srcPath + srcFilePath, _tgtPath + tgtFilePath, previewLists);
						_tgtDirtyFilesList.removeByPrimary(createFilePath);
					}
					else if (srcFlag.Equals("C") && _srcRenameList.containsPriKey(relativePath))
					{
						String delFile = _srcRenameList.PriSub[relativePath];
						if (_tgtRenameList.containsSecKey(delFile) && !_tgtRenameList.containsPriKey(relativePath))
						{
							List<String> lstFiles = _tgtRenameList.SubPri[relativePath];
							String createFilePath = lstFiles[0];
							tgtFlag = "C"; tgtFile = _tgtRenameList.getByPrimary(createFilePath);
							int iSlash = relativePath.LastIndexOf('\\');

							String srcFilePath = "";
							String tgtFilePath = "";
							if (iSlash > 0)
							{
								srcFilePath = relativePath.Substring(0, iSlash);
								tgtFilePath = createFilePath.Substring(0, iSlash);
							}
							previewSyncAction(srcFile, tgtFile, srcFlag, tgtFlag, _srcPath + srcFilePath, _tgtPath + tgtFilePath, previewLists);
							_tgtDirtyFilesList.removeByPrimary(createFilePath);
						}
						else if (_tgtRenameList.containsSecKey(delFile) && _tgtRenameList.containsPriKey(relativePath))
						{
							_updatedList.add(relativePath, srcFile.Hash, srcFile);
							_tgtDirtyFilesList.removeByPrimary(relativePath);
						}
					}
					else
					{
						int iSlash = relativePath.LastIndexOf('\\');
						String filePath = "";
						if (iSlash > 0) filePath = relativePath.Substring(0, iSlash);
						previewSyncAction(srcFile, tgtFile, srcFlag, tgtFlag, _srcPath + filePath, _tgtPath + filePath, previewLists);
						_tgtDirtyFilesList.removeByPrimary(relativePath);
					}
				}
			}

			foreach (var myTgtRecord in _tgtDirtyFilesList.PriSub)
			{
				String relativePath = myTgtRecord.Key;

				String tgtSecondKey = myTgtRecord.Value;
				String tgtFlag = "" + tgtSecondKey[0];
				String tgtHashCode = tgtSecondKey.Substring(2);
				tgtFile = _tgtDirtyFilesList.getByPrimary(relativePath);

				if (_srcCleanFilesList.containsPriKey(relativePath))
				{
					if (tgtFlag.Equals("M"))
					{
						tgtFile.Match = new FileUnit(_srcPath + relativePath);
						String hashCode = MyUtility.computeMyHash(tgtFile);
						tgtFile.Hash = hashCode;
						previewLists.NewTargetFilesList.Add(tgtFile);
					}
					if (tgtFlag.Equals("D"))
					{
						if (!_tgtRenameList.containsSecKey(relativePath))
						{
							previewLists.DeleteSourceFilesList.Add(new FileUnit(_srcPath + relativePath));
						}
					}
				}
				else if (!_srcDirtyFilesList.containsPriKey(relativePath))
				{
					if (tgtFlag.Equals("C"))
					{
						if (!_tgtRenameList.containsPriKey(relativePath))
						{
							String hashCode = MyUtility.computeMyHash(tgtFile);
							tgtFile.Hash = hashCode;
							previewLists.SingleTargetFilesList.Add(tgtFile);
						}
						else
						{
							String delRelativePath = _tgtRenameList.PriSub[relativePath];
							if (_srcCleanFilesList.containsPriKey(delRelativePath))
							{
								FileUnit fileMeta = _tgtDirtyFilesList.getByPrimary(relativePath);
								_updatedList.add(relativePath, fileMeta.Hash, fileMeta);
								checkAndCreateFolder(_srcPath + relativePath);
								File.Move(_srcPath + delRelativePath, _srcPath + relativePath);
								_srcCleanFilesList.removeByPrimary(delRelativePath);
							}
						}
					}
				}
			}

			foreach (var mySrcRecord in _srcDirtyFoldersList.PriSub)
			{
				String relativePath = mySrcRecord.Key;
				String srcSecondKey = mySrcRecord.Value;
				String srcFlag = "" + srcSecondKey[0];
				FileUnit srcFileMeta = _srcDirtyFoldersList.getByPrimary(relativePath);

				if (srcFlag.Equals("C"))
				{
					if (!Directory.Exists(_srcPath + relativePath))
					{
                        previewLists.SingleTargetFilesList.Add(new FileUnit(_tgtPath + relativePath));
						//previewLists.SingleSourceFilesList.Add(new FileUnit(_srcPath + relativePath));
					}
					if (!Directory.Exists(_tgtPath + relativePath))
					{
                        previewLists.SingleSourceFilesList.Add(new FileUnit(_srcPath + relativePath));
						//previewLists.SingleTargetFilesList.Add(new FileUnit(_tgtPath + relativePath));
					}
				}
				else if (srcFlag.Equals("D"))
				{
					if (_tgtDirtyFoldersList.containsSecKey("D-" + relativePath))
					{
						if (Directory.Exists(_srcPath + relativePath))
						{ Directory.Delete(_srcPath + relativePath, true); }
						if (Directory.Exists(_tgtPath + relativePath))
						{ Directory.Delete(_tgtPath + relativePath, true); }
						_tgtDirtyFoldersList.removeByPrimary(relativePath);
					}
					else if (_tgtCleanFoldersList.containsPriKey(relativePath))
					{
						if (!Directory.Exists(_srcPath + relativePath))
						{
							previewLists.DeleteSourceFilesList.Add(new FileUnit(_tgtPath + relativePath));
						}
					}
				}
			}
			foreach (var myTgtRecord in _tgtDirtyFoldersList.PriSub)
			{
				String relativePath = myTgtRecord.Key;
				String tgtSecondKey = myTgtRecord.Value;
				String tgtFlag = "" + tgtSecondKey[0];
				FileUnit tgtFileMeta = _tgtDirtyFoldersList.getByPrimary(relativePath);

				if (tgtFlag.Equals("C"))
				{
					if (!Directory.Exists(_srcPath + relativePath))
					{
                        previewLists.SingleTargetFilesList.Add(new FileUnit(_tgtPath + relativePath));
						//previewLists.SingleSourceFilesList.Add(new FileUnit(_srcPath + relativePath));
					}
					if (!Directory.Exists(_tgtPath + relativePath))
					{
                        previewLists.SingleSourceFilesList.Add(new FileUnit(_srcPath + relativePath));
						//previewLists.SingleTargetFilesList.Add(new FileUnit(_tgtPath + relativePath));
					}
				}
				else if (tgtFlag.Equals("D"))
				{
					if (_srcCleanFoldersList.containsPriKey(relativePath))
					{
						if (!Directory.Exists(_tgtPath + relativePath))
						{
							previewLists.DeleteSourceFilesList.Add(new FileUnit(_srcPath + relativePath));
						}
					}
				}
			}

            return previewLists;
		}

		public void SyncPreviewAction(FolderDiffLists previewList)
		{
			_summary.startTime = DateTime.Now;

			List<FileUnit> singleSourceFilesList = previewList.SingleSourceFilesList; // Copy Src to Tgt
			List<FileUnit> singleTargetFilesList = previewList.SingleTargetFilesList; // Copy Tgt to Src
			List<FileUnit> newSourceFilesList = previewList.NewSourceFilesList;	// Copy Src To Tgt
			List<FileUnit> newTargetFilesList = previewList.NewTargetFilesList; 	// Copy Tgt to Src
			List<FileUnit> deleteSourceFilesList = previewList.DeleteSourceFilesList;	// Del Src
			List<FileUnit> deleteTargetFilesList = previewList.DeleteTargetFilesList;	// Del Tgt
			List<FileUnit> keepBothFilesLists = previewList.KeepBothFilesList;		// Copy to both destination
			List<FileUnit> unChangedFilesList = previewList.UnChangedFilesList;

			foreach (FileUnit myRecord in singleSourceFilesList)
			{
				String strFullPath = myRecord.AbsolutePath;
				String strRelativePath = "";
				if (strFullPath.StartsWith(_srcPath))
				{ strRelativePath = strFullPath.Substring(_srcPath.Length); }
				else if (strFullPath.StartsWith(_tgtPath))
				{ strRelativePath = strFullPath.Substring(_tgtPath.Length); }

				if (myRecord.IsDirectory)
				{
					Directory.CreateDirectory(_tgtPath + strRelativePath);
					_updatedList.add(strRelativePath, myRecord);
					_summary.iTgtFolderCreate++;
					Logger.WriteEntry("FOLDER ACTION - Create " + _tgtPath + strRelativePath);
				}
				else
				{
					checkAndCreateFolder(_tgtPath + strRelativePath);
					File.Copy(_srcPath + strRelativePath, _tgtPath + strRelativePath, true);
					_updatedList.add(strRelativePath, myRecord.Hash, myRecord);
					_summary.iSrcFileCopy++;
					Logger.WriteEntry("FILE ACTION - Copy " + _srcPath + strRelativePath + " to " + _tgtPath + strRelativePath);
				}
			}
			foreach (FileUnit myRecord in singleTargetFilesList)
			{
				String strFullPath = myRecord.AbsolutePath;
				String strRelativePath = "";
				if (strFullPath.StartsWith(_srcPath))
				{ strRelativePath = strFullPath.Substring(_srcPath.Length); }
				else if (strFullPath.StartsWith(_tgtPath))
				{ strRelativePath = strFullPath.Substring(_tgtPath.Length); }

				if (myRecord.IsDirectory)
				{
					Directory.CreateDirectory(_srcPath + strRelativePath);
					_updatedList.add(strRelativePath, myRecord.Hash, myRecord);
					_summary.iSrcFolderCreate++;
					Logger.WriteEntry("FOLDER ACTION - Create " + _srcPath + strRelativePath);
				}
				else
				{
					checkAndCreateFolder(_srcPath + strRelativePath);
					File.Copy(_tgtPath + strRelativePath, _srcPath + strRelativePath, true);
					_updatedList.add(strRelativePath, myRecord.Hash, myRecord);
					_summary.iTgtFileCopy++;
					Logger.WriteEntry("FILE ACTION - Copy " + _tgtPath + strRelativePath + " to " + _srcPath + strRelativePath);
				}
			}
			foreach (FileUnit myRecord in newSourceFilesList)
			{
				String strFullPath = myRecord.AbsolutePath;
				String strRelativePath = "";
				if (strFullPath.StartsWith(_srcPath))
				{ strRelativePath = strFullPath.Substring(_srcPath.Length); }
				else if (strFullPath.StartsWith(_tgtPath))
				{ strRelativePath = strFullPath.Substring(_tgtPath.Length); }

				if (myRecord.IsDirectory)
				{
					Directory.CreateDirectory(_tgtPath + strRelativePath);
					_tgtCleanFoldersList.removeByPrimary(strRelativePath);
					_updatedList.add(strRelativePath, myRecord);
					_summary.iTgtFolderCreate++;
					Logger.WriteEntry("FOLDER ACTION - Create " + _tgtPath + strRelativePath);
				}
				else
				{
					checkAndCreateFolder(_tgtPath + strRelativePath);
					File.Copy(_srcPath + strRelativePath, _tgtPath + strRelativePath, true);
					_tgtCleanFilesList.removeByPrimary(strRelativePath);
					_updatedList.add(strRelativePath, myRecord.Hash, myRecord);
					_summary.iSrcFileCopy++; _summary.iTgtFileOverwrite++;
					Logger.WriteEntry("FILE ACTION - Copy " + _srcPath + strRelativePath + " to " + _tgtPath + strRelativePath);
				}
			}
			foreach (FileUnit myRecord in newTargetFilesList)
			{
				String strFullPath = myRecord.AbsolutePath;
				String strRelativePath = "";
				if (strFullPath.StartsWith(_srcPath))
				{ strRelativePath = strFullPath.Substring(_srcPath.Length); }
				else if (strFullPath.StartsWith(_tgtPath))
				{ strRelativePath = strFullPath.Substring(_tgtPath.Length); }

				if (myRecord.IsDirectory)
				{
					Directory.CreateDirectory(_srcPath + strRelativePath);
					_srcCleanFoldersList.removeByPrimary(strRelativePath);
					_updatedList.add(strRelativePath, myRecord);
					_summary.iSrcFolderCreate++;
					Logger.WriteEntry("FOLDER ACTION - Create " + _srcPath + strRelativePath);
				}
				else
				{
					checkAndCreateFolder(_srcPath + strRelativePath);
					File.Copy(_tgtPath + strRelativePath, _srcPath + strRelativePath, true);
					_srcCleanFilesList.removeByPrimary(strRelativePath);
					_updatedList.add(strRelativePath, myRecord.Hash, myRecord);
					_summary.iSrcFileCopy++; _summary.iTgtFileOverwrite++;
					Logger.WriteEntry("FILE ACTION - Copy " + _tgtPath + strRelativePath + " to " + _srcPath + strRelativePath);
				}
			}
			foreach (FileUnit myRecord in keepBothFilesLists)
			{
				String strFullPath = myRecord.AbsolutePath;
				String strRelativePath = "";
				if (strFullPath.StartsWith(_srcPath))
				{ strRelativePath = strFullPath.Substring(_srcPath.Length); }
				else if (strFullPath.StartsWith(_tgtPath))
				{ strRelativePath = strFullPath.Substring(_tgtPath.Length); }
				String srcPath = _srcPath + strRelativePath;
				String tgtPath = _tgtPath + strRelativePath;

				if (!myRecord.IsDirectory)
				{
					String srcFileName = myRecord.Name;
					String tgtFileName = myRecord.Name;
					do
					{
						int iExt = srcFileName.LastIndexOf('.');		// Locate the index position of the file extension
						if (iExt > 0) 	// File with file extension
						{
							srcFileName = srcFileName.Substring(0, iExt) + "(1)" + srcFileName.Substring(iExt);
							tgtFileName = tgtFileName.Substring(0, iExt) + "(2)" + tgtFileName.Substring(iExt);
						}
						else if (iExt == -1)	// File without file extension
						{
							srcFileName = srcFileName + "(1)";
							tgtFileName = tgtFileName + "(2)";
						}
					} while (File.Exists(srcPath + srcFileName) || File.Exists(srcPath + tgtFileName)
						 || File.Exists(tgtPath + srcFileName) || File.Exists(tgtPath + tgtFileName));


					File.Move(srcPath + myRecord.Name, srcPath + srcFileName);
					File.Move(tgtPath + myRecord.Name, tgtPath + tgtFileName);

					checkAndCreateFolder(tgtPath + srcFileName);
					checkAndCreateFolder(srcPath + tgtFileName);
					File.Copy(srcPath + srcFileName, tgtPath + srcFileName);
					File.Copy(tgtPath + tgtFileName, srcPath + tgtFileName);

					FileUnit srcFileMeta = new FileUnit(srcPath + srcFileName);
					String srcHashcode = MyUtility.computeMyHash(srcFileMeta);
					srcFileMeta.Hash = srcHashcode;

					FileUnit tgtFileMeta = new FileUnit(tgtPath + tgtFileName);
					String tgtHashcode = MyUtility.computeMyHash(tgtFileMeta);
					tgtFileMeta.Hash = tgtHashcode;

					_updatedList.add(srcPath.Substring(_srcPath.Length) + srcFileName, srcHashcode, srcFileMeta);
					_updatedList.add(tgtPath.Substring(_tgtPath.Length) + tgtFileName, tgtHashcode, tgtFileMeta);

					_summary.iSrcFileCopy++; _summary.iTgtFileCopy++;
					Logger.WriteEntry("FILE ACTION - Rename, Copy " + srcPath + "\\" + srcFileName + " to " + tgtPath + "\\" + srcFileName);
					Logger.WriteEntry("FILE ACTION - Rename, Copy " + tgtPath + "\\" + tgtFileName + " to " + srcPath + "\\" + srcFileName);
				}
			}
			foreach (FileUnit myRecord in unChangedFilesList)
			{
					String strFullPath = myRecord.AbsolutePath;
					String strRelativePath = "";
					if (strFullPath.StartsWith(_srcPath))
					{ strRelativePath = strFullPath.Substring(_srcPath.Length); }
					else if (strFullPath.StartsWith(_tgtPath))
					{ strRelativePath = strFullPath.Substring(_tgtPath.Length); }
					_updatedList.add(strRelativePath, myRecord.Hash, myRecord);
			}
			foreach (FileUnit myRecord in deleteSourceFilesList)
			{
				String strFullPath = myRecord.AbsolutePath;
				String strRelativePath = "";
				if (strFullPath.StartsWith(_srcPath))
				{ strRelativePath = strFullPath.Substring(_srcPath.Length); }
				else if (strFullPath.StartsWith(_tgtPath))
				{ strRelativePath = strFullPath.Substring(_tgtPath.Length); }

				if (myRecord.IsDirectory)
				{
					Directory.Delete(_srcPath + strRelativePath);
					_summary.iSrcFolderDelete++;
					Logger.WriteEntry("FOLDER ACTION - Delete " + _srcPath + strRelativePath);
				}
				else
				{
					File.Delete(_srcPath + strRelativePath);
					_summary.iSrcFileDelete++;
					Logger.WriteEntry("FILE ACTION - Delete " + _srcPath + strRelativePath);
				}
			}
			foreach (FileUnit myRecord in deleteTargetFilesList)
			{
				String strFullPath = myRecord.AbsolutePath;
				String strRelativePath = "";
				if (strFullPath.StartsWith(_srcPath))
				{ strRelativePath = strFullPath.Substring(_srcPath.Length); }
				else if (strFullPath.StartsWith(_tgtPath))
				{ strRelativePath = strFullPath.Substring(_tgtPath.Length); }

				if (myRecord.IsDirectory)
				{
					Directory.Delete(_tgtPath + strRelativePath);
					_summary.iTgtFolderDelete++;
					Logger.WriteEntry("FOLDER ACTION - Delete " + _tgtPath + strRelativePath);
				}
				else
				{
					File.Delete(_tgtPath + strRelativePath);
					_summary.iTgtFileDelete++;
					Logger.WriteEntry("FILE ACTION - Delete " + _tgtPath + strRelativePath);
				}
			}

			foreach (var myRecord in _srcCleanFilesList.PriSub)
			{
				String relativePath = myRecord.Key;
				String secondKey = myRecord.Value;
				FileUnit fileMeta = _srcCleanFilesList.Primary[relativePath];
				_updatedList.add(relativePath, secondKey, fileMeta);
			}

			foreach (var myRecord in _srcCleanFoldersList.PriSub)
			{
				String relativePath = myRecord.Key;
				FileUnit fileMeta = _srcCleanFilesList.Primary[relativePath];
				_updatedList.add(relativePath, fileMeta);
			}
			_summary.endTime = DateTime.Now;
			Logger.CloseLog();
		}

		public void SyncWithMeta()
		{

			_summary.startTime = DateTime.Now;

			// Get the list for source and target.
			_srcDirtyFilesList = _srcList.DirtyFiles; _srcCleanFilesList = _srcList.CleanFiles;
			_srcDirtyFoldersList = _srcList.DirtyDirs; _srcCleanFoldersList = _srcList.CleanDirs;
			_tgtDirtyFilesList = _tgtList.DirtyFiles; _tgtCleanFilesList = _tgtList.CleanFiles;
			_tgtDirtyFoldersList = _tgtList.DirtyDirs; _tgtCleanFoldersList = _tgtList.CleanDirs;

			FileUnit srcFile;
			FileUnit tgtFile;

			// Check "RENAME" operation.
			foreach (var mySrcRecord in _srcDirtyFilesList.PriSub)
			{
				String relativePath = mySrcRecord.Key;
				String srcSecondKey = mySrcRecord.Value;
				String srcFlag = "" + srcSecondKey[0];
				String srcHashCode = srcSecondKey.Substring(2);

				if (srcFlag.Equals("C"))
				{
					if (_srcDirtyFilesList.containsSecKey("D-" + srcHashCode))
					{
						if (!_srcRenameList.containsPriKey(relativePath))
						{
							List<String> delFiles = _srcDirtyFilesList.getBySecondary("D-" + srcHashCode);
							foreach (String myFile in delFiles)
							{
								if (!_srcRenameList.containsSecKey(myFile))
								{
									_srcRenameList.add(relativePath, myFile, _srcDirtyFilesList.getByPrimary(relativePath));
									break;
								}
							}
						}
					}
				}
				else if (srcFlag.Equals("D"))
				{
					if (_srcDirtyFilesList.containsSecKey("C-" + srcHashCode))
					{
						if (!_srcRenameList.containsSecKey(relativePath))
						{
							List<String> createFiles = _srcDirtyFilesList.getBySecondary("C-" + relativePath);
							foreach (String myFile in createFiles)
							{
								if (!_srcRenameList.containsPriKey(myFile))
								{
									_srcRenameList.add(myFile, relativePath, _srcDirtyFilesList.getByPrimary(myFile));
									break;
								}
							}
						}
					}
				}
			}
			foreach (var myTgtRecord in _tgtDirtyFilesList.PriSub)
			{
				String relativePath = myTgtRecord.Key;
				String tgtSecondKey = myTgtRecord.Value;
				String tgtFlag = "" + tgtSecondKey[0];
				String tgtHashCode = tgtSecondKey.Substring(2);

				if (tgtFlag.Equals("C"))
				{
					if (_tgtDirtyFilesList.containsSecKey("D-" + tgtHashCode))
					{
						if (!_tgtRenameList.containsPriKey(relativePath))
						{
							List<String> delFiles = _tgtDirtyFilesList.getBySecondary("D-" + tgtHashCode);
							foreach (String myFile in delFiles)
							{
								if (!_tgtRenameList.containsSecKey(myFile))
								{
									_tgtRenameList.add(relativePath, myFile, _tgtDirtyFilesList.getByPrimary(relativePath));
									break;
								}
							}
						}
					}
				}
				else if (tgtFlag.Equals("D"))
				{
					if (_tgtDirtyFilesList.containsSecKey("C-" + tgtHashCode))
					{
						if (!_tgtRenameList.containsSecKey(relativePath))
						{
							List<String> createFiles = _tgtDirtyFilesList.getBySecondary("C-" + relativePath);
							foreach (String myFile in createFiles)
							{
								if (!_tgtRenameList.containsPriKey(myFile))
								{
									_tgtRenameList.add(myFile, relativePath, _tgtDirtyFilesList.getByPrimary(myFile));
									break;
								}
							}
						}
					}
				}
			}

			foreach (var mySrcRecord in _srcDirtyFilesList.PriSub)
			{
				String relativePath = mySrcRecord.Key;

				String srcSecondKey = mySrcRecord.Value;
				String srcFlag = "" + srcSecondKey[0];
				String srcHashCode = srcSecondKey.Substring(2);
				srcFile = _srcDirtyFilesList.getByPrimary(relativePath);

				if (_tgtCleanFilesList.containsPriKey(relativePath))
				{
					if (srcFlag.Equals("M"))
					{
						FileUnit fileMeta = new FileUnit(_srcPath + relativePath);
						String hashCode = MyUtility.computeMyHash(fileMeta);
						fileMeta.Hash = hashCode;
						checkAndCreateFolder(_tgtPath + relativePath);
						File.Copy(_srcPath + relativePath, _tgtPath + relativePath, true);
						_updatedList.add(relativePath, hashCode, fileMeta);
						_tgtCleanFilesList.removeByPrimary(relativePath);
						_summary.iSrcFileCopy++; _summary.iTgtFileOverwrite++;
						Logger.WriteEntry("FILE ACTION - Copy " + _srcPath + relativePath + " to " + _tgtPath + relativePath);
					}
					if (srcFlag.Equals("D"))
					{
						if (!_srcRenameList.containsSecKey(relativePath))
						{
							File.Delete(_tgtPath + relativePath);
							_tgtCleanFilesList.removeByPrimary(relativePath);
							_summary.iTgtFileDelete++;
							Logger.WriteEntry("FILE ACTION - Delete " + _tgtPath + relativePath);
						}
					}
				}
				else if (!_tgtDirtyFilesList.containsPriKey(relativePath))
				{
					if (srcFlag.Equals("C"))
					{
						if (!_srcRenameList.containsPriKey(relativePath))
						{
							FileUnit fileMeta = new FileUnit(_srcPath + relativePath);
							String hashCode = MyUtility.computeMyHash(fileMeta);
							fileMeta.Hash = hashCode;
							checkAndCreateFolder(_tgtPath + relativePath);
							File.Copy(_srcPath + relativePath, _tgtPath + relativePath);
							_updatedList.add(relativePath, hashCode, fileMeta);
							_summary.iSrcFileCopy++;
							Logger.WriteEntry("FILE ACTION - Copy " + _srcPath + relativePath + " to " + _tgtPath + relativePath);
						}
						else
						{
							String delRelativePath = _srcRenameList.PriSub[relativePath];
							if (_tgtCleanFilesList.containsPriKey(delRelativePath))
							{
								FileUnit fileMeta = _srcDirtyFilesList.getByPrimary(relativePath);
								_updatedList.add(relativePath, fileMeta.Hash.Substring(2), fileMeta);
								checkAndCreateFolder(_tgtPath + relativePath);
								File.Move(_tgtPath + delRelativePath, _tgtPath + relativePath);
								_tgtCleanFilesList.removeByPrimary(delRelativePath);
								_summary.iTgtFileRename++;
								Logger.WriteEntry("FILE ACTION - Move " + _tgtPath + delRelativePath + " to " + _tgtPath + relativePath);
							}
                            else if (_tgtRenameList.containsSecKey(delRelativePath) && !_tgtRenameList.containsPriKey(relativePath))
                            {
                                List<String> lstFiles = _tgtRenameList.SubPri[delRelativePath];
                                String createFilePath = lstFiles[0];
                                tgtFile = _tgtRenameList.getByPrimary(createFilePath);
                                int iSrcSlash = relativePath.LastIndexOf('\\');
                                int iTgtSlash = createFilePath.LastIndexOf('\\');

                                String srcFilePath = "";
                                if (iSrcSlash > 0)
                                { srcFilePath = relativePath.Substring(0, iSrcSlash); }
                                String tgtFilePath = "";
                                if (iTgtSlash > 0)
                                { tgtFilePath = createFilePath.Substring(0, iTgtSlash); }
                                if (!srcFilePath.Equals(tgtFilePath))
                                {
                                    FileUnit folderMeta = new FileUnit(_srcPath + srcFilePath);
                                    if (_srcDirtyFoldersList.containsPriKey(srcFilePath))
                                        _srcDirtyFoldersList.removeByPrimary(srcFilePath);
                                    if (_tgtDirtyFoldersList.containsPriKey(srcFilePath))
                                        _tgtDirtyFoldersList.removeByPrimary(srcFilePath);
                                    _srcDirtyFoldersList.add(srcFilePath, "D-" + srcFilePath, folderMeta);
                                    _tgtDirtyFoldersList.add(srcFilePath, "D-" + srcFilePath, folderMeta);
                                    checkAndCreateFolder(_srcPath + createFilePath);
                                    File.Move(_srcPath + relativePath, _srcPath + tgtFilePath + relativePath.Substring(iSrcSlash));
                                    srcFilePath = tgtFilePath;
                                }
                                executeSyncAction(srcFile, tgtFile, srcFlag, "C", _srcPath + srcFilePath, _tgtPath + tgtFilePath);
                                _tgtDirtyFilesList.removeByPrimary(createFilePath);
                            }
                            else if (_tgtDirtyFilesList.containsPriKey(delRelativePath))
                            {
                                String tgtFlag = "" + _tgtDirtyFilesList.PriSub[delRelativePath][0];
                                tgtFile = _tgtDirtyFilesList.getByPrimary(delRelativePath);
                                int iSrcSlash = relativePath.LastIndexOf('\\');
                                int iTgtSlash = delRelativePath.LastIndexOf('\\');

                                String srcFilePath = "";
                                if (iSrcSlash > 0)
                                { srcFilePath = relativePath.Substring(0, iSrcSlash); }
                                String tgtFilePath = "";
                                if (iTgtSlash > 0)
                                { tgtFilePath = delRelativePath.Substring(0, iTgtSlash); }
                                if (!srcFilePath.Equals(tgtFilePath))
                                {
                                    FileUnit folderMeta = new FileUnit(_tgtPath + tgtFilePath);
                                    if (_srcDirtyFoldersList.containsPriKey(srcFilePath))
                                        _srcDirtyFoldersList.removeByPrimary(srcFilePath);
                                    if (_tgtDirtyFoldersList.containsPriKey(srcFilePath))
                                        _tgtDirtyFoldersList.removeByPrimary(srcFilePath);
                                    _srcDirtyFoldersList.add(srcFilePath, "D-" + srcFilePath, folderMeta);
                                    _tgtDirtyFoldersList.add(srcFilePath, "D-" + srcFilePath, folderMeta);
                                    checkAndCreateFolder(_srcPath + delRelativePath);
                                    File.Move(_srcPath + relativePath, _srcPath + tgtFilePath + relativePath.Substring(iSrcSlash));
                                    srcFilePath = tgtFilePath;
                                }
                                executeSyncAction(srcFile, tgtFile, srcFlag, tgtFlag, _srcPath + srcFilePath, _tgtPath + tgtFilePath);
                                _tgtDirtyFilesList.removeByPrimary(delRelativePath);
                            }
						}
					}
				}
				else if (_tgtDirtyFilesList.containsPriKey(relativePath))
				{
					string tgtSecondKey = _tgtDirtyFilesList.PriSub[relativePath];
					String tgtFlag = "" + tgtSecondKey[0];
					String tgtHashCode = tgtSecondKey.Substring(2);
					tgtFile = _tgtDirtyFilesList.getByPrimary(relativePath);

					if (srcFlag.Equals("D") && _srcRenameList.containsSecKey(relativePath))
						continue;
					if (tgtFlag.Equals("D") && _tgtRenameList.containsSecKey(relativePath))
					{
						List<String> lstFiles = _tgtRenameList.SubPri[relativePath];
						String createFilePath = lstFiles[0];
						tgtFlag = "C"; tgtFile = _tgtRenameList.getByPrimary(createFilePath);

                        int iSrcSlash = relativePath.LastIndexOf('\\');
                        int iTgtSlash = createFilePath.LastIndexOf('\\');

                        String srcFilePath = "";
                        if (iSrcSlash > 0)
                        { srcFilePath = relativePath.Substring(0, iSrcSlash); }
                        String tgtFilePath = "";
                        if (iTgtSlash > 0)
                        { tgtFilePath = createFilePath.Substring(0, iTgtSlash); }
                        if (!srcFilePath.Equals(tgtFilePath))
                        {
                            FileUnit folderMeta = new FileUnit(_srcPath + srcFilePath);
                            if (_srcDirtyFoldersList.containsPriKey(srcFilePath))
                                _srcDirtyFoldersList.removeByPrimary(srcFilePath);
                            _srcDirtyFoldersList.add(srcFilePath, "D-" + srcFilePath, folderMeta);
                            if (_tgtDirtyFoldersList.containsPriKey(srcFilePath))
                                _tgtDirtyFoldersList.removeByPrimary(srcFilePath);
                            _tgtDirtyFoldersList.add(srcFilePath, "D-" + srcFilePath, folderMeta);
                            checkAndCreateFolder(_srcPath + createFilePath);
                            File.Move(_srcPath + relativePath, _srcPath + tgtFilePath + relativePath.Substring(iSrcSlash));
                            srcFilePath = tgtFilePath;
                        }
                        executeSyncAction(srcFile, tgtFile, srcFlag, "C", _srcPath + srcFilePath, _tgtPath + tgtFilePath);
                        _tgtDirtyFilesList.removeByPrimary(createFilePath);
					}
					else if (srcFlag.Equals("C") && _srcRenameList.containsPriKey(relativePath))
					{
						String delFile = _srcRenameList.PriSub[relativePath];
                        if (_tgtRenameList.containsSecKey(delFile) && _tgtRenameList.containsPriKey(relativePath))
						{
							_updatedList.add(relativePath, srcFile.Hash, srcFile);
							_tgtDirtyFilesList.removeByPrimary(relativePath);
						}
					}
					else
					{
						int iSlash = relativePath.LastIndexOf('\\');
						String filePath = "";
						if (iSlash > 0) filePath = relativePath.Substring(0, iSlash);
						executeSyncAction(srcFile, tgtFile, srcFlag, tgtFlag, _srcPath + filePath, _tgtPath + filePath);
						_tgtDirtyFilesList.removeByPrimary(relativePath);
					}
				}
			}

			foreach (var myTgtRecord in _tgtDirtyFilesList.PriSub)
			{
				String relativePath = myTgtRecord.Key;

				String tgtSecondKey = myTgtRecord.Value;
				String tgtFlag = "" + tgtSecondKey[0];
				String tgtHashCode = tgtSecondKey.Substring(2);
				tgtFile = _tgtDirtyFilesList.getByPrimary(relativePath);

				if (_srcCleanFilesList.containsPriKey(relativePath))
				{
					if (tgtFlag.Equals("M"))
					{
						FileUnit fileMeta = new FileUnit(_tgtPath + relativePath);
						String hashCode = MyUtility.computeMyHash(fileMeta);
						fileMeta.Hash = hashCode;
						checkAndCreateFolder(_srcPath + relativePath);
						File.Copy(_tgtPath + relativePath, _srcPath + relativePath, true);
						_updatedList.add(relativePath, hashCode, fileMeta);
						_srcCleanFilesList.removeByPrimary(relativePath);
						_summary.iTgtFileCopy++; _summary.iSrcFileOverwrite++;
						Logger.WriteEntry("FILE ACTION - Copy " + _tgtPath + relativePath + " to " + _srcPath + relativePath);
					}
					if (tgtFlag.Equals("D"))
					{
						if (!_tgtRenameList.containsSecKey(relativePath))
						{
							File.Delete(_srcPath + relativePath);
							_srcCleanFilesList.removeByPrimary(relativePath);
							_summary.iSrcFileDelete++;
							Logger.WriteEntry("FILE ACTION - Delete " + _srcPath + relativePath);
						}
					}
				}
				else if (!_srcDirtyFilesList.containsPriKey(relativePath))
				{
					if (tgtFlag.Equals("C"))
					{
						if (!_tgtRenameList.containsPriKey(relativePath))
						{
							FileUnit fileMeta = new FileUnit(_tgtPath + relativePath);
							String hashCode = MyUtility.computeMyHash(fileMeta);
							fileMeta.Hash = hashCode;
							checkAndCreateFolder(_srcPath + relativePath);
							File.Copy(_tgtPath + relativePath, _srcPath + relativePath);
							_updatedList.add(relativePath, hashCode, fileMeta);
							_summary.iTgtFileCopy++;
							Logger.WriteEntry("FILE ACTION - Copy " + _tgtPath + relativePath + " to " + _srcPath + relativePath);
						}
						else
						{
							String delRelativePath = _tgtRenameList.PriSub[relativePath];
							if (_srcCleanFilesList.containsPriKey(delRelativePath))
							{
								FileUnit fileMeta = _tgtDirtyFilesList.getByPrimary(relativePath);
								_updatedList.add(relativePath, fileMeta.Hash.Substring(2), fileMeta);
								checkAndCreateFolder(_srcPath + relativePath);
								File.Move(_srcPath + delRelativePath, _srcPath + relativePath);
								_summary.iTgtFileRename++;
								Logger.WriteEntry("FILE ACTION - Move " + _srcPath + delRelativePath + " to " + _srcPath + relativePath);
								_srcCleanFilesList.removeByPrimary(delRelativePath);
							}

						}
					}
				}
			}

			foreach (var myRecord in _srcCleanFilesList.PriSub)
			{
				String relativePath = myRecord.Key;
				String secondKey = myRecord.Value;
				FileUnit fileMeta = _srcCleanFilesList.Primary[relativePath];
				_updatedList.add(relativePath, secondKey, fileMeta);
			}

			foreach (var mySrcRecord in _srcDirtyFoldersList.PriSub)
			{
				String relativePath = mySrcRecord.Key;
				String srcSecondKey = mySrcRecord.Value;
				String srcFlag = "" + srcSecondKey[0];
				FileUnit srcFileMeta = _srcDirtyFoldersList.getByPrimary(relativePath);

				if (srcFlag.Equals("C"))
				{
					_summary.iSrcFolderCreate++;
					Logger.WriteEntry("FOLDER ACTION - Create " + _srcPath + relativePath);
					if (!Directory.Exists(_srcPath + relativePath))
					{ Directory.CreateDirectory(_srcPath + relativePath); }
					if (!Directory.Exists(_tgtPath + relativePath))
					{ Directory.CreateDirectory(_tgtPath + relativePath); }
					if (_tgtDirtyFoldersList.containsSecKey("C-" + relativePath))
					{ _tgtDirtyFoldersList.removeByPrimary(relativePath); }
					if (!_updatedList.containsPriKey(relativePath))
					{ _updatedList.add(relativePath, srcFileMeta); }
				}
				else if (srcFlag.Equals("D"))
				{
					if (_tgtDirtyFoldersList.containsSecKey("D-" + relativePath))
					{
						if (Directory.Exists(_srcPath + relativePath))
						{
							Directory.Delete(_srcPath + relativePath, true);
							_summary.iSrcFolderDelete++;
							Logger.WriteEntry("FOLDER ACTION - Delete " + _srcPath + relativePath);
						}
						if (Directory.Exists(_tgtPath + relativePath))
						{
							Directory.Delete(_tgtPath + relativePath, true);
							_summary.iTgtFolderDelete++;
							Logger.WriteEntry("FOLDER ACTION - Delete " + _tgtPath + relativePath);
						}
						_tgtDirtyFoldersList.removeByPrimary(relativePath);
					}
					else if (_tgtCleanFoldersList.containsPriKey(relativePath))
					{
						if (Directory.Exists(_srcPath + relativePath))
						{ _updatedList.add(relativePath, srcFileMeta); }
						else
						{
							Directory.Delete(_tgtPath + relativePath, true);
							_tgtCleanFoldersList.removeByPrimary(relativePath);
							_summary.iTgtFolderDelete++;
							Logger.WriteEntry("FOLDER ACTION - Delete " + _tgtPath + relativePath);
						}
					}
				}
			}
			foreach (var myTgtRecord in _tgtDirtyFoldersList.PriSub)
			{
				String relativePath = myTgtRecord.Key;
				String tgtSecondKey = myTgtRecord.Value;
				String tgtFlag = "" + tgtSecondKey[0];
				FileUnit tgtFileMeta = _tgtDirtyFoldersList.getByPrimary(relativePath);

				if (tgtFlag.Equals("C"))
				{
					_summary.iTgtFolderCreate++;
					Logger.WriteEntry("FOLDER ACTION - Create " + _tgtPath + relativePath);
					if (!Directory.Exists(_srcPath + relativePath))
					{ Directory.CreateDirectory(_srcPath + relativePath); }
					if (!Directory.Exists(_tgtPath + relativePath))
					{ Directory.CreateDirectory(_tgtPath + relativePath); }
					if (!_updatedList.containsPriKey(relativePath))
					{ _updatedList.add(relativePath, tgtFileMeta); }
				}
				else if (tgtFlag.Equals("D"))
				{
					if (_srcCleanFoldersList.containsPriKey(relativePath))
					{
						if (Directory.Exists(_tgtPath + relativePath))
						{ _updatedList.add(relativePath, tgtFileMeta); }
						else
						{
							Directory.Delete(_srcPath + relativePath, true);
							_srcCleanFoldersList.removeByPrimary(relativePath);
							_summary.iSrcFolderDelete++;
							Logger.WriteEntry("FOLDER ACTION - Delete " + _srcPath + relativePath);
						}
					}
				}
			}
			foreach (var myRecord in _srcCleanFoldersList.PriSub)
			{
				String relativePath = myRecord.Key;
				FileUnit fileMeta = _srcCleanFilesList.Primary[relativePath];
				_updatedList.add(relativePath, fileMeta);
			}
			_summary.endTime = DateTime.Now;
			Logger.CloseLog();
		}

		private SyncAction checkConflicts(FileUnit sourceDirtyFile, FileUnit destDirtyFile, String s, String t)
		{
			//Compare every single file in dirty source list with dirty destination list to determine what action 
			//to be taken. There are total of four kinds of flags associate with every file in dirty source list. 
			if (s.Equals("M"))
			{
				if (t.Equals("M"))
				{
					switch (_taskSettings.SrcTgtConflict)
					{
						case TaskSettings.ConflictSrcTgtAction.KeepBothCopies:
							return SyncAction.keepBothCopy;
						case TaskSettings.ConflictSrcTgtAction.KeepLatestCopy:
							if (sourceDirtyFile.LastWriteTime > destDirtyFile.LastWriteTime)	//keep the lastest file if two files are both modified
							{ return SyncAction.keepSource; }
							else
							{ return SyncAction.keepTarget; }
						case TaskSettings.ConflictSrcTgtAction.SourceOverwriteTarget:
							return SyncAction.keepSource;
						case TaskSettings.ConflictSrcTgtAction.TargetOverwriteSource:
							return SyncAction.keepTarget;
						default:
							return SyncAction.keepBothCopy;
					}
				}
				else if (t.Equals("D")) //There are two possibilities for a file which was marked as DELETE on destination dirty list.
				{
					switch (_taskSettings.SrcConflict)
					{
						case TaskSettings.ConflictSrcAction.CopyFileToTarget:
							return SyncAction.keepSource;//copy source to destination
						case TaskSettings.ConflictSrcAction.DeleteSourceFile:
							return SyncAction.DeleteSourceFile;//delete source file
						default:
							return SyncAction.keepSource;
					}
				}
				else if (t.Equals("C"))
				{
					switch (_taskSettings.SrcTgtConflict)
					{
						case TaskSettings.ConflictSrcTgtAction.KeepBothCopies:
							return SyncAction.keepBothCopy;
						case TaskSettings.ConflictSrcTgtAction.KeepLatestCopy:
							if (sourceDirtyFile.LastWriteTime > destDirtyFile.LastWriteTime)	//keep the lastest file if two files are both modified
							{ return SyncAction.keepSource; }
							else
							{ return SyncAction.keepTarget; }
						case TaskSettings.ConflictSrcTgtAction.SourceOverwriteTarget:
							return SyncAction.keepSource;
						case TaskSettings.ConflictSrcTgtAction.TargetOverwriteSource:
							return SyncAction.keepTarget;
						default:
							return SyncAction.keepBothCopy;
					}
				}
				else if (t.Equals("R"))
				{
					switch (_taskSettings.SrcTgtConflict)
					{
						case TaskSettings.ConflictSrcTgtAction.KeepBothCopies:
							return SyncAction.keepBothCopy;
						case TaskSettings.ConflictSrcTgtAction.KeepLatestCopy:
							if (sourceDirtyFile.LastWriteTime > destDirtyFile.LastWriteTime)	//keep the lastest file if two files are both modified
							{ return SyncAction.keepSource; }
							else
							{ return SyncAction.keepTarget; }
						case TaskSettings.ConflictSrcTgtAction.SourceOverwriteTarget:
							return SyncAction.keepSource;
						case TaskSettings.ConflictSrcTgtAction.TargetOverwriteSource:
							return SyncAction.keepTarget;
						default:
							return SyncAction.keepBothCopy;
					}
				}
				else
				{ return SyncAction.noAction; }
			}
			else if (s.Equals("C"))
			{
				if (t.Equals("C"))
				{
					// Check Renaming. We suspect the two files has been renamed(i.e.contents are the same) if both of them 
					// were marked as CREATE, so we need to check their contents.
					switch (_taskSettings.SrcTgtConflict)
					{
						case TaskSettings.ConflictSrcTgtAction.KeepBothCopies:
							if (sourceDirtyFile.LastWriteTime == destDirtyFile.LastWriteTime)
								return SyncAction.noAction;
							else
								return SyncAction.keepBothCopy;
						case TaskSettings.ConflictSrcTgtAction.KeepLatestCopy:
							if (sourceDirtyFile.LastWriteTime > destDirtyFile.LastWriteTime)	//keep the lastest file if two files are both modified
							{ return SyncAction.keepSource; }
							else
							{ return SyncAction.keepTarget; }
						case TaskSettings.ConflictSrcTgtAction.SourceOverwriteTarget:
							return SyncAction.keepSource;
						case TaskSettings.ConflictSrcTgtAction.TargetOverwriteSource:
							return SyncAction.keepTarget;
						default:
							return SyncAction.keepBothCopy;
					}
				}
				else if (t.Equals("D"))
				{
					switch (_taskSettings.SrcConflict)
					{
						case TaskSettings.ConflictSrcAction.CopyFileToTarget:
							return SyncAction.keepSource;//copy source to destination
						case TaskSettings.ConflictSrcAction.DeleteSourceFile:
							return SyncAction.DeleteSourceFile;//delete source file
						default:
							return SyncAction.keepSource;
					}
				}
				else if (t.Equals("R"))
				{
					switch (_taskSettings.SrcTgtConflict)
					{
						case TaskSettings.ConflictSrcTgtAction.KeepBothCopies:
							return SyncAction.keepBothCopy;
						case TaskSettings.ConflictSrcTgtAction.KeepLatestCopy:
							if (sourceDirtyFile.LastWriteTime > destDirtyFile.LastWriteTime)	//keep the lastest file if two files are both modified
							{ return SyncAction.keepSource; }
							else
							{ return SyncAction.keepTarget; }
						case TaskSettings.ConflictSrcTgtAction.SourceOverwriteTarget:
							return SyncAction.keepSource;
						case TaskSettings.ConflictSrcTgtAction.TargetOverwriteSource:
							return SyncAction.keepTarget;
						default:
							return SyncAction.keepBothCopy;
					}
				}
				else if (t.Equals("M"))
				{
					switch (_taskSettings.SrcTgtConflict)
					{
						case TaskSettings.ConflictSrcTgtAction.KeepBothCopies:
							return SyncAction.keepBothCopy;
						case TaskSettings.ConflictSrcTgtAction.KeepLatestCopy:
							if (sourceDirtyFile.LastWriteTime > destDirtyFile.LastWriteTime)	//keep the lastest file if two files are both modified
							{ return SyncAction.keepSource; }
							else
							{ return SyncAction.keepTarget; }
						case TaskSettings.ConflictSrcTgtAction.SourceOverwriteTarget:
							return SyncAction.keepSource;
						case TaskSettings.ConflictSrcTgtAction.TargetOverwriteSource:
							return SyncAction.keepTarget;
						default:
							return SyncAction.keepBothCopy;
					}
				}
				else
				{ return SyncAction.noAction; }
			}
			else if (s.Equals("D"))
			{
				if (t.Equals("C") || t.Equals("M"))
				{
					switch (_taskSettings.TgtConflict)
					{
						case TaskSettings.ConflictTgtAction.CopyFileToSource:
							return SyncAction.keepTarget;//copy destination to source
						case TaskSettings.ConflictTgtAction.DeleteTargetFile:
							return SyncAction.DeleteTargetFile;//delete destination file
						default:
							return SyncAction.keepTarget;
					}
				}
				else if (t.Equals("D"))
				{ return SyncAction.noAction; }
				else
				{ return SyncAction.noAction; }
			}
			else if (s.Equals("R"))
			{
				if (t.Equals("C"))
				{
					switch (_taskSettings.SrcTgtConflict)
					{
						case TaskSettings.ConflictSrcTgtAction.KeepBothCopies:
							return SyncAction.keepBothCopy;
						case TaskSettings.ConflictSrcTgtAction.KeepLatestCopy:
							if (sourceDirtyFile.LastWriteTime > destDirtyFile.LastWriteTime)	//keep the lastest file if two files are both modified
							{ return SyncAction.keepSource; }
							else
							{ return SyncAction.keepTarget; }
						case TaskSettings.ConflictSrcTgtAction.SourceOverwriteTarget:
							return SyncAction.keepSource;
						case TaskSettings.ConflictSrcTgtAction.TargetOverwriteSource:
							return SyncAction.keepTarget;
						default:
							return SyncAction.keepBothCopy;
					}
				}
				else if (t.Equals("D"))
				{
					switch (_taskSettings.SrcConflict)
					{
						case TaskSettings.ConflictSrcAction.CopyFileToTarget:
							return SyncAction.keepSource;//copy source to destination
						case TaskSettings.ConflictSrcAction.DeleteSourceFile:
							return SyncAction.DeleteSourceFile;//delete source file
						default:
							return SyncAction.keepSource;
					}
				}
				else if (t.Equals("R"))
				{
					switch (_taskSettings.SrcTgtConflict)
					{
						case TaskSettings.ConflictSrcTgtAction.KeepBothCopies:
							return SyncAction.keepBothCopy;
						case TaskSettings.ConflictSrcTgtAction.KeepLatestCopy:
							if (sourceDirtyFile.LastWriteTime > destDirtyFile.LastWriteTime)	//keep the lastest file if two files are both modified
							{ return SyncAction.keepSource; }
							else
							{ return SyncAction.keepTarget; }
						case TaskSettings.ConflictSrcTgtAction.SourceOverwriteTarget:
							return SyncAction.keepSource;
						case TaskSettings.ConflictSrcTgtAction.TargetOverwriteSource:
							return SyncAction.keepTarget;
						default:
							return SyncAction.keepBothCopy;
					}
				}
				else if (t.Equals("M"))
				{
					switch (_taskSettings.SrcTgtConflict)
					{
						case TaskSettings.ConflictSrcTgtAction.KeepBothCopies:
							return SyncAction.keepBothCopy;
						case TaskSettings.ConflictSrcTgtAction.KeepLatestCopy:
							if (sourceDirtyFile.LastWriteTime > destDirtyFile.LastWriteTime)	//keep the lastest file if two files are both modified
							{ return SyncAction.keepSource; }
							else
							{ return SyncAction.keepTarget; }
						case TaskSettings.ConflictSrcTgtAction.SourceOverwriteTarget:
							return SyncAction.keepSource;
						case TaskSettings.ConflictSrcTgtAction.TargetOverwriteSource:
							return SyncAction.keepTarget;
						default:
							return SyncAction.keepBothCopy;
					}
				}
				else
				{ return SyncAction.noAction; }
			}
			return SyncAction.noAction;
		}

		private void previewSyncAction(FileUnit srcFile, FileUnit tgtFile, String srcFlag, String tgtFlag,
																	 String srcPath, String tgtPath, FolderDiffLists previewList)
		{
			switch (checkConflicts(srcFile, tgtFile, srcFlag, tgtFlag))
			{
				case SyncAction.keepBothCopy:
					{
						String srcFileName = srcFile.Name;
						String tgtFileName = tgtFile.Name;

            FileUnit src = new FileUnit(srcPath + "\\" + srcFileName);
            src.Match = new FileUnit(tgtPath + "\\" + tgtFileName);
						previewList.KeepBothFilesList.Add(src);
					}
					break;
				case SyncAction.keepLatestCopy:
					{
						if (srcFile.LastWriteTime > tgtFile.LastWriteTime)
						{
							FileUnit src = new FileUnit(srcPath + "\\" + srcFile.Name);
							src.Match = new FileUnit(tgtPath + "\\" + srcFile.Name);
							previewList.NewSourceFilesList.Add(src);
						}
						else if (srcFile.LastWriteTime < tgtFile.LastWriteTime)
						{
							FileUnit tgt = new FileUnit(tgtPath + "\\" + srcFile.Name);
							tgt.Match = new FileUnit(srcPath + "\\" + srcFile.Name);
							previewList.NewSourceFilesList.Add(tgt);
						}
					}
					break;
				case SyncAction.keepSource:
					{
						FileUnit src = new FileUnit(srcPath + "\\" + srcFile.Name);
						src.Match = new FileUnit(tgtPath + "\\" + srcFile.Name);
						previewList.NewSourceFilesList.Add(src);
					}
					break;
				case SyncAction.keepTarget:
					{
						FileUnit tgt = new FileUnit(tgtPath + "\\" + srcFile.Name);
						tgt.Match = new FileUnit(srcPath + "\\" + srcFile.Name);
						previewList.NewSourceFilesList.Add(tgt);
					}
					break;
				case SyncAction.DeleteSourceFile:
					{
						FileUnit src = new FileUnit(srcPath + "\\" + srcFile.Name);
						//src.Match = new FileUnit(tgtPath + "\\" + srcFile.Name);
						previewList.DeleteSourceFilesList.Add(src);
					}
					break;
				case SyncAction.DeleteTargetFile:
					{
						FileUnit tgt = new FileUnit(tgtPath + "\\" + tgtFile.Name);
						//tgt.Match = new FileUnit(srcPath + "\\" + tgtFile.Name);
						previewList.DeleteTargetFilesList.Add(tgt);

					}
					break;
				case SyncAction.noAction:
					break;
			}
		}

		private void executeSyncAction(FileUnit srcFile, FileUnit tgtFile, String srcFlag, String tgtFlag, String srcPath, String tgtPath)
		{
			switch (checkConflicts(srcFile, tgtFile, srcFlag, tgtFlag))
			{
				case SyncAction.keepBothCopy:
					{
						String srcFileName = srcFile.Name;
						String tgtFileName = tgtFile.Name;
						if (srcFile.Name.Equals(tgtFile.Name))
						{
							do
							{
								int iExt = srcFileName.LastIndexOf('.');		// Locate the index position of the file extension
								if (iExt > 0) 	// File with file extension
								{
									srcFileName = srcFileName.Substring(0, iExt) + "(1)" + srcFileName.Substring(iExt);
									tgtFileName = tgtFileName.Substring(0, iExt) + "(2)" + tgtFileName.Substring(iExt);
								}
								else if (iExt == -1)	// File without file extension
								{
									srcFileName = srcFileName + "(1)";
									tgtFileName = tgtFileName + "(2)";
								}
							} while (File.Exists(srcPath + "\\" + srcFileName) || File.Exists(srcPath + "\\" + tgtFileName)
									|| File.Exists(tgtPath + "\\" + srcFileName) || File.Exists(tgtPath + "\\" + tgtFileName));


							File.Move(srcPath + "\\" + srcFile.Name, srcPath + "\\" + srcFileName);
							File.Move(tgtPath + "\\" + tgtFile.Name, tgtPath + "\\" + tgtFileName);
						}
						checkAndCreateFolder(tgtPath + "\\" + srcFileName);
						checkAndCreateFolder(srcPath + "\\" + tgtFileName);
						File.Copy(srcPath + "\\" + srcFileName, tgtPath + "\\" + srcFileName);
						File.Copy(tgtPath + "\\" + tgtFileName, srcPath + "\\" + tgtFileName);

						FileUnit srcFileMeta = new FileUnit(srcPath + "\\" + srcFileName);
						String srcHashcode = MyUtility.computeMyHash(srcFileMeta);
						srcFileMeta.Hash = srcHashcode;

						FileUnit tgtFileMeta = new FileUnit(tgtPath + "\\" + tgtFileName);
						String tgtHashcode = MyUtility.computeMyHash(tgtFileMeta);
						tgtFileMeta.Hash = tgtHashcode;

						_updatedList.add(srcPath.Substring(_srcPath.Length) + "\\" + srcFileName, srcHashcode, srcFileMeta);
						_updatedList.add(tgtPath.Substring(_tgtPath.Length) + "\\" + tgtFileName, tgtHashcode, tgtFileMeta);

						_summary.iSrcFileCopy++; _summary.iTgtFileCopy++;
						Logger.WriteEntry("FILE ACTION - Rename, Copy " + srcPath + "\\" + srcFileName + " to " + tgtPath + "\\" + srcFileName);
						Logger.WriteEntry("FILE ACTION - Rename, Copy " + tgtPath + "\\" + tgtFileName + " to " + srcPath + "\\" + srcFileName);

					}
					break;
				case SyncAction.keepLatestCopy:
					{
						if (srcFile.LastWriteTime > tgtFile.LastWriteTime)
						{
							FileUnit fileMeta = new FileUnit(srcPath + "\\" + srcFile.Name);
							String strHashcode = MyUtility.computeMyHash(fileMeta);
							fileMeta.Hash = strHashcode;

							checkAndCreateFolder(tgtPath + "\\" + srcFile.Name);
							File.Copy(srcPath + "\\" + srcFile.Name, tgtPath + "\\" + srcFile.Name, true);

							_updatedList.add(srcPath.Substring(_srcPath.Length) + "\\" + srcFile.Name, strHashcode, fileMeta);
							_summary.iSrcFileCopy++; _summary.iTgtFileOverwrite++;
							Logger.WriteEntry("FILE ACTION - Copy " + srcPath + "\\" + srcFile.Name + " to " + tgtPath + "\\" + srcFile.Name);
						}
						else if (srcFile.LastWriteTime < tgtFile.LastWriteTime)
						{
							FileUnit fileMeta = new FileUnit(tgtPath + "\\" + tgtFile.Name);
							String strHashcode = MyUtility.computeMyHash(fileMeta);
							fileMeta.Hash = strHashcode;

							checkAndCreateFolder(srcPath + "\\" + tgtFile.Name);
							File.Copy(tgtPath + "\\" + tgtFile.Name, srcPath + "\\" + tgtFile.Name, true);

							_updatedList.add(tgtPath.Substring(_tgtPath.Length) + "\\" + tgtFile.Name, strHashcode, fileMeta);
							_summary.iTgtFileCopy++; _summary.iSrcFileOverwrite++;
							Logger.WriteEntry("FILE ACTION - Copy " + tgtPath + "\\" + tgtFile.Name + " to " + srcPath + "\\" + tgtFile.Name);

						}
					}
					break;
				case SyncAction.keepSource:
					{
						FileUnit fileMeta = new FileUnit(srcPath + "\\" + srcFile.Name);
						String strHashcode = MyUtility.computeMyHash(fileMeta);
						fileMeta.Hash = strHashcode;

						checkAndCreateFolder(tgtPath + "\\" + srcFile.Name);
						File.Copy(srcPath + "\\" + srcFile.Name, tgtPath + "\\" + srcFile.Name, true);

						_updatedList.add(srcPath.Substring(_srcPath.Length) + "\\" + srcFile.Name, strHashcode, fileMeta);

						_summary.iSrcFileCopy++;
						Logger.WriteEntry("FILE ACTION - Copy " + srcPath + "\\" + srcFile.Name + " to " + tgtPath + "\\" + srcFile.Name);
					}
					break;
				case SyncAction.keepTarget:
					{
						FileUnit fileMeta = new FileUnit(tgtPath + "\\" + tgtFile.Name);
						String strHashcode = MyUtility.computeMyHash(fileMeta);
						fileMeta.Hash = strHashcode;

						checkAndCreateFolder(srcPath + "\\" + tgtFile.Name);
						File.Copy(tgtPath + "\\" + tgtFile.Name, srcPath + "\\" + tgtFile.Name, true);

						_updatedList.add(tgtPath.Substring(_tgtPath.Length) + "\\" + tgtFile.Name, strHashcode, fileMeta);

						_summary.iTgtFileCopy++;
						Logger.WriteEntry("FILE ACTION - Copy " + tgtPath + "\\" + tgtFile.Name + " to " + srcPath + "\\" + tgtFile.Name);
					}
					break;
				case SyncAction.DeleteSourceFile:
					{
						File.Delete(srcPath + "\\" + srcFile.Name);
						_summary.iSrcFileDelete++;
						Logger.WriteEntry("FILE ACTION - Delete " + srcPath + "\\" + srcFile.Name);
					}
					break;
				case SyncAction.DeleteTargetFile:
					{
						File.Delete(tgtPath + "\\" + tgtFile.Name);
						_summary.iTgtFileDelete++;
						Logger.WriteEntry("FILE ACTION - Delete " + tgtPath + "\\" + tgtFile.Name);
					}
					break;
				case SyncAction.noAction:
					break;
			}
		}

		private void checkAndCreateFolder(String fullPath)
		{
			String[] strFolders = fullPath.Split('\\');
			String strFolder = strFolders[0];
			
			for (int i = 1; i < strFolders.Length - 1; i++)
			{
				strFolder = strFolder + "\\" + strFolders[i];
				if (!Directory.Exists(strFolder))
					Directory.CreateDirectory(strFolder);
			}
		}
	}
}