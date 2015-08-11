## SyncSharp Version 2.1 ##
  * Bug fixes
    1. Bug fixed for deletion of task after re-ordering it in PlugSync form

  * Enhancements
    1. Closed view log form after user has chosen to delete the log file
    1. Change UI for PlugSync form

  * Documentations
    1. User Guide V2.1
    1. Developer Guide V2.1

## SyncSharp Version 2.0 ##
  * Feature list
    1. Preview a synchronization task
    1. Restore a backup task
    1. Added in help function

  * Bug fixes
    1. Bug fixed in files attributes filters
    1. Bug fixed for files exclusion filters
    1. Folder filter form crashes upon opening when source or target folder is deleted from disk in the task
    1. System crash when syncing files with absolute path more than 260 characters
    1. Normal file that overwrites a hidden file will cause an exception
    1. Incorrect amount of free space remaining, when folders are already in sync

  * UI enhancements
    1. Change font for count down timer in PlugSync form
    1. Change UI for General tab in Task setup form
    1. Apply button : Disabled upon clicking, and re-enabled upon changes detected
    1. Change the format of logger entries
    1. Added in analyze, restore and help buttons in main form
    1. Added in Synchronization Preview form

  * Documentations
    1. Developer Guide V2.0
    1. User Guide V2.0


## SyncSharp Version 1.0 ##
  * Minor UI enhancements
    1. Disable buttons when multiple tasks are selected for modify, rename and view log
    1. Added in folder icons in folder filter form.
    1. Change UI for PlugSync form

  * Bug fixes
    1. Exclusion of folders filters is not working correctly.
    1. Selecting folders A and A1 is disallowed by the program
    1. Program will create weird folders and files when a root drive is synced
    1. Applying folders filters and will sometimes delete the folders excluded when synced
    1. Files/folders names during detection are case sensitive
    1. SyncSharp crashes when creating log files for task name that contains invalid characters
    1. Metadata files are not deleted when the task is deleted
    1. Importing an invalid profile or profile with existing name will crash the system

  * Documentations
    1. Developer Guide V1.0
    1. User Guide V1.0

## SyncSharp Version 0.9 ##
  * Feature list
    1. Create/delete/Modify tasks
    1. Import/Export synchronization profiles
    1. Configure settings for files conflicts
    1. Synchronize source and target folders
    1. Set inclusion/exclusion filters
    1. Generate log file after each synchronization operation

  * Documentations
    1. Developer Guide V0.9
    1. User Guide V0.9