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
		#region Attributes
		private CustomDictionary<string, string, FileUnit> _sCleanFiles;
		private CustomDictionary<string, string, FileUnit> _sDirtyFiles;
		private CustomDictionary<string, string, FileUnit> _tCleanFiles;
		private CustomDictionary<string, string, FileUnit> _tDirtyFiles;
		private CustomDictionary<string, string, FileUnit> _sDirtyDirs;
		private CustomDictionary<string, string, FileUnit> _sCleanDirs;
		private CustomDictionary<string, string, FileUnit> _tDirtyDirs;
		private CustomDictionary<string, string, FileUnit> _tCleanDirs;
		private CustomDictionary<string, string, FileUnit> _backupFiles;
		private List<String> _fileExclusions;
		private FileList _sourceList, _targetList;
		private SyncTask _task;
		private long _srcDirtySize, _tgtDirtySize;
		private bool _openFileDetected;
		private CustomDictionary<string, string, FileUnit> _sMetaData, _tMetaData;
		private List<FileUnit> _srcFiles = new List<FileUnit>();
		private List<FileUnit> _tgtFiles = new List<FileUnit>();
		#endregion

		#region Properties
		public CustomDictionary<string, string, FileUnit> SCleanFiles
		{
			get { return _sCleanFiles; }
			set { _sCleanFiles = value; }
		}
		public CustomDictionary<string, string, FileUnit> SDirtyFiles
		{
			get { return _sDirtyFiles; }
			set { _sDirtyFiles = value; }
		}
		public CustomDictionary<string, string, FileUnit> TCleanFiles
		{
			get { return _tCleanFiles; }
			set { _tCleanFiles = value; }
		}
		public CustomDictionary<string, string, FileUnit> TDirtyFiles
		{
			get { return _tDirtyFiles; }
			set { _tDirtyFiles = value; }
		}
		public CustomDictionary<string, string, FileUnit> SDirtyDirs
		{
			get { return _sDirtyDirs; }
			set { _sDirtyDirs = value; }
		}
		public CustomDictionary<string, string, FileUnit> SCleanDirs
		{
			get { return _sCleanDirs; }
			set { _sCleanDirs = value; }
		}
		public CustomDictionary<string, string, FileUnit> TDirtyDirs
		{
			get { return _tDirtyDirs; }
			set { _tDirtyDirs = value; }
		}
		public CustomDictionary<string, string, FileUnit> TCleanDirs
		{
			get { return _tCleanDirs; }
			set { _tCleanDirs = value; }
		}
		public CustomDictionary<string, string, FileUnit> BackupFiles
		{
			get { return _backupFiles; }
			set { _backupFiles = value; }
		}
		public List<String> FileExclusions
		{
			get { return _fileExclusions; }
			set { _fileExclusions = value; }
		}
		public FileList SourceList
		{
			get { return _sourceList; }
			set { _sourceList = value; }
		}
		public FileList TargetList
		{
			get { return _targetList; }
			set { _targetList = value; }
		}
		public SyncTask Task
		{
			get { return _task; }
			set { _task = value; }
		}
		public long SrcDirtySize
		{
			get { return _srcDirtySize; }
			set { _srcDirtySize = value; }
		}
		public long TgtDirtySize
		{
			get { return _tgtDirtySize; }
			set { _tgtDirtySize = value; }
		}
		public bool OpenFileDetected
		{
			get { return _openFileDetected; }
			set { _openFileDetected = value; }
		}
		public CustomDictionary<string, string, FileUnit> SMetaData
		{
			get { return _sMetaData; }
			set { _sMetaData = value; }
		}
		public CustomDictionary<string, string, FileUnit> TMetaData
		{
			get { return _tMetaData; }
			set { _tMetaData = value; }
		}
		public List<FileUnit> SrcFiles
		{
			get { return _srcFiles; }
			set { _srcFiles = value; }
		}
		public List<FileUnit> TgtFiles
		{
			get { return _tgtFiles; }
			set { _tgtFiles = value; }
		}
		#endregion

		#region Constructor
		public Detector(String metaDataDir, SyncTask syncTask)
		{
			_sCleanFiles = new CustomDictionary<string, string, FileUnit>();
			_sDirtyFiles = new CustomDictionary<string, string, FileUnit>();
			_tCleanFiles = new CustomDictionary<string, string, FileUnit>();
			_tDirtyFiles = new CustomDictionary<string, string, FileUnit>();
			_sDirtyDirs = new CustomDictionary<string, string, FileUnit>();
			_sCleanDirs = new CustomDictionary<string, string, FileUnit>();
			_tDirtyDirs = new CustomDictionary<string, string, FileUnit>();
			_tCleanDirs = new CustomDictionary<string, string, FileUnit>();
			_backupFiles = new CustomDictionary<string, string, FileUnit>();
			_fileExclusions = new List<string>();
			_task = syncTask;
			_sMetaData = SyncMetaData.ReadMetaData(metaDataDir + @"\" + syncTask.Name + "src.meta");
			_tMetaData = SyncMetaData.ReadMetaData(metaDataDir + @"\" + syncTask.Name + "tgt.meta");
			_srcDirtySize = 0;
			_tgtDirtySize = 0;
		}
		#endregion

		#region Methods
		/// <summary>
		/// Returns true if Source/Target folders are synchronized
		/// </summary>
		/// <returns></returns>
		public bool IsSynchronized()
		{
			return (_sDirtyFiles.Primary.Count == 0 && _tDirtyFiles.Primary.Count == 0 &&
					_sDirtyDirs.Primary.Count == 0 && _tDirtyDirs.Primary.Count == 0);
		}

		/// <summary>
		/// Returns true if both Source and Target metadata exists
		/// </summary>
		/// <returns></returns>
		public bool MetaDataExists()
		{
			return (_sMetaData != null && _tMetaData != null);
		}

		/// <summary>
		/// Performs a comparison between Source and Target folders.  FileLists for reconciler is generated at the end
		/// </summary>
		public void CompareFolders()
		{
			int sRevPathLen =  _task.Source.Length;
			int tRevPathLen =  _task.Target.Length;

			Stack<string> stack = new Stack<string>();

			stack.Push(_task.Source);
			GetCurrentSrcInfo(_srcFiles, stack);

			stack.Push(_task.Target);
			GetCurrentTgtInfo(_tgtFiles, stack);

			RemoveExclusions(sRevPathLen, tRevPathLen);

			CompareSrcFileUnits(sRevPathLen, _srcFiles, _tgtFiles);
			CompareTgtFileUnits(tRevPathLen, _tgtFiles, _srcFiles);

			AddSrcDeletionToList();
			AddTgtDeletionToList();

			CreateFileLists();
		}

		/// <summary>
		/// Removes files/folders, based on the Filters set up by this SyncTask
		/// </summary>
		/// <param name="sRevPathLen"></param>
		/// <param name="tRevPathLen"></param>
		private void RemoveExclusions(int sRevPathLen, int tRevPathLen)
		{
			List<FileUnit> tempSrcFiles = new List<FileUnit>();
			List<FileUnit> tempDestFiles = new List<FileUnit>();

			#region removeSrcExclusions
			foreach (FileUnit u in _srcFiles)
			{
				String folderRelativePath = u.AbsolutePath.Substring(sRevPathLen);
				if (u.IsDirectory)
				{
					if (!_task.Filters.IsSourceDirExcluded(u.AbsolutePath))
						tempSrcFiles.Add(u);
					else
					{
						if (_sMetaData != null && _sMetaData.containsPriKey(folderRelativePath))
							_sMetaData.removeByPrimary(folderRelativePath);
						u.Hash = folderRelativePath;
						_sCleanDirs.add(folderRelativePath, u.Hash, u);
					}
				}
				else
				{
					String relativePath = u.AbsolutePath.Substring(sRevPathLen);
					bool targetExcluded = false;
					if (File.Exists(_task.Target + relativePath))
						targetExcluded = _task.Filters.IsFileExcluded(new FileInfo(_task.Target + relativePath));
					if (!_task.Filters.IsFileExcluded(new FileInfo(u.AbsolutePath)) && !_task.Filters.IsSourceDirExcluded(u.AbsolutePath)
							&& !targetExcluded)
					{
						tempSrcFiles.Add(u);
					}
					else
					{
						if (_sMetaData != null && _sMetaData.containsPriKey(relativePath))
							_sMetaData.removeByPrimary(relativePath);
						if (_tMetaData != null && _tMetaData.containsPriKey(relativePath))
							_tMetaData.removeByPrimary(relativePath);
						_fileExclusions.Add(relativePath);
						u.Hash = Utility.computeMyHash(u);
						_sCleanFiles.add(relativePath, u.Hash, u);
						if (File.Exists(_task.Target + relativePath))
						{
							FileUnit targetUnit = new FileUnit(_task.Target + relativePath);
							targetUnit.Hash = Utility.computeMyHash(targetUnit);
							_tCleanFiles.add(relativePath, targetUnit.Hash, targetUnit);
						}
					}
				}
			}
			#endregion

			#region removeTgtExclusions
			foreach (FileUnit u in _tgtFiles)
			{
				String folderRelativePath = u.AbsolutePath.Substring(tRevPathLen);
				if (u.IsDirectory)
				{
					if (!_task.Filters.IsTargetDirExcluded(u.AbsolutePath))
						tempDestFiles.Add(u);
					else
					{
						if (_tMetaData != null && _tMetaData.containsPriKey(folderRelativePath))
							_tMetaData.removeByPrimary(folderRelativePath);
						u.Hash = folderRelativePath;
						_tCleanDirs.add(folderRelativePath, u.Hash, u);
					}
				}
				else
				{
					String relativePath = u.AbsolutePath.Substring(tRevPathLen);
					if (!_fileExclusions.Contains(relativePath) && !_task.Filters.IsFileExcluded(new FileInfo(u.AbsolutePath)) &&
						!_task.Filters.IsTargetDirExcluded(u.AbsolutePath))
					{
						tempDestFiles.Add(u);
					}
					else
					{
						if (_sMetaData != null && _sMetaData.containsPriKey(relativePath))
							_sMetaData.removeByPrimary(relativePath);
						if (_tMetaData != null && _tMetaData.containsPriKey(relativePath))
							_tMetaData.removeByPrimary(relativePath);
						_fileExclusions.Add(relativePath);
						if (!_tCleanFiles.containsPriKey(relativePath))
						{
							u.Hash = Utility.computeMyHash(u);
							_tCleanFiles.add(relativePath, u.Hash, u);
						}
					}
				}
			}
			#endregion

			_srcFiles = tempSrcFiles;
			_tgtFiles = tempDestFiles;
		}

		/// <summary>
		/// Retrieves all the files/folders on the Source directory, and checks for any open files
		/// </summary>
		/// <param name="srcFiles"></param>
		/// <param name="stack"></param>
		private void GetCurrentSrcInfo(List<FileUnit> srcFiles, Stack<string> stack)
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
						//if (!_task.Filters.IsFileExcluded(new FileInfo(fileName)))
						srcFiles.Add(new FileUnit(fileName));
					}
					catch
					{
						_openFileDetected = true;
						break;
					}
				}

				foreach (string folderName in Directory.GetDirectories(folder))
				{
					stack.Push(folderName);
					srcFiles.Add(new FileUnit(folderName));
				}
			}
		}

		/// <summary>
		/// Retrieves all the files/folders on the Target directory, and checks for any open files
		/// </summary>
		/// <param name="destFiles"></param>
		/// <param name="stack"></param>
		private void GetCurrentTgtInfo(List<FileUnit> destFiles, Stack<string> stack)
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
						destFiles.Add(new FileUnit(fileName));
					}
					catch
					{
						_openFileDetected = true;
						break;
					}
				}

				foreach (string folderName in Directory.GetDirectories(folder))
				{
					stack.Push(folderName);
					destFiles.Add(new FileUnit(folderName));
				}
			}
		}

		/// <summary>
		/// Iterate through the Source FileUnits, and calls CompareSrcDirs/CompareSrcFiles depending if they are Folders or Files
		/// </summary>
		/// <param name="sRevPathLen"></param>
		/// <param name="srcFiles"></param>
		/// <param name="destFiles"></param>
		private void CompareSrcFileUnits(int sRevPathLen, List<FileUnit> srcFiles, List<FileUnit> destFiles)
		{
			foreach (FileUnit u in srcFiles)
			{
				String folderRelativePath = u.AbsolutePath.Substring(sRevPathLen);
				if (u.IsDirectory)
				{
					CompareSrcDirs(u, folderRelativePath);
				}
				else
				{
					String relativePath = u.AbsolutePath.Substring(sRevPathLen);
					CompareSrcFiles(u, relativePath);
				}
			}
		}

		/// <summary>
		/// Performs comparison of Folders between current state and meta data state
		/// </summary>
		/// <param name="u"></param>
		/// <param name="folderRelativePath"></param>
		private void CompareSrcDirs(FileUnit u, String folderRelativePath)
		{
			if (MetaDataExists())
			{
				if (_sMetaData.Primary.ContainsKey(folderRelativePath))
				{
					u.Hash = folderRelativePath;
					_sCleanDirs.add(folderRelativePath, u.Hash, u);
					_sMetaData.removeByPrimary(folderRelativePath);
				}
				else
				{
					u.Hash = "C-" + folderRelativePath;
					_sDirtyDirs.add(folderRelativePath, u.Hash, u);
					_backupFiles.add(folderRelativePath, u.Hash, u);
				}
			}
			else
			{
				u.Hash = "C-" + folderRelativePath;
				_sDirtyDirs.add(folderRelativePath, u.Hash, u);
				_backupFiles.add(folderRelativePath, u.Hash, u);
			}
		}

		/// <summary>
		/// Performs comparison of Files between current state and meta data state
		/// </summary>
		/// <param name="u"></param>
		/// <param name="relativePath"></param>
		private void CompareSrcFiles(FileUnit u, String relativePath)
		{
			if (MetaDataExists())
			{
				if (_sMetaData.Primary.ContainsKey(relativePath))
				{
					if ((u.LastWriteTime - _sMetaData.getByPrimary(relativePath).LastWriteTime).Duration().TotalSeconds <= _task.Settings.IgnoreTimeChange
						|| Utility.computeMyHash(u).Equals(_sMetaData.getByPrimary(relativePath).Hash))
					{
						_sCleanFiles.add(relativePath, _sMetaData.PriSub[relativePath], u);
						_sMetaData.removeByPrimary(relativePath);
					}
					else
					{
						_srcDirtySize += u.Size;
						u.Hash = "M-" + _sMetaData.PriSub[relativePath];
						_sDirtyFiles.add(relativePath, u.Hash, u);
						_backupFiles.add(relativePath, u.Hash, u);
						_sMetaData.removeByPrimary(relativePath);
					}
				}
				else
				{
					_srcDirtySize += u.Size;
					u.Hash = "C-" + Utility.computeMyHash(u);
					_sDirtyFiles.add(relativePath, u.Hash, u);
					_backupFiles.add(relativePath, u.Hash, u);
				}
			}
			else
			{
				_srcDirtySize += u.Size;
				u.Hash = "C-" + Utility.computeMyHash(u);
				_sDirtyFiles.add(relativePath, u.Hash, u);
				_backupFiles.add(relativePath, u.Hash, u);
			}
		}

		/// <summary>
		/// Iterate through the Target FileUnits, and calls CompareTgtDirs/CompareTgtFiles depending if they are Folders or Files
		/// </summary>
		/// <param name="tRevPathLen"></param>
		/// <param name="destFiles"></param>
		/// <param name="srcFiles"></param>
		private void CompareTgtFileUnits(int tRevPathLen, List<FileUnit> destFiles, List<FileUnit> srcFiles)
		{
			foreach (FileUnit u in destFiles)
			{
				String folderRelativePath = u.AbsolutePath.Substring(tRevPathLen);
				if (u.IsDirectory)
				{
					CompareTgtDirs(u, folderRelativePath);
				}
				else
				{
					String relativePath = u.AbsolutePath.Substring(tRevPathLen);
					CompareTgtFiles(u, relativePath);
				}
			}
		}

		/// <summary>
		/// Performs comparison of Folders between current state and meta data state
		/// </summary>
		/// <param name="u"></param>
		/// <param name="folderRelativePath"></param>
		private void CompareTgtDirs(FileUnit u, String folderRelativePath)
		{
			if (MetaDataExists())
			{
				if (_tMetaData.Primary.ContainsKey(folderRelativePath))
				{
					u.Hash = folderRelativePath;
					_tCleanDirs.add(folderRelativePath, u.Hash, u);
					_tMetaData.removeByPrimary(folderRelativePath);
				}
				else
				{
					u.Hash = "C-" + folderRelativePath;
					_tDirtyDirs.add(folderRelativePath, u.Hash, u);
				}
			}
			else
			{
				u.Hash = "C-" + folderRelativePath;
				_tDirtyDirs.add(folderRelativePath, u.Hash, u);
			}
		}

		/// <summary>
		/// Performs comparison of Files between current state and meta data state
		/// </summary>
		/// <param name="u"></param>
		/// <param name="relativePath"></param>
		private void CompareTgtFiles(FileUnit u, String relativePath)
		{
			if (MetaDataExists())
			{
				if (_tMetaData.Primary.ContainsKey(relativePath))
				{
					if ((u.LastWriteTime - _tMetaData.getByPrimary(relativePath).LastWriteTime).Duration().TotalSeconds <= _task.Settings.IgnoreTimeChange
						|| Utility.computeMyHash(u).Equals(_tMetaData.getByPrimary(relativePath).Hash))
					{
						u.Hash = _tMetaData.PriSub[relativePath];
						_tCleanFiles.add(relativePath, u.Hash, u);
						_tMetaData.removeByPrimary(relativePath);
					}
					else
					{
						_tgtDirtySize += u.Size;
						u.Hash = "M-" + _tMetaData.PriSub[relativePath];
						_tDirtyFiles.add(relativePath, u.Hash, u);
						_tMetaData.removeByPrimary(relativePath);
					}
				}
				else
				{
					_tgtDirtySize += u.Size;
					u.Hash = "C-" + Utility.computeMyHash(u);
					_tDirtyFiles.add(relativePath, u.Hash, u);
				}
			}
			else
			{
				_tgtDirtySize += u.Size;
				u.Hash = "C-" + Utility.computeMyHash(u);
				_tDirtyFiles.add(relativePath, u.Hash, u);
			}
		}

		/// <summary>
		/// Adds remaining Source meta data to Dirty list as deleted files
		/// </summary>
		private void AddSrcDeletionToList()
		{
			if (MetaDataExists())
			{
				foreach (var item in _sMetaData.Primary)
				{
					if (item.Value.IsDirectory)
					{
						item.Value.Hash = "D-" + item.Key;
						_sDirtyDirs.add(item.Key, item.Value.Hash, item.Value);
					}
					else
					{
						item.Value.Hash = "D-" + _sMetaData.PriSub[item.Key];
						_sDirtyFiles.add(item.Key, item.Value.Hash, item.Value);
					}
				}
			}
		}

		/// <summary>
		/// Adds remaining Target meta data to Dirty list as deleted files
		/// </summary>
		private void AddTgtDeletionToList()
		{
			if (MetaDataExists())
			{
				foreach (var item in _tMetaData.Primary)
				{
					if (item.Value.IsDirectory)
					{
						item.Value.Hash = "D-" + item.Key;
						_tDirtyDirs.add(item.Key, item.Value.Hash, item.Value);
					}
					else
					{
						item.Value.Hash = "D-" + _tMetaData.PriSub[item.Key];
						_tDirtyFiles.add(item.Key, item.Value.Hash, item.Value);
					}
				}
			}
		}

		/// <summary>
		/// Generate file lists for reconciler component
		/// </summary>
		private void CreateFileLists()
		{
			_sourceList = new FileList(_sCleanFiles, _sDirtyFiles, _sDirtyDirs, _sCleanDirs);
			_targetList = new FileList(_tCleanFiles, _tDirtyFiles, _tDirtyDirs, _tCleanDirs);
		}

		/// <summary>
		/// Returns source list
		/// </summary>
		/// <returns></returns>
		public FileList GetSrcList()
		{
			return _sourceList;
		}

		/// <summary>
		/// Returns target list
		/// </summary>
		/// <returns></returns>
		public FileList GetTgtList()
		{
			return _targetList;
		}

		#endregion
	}
}