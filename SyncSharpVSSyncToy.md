<font face='Segoe UI'>
<h2>Introduction</h2>

Our team has run the following test cases on Microsoft SyncToy V2.1 and SyncSharp V2.0. The test cases were performed on the following PC:<br>
<br>
<table><thead><th> <b>Processor</b> </th><th> Intel Core 2 Duo T7700@2.40 GHz </th></thead><tbody>
<tr><td> <b>Ram</b>       </td><td> 2 GB                            </td></tr>
<tr><td> <b>Harddisk space</b> </td><td> 160 GB                          </td></tr>
<tr><td> <b>OS</b>        </td><td> Windows Vista 32 bits           </td></tr></tbody></table>

<hr />

<h3>Test Case 1</h3>

Test Case Scenario:<br>
<br>
<i>(No sync is performed yet)</i><br />
Source folder: 11 875 files, 2 682 folders Total: 11.5 GB <br />
Target folder: Empty Total: 0 B<br>
<br>
Action: 1)Sync folder pair<br>
<br>
Results:<br>
<br>
<table><thead><th> </th><th> <b>SyncSharp</b> </th><th> <b>SyncToy</b> </th></thead><tbody>
<tr><td> Total time taken </td><td> <b>14 mins 27 secs</b> </td><td> <b>15 mins</b> </td></tr></tbody></table>

<hr />

<h3>Test Case 2</h3>

Test Case Scenario:<br>
<br>
<i>(After first time sync)</i><br />
Source folder: 47 files, 0 folders Total: 11.1 GB <br />
Target folder: 47 files, 0 folders Total: 11.1 GB<br>
<br>
Action: 1)Rename the entire source folder. 2)Sync folder pair<br>
<br>
Results:<br>
<br>
<table><thead><th> </th><th> <b>SyncSharp</b> </th><th> <b>SyncToy</b> </th></thead><tbody>
<tr><td> Total time taken </td><td> <b>6 mins 5 secs</b> </td><td> <b>10 mins 20 secs</b> </td></tr></tbody></table>

<hr />

<h3>Test Case 3</h3>

Test Case Scenario:<br>
<br>
<i>(After first time sync)</i><br />
Source folder: 23 547 files, 3 330 folders Total size: 11.3 GB <br />
Target folder: 23 547 files, 3 330 folders Total size: 11.3 GB<br>
<br>
Action: 1)Rename folders, modify, create and delete files. 2)Sync folder pair<br>
<br>
Results:<br>
<br>
<table><thead><th> </th><th> <b>SyncSharp</b> </th><th> <b>SyncToy</b> </th></thead><tbody>
<tr><td> Total time taken </td><td> <b>3 mins 58 secs</b> </td><td> <b>9 mins 4 secs</b> </td></tr></tbody></table>

</font>