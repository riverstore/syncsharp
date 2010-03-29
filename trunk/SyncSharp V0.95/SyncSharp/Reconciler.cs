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
        private List<FileUnit> unChangedFilesList;
        private List<FileUnit> keepBothFilesLists;		// Copy to both destination

        public FolderDiffLists()
        {
            singleSourceFilesList = new List<FileUnit>();
            singleTargetFilesList = new List<FileUnit>();
            newSourceFilesList = new List<FileUnit>();
            newTargetFilesList = new List<FileUnit>();
            deleteSourceFilesList = new List<FileUnit>();
            deleteTargetFilesList = new List<FileUnit>();
            unChangedFilesList = new List<FileUnit>();
            keepBothFilesLists = new List<FileUnit>();
        }

        public List<FileUnit> KeepBothFilesList
        {
            get { return keepBothFilesLists; }
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
        public List<FileUnit> UnChangedFilesList
        {
            get { return unChangedFilesList; }
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

        public Reconciler(FileList srcList, FileList tgtList, SyncTask task, String metaDataDir)
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
            Logger.CreateLog(metaDataDir + @"\" + task.Name + ".log");
            _summary = new SyncSummary();
            _summary.logFile = metaDataDir + @"\" + task.Name + ".log";
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

        public FolderDiffLists PreviewWithMetaData()
        {
            return new FolderDiffLists();
        }

        public void SyncPreviewAction(FolderDiffLists f)
        {
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
            // Check "RENAME" operation on source replica.
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
                            List<String> createFiles = _srcDirtyFilesList.getBySecondary("C-" + srcHashCode);
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
            // Check "RENAME" operation on target replica.
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
                            List<String> createFiles = _tgtDirtyFilesList.getBySecondary("C-" + tgtHashCode);
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

            // Tranverse through to check for "Dirty" Files on source replica.
            foreach (var mySrcRecord in _srcDirtyFilesList.PriSub)
            {
                String relativePath = mySrcRecord.Key;
                String srcSecondKey = mySrcRecord.Value;
                String srcFlag = "" + srcSecondKey[0];
                String srcHashCode = srcSecondKey.Substring(2);
                srcFile = _srcDirtyFilesList.getByPrimary(relativePath);
                if (srcFlag.Equals("D") && _srcRenameList.containsSecKey(relativePath)) // Not a genuine delete, it a rename, skip;
                    continue;
                if (_tgtCleanFilesList.containsPriKey(relativePath))    // Check for same file "Clean" on target replica.
                {
                    if (srcFlag.Equals("M"))
                    {
                        FileUnit fileMeta = new FileUnit(_srcPath + relativePath);
                        String hashCode = Utility.computeMyHash(fileMeta);
                        fileMeta.Hash = hashCode;
                        checkAndCreateFolder(_tgtPath + relativePath);
                        File.Copy(_srcPath + relativePath, _tgtPath + relativePath, true);
                        _updatedList.add(relativePath, hashCode, fileMeta);
                        _tgtCleanFilesList.removeByPrimary(relativePath);
                        _summary.iSrcFileCopy++; _summary.iTgtFileOverwrite++;
                        Logger.WriteEntry("FILE ACTION - Copy " + _srcPath + relativePath + " to " + _tgtPath + relativePath);
                    }
                    else if (srcFlag.Equals("D"))
                    {
                        if (!_srcRenameList.containsSecKey(relativePath))   // Check for genuine delete file.
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
                        if (!_srcRenameList.containsPriKey(relativePath))   // Check for genuine create file.
                        {
                            FileUnit fileMeta = new FileUnit(_srcPath + relativePath);
                            String hashCode = Utility.computeMyHash(fileMeta);
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
                            else if (_tgtDirtyFilesList.containsPriKey(delRelativePath))
                            {
                                String srcRelativePath = ""; String tgtRelativePath = "";
                                String tgtFlag = "";
                                if (_tgtRenameList.containsSecKey(delRelativePath) && !_tgtRenameList.containsPriKey(relativePath))
                                {
                                    tgtFlag = "C";
                                    List<String> lstFiles = _tgtRenameList.SubPri[delRelativePath];
                                    srcRelativePath = relativePath;
                                    tgtRelativePath = lstFiles[0];
                                    tgtFile = _tgtRenameList.getByPrimary(tgtRelativePath);
                                }
                                else
                                {
                                    tgtFlag = "" + _tgtDirtyFilesList.PriSub[delRelativePath][0];
                                    srcRelativePath = relativePath;
                                    tgtRelativePath = delRelativePath;
                                    tgtFile = _tgtDirtyFilesList.getByPrimary(tgtRelativePath);
                                }

                                int iSrcSlash = srcRelativePath.LastIndexOf('\\');
                                int iTgtSlash = tgtRelativePath.LastIndexOf('\\');
                                String srcFilePath = "";
                                if (iSrcSlash > 0)
                                { srcFilePath = srcRelativePath.Substring(0, iSrcSlash); }
                                String tgtFilePath = "";
                                if (iTgtSlash > 0)
                                { tgtFilePath = tgtRelativePath.Substring(0, iTgtSlash); }
                                if (!srcFilePath.Equals(tgtFilePath))
                                {
                                    FileUnit folderMeta;
                                    if (_taskSettings.FolderConflict == TaskSettings.ConflictFolderAction.KeepSourceName)
                                    {
                                        checkAndCreateFolder(_tgtPath + srcRelativePath);
                                        if (!tgtFlag.Equals("D"))
                                        {
                                            folderMeta = new FileUnit(_srcPath + srcFilePath);
                                            if (_srcDirtyFoldersList.containsPriKey(tgtFilePath))
                                                _srcDirtyFoldersList.removeByPrimary(tgtFilePath);
                                            if (_tgtDirtyFoldersList.containsPriKey(tgtFilePath))
                                                _tgtDirtyFoldersList.removeByPrimary(tgtFilePath);
                                            _srcDirtyFoldersList.add(tgtFilePath, "D-" + tgtFilePath, folderMeta);
                                            _tgtDirtyFoldersList.add(tgtFilePath, "D-" + tgtFilePath, folderMeta);
                                            File.Move(_tgtPath + tgtRelativePath, _tgtPath + srcFilePath + tgtRelativePath.Substring(iTgtSlash));
                                            if (!tgtFlag.Equals("M"))
                                                _updatedList.add(srcFilePath + tgtRelativePath.Substring(iTgtSlash), tgtFile.Hash.Substring(2), tgtFile);
                                        }
                                        tgtFilePath = srcFilePath;
                                    }
                                    else
                                    {
                                        checkAndCreateFolder(_srcPath + tgtRelativePath);
                                        if (!tgtFlag.Equals("D"))
                                        {
                                            folderMeta = new FileUnit(_tgtPath + tgtFilePath);
                                            if (_srcDirtyFoldersList.containsPriKey(srcFilePath))
                                                _srcDirtyFoldersList.removeByPrimary(srcFilePath);
                                            if (_tgtDirtyFoldersList.containsPriKey(srcFilePath))
                                                _tgtDirtyFoldersList.removeByPrimary(srcFilePath);
                                            _srcDirtyFoldersList.add(srcFilePath, "D-" + srcFilePath, folderMeta);
                                            _tgtDirtyFoldersList.add(srcFilePath, "D-" + srcFilePath, folderMeta);
                                            File.Move(_srcPath + srcRelativePath, _srcPath + tgtFilePath + srcRelativePath.Substring(iSrcSlash));
                                            if (!tgtFlag.Equals("M"))
                                                _updatedList.add(tgtFilePath + srcRelativePath.Substring(iSrcSlash), srcFile.Hash.Substring(2), srcFile);
                                        }
                                        srcFilePath = tgtFilePath;
                                    }
                                }
                                executeSyncAction(srcFile, tgtFile, srcFlag, tgtFlag, _srcPath + srcFilePath, _tgtPath + tgtFilePath);
                                _tgtDirtyFilesList.removeByPrimary(tgtRelativePath);
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
                            if (_taskSettings.FolderConflict == TaskSettings.ConflictFolderAction.KeepSourceName)
                            {
                                FileUnit folderMeta = new FileUnit(_srcPath + srcFilePath);
                                if (_srcDirtyFoldersList.containsPriKey(tgtFilePath))
                                    _srcDirtyFoldersList.removeByPrimary(tgtFilePath);
                                _srcDirtyFoldersList.add(tgtFilePath, "D-" + tgtFilePath, folderMeta);
                                if (_tgtDirtyFoldersList.containsPriKey(tgtFilePath))
                                    _tgtDirtyFoldersList.removeByPrimary(tgtFilePath);
                                _tgtDirtyFoldersList.add(tgtFilePath, "D-" + tgtFilePath, folderMeta);
                                checkAndCreateFolder(_tgtPath + relativePath);
                                File.Move(_tgtPath + createFilePath, _tgtPath + tgtFilePath + relativePath.Substring(iTgtSlash));
                                _updatedList.add(tgtFilePath + relativePath.Substring(iTgtSlash), tgtFile.Hash.Substring(2), tgtFile);
                                tgtFilePath = srcFilePath;
                            }
                            else
                            {
                                FileUnit folderMeta = new FileUnit(_tgtPath + tgtFilePath);
                                if (_srcDirtyFoldersList.containsPriKey(srcFilePath))
                                    _srcDirtyFoldersList.removeByPrimary(srcFilePath);
                                if (_tgtDirtyFoldersList.containsPriKey(srcFilePath))
                                    _tgtDirtyFoldersList.removeByPrimary(srcFilePath);
                                _srcDirtyFoldersList.add(srcFilePath, "D-" + srcFilePath, folderMeta);
                                _tgtDirtyFoldersList.add(srcFilePath, "D-" + srcFilePath, folderMeta);
                                checkAndCreateFolder(_srcPath + createFilePath);
                                File.Move(_srcPath + relativePath, _srcPath + tgtFilePath + relativePath.Substring(iSrcSlash));
                                _updatedList.add(tgtFilePath + relativePath.Substring(iSrcSlash), srcFile.Hash.Substring(2), srcFile);
                                srcFilePath = tgtFilePath;
                            }
                        }
                        executeSyncAction(srcFile, tgtFile, srcFlag, "C", _srcPath + srcFilePath, _tgtPath + tgtFilePath);
                        _tgtDirtyFilesList.removeByPrimary(createFilePath);
                    }
                    else if (srcFlag.Equals("C") && _srcRenameList.containsPriKey(relativePath))
                    {
                        String delSrcFile = _srcRenameList.PriSub[relativePath];
                        if (_tgtDirtyFilesList.containsPriKey(relativePath))
                        {
                            int iSlash = relativePath.LastIndexOf('\\');
                            String filePath = "";
                            if (iSlash > 0)
                            { filePath = relativePath.Substring(0, iSlash); }
                            executeSyncAction(srcFile, tgtFile, srcFlag, tgtFlag, _srcPath + filePath, _tgtPath + filePath);
                            _tgtDirtyFilesList.removeByPrimary(relativePath);
                            if (_tgtCleanFilesList.containsPriKey(delSrcFile))
                            { _tgtCleanFilesList.removeByPrimary(delSrcFile); }
                            if (File.Exists(_tgtPath + delSrcFile))
                                File.Delete(_tgtPath + delSrcFile);
                            if (tgtFlag.Equals("C") && _tgtRenameList.containsPriKey(relativePath))
                            {
                                String delTgtFile = _tgtRenameList.PriSub[relativePath];
                                if (_srcCleanFilesList.containsPriKey(delTgtFile))
                                { _srcCleanFilesList.removeByPrimary(delTgtFile); }
                                if (File.Exists(_srcPath + delTgtFile))
                                    File.Delete(_srcPath + delTgtFile);
                            }
                        }
                    }
                    else
                    {
                        if (srcFlag.Equals("C") && _tgtRenameList.containsPriKey(relativePath))
                        {
                            String delRelativePath = _tgtRenameList.PriSub[relativePath];
                            if (_srcCleanFilesList.containsPriKey(delRelativePath))
                            { _srcCleanFilesList.removeByPrimary(delRelativePath); }
                            if (File.Exists(_srcPath + delRelativePath))
                                File.Delete(_srcPath + delRelativePath);
                        }
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
                        String hashCode = Utility.computeMyHash(fileMeta);
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
                            String hashCode = Utility.computeMyHash(fileMeta);
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
                            if (_tgtCleanFoldersList.containsPriKey(relativePath))
                                _tgtCleanFoldersList.removeByPrimary(relativePath);
                            if (Directory.Exists(_tgtPath + relativePath))
                            {
                                Directory.Delete(_tgtPath + relativePath, true);
                                _summary.iTgtFolderDelete++;
                                Logger.WriteEntry("FOLDER ACTION - Delete " + _tgtPath + relativePath);
                            }
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
                            if (_srcCleanFoldersList.containsPriKey(relativePath))
                                _srcCleanFoldersList.removeByPrimary(relativePath);
                            if (Directory.Exists(_srcPath + relativePath))
                            {
                                Directory.Delete(_srcPath + relativePath, true);
                                _summary.iSrcFolderDelete++;
                                Logger.WriteEntry("FOLDER ACTION - Delete " + _srcPath + relativePath);
                            }
                        }
                    }
                }
            }
            foreach (var myRecord in _srcCleanFoldersList.PriSub)
            {
                String relativePath = myRecord.Key;
                //FileUnit fileMeta = _srcCleanFilesList.Primary[relativePath];
                FileUnit fileMeta = _srcCleanFoldersList.Primary[relativePath];
                _updatedList.add(relativePath, fileMeta);
            }
            _summary.endTime = DateTime.Now;
            Logger.CloseLog();
        }

        private SyncAction checkConflicts(FileUnit sourceDirtyFile, FileUnit destDirtyFile, String s, String t)
        {
            //Compare every single file in dirty source list with dirty destination list to determine what action 
            //to be taken. There are total of four kinds of flags associate with every file in dirty source list. 
            if (s.Equals("M") || s.Equals("R"))
            {
                if (t.Equals("M") || t.Equals("C") || t.Equals("R"))
                {
                    switch (_taskSettings.SrcTgtConflict)
                    {
                        case TaskSettings.ConflictSrcTgtAction.KeepBothCopies:
                            return SyncAction.KeepBothCopies;
                        case TaskSettings.ConflictSrcTgtAction.KeepLatestCopy:
                            if (sourceDirtyFile.LastWriteTime > destDirtyFile.LastWriteTime)	//keep the lastest file if two files are both modified
                            { return SyncAction.KeepSource; }
                            else
                            { return SyncAction.KeepTarget; }
                        case TaskSettings.ConflictSrcTgtAction.SourceOverwriteTarget:
                            return SyncAction.KeepSource;
                        case TaskSettings.ConflictSrcTgtAction.TargetOverwriteSource:
                            return SyncAction.KeepTarget;
                        default:
                            return SyncAction.KeepBothCopies;
                    }
                }
                else if (t.Equals("D")) //There are two possibilities for a file which was marked as DELETE on destination dirty list.
                {
                    switch (_taskSettings.SrcConflict)
                    {
                        case TaskSettings.ConflictSrcAction.CopyFileToTarget:
                            return SyncAction.KeepSource;//copy source to destination
                        case TaskSettings.ConflictSrcAction.DeleteSourceFile:
                            return SyncAction.DeleteSourceFile;//delete source file
                        default:
                            return SyncAction.KeepSource;
                    }
                }
                else
                { return SyncAction.NoAction; }
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
                            if (sourceDirtyFile.Hash.Equals(destDirtyFile.Hash))
                                return SyncAction.NoAction;
                            else
                                return SyncAction.KeepBothCopies;
                        case TaskSettings.ConflictSrcTgtAction.KeepLatestCopy:
                            if (sourceDirtyFile.LastWriteTime > destDirtyFile.LastWriteTime)	//keep the lastest file if two files are both modified
                            { return SyncAction.KeepSource; }
                            else
                            { return SyncAction.KeepTarget; }
                        case TaskSettings.ConflictSrcTgtAction.SourceOverwriteTarget:
                            return SyncAction.KeepSource;
                        case TaskSettings.ConflictSrcTgtAction.TargetOverwriteSource:
                            return SyncAction.KeepTarget;
                        default:
                            return SyncAction.KeepBothCopies;
                    }
                }
                else if (t.Equals("D"))
                {
                    switch (_taskSettings.SrcConflict)
                    {
                        case TaskSettings.ConflictSrcAction.CopyFileToTarget:
                            return SyncAction.KeepSource;//copy source to destination
                        case TaskSettings.ConflictSrcAction.DeleteSourceFile:
                            return SyncAction.DeleteSourceFile;//delete source file
                        default:
                            return SyncAction.KeepSource;
                    }
                }
                else if (t.Equals("R") || t.Equals("M"))
                {
                    switch (_taskSettings.SrcTgtConflict)
                    {
                        case TaskSettings.ConflictSrcTgtAction.KeepBothCopies:
                            return SyncAction.KeepBothCopies;
                        case TaskSettings.ConflictSrcTgtAction.KeepLatestCopy:
                            if (sourceDirtyFile.LastWriteTime > destDirtyFile.LastWriteTime)	//keep the lastest file if two files are both modified
                            { return SyncAction.KeepSource; }
                            else
                            { return SyncAction.KeepTarget; }
                        case TaskSettings.ConflictSrcTgtAction.SourceOverwriteTarget:
                            return SyncAction.KeepSource;
                        case TaskSettings.ConflictSrcTgtAction.TargetOverwriteSource:
                            return SyncAction.KeepTarget;
                        default:
                            return SyncAction.KeepBothCopies;
                    }
                }
                else
                { return SyncAction.NoAction; }
            }
            else if (s.Equals("D"))
            {
                if (t.Equals("C") || t.Equals("M"))
                {
                    switch (_taskSettings.TgtConflict)
                    {
                        case TaskSettings.ConflictTgtAction.CopyFileToSource:
                            return SyncAction.KeepTarget;//copy destination to source
                        case TaskSettings.ConflictTgtAction.DeleteTargetFile:
                            return SyncAction.DeleteTargetFile;//delete destination file
                        default:
                            return SyncAction.KeepTarget;
                    }
                }
                else if (t.Equals("D"))
                { return SyncAction.NoAction; }
                else
                { return SyncAction.NoAction; }
            }
            else
                return SyncAction.NoAction;
        }

        private void executeSyncAction(FileUnit srcFile, FileUnit tgtFile, String srcFlag, String tgtFlag, String srcPath, String tgtPath)
        {
            switch (checkConflicts(srcFile, tgtFile, srcFlag, tgtFlag))
            {
                case SyncAction.KeepBothCopies:
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
                        String srcHashcode = Utility.computeMyHash(srcFileMeta);
                        srcFileMeta.Hash = srcHashcode;
                        FileUnit tgtFileMeta = new FileUnit(tgtPath + "\\" + tgtFileName);
                        String tgtHashcode = Utility.computeMyHash(tgtFileMeta);
                        tgtFileMeta.Hash = tgtHashcode;
                        _updatedList.add(srcPath.Substring(_srcPath.Length) + "\\" + srcFileName, srcHashcode, srcFileMeta);
                        _updatedList.add(tgtPath.Substring(_tgtPath.Length) + "\\" + tgtFileName, tgtHashcode, tgtFileMeta);
                        _summary.iSrcFileCopy++; _summary.iTgtFileCopy++;
                        Logger.WriteEntry("FILE ACTION - Rename, Copy " + srcPath + "\\" + srcFileName + " to " + tgtPath + "\\" + srcFileName);
                        Logger.WriteEntry("FILE ACTION - Rename, Copy " + tgtPath + "\\" + tgtFileName + " to " + srcPath + "\\" + tgtFileName);
                    }
                    break;
                case SyncAction.KeepLatestCopy:
                    {
                        if (srcFile.LastWriteTime > tgtFile.LastWriteTime)
                        {
                            FileUnit fileMeta = new FileUnit(srcPath + "\\" + srcFile.Name);
                            String strHashcode = Utility.computeMyHash(fileMeta);
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
                            String strHashcode = Utility.computeMyHash(fileMeta);
                            fileMeta.Hash = strHashcode;
                            checkAndCreateFolder(srcPath + "\\" + tgtFile.Name);
                            File.Copy(tgtPath + "\\" + tgtFile.Name, srcPath + "\\" + tgtFile.Name, true);
                            _updatedList.add(tgtPath.Substring(_tgtPath.Length) + "\\" + tgtFile.Name, strHashcode, fileMeta);
                            _summary.iTgtFileCopy++; _summary.iSrcFileOverwrite++;
                            Logger.WriteEntry("FILE ACTION - Copy " + tgtPath + "\\" + tgtFile.Name + " to " + srcPath + "\\" + tgtFile.Name);
                        }
                    }
                    break;
                case SyncAction.KeepSource:
                    {
                        FileUnit fileMeta = new FileUnit(srcPath + "\\" + srcFile.Name);
                        String strHashcode = Utility.computeMyHash(fileMeta);
                        fileMeta.Hash = strHashcode;
                        checkAndCreateFolder(tgtPath + "\\" + srcFile.Name);
                        File.Copy(srcPath + "\\" + srcFile.Name, tgtPath + "\\" + srcFile.Name, true);
                        _updatedList.add(srcPath.Substring(_srcPath.Length) + "\\" + srcFile.Name, strHashcode, fileMeta);
                        _summary.iSrcFileCopy++;
                        Logger.WriteEntry("FILE ACTION - Copy " + srcPath + "\\" + srcFile.Name + " to " + tgtPath + "\\" + srcFile.Name);
                    }
                    break;
                case SyncAction.KeepTarget:
                    {
                        FileUnit fileMeta = new FileUnit(tgtPath + "\\" + tgtFile.Name);
                        String strHashcode = Utility.computeMyHash(fileMeta);
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
                case SyncAction.NoAction:
                    {
                        if (srcFlag.Equals("C") && tgtFlag.Equals("C"))
                        {
                            String relPath = (srcPath + "\\" + srcFile.Name).Substring(_srcPath.Length);
                            if (!_updatedList.containsPriKey(relPath))
                            {
                                FileUnit fileMeta = new FileUnit(_srcPath + relPath);
                                String hashCode = Utility.computeMyHash(fileMeta);
                                fileMeta.Hash = hashCode;
                                _updatedList.add(relPath, hashCode, fileMeta);
                            }
                        }
                    }
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