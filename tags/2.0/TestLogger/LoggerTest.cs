using System;
using SyncSharp.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace TestLogger
{
    
    
    /// <summary>
    ///This is a test class for LoggerTest and is uintended
    ///to contain all LoggerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class LoggerTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for SyncSetWriteLog
        /// normal case. start
        ///</summary>
        [TestMethod()]
        public void SyncSetWriteLogTestStart1()
        {
            string machineId = "macID";
            string syncTaskName = "SyncSetWriteLogTestSTART"; 
            bool start = true; 
            bool expected = true;
            bool actual = false;            
                
            actual = Logger.SyncSetWriteLog(machineId, syncTaskName, start);            
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for SyncSetWriteLog
        /// fail case. start. params null or empty
        ///</summary>
        [TestMethod()]
        public void SyncSetWriteLogTestStart2()
        {
            string machineId = "macID";
            string syncTaskName = "";
            bool start = true;
            bool expected = false;
            bool actual = true;
            try
            {
                actual = Logger.SyncSetWriteLog(machineId, syncTaskName, start);
            }
            catch(ArgumentException ae)
            {
                actual = false;
                Assert.AreEqual(expected, actual);
                Logger.WriteErrorLog(ae.Message);
                Logger.WriteErrorLog(ae.StackTrace);
                return; 
            }
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method. Check file output log.");
        }

        /// <summary>
        ///A test for SyncSetWriteLog
        /// normal case. end
        ///</summary>
        [TestMethod()]
        public void SyncSetWriteLogTestEnd1()
        {
            string machineId = "macID";
            string syncTaskName = "SyncSetWriteLogTestEND"; 
            bool start = false; 
            bool expected = true; 
            bool actual = false;

            WriteLogTest();
            actual = Logger.SyncSetWriteLog(machineId, syncTaskName, start);
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for SyncPlanWriteLog
        ///normal case
        [TestMethod()]
        public void SyncPlanWriteLogTest1()
        {
            string machineId = "macID";
            string syncTaskName = "SyncPlanWriteLog"; 
            uint machineCopyTotal = 0; 
            ulong machineCopySize = 0; 
            uint machineDeleteTotal = 0; 
            ulong machineDeleteSize = 0; 
            uint machineRenameTotal = 0; 
            ulong machineRenameSize = 0; 
            uint usbCopyTotal = 0; 
            ulong usbCopySize = 0; 
            uint usbDeleteTotal = 0; 
            ulong usbDeleteSize = 0; 
            uint usbRenameTotal = 0; 
            ulong usbRenameSize = 0; 
            bool expected = true; 
            bool actual = false;
            
            actual = Logger.SyncPlanWriteLog(machineId, syncTaskName, machineCopyTotal, machineCopySize, machineDeleteTotal, machineDeleteSize, machineRenameTotal, machineRenameSize, usbCopyTotal, usbCopySize, usbDeleteTotal, usbDeleteSize, usbRenameTotal, usbRenameSize);            
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for SyncPlanWriteLog
        ///fail case. param null 
        [TestMethod()]
        public void SyncPlanWriteLogTest2()
        {
            string machineId = "macID";
            string syncTaskName = "";
            uint machineCopyTotal = 0;
            ulong machineCopySize = 0;
            uint machineDeleteTotal = 0;
            ulong machineDeleteSize = 0;
            uint machineRenameTotal = 0;
            ulong machineRenameSize = 0;
            uint usbCopyTotal = 0;
            ulong usbCopySize = 0;
            uint usbDeleteTotal = 0;
            ulong usbDeleteSize = 0;
            uint usbRenameTotal = 0;
            ulong usbRenameSize = 0;
            bool expected = false;
            bool actual = true;

            try
            {
                actual = Logger.SyncPlanWriteLog(machineId, syncTaskName, machineCopyTotal, machineCopySize, machineDeleteTotal, machineDeleteSize, machineRenameTotal, machineRenameSize, usbCopyTotal, usbCopySize, usbDeleteTotal, usbDeleteSize, usbRenameTotal, usbRenameSize);
            }
            catch (ArgumentException ae)
            {
                actual = false;
                Assert.AreEqual(expected, actual);
                Logger.WriteErrorLog(ae.Message);
                Logger.WriteErrorLog(ae.StackTrace);
                return;
            }
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method. Check file output log.");
        }

        /// <summary>
        ///A test for SyncPlanWriteLog
        ///fail case. param null 
        [TestMethod()]
        public void SyncPlanWriteLogTest3()
        {
            string machineId = null;
            string syncTaskName = "syt1";
            uint machineCopyTotal = 0;
            ulong machineCopySize = 0;
            uint machineDeleteTotal = 0;
            ulong machineDeleteSize = 0;
            uint machineRenameTotal = 0;
            ulong machineRenameSize = 0;
            uint usbCopyTotal = 0;
            ulong usbCopySize = 0;
            uint usbDeleteTotal = 0;
            ulong usbDeleteSize = 0;
            uint usbRenameTotal = 0;
            ulong usbRenameSize = 0;
            bool expected = false;
            bool actual = true;

            try
            {
                actual = Logger.SyncPlanWriteLog(machineId, syncTaskName, machineCopyTotal, machineCopySize, machineDeleteTotal, machineDeleteSize, machineRenameTotal, machineRenameSize, usbCopyTotal, usbCopySize, usbDeleteTotal, usbDeleteSize, usbRenameTotal, usbRenameSize);
            }
            catch (ArgumentNullException ane)
            {
                actual = false;
                Assert.AreEqual(expected, actual);
                Logger.WriteErrorLog(ane.Message);
                Logger.WriteErrorLog(ane.StackTrace);
                return;
            }
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method. Check file output log.");
        }

        /// <summary>
        ///A test for SyncPlanWriteLog
        ///fail case. param out of range, -ve
        [TestMethod()]
        public void SyncPlanWriteLogTest4()
        {
            string machineId = "mac1";
            string syncTaskName = "syt1";
            uint machineCopyTotal = 0;
            ulong machineCopySize = 0;
            uint machineDeleteTotal = 4;
            ulong machineDeleteSize = 0;
            uint machineRenameTotal = 0;
            ulong machineRenameSize = 0;
            uint usbCopyTotal = 0;
            ulong usbCopySize = 0;
            uint usbDeleteTotal = 0;
            ulong usbDeleteSize = 0;
            uint usbRenameTotal = 0;
            ulong usbRenameSize = 0;
            bool expected = false;
            bool actual = true;

            try
            {
                actual = Logger.SyncPlanWriteLog(machineId, syncTaskName, machineCopyTotal, machineCopySize, machineDeleteTotal, machineDeleteSize, machineRenameTotal, machineRenameSize, usbCopyTotal, usbCopySize, usbDeleteTotal, usbDeleteSize, usbRenameTotal, usbRenameSize);
            }
            catch (ArgumentOutOfRangeException aoore)
            {
                actual = false;
                Assert.AreEqual(expected, actual);
                Logger.WriteErrorLog(aoore.Message);
                Logger.WriteErrorLog(aoore.StackTrace);
                return;
            }
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method. Check file output log.");
        }

        /// <summary>
        ///A test for WriteLog
        /// normal case
        /// machine copy
        ///</summary>
        [TestMethod()]
        public void WriteLogTest()
        {
/*
            _logFileName = metaDataDir + @"\" + syncTaskName + ".log";
            _logFileLocation = metaDataDir + @"\";
            @".\Log\" + syncTaskName + ".log"
*/

            Logger.SyncSetWriteLog(@".\Log\", "SyncPlanWriteLog", true);
            Logger.LogType logType = Logger.LogType.CopySRC;
            string srcPath = @"C:\anc\can\abc.ind";
            ulong srcSize = 6546890;
            string tgtPath = @"H:\anc\can\abc.ind";
            ulong tgtSize = 6546890; 

            bool expected = true; 
            bool actual;
            actual = Logger.WriteLog(logType, srcPath, srcSize, tgtPath, tgtSize);
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for WriteLog
        /// normal case
        /// machine copy
        ///</summary>
        [TestMethod()]
        public void WriteLogTest2a()
        {
            Logger.SyncSetWriteLog(@".\Log\", "SyncPlanWriteLog", true);
            Logger.LogType logType = Logger.LogType.DeleteTGT;
            string srcPath = null;
            ulong srcSize = 6546890;
            string tgtPath = @"H:\anc\can\abc.ind";
            ulong tgtSize = 6546890;

            bool expected = true;
            bool actual;
            actual = Logger.WriteLog(logType, srcPath, srcSize, tgtPath, tgtSize);
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for WriteLog
        /// fail case
        /// delete with 2 paths
        ///</summary>
        [TestMethod()]
        public void WriteLogTest2()
        {
            Logger.SyncSetWriteLog(@".\Log\", "SyncPlanWriteLog", true);
            Logger.LogType logType = Logger.LogType.DeleteTGT;
            string srcPath = @"C:\anc\can\abc.ind";
            ulong srcSize = 6546890;
            string tgtPath = @"H:\anc\can\abc.ind";
            ulong tgtSize = 6546890;
            bool expected = false;
            bool actual = true;
            try
            {
                actual = Logger.WriteLog(logType, srcPath, srcSize, tgtPath, tgtSize);
            }
            catch (ArgumentException ae)
            {
                actual = false;
                Assert.AreEqual(expected, actual);
                Logger.WriteErrorLog(ae.Message);
                Logger.WriteErrorLog(ae.StackTrace);
                return;
            }
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for WriteLog
        /// fail case
        /// copy from target: 1 path null
        ///</summary>
        [TestMethod()]
        public void WriteLogTest3()
        {
            Logger.SyncSetWriteLog(@".\Log\", "SyncPlanWriteLog", true);
            Logger.LogType logType = Logger.LogType.CopyTGT;
 
            string srcPath = null;
            ulong srcSize = 6546890;
            string tgtPath = @"C:\anc\can\abc.ind";
            ulong tgtSize = 6546890;

            bool expected = false;
            bool actual = true;
            try
            {
                actual = Logger.WriteLog(logType, srcPath, srcSize, tgtPath, tgtSize);
            }
            catch (ArgumentOutOfRangeException aoore)
            {
                actual = false;
                Assert.AreEqual(expected, actual);
                Logger.WriteErrorLog(aoore.Message);
                Logger.WriteErrorLog(aoore.StackTrace);
                return;
            }
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }
/*

        /// <summary>
        ///A test for WriteLog
        /// fail case
        /// machine copy
        ///</summary>
        [TestMethod()]
        public void WriteLogTest4()
        {
            uint logType = (uint)Logger.LogType.;
            string machineId = "macID";
            string syncTaskName = "SyncPlanWriteLog";
            string machineSrcPath = @"C:\anc\can\abc.ind";
            ulong machineSrcSize = 6546890;
            string machineDestPath = @"H:\anc\can\abc.ind";
            ulong machineDestSize = 6546890;
            string usbSrcPath = null;
            ulong usbSrcSize = 0;
            string usbDestPath = @"H:\anc\can\abc.ind";
            ulong usbDestSize = 0;
            string errorMsg = @"H:\anc\can\abc.ind";
            bool expected = false;
            bool actual;
            actual = Logger.WriteLog(logType, machineId, syncTaskName, machineSrcPath, machineSrcSize, machineDestPath, machineDestSize, usbSrcPath, usbSrcSize, usbDestPath, usbDestSize, errorMsg);
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }
*/

        /// <summary>
        ///A test for WriteErrorLog
        ///normal case
        [TestMethod()]
        public void WriteErrorLogTest()
        {
            string errorMsg = "klsfskldfn";
            bool expected = true; 
            bool actual;
            actual = Logger.WriteErrorLog(errorMsg);
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for WriteErrorLog
        ///fail case
        /// empty string passed
        [TestMethod()]
        public void WriteErrorLogTest2()
        {
            //string errorMsg = string.Empty;
            string errorMsg = "";
            bool expected = false; 
            bool actual = true;
            try
            {
                actual = Logger.WriteErrorLog(errorMsg);
            }
            catch (ArgumentException ae)
            {
                actual = false;
                Assert.AreEqual(expected, actual);
                Logger.WriteErrorLog(ae.Message);
                Logger.WriteErrorLog(ae.StackTrace);
                return;
            }
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method. Check file output error log.");
        }

        /// <summary>
        ///A test for WriteErrorLog
        ///fail case
        /// null param
        [TestMethod()]
        public void WriteErrorLogTest3()
        {
            string errorMsg = null;
            bool expected = false;
            bool actual = true; 
            
	        try{	        
	            actual = Logger.WriteErrorLog(errorMsg);	
	        }
	        catch (ArgumentNullException ane)
	        {
                actual = false;
                Assert.AreEqual(expected, actual);
	            Logger.WriteErrorLog(ane.Message);
                Logger.WriteErrorLog(ane.StackTrace);	
                return;
	        }
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method. Check file output error log.");
        }

    }
}
