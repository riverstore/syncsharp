SyncSharp V2.0
==============

Copyright (c) 2010
Excalibur.
All rights reserved.

Syncsharp is developed for users who work with multiple computers and 
need to synchronize files that reside in different computers through 
an immediate device such as a USB drive. It is a Windows based application 
that allows users to sync files between multiple computers through an 
immediate device with no installation required. The file synchronization 
process is streamlined through a list of pre-determined user preferences.  



Documentation
-------------

Documentation for SyncSharp V2.0 can be found online:

    http://www.syncsharp.info

FAQs, glossary, wikipages, source code, and a full list of APIs are also available
online at our website for online view or for download.


Issue Tracker and Mailing List
------------------------------

We're accepting bug reports about all aspects of SyncSharp.  Enhances
are also welcome.  Please use the issue tracker:

    http://code.google.com/p/syncsharp/issues/list

If you're not sure whether you're dealing with a bug or a feature, use
the feedback forum:
	
    http://groups.google.com/group/syncsharp-feedback

To subscribe to the list, go to:

    http://groups.google.com.sg/group/syncsharp-feedback/subscribe


Building the application from source code
-----------------------------------------

SyncSharp was built using Microsoft Visual Studio 2008.  The source code for
SyncSHarp is provided as a project solution for SyncSharpV2.0


Running the ATD
---------------

Developers may wish to run our Automatic Test Driver (ATD) on the source files.
A list of test files are provided in the 'ATD Test Cases' folder:
     
    testcases.txt     :  A list of different test cases for the ATD to perform on

For each of the test case files, any line which starts with at least one '*' is
a comment and will not be read by the ATD.

Some setup information needs to be provided  before the ATD can be started, a sample
is given below:

    SETUPSOURCE C:\Users\Chris\Desktop\a\
    SETUPTARGET C:\Users\Chris\Desktop\b\

Setup the source and target folder for the ATD to perform synchronization on.
Note:  That these folders does not have to be currently existing.  They do
however need to be valid folders.

    SOURCESUBDIR C:\Users\Chris\Desktop\a\suba\
    TARGETSUBDIR C:\Users\Chris\Desktop\b\subb\

Profile testing needs the source and target folders described above to contain 1
sub folder each.  In the above example, the source and target folders each 
contain a \suba\ and \subb\ folder respectively


    TGTDELETECONFLICT DELETE
    SRCDELETECONFLICT DELETE
    DOUBLECONFLICT LATEST | KEEPSOURCE | KEEPTARGET
    RENAMECONFLICT TOTARGET

There are four types of conflict settings as shown above, some with more than one option.
Any combination of settings may be used for testing.

To run the ATD use the following command:

1)    SyncSharpATD.exe testcases.txt output.txt

where output.txt is the output location of the ATD results 

2)    Or run the SyncSharpATD project directly from Microsoft Visual Studio 2008.








