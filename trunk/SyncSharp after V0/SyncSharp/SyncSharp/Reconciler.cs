using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SyncSharp.DataModel;
using SyncSharp.Storage;

namespace SyncSharp.Business
{
    public enum SyncAction
    {
        keepBothCopy,
        keepLatestCopy,
        keepSource,
        keepTarget
    }
    
    public class Reconciler
    {
        FileList _srcList;
        FileList _tgtList;
        String _srcPath;
        String _tgtPath;
        TaskSettings _taskSettings;

        public CustomDictionary<string, string, FileUnit> _dirtyList;
        public CustomDictionary<string, string, FileUnit> _cleanList;
				public CustomDictionary<string, string, FileUnit> _updatedList;

        public Reconciler(FileList srcList, FileList tgtList, TaskSettings taskSettings) 
        {
            _srcList = srcList;
            _tgtList = tgtList;
            _taskSettings = taskSettings;
            _srcPath = "";
            _tgtPath = "";
        }

        public void PreviewWithoutMetaData()
        {

        }

        public void PreviewWithMetaData()
        {
        }

        public void SyncWithoutMetaData(String srcPath, String tgtPath)
        {
            // Set initial information.
            _srcPath = srcPath; _tgtPath = tgtPath;
            DirectoryInfo srcStart = new DirectoryInfo(_srcPath);
            DirectoryInfo tgtStart = new DirectoryInfo(_tgtPath);
            _dirtyList = new CustomDictionary<string, string, FileUnit>();
            _cleanList = new CustomDictionary<string, string, FileUnit>();

            // Retrieves local files and folders
            FileInfo[] srcFiles = srcStart.GetFiles();
            DirectoryInfo[] srcFolders = srcStart.GetDirectories();
            FileInfo[] tgtFiles = tgtStart.GetFiles();
            DirectoryInfo[] tgtFolders = tgtStart.GetDirectories();

            // Sync Local Files
            SyncLocalFiles(srcFiles, tgtFiles, _srcPath, _tgtPath);
            // Sync Local Folders
            SyncLocalFolders(srcFolders, tgtFolders, _srcPath, _tgtPath);

						// Combine list for new metadata
						if (_dirtyList.Primary.Count >= _cleanList.Primary.Count)
						{
							foreach (var item in _cleanList.Primary)
							{
								_dirtyList.Primary.Add(item.Key, item.Value);
							}
							foreach (var item in _cleanList.PriSub)
							{
								_dirtyList.PriSub.Add(item.Key, item.Value);
							}
							foreach (var item in _cleanList.SubPri)
							{
								_dirtyList.SubPri.Add(item.Key, item.Value);
							}
							_updatedList = _dirtyList;
						}
						else
						{
							foreach (var item in _dirtyList.Primary)
							{
								_cleanList.Primary.Add(item.Key, item.Value);
							}
							foreach (var item in _dirtyList.PriSub)
							{
								_cleanList.PriSub.Add(item.Key, item.Value);
							}
							foreach (var item in _dirtyList.SubPri)
							{
								_cleanList.SubPri.Add(item.Key, item.Value);
							}
							_updatedList = _cleanList;
						}
        }

        public void SyncWithMetaData()
        {

        }

        // Function: sync local files that without metadata.
        private void SyncLocalFiles(FileInfo[] srcFiles, FileInfo[] tgtFiles, String srcPath, String tgtPath)
        {
            bool bDone = false;
            int iSrc = 0; FileInfo srcFile;
            int iTgt = 0; FileInfo tgtFile;
           

            while (!bDone && (srcFiles.Length > 0 && tgtFiles.Length > 0))
            {
                srcFile = srcFiles[iSrc];
                tgtFile = tgtFiles[iTgt];

                if (srcFile.Name.Equals(tgtFile.Name))
                {
                    if (srcFile.LastWriteTimeUtc == tgtFile.LastWriteTimeUtc)
                    {
                        FileUnit fileMeta = new FileUnit(srcFile.FullName);
                        String relativePath = srcFile.FullName.Substring(_srcPath.Length);
                        String hashCode = MyUtility.computeMyHash(fileMeta);
                        _cleanList.add(relativePath, hashCode, fileMeta);
                    }
                    else
                    {
                        switch (checkConflicts(new FileUnit(srcFile.FullName), new FileUnit(tgtFile.FullName)))
                        {
                            case SyncAction.keepBothCopy:
                                String srcFileName = srcFile.Name.Replace(".", " (1).");
                                String tgtFileName = tgtFile.Name.Replace(".", " (2).");
                                File.Move(srcFile.FullName, srcPath + "\\" + srcFileName);
                                File.Move(tgtFile.FullName, tgtPath + "\\" + tgtFileName);
                                File.Copy(srcPath + "\\" + srcFileName, tgtPath + "\\" + srcFileName);
                                File.Copy(tgtPath + "\\" + tgtFileName, srcPath + "\\" + tgtFileName);

                                String srcFileFullName = srcPath + "\\" + srcFileName;
                                String tgtFileFullName = tgtPath + "\\" + tgtFileName;
                                FileUnit srcFileMeta = new FileUnit(srcFileFullName);
                                FileUnit tgtFileMeta = new FileUnit(srcFileFullName);
                                _dirtyList.add(srcFileFullName.Substring(_srcPath.Length), MyUtility.computeMyHash(srcFileMeta), srcFileMeta);
                                _dirtyList.add(tgtFileFullName.Substring(_tgtPath.Length), MyUtility.computeMyHash(tgtFileMeta), tgtFileMeta);

                                break;
                            case SyncAction.keepLatestCopy:

                                break;
                            case SyncAction.keepSource:

                                break;
                            case SyncAction.keepTarget:

                                break;
                        }
                        // If perform action, add to dirty list;
                    }
                    iSrc++; iTgt++;
                }
                else
                {
                    if (string.Compare(srcFile.Name, tgtFile.Name) < 0)
                    {
                        // Perform Action.
                        String strTarget = tgtPath + "\\" + srcFile.Name;
                        File.Copy(srcFile.FullName, strTarget);
                        // Store MetaData
                        FileUnit fileMeta = new FileUnit(srcFile.FullName);
                        String relativePath = srcFile.FullName.Substring(_srcPath.Length);
                        String hashCode = MyUtility.computeMyHash(fileMeta);
                        _dirtyList.add(relativePath, hashCode, fileMeta);
                        iSrc++;
                    }
                    else
                    {
                        // Perform Action.
                        String strTarget = srcPath + "\\" + tgtFile.Name;
                        File.Copy(tgtFile.FullName, strTarget);
                        // Store MetaData
                        FileUnit fileMeta = new FileUnit(tgtFile.FullName);
                        String relativePath = tgtFile.FullName.Substring(_tgtPath.Length);
                        String hashCode = MyUtility.computeMyHash(fileMeta);
                        _dirtyList.add(relativePath, hashCode, fileMeta);
                        iTgt++;
                    }
                }
                if (iSrc == srcFiles.Length || iTgt == tgtFiles.Length)
                {
                    bDone = true;
                }
            }

            if (iSrc == srcFiles.Length && iTgt < tgtFiles.Length)
            {
                for (int i = iTgt; i < tgtFiles.Length; i++)
                {
                    tgtFile = tgtFiles[i];
                    String strTarget = srcPath + "\\" + tgtFile.Name;
                    File.Copy(tgtFile.FullName, strTarget);
                    // Store MetaData
                    FileUnit fileMeta = new FileUnit(tgtFile.FullName);
                    String relativePath = tgtFile.FullName.Substring(_tgtPath.Length);
                    String hashCode = MyUtility.computeMyHash(fileMeta);
                    _dirtyList.add(relativePath, hashCode, fileMeta);
                }
            }
            else if (iTgt == tgtFiles.Length && iSrc < srcFiles.Length)
            {
                for (int i = iSrc; i < srcFiles.Length; i++)
                {
                    srcFile = srcFiles[i];
                    // Perform Action.
                    String strTarget = tgtPath + "\\" + srcFile.Name;
                    File.Copy(srcFile.FullName, strTarget);
                    // Store MetaData
                    FileUnit fileMeta = new FileUnit(srcFile.FullName);
                    String relativePath = srcFile.FullName.Substring(_srcPath.Length);
                    String hashCode = MyUtility.computeMyHash(fileMeta);
                    _dirtyList.add(relativePath, hashCode, fileMeta);
                }
            }
        }

        //Function: sync local folders that without metadata.
        private void SyncLocalFolders(DirectoryInfo[] srcFolders, DirectoryInfo[] tgtFolders, String srcPath, String tgtPath)
        {
            Boolean bDone = false;
            int iSrc = 0; DirectoryInfo srcFolder;
            int iTgt = 0; DirectoryInfo tgtFolder;

            while (!bDone && (srcFolders.Length > 0 && tgtFolders.Length > 0))
            {
                srcFolder = srcFolders[iSrc];
                tgtFolder = tgtFolders[iTgt];

                if (srcFolder.Name.Equals(tgtFolder.Name))
                {
                    String newSrcPath = srcPath + "\\" + srcFolder.Name;
                    String newTgtPath = tgtPath + "\\" + srcFolder.Name;
                    SyncLocalFiles(srcFolder.GetFiles(), tgtFolder.GetFiles(), newSrcPath, newTgtPath);
                    SyncLocalFolders(srcFolder.GetDirectories(), tgtFolder.GetDirectories(), newSrcPath, newTgtPath);
                    
                    FileUnit fileUnit = new FileUnit(srcFolder.FullName);
                    String relativePath = srcFolder.FullName.Substring(_srcPath.Length);
                    _cleanList.add(relativePath, fileUnit);
                    iSrc++; iTgt++;
                }
                else
                {
                    if (string.Compare(srcFolder.Name, tgtFolder.Name) < 0)
                    {
                        String strTarget = tgtPath + "\\" + srcFolder.Name;
                        Directory.CreateDirectory(strTarget);
                        String newSrcPath = srcPath + "\\" + srcFolder.Name;
                        String newTgtPath = tgtPath + "\\" + srcFolder.Name;
                        SyncLocalFiles(srcFolder.GetFiles(), tgtFolder.GetFiles(), newSrcPath, newTgtPath);
                        SyncLocalFolders(srcFolder.GetDirectories(), tgtFolder.GetDirectories(), newSrcPath, newTgtPath);

                        FileUnit fileUnit = new FileUnit(srcFolder.FullName);
                        String relativePath = srcFolder.FullName.Substring(_srcPath.Length);
                        _dirtyList.add(relativePath, fileUnit);
                        iSrc++;
                    }
                    else
                    {
                        String strTarget = srcPath + "\\" + tgtFolder.Name;
                        Directory.CreateDirectory(strTarget);
                        String newSrcPath = srcPath + "\\" + tgtFolder.Name;
                        String newTgtPath = tgtPath + "\\" + tgtFolder.Name;
                        SyncLocalFiles(srcFolder.GetFiles(), tgtFolder.GetFiles(), newSrcPath, newTgtPath);
                        SyncLocalFolders(srcFolder.GetDirectories(), tgtFolder.GetDirectories(), newSrcPath, newTgtPath);

                        FileUnit fileUnit = new FileUnit(tgtFolder.FullName);
                        String relativePath = tgtFolder.FullName.Substring(_tgtPath.Length);
                        _dirtyList.add(relativePath, fileUnit);
                        iTgt++;
                    }
                }
                if (iSrc == srcFolders.Length || iTgt == tgtFolders.Length)
                {
                    bDone = true;
                }
            }
            if (iSrc == srcFolders.Length && iTgt < tgtFolders.Length)
            {
                for (int i = iTgt; i < tgtFolders.Length; i++)
                {
                    tgtFolder = tgtFolders[i];
                    String strTarget = srcPath + "\\" + tgtFolder.Name;
                    Directory.CreateDirectory(strTarget);
                    String newSrcPath = srcPath + "\\" + tgtFolder.Name;
                    String newTgtPath = tgtPath + "\\" + tgtFolder.Name;
                    srcFolder = new DirectoryInfo(newSrcPath);
                    SyncLocalFiles(srcFolder.GetFiles(), tgtFolder.GetFiles(), newSrcPath, newTgtPath);
                    SyncLocalFolders(srcFolder.GetDirectories(), tgtFolder.GetDirectories(), newSrcPath, newTgtPath);

                    FileUnit fileUnit = new FileUnit(tgtFolder.FullName);
                    String relativePath = tgtFolder.FullName.Substring(_tgtPath.Length);
                    _dirtyList.add(relativePath, fileUnit);
                }
            }
            else if (iTgt == tgtFolders.Length && iSrc < srcFolders.Length)
            {
                for (int i = iSrc; i < srcFolders.Length; i++)
                {
                    srcFolder = srcFolders[i];
                    String strTarget = tgtPath + "\\" + srcFolder.Name;
                    Directory.CreateDirectory(strTarget);
                    String newSrcPath = srcPath + "\\" + srcFolder.Name;
                    String newTgtPath = tgtPath + "\\" + srcFolder.Name;
                    tgtFolder = new DirectoryInfo(newTgtPath);
                    SyncLocalFiles(srcFolder.GetFiles(), tgtFolder.GetFiles(), newSrcPath, newTgtPath);
                    SyncLocalFolders(srcFolder.GetDirectories(), tgtFolder.GetDirectories(), newSrcPath, newTgtPath);

                    FileUnit fileUnit = new FileUnit(srcFolder.FullName);
                    String relativePath = srcFolder.FullName.Substring(_srcPath.Length);
                    _dirtyList.add(relativePath, fileUnit);
                }
            }
        }

        private SyncAction checkConflicts(FileUnit srcFile, FileUnit tgtFile)
        {
            //if (srcFile.LastWriteTime != tgtFile.LastWriteTime)
            //{
                switch (_taskSettings.UpdateConflict)
                {
                    case 0: 
                        return SyncAction.keepBothCopy;
                    case 1: 
                        return SyncAction.keepLatestCopy;
                    case 2: 
                        return SyncAction.keepSource;
                    case 3: 
                        return SyncAction.keepTarget;

                }
            //}
                return SyncAction.keepBothCopy;
        }
    }
}   
    