using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SyncSharp.Business;
using SyncSharp.DataModel;
using SyncSharp.Storage;
using System.IO;
using System.Threading;
using System.Timers;
using Timer = System.Threading.Timer;

namespace ATDSyncSharp
{
	/// <summary>
	/// Written by Loh Jianxiong Christopher
	/// </summary>
	class ATD
	{
		#region Attributes
		private List<TestCase> _cases = new List<TestCase>();
		private String _inputFile, _outputFile, _source, _target, _taskSource, _taskTarget, _sourceSubDir, _targetSubDir;
		private SyncTask _curTask;
		private TaskSettings _curSettings = new TaskSettings();
		private Filters _curFilters = new Filters();
		private Detector _detectorTester;
		private SyncProfile _curProfile;
		private int _totalCases, _totalPassed, _totalFailed;
		private double _percentPassed;
		private SyncSharpLogic _curLogic;
		#endregion

		#region Properties
		public List<TestCase> Cases
		{
			get { return _cases; }
			set { _cases = value; }
		}
		public String InputFile
		{
			get { return _inputFile; }
			set { _inputFile = value; }
		}
		public String OutputFile
		{
			get { return _inputFile; }
			set { _inputFile = value; }
		}
		public String Source
		{
			get { return _source; }
			set { _source = value; }
		}
		public String Target
		{
			get { return _target; }
			set { _target = value; }
		}
		public String TaskSource
		{
			get { return _taskSource; }
			set { _taskSource = value; }
		}
		public String TaskTarget
		{
			get { return _taskTarget; }
			set { _taskTarget = value; }
		}
		public String SourceSubDir
		{
			get { return _sourceSubDir; }
			set { _sourceSubDir = value; }
		}
		public String TargetSubDir
		{
			get { return _targetSubDir; }
			set { _targetSubDir = value; }
		}
		public SyncTask CurTask
		{
			get { return _curTask; }
			set { _curTask = value; }
		}
		public TaskSettings CurSettings
		{
			get { return _curSettings; }
			set { _curSettings = value; }
		}
		public Filters CurFilters
		{
			get { return _curFilters; }
			set { _curFilters = value; }
		}
		public Detector DetectorTester
		{
			get { return _detectorTester; }
			set { _detectorTester = value; }
		}
		public SyncProfile CurProfile
		{
			get { return _curProfile; }
			set { _curProfile = value; }
		}
		public int TotalCases
		{
			get { return _totalCases; }
			set { _totalCases = value; }
		}
		public int TotalPassed
		{
			get { return _totalPassed; }
			set { _totalPassed = value; }
		}
		public int TotalFailed
		{
			get { return _totalFailed; }
			set { _totalFailed = value; }
		}
		public double PercentPassed
		{
			get { return _percentPassed; }
			set { _percentPassed = value; }
		}
		public SyncSharpLogic CurLogic
		{
			get { return _curLogic; }
			set { _curLogic = value; }
		}
		#endregion

		#region Constructor
		public ATD(String inputFile, String outputFile)
		{
			this._inputFile = inputFile; this._outputFile = outputFile;
			_curSettings.IgnoreTimeChange = 0;
		}
		#endregion

		static void Main(string[] args)
		{
			String inputFile = args[0];
			String outputFile = args[1];
			Logger.LogFileLocation = @".\";
			Logger.LogFileName = @"ATDLog.txt";
			Console.WriteLine("ATD ready for testing, press any key to continue, press q to quit testing...");
			char userKey = (char)Console.ReadKey().KeyChar;

			while (!userKey.Equals('q'))
			{
				ATD myTester = new ATD(inputFile, outputFile);
				myTester.Cleanup();
				myTester.ResetStatistics();
				List<string> allFiles = myTester.ReadTestCases();

				foreach (var testFile in allFiles)
				{
					myTester._cases.Clear();
					myTester.ResetSettings();
					myTester.SetupTest(testFile);

					myTester._curTask = new SyncTask("TestCases", myTester._source, myTester._target, true, false, false,
																					 myTester._curSettings, myTester._curFilters);
					myTester._curLogic = new SyncSharpLogic();
					myTester._curProfile = new SyncProfile(myTester._curLogic.GetMachineID());

					myTester.ExecuteTests(myTester._curTask, myTester._curProfile, myTester._curLogic);

					myTester.WriteResult();
				}
				myTester.GenerateStatistics();
				myTester.PrintStatistics();
				myTester.Cleanup();
				System.Diagnostics.Process.Start(outputFile);
				Console.WriteLine("ATD ready for testing, press any key to continue, press q to quit testing...");
				userKey = (char)Console.ReadKey().KeyChar;
			}
		}

		#region Statistics Methods

		private void ResetStatistics()
		{
			_totalCases = 0;
			_totalPassed = 0;
			_totalFailed = 0;
			_percentPassed = 0;
		}

		private void GenerateStatistics()
		{
			if (_totalCases > 0)
			{
				_percentPassed = ((double)_totalPassed / (double)_totalCases) * (double)100;
			}
		}

		private void PrintStatistics()
		{
			StreamWriter sr = new StreamWriter(_outputFile, true);
			String output = "Total number of test cases: " + _totalCases.ToString() + "\r\n" +
			"Total number of passes: " + _totalPassed.ToString() + "\r\n" +
			"Total number of fails: " + _totalFailed.ToString() + "\r\n" +
			"Total percentage passed: " + _percentPassed.ToString() + "\r\n";
			sr.WriteLine(output);
			sr.Close();
		}

		#endregion

		#region Read/Execute/Write Methods

		private List<string> ReadTestCases()
		{
			List<String> allFiles = new List<String>();
			StreamReader re = File.OpenText(_inputFile);
			String line;
			while ((line = re.ReadLine()) != null)
			{
				if (line.StartsWith("*")) continue;
				allFiles.Add(line);
			}
			return allFiles;

			//foreach (var testFile in allFiles)
			//{
			//  re = File.OpenText(testFile);
			//  while ((line = re.ReadLine()) != null)
			//  {
			//    if (line.Trim().StartsWith("SETUPSOURCE"))
			//    {
			//      _source = line.Substring(11).Trim();
			//      if (!Directory.Exists(_source))
			//        Directory.CreateDirectory(_source);
			//      continue;
			//    }
			//    if (line.Trim().StartsWith("SETUPTARGET"))
			//    {
			//      _target = line.Substring(11).Trim();
			//      if (!Directory.Exists(_target))
			//        Directory.CreateDirectory(_target);
			//      continue;
			//    }
			//    if (line.Trim().StartsWith("TASKSOURCE"))
			//    {
			//      _taskSource = line.Substring(10).Trim();
			//      if (!Directory.Exists(_taskSource))
			//        Directory.CreateDirectory(_taskSource);
			//      continue;
			//    }
			//    if (line.Trim().StartsWith("TASKTARGET"))
			//    {
			//      _taskTarget = line.Substring(10).Trim();
			//      if (!Directory.Exists(_taskTarget))
			//        Directory.CreateDirectory(_taskTarget);
			//      continue;
			//    }
			//    if (line.Trim().StartsWith("SOURCESUBDIR"))
			//    {
			//      _sourceSubDir = line.Substring(12).Trim();
			//      if (!Directory.Exists(_sourceSubDir))
			//        Directory.CreateDirectory(_sourceSubDir);
			//      continue;
			//    }
			//    if (line.Trim().StartsWith("TARGETSUBDIR"))
			//    {
			//      _targetSubDir = line.Substring(12).Trim();
			//      if (!Directory.Exists(_targetSubDir))
			//        Directory.CreateDirectory(_targetSubDir);
			//      continue;
			//    }
			//    if (line.Trim().StartsWith("ADDFILEEXCLUDE"))
			//    {
			//      CurFilters.FileExcludeList.Add(line.Substring(14).Trim());
			//      continue;
			//    }
			//    if (line.Trim().StartsWith("ADDFILEINCLUDE"))
			//    {
			//      CurFilters.FileIncludeList.Add(line.Substring(14).Trim());
			//      continue;
			//    }
			//    if (line.Trim().StartsWith("SRCFOLDEREXCLUDE"))
			//    {
			//      CurFilters.SourceFolderExcludeList.Add(line.Substring(16).Trim());
			//      continue;
			//    }
			//    if (line.Trim().StartsWith("TGTFOLDEREXCLUDE"))
			//    {
			//      CurFilters.TargetFolderExcludeList.Add(line.Substring(16).Trim());
			//      continue;
			//    }
			//    if (!line.Trim().Equals("") && !line.Trim().StartsWith("*"))
			//    {
			//      _cases.Add(new TestCase(line));
			//      _totalCases++;
			//      continue;
			//    }
			//  }
			//}
			//re.Close();
		}

		private void ResetSettings()
		{
			_curFilters = new Filters();
			_curSettings = new TaskSettings();
			_curSettings.IgnoreTimeChange = 0;
		}

		private void SetupTest(string testFile)
		{
			StreamReader re = null;
			String line;
			re = File.OpenText(testFile);
			while ((line = re.ReadLine()) != null)
			{
				if (line.Trim().StartsWith("SETUPSOURCE"))
				{
					_source = line.Substring(11).Trim();
					if (!Directory.Exists(_source))
						Directory.CreateDirectory(_source);
					continue;
				}
				if (line.Trim().StartsWith("SETUPTARGET"))
				{
					_target = line.Substring(11).Trim();
					if (!Directory.Exists(_target))
						Directory.CreateDirectory(_target);
					continue;
				}
				if (line.Trim().StartsWith("TASKSOURCE"))
				{
					_taskSource = line.Substring(10).Trim();
					if (!Directory.Exists(_taskSource))
						Directory.CreateDirectory(_taskSource);
					continue;
				}
				if (line.Trim().StartsWith("TASKTARGET"))
				{
					_taskTarget = line.Substring(10).Trim();
					if (!Directory.Exists(_taskTarget))
						Directory.CreateDirectory(_taskTarget);
					continue;
				}
				if (line.Trim().StartsWith("SOURCESUBDIR"))
				{
					_sourceSubDir = line.Substring(12).Trim();
					if (!Directory.Exists(_sourceSubDir))
						Directory.CreateDirectory(_sourceSubDir);
					continue;
				}
				if (line.Trim().StartsWith("TARGETSUBDIR"))
				{
					_targetSubDir = line.Substring(12).Trim();
					if (!Directory.Exists(_targetSubDir))
						Directory.CreateDirectory(_targetSubDir);
					continue;
				}
				if (line.Trim().StartsWith("ADDFILEEXCLUDE"))
				{
					_curFilters.FileExcludeList.Add(line.Substring(14).Trim());
					continue;
				}
				if (line.Trim().StartsWith("ADDFILEINCLUDE"))
				{
					_curFilters.FileIncludeList.Add(line.Substring(14).Trim());
					continue;
				}
				if (line.Trim().StartsWith("SRCFOLDEREXCLUDE"))
				{
					_curFilters.SourceFolderExcludeList.Add(line.Substring(16).Trim());
					continue;
				}
				if (line.Trim().StartsWith("TGTFOLDEREXCLUDE"))
				{
					_curFilters.TargetFolderExcludeList.Add(line.Substring(16).Trim());
					continue;
				}
				if (line.Trim().StartsWith("TGTDELETECONFLICT"))
				{
					if (line.Substring(17).Trim().Equals("DELETE"))
						_curSettings.SrcConflict = TaskSettings.ConflictSrcAction.DeleteSourceFile;
					continue;
				}
				if (line.Trim().StartsWith("SRCDELETECONFLICT"))
				{
					if (line.Substring(17).Trim().Equals("DELETE"))
						_curSettings.TgtConflict = TaskSettings.ConflictTgtAction.DeleteTargetFile;
					continue;
				}
				if (line.Trim().StartsWith("DOUBLECONFLICT"))
				{
					if (line.Substring(14).Trim().Equals("LATEST"))
						_curSettings.SrcTgtConflict = TaskSettings.ConflictSrcTgtAction.KeepLatestCopy;
					if (line.Substring(14).Trim().Equals("KEEPSOURCE"))
						_curSettings.SrcTgtConflict = TaskSettings.ConflictSrcTgtAction.SourceOverwriteTarget;
					if (line.Substring(14).Trim().Equals("KEEPTARGET"))
						_curSettings.SrcTgtConflict = TaskSettings.ConflictSrcTgtAction.TargetOverwriteSource;
						continue;
				}
				if (line.Trim().StartsWith("RENAMECONFLICT"))
				{
					if (line.Substring(14).Trim().Equals("TOTARGET"))
						_curSettings.FolderConflict = TaskSettings.ConflictFolderAction.KeepTargetName;
					continue;
				}
				if (!line.Trim().Equals("") && !line.Trim().StartsWith("*"))
				{
					_cases.Add(new TestCase(line));
					_totalCases++;
					continue;
				}
			}
			re.Close();
		}

		public void ExecuteTests(SyncTask curTask, SyncProfile curProfile, SyncSharpLogic curLogic)
		{
			foreach (TestCase t in _cases)
			{
				if (t.Method.Equals("CompareFolders"))
				{
					Console.WriteLine("Performing CompareFolders Test for ID: " + t.ID);
					Console.WriteLine("");
					TestCompareFolders(t, curTask);
				}
				else if (t.Method.Equals("Sync"))
				{
					Console.WriteLine("Performing Sync Test for ID: " + t.ID);
					Console.WriteLine("");
					TestSync(t, curTask);
				}
				else if (t.Method.Equals("AddTask"))
				{
					Console.WriteLine("Performing AddTask Test for ID: " + t.ID);
					Console.WriteLine("");
					TestAddTask(t, curProfile);
				}
				else if (t.Method.Equals("TaskExists"))
				{
					Console.WriteLine("Performing TaskExists check for ID: " + t.ID);
					Console.WriteLine("");
					TestTaskExists(t, curProfile);
				}
				else if (t.Method.Equals("GetTask"))
				{
					Console.WriteLine("Performing GetTask check for ID: " + t.ID);
					Console.WriteLine("");
					TestGetTask(t, curProfile);
				}
				else if (t.Method.Equals("RemoveTask"))
				{
					Console.WriteLine("Performing RemoveTask check for ID: " + t.ID);
					Console.WriteLine("");
					TestRemoveTask(t, curProfile);
				}
				else if (t.Method.Equals("CheckProfileExists"))
				{
					Console.WriteLine("Performing CheckProfileExist check for ID: " + t.ID);
					Console.WriteLine("");
					TestCheckProfileExists(t, curLogic);
				}
				else if (t.Method.Equals("LoadProfile"))
				{
					Console.WriteLine("Performing LoadProfile test for ID: " + t.ID);
					Console.WriteLine("");
					TestLoadProfile(t, curLogic);
				}
				else if (t.Method.Equals("CheckAutorun"))
				{
					Console.WriteLine("Performing CheckAutoRun test for ID: " + t.ID);
					Console.WriteLine("");
					TestCheckAutoRun(t, curLogic);
				}
				else
					throw new Exception("unsupported method " + t.Method);
			}
		}

		private void WriteResult()
		{
			StreamWriter sr = new StreamWriter(_outputFile, true);
			//sr.WriteLine(this.PrintStatistics());
			foreach (TestCase t in _cases)
			{
				sr.Write(t.ToOutputString());
				sr.WriteLine();
			}
			sr.WriteLine();
			//sr.WriteLine(this.PrintStatistics());
			sr.Close();
		}

		#endregion

		#region Test Methods

		private void TestCheckAutoRun(TestCase t, SyncSharpLogic curLogic)
		{
			char[] delimiters = new char[] { ',' };
			String parameters = t.Param1;
			String[] inputParameters = parameters.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
			bool autoRun = (inputParameters[0].Equals("True")) ? true : false;
			curLogic.CheckAutorun(autoRun);
			t.Actual = File.Exists(@".\Autorun.inf") ? "True" : "False";
			t.Passed = t.Actual.Equals(t.Param2) ? true : false;
			if (t.Passed) _totalPassed++;
			else _totalFailed++;
		}

		public void TestLoadProfile(TestCase t, SyncSharpLogic curLogic)
		{
			curLogic.LoadProfile();
			curLogic.SaveProfile();
			TestCheckProfileExists(t, curLogic);
		}

		private void TestCheckProfileExists(TestCase t, SyncSharpLogic curLogic)
		{
			t.Actual = (curLogic.CheckProfileExists(curLogic.GetMachineID())) ? "True" : "False";
			t.Passed = (t.Actual.Equals(t.Param1)) ? true : false;
			if (t.Passed) _totalPassed++;
			else _totalFailed++;
		}

		private void TestRemoveTask(TestCase t, SyncProfile curProfile)
		{
			char[] delimiters = new char[] { ',' };
			String parameters = t.Param1;
			String[] inputParameters = parameters.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
			if (curProfile.TaskExists(inputParameters[0]))
			{
				curProfile.RemoveTask(curProfile.GetTask(inputParameters[0]), "");
				t.Actual = "True";
			}
			else
			{
				t.Actual = "False";
			}
			t.Passed = (t.Actual.Equals(t.Param2)) ? true : false;
			if (t.Passed) _totalPassed++;
			else _totalFailed++;
		}

		private void TestGetTask(TestCase t, SyncProfile curProfile)
		{
			char[] delimiters = new char[] { ',' };
			String parameters = t.Param1;
			String[] inputParameters = parameters.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
			SyncTask retrievedTask = curProfile.GetTask(inputParameters[0]);
			if (retrievedTask == null)
			{
				t.Actual = "null";
			}
			else
			{
				t.Actual = retrievedTask.Name;
			}
			t.Passed = (t.Actual.Equals(t.Param2)) ? true : false;
			if (t.Passed) _totalPassed++;
			else _totalFailed++;
		}

		private void TestTaskExists(TestCase t, SyncProfile curProfile)
		{
			char[] delimiters = new char[] { ',' };
			String parameters = t.Param1;
			String[] inputParameters = parameters.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
			t.Actual = curProfile.TaskExists(inputParameters[0]).ToString();
			t.Passed = t.Actual.Equals(t.Param2) ? true : false;
			if (t.Passed) _totalPassed++;
			else _totalFailed++;
		}

		private void TestAddTask(TestCase t, SyncProfile curProfile)
		{
			char[] delimiters = new char[] { ',' };
			String parameters = t.Param1;
			String[] inputParameters = parameters.Split(delimiters);
			Validation.ErrorMsgCode errorTaskName = Validation.CheckTaskName(inputParameters[0], curProfile);
			Validation.ErrorMsgCode errorDuplicate = Validation.CheckFolderPair(ref inputParameters[1], ref inputParameters[2], curProfile, null);

			if (errorTaskName == Validation.ErrorMsgCode.NoError && errorDuplicate == Validation.ErrorMsgCode.NoError)
			{
				SyncTask newTask = new SyncTask(inputParameters[0], inputParameters[1], inputParameters[2]);
				curProfile.AddTask(newTask);
			}

			#region Get error msg code for task name
			switch (errorTaskName)
			{
				case Validation.ErrorMsgCode.EmptyTaskName:
					t.Actual = "EmptyTaskName";
					break;
				case Validation.ErrorMsgCode.DuplicateTaskName:
					t.Actual = "DuplicateTaskName";
					break;
				case Validation.ErrorMsgCode.InvalidTaskName:
					t.Actual = "InvalidTaskName";
					break;
				default:
					t.Actual = "NoError";
					break;
			}
			#endregion

			#region Get error msg code for folder pair
			switch (errorDuplicate)
			{
				case Validation.ErrorMsgCode.EmptySource:
					t.Actual += " EmptySource";
					break;
				case Validation.ErrorMsgCode.EmptyTarget:
					t.Actual += " EmptyTarget";
					break;
				case Validation.ErrorMsgCode.InvalidSource:
					t.Actual += " InvalidSource";
					break;
				case Validation.ErrorMsgCode.InvalidTarget:
					t.Actual += " InvalidTarget";
					break;
				case Validation.ErrorMsgCode.SameSourceTarget:
					t.Actual += " SameSourceTarget";
					break;
				case Validation.ErrorMsgCode.SourceIsASubDirOfTarget:
					t.Actual += " SourceIsASubDirOfTarget";
					break;
				case Validation.ErrorMsgCode.TargetIsASubDirOfSource:
					t.Actual += " TargetIsASubDirOfSource";
					break;
				case Validation.ErrorMsgCode.DuplicateFolderPair:
					t.Actual += " DuplicateFolderPair";
					break;
				default:
					t.Actual += " NoError";
					break;
			}
			#endregion

			t.Passed = (t.Actual.Equals(t.Param2)) ? true : false;
			if (t.Passed) _totalPassed++;
			else _totalFailed++;
		}

		private void TestSync(TestCase t, SyncTask curTask)
		{
			RemoveExistingDirectories(curTask);
			Thread.Sleep(25);
			CreateTestDirectories(curTask);
			Thread.Sleep(25);

			CustomDictionary<String, String, FileUnit> srcMeta = new CustomDictionary<string, string, FileUnit>();
			CustomDictionary<String, String, FileUnit> tgtMeta = new CustomDictionary<string, string, FileUnit>();
			int srcLength = curTask.Source.Length;
			int tgtLength = curTask.Target.Length;
			char[] delimiters = new char[] { ',' };

			String createScenario = t.Param2;
			String[] createFiles = createScenario.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

			CreateOldState(curTask, t, createFiles, srcMeta, srcLength, tgtMeta, tgtLength);
			Thread.Sleep(25);

			String scenario = t.Param3;
			String[] performChanges = scenario.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

			SimulateChanges(curTask, performChanges);

			Detector tester;
			TestCompareFolders(curTask, t, srcMeta, tgtMeta, out tester);

			Reconciler reconciler = new Reconciler(tester.SourceList, tester.TargetList, curTask, "");
			Console.WriteLine("Performing Sync...");
			reconciler.Sync();

			tester = null;
			tester = new Detector("", curTask);
			SyncMetaData.WriteMetaData(@".\srcmetatest", reconciler.UpdatedList);
			SyncMetaData.WriteMetaData(@".\tgtmetatest", reconciler.UpdatedList);
			tester.SMetaData = SyncMetaData.ReadMetaData(@".\srcmetatest");
			tester.TMetaData = SyncMetaData.ReadMetaData(@".\tgtmetatest");
			Console.WriteLine("Comparing Output...");
			tester.CompareFolders();
			tester.IsSynchronized();

			t.Actual = tester.IsSynchronized() ? true.ToString() : false.ToString();
			t.Passed = (t.Actual.Equals(t.Param4.Trim())) ? true : false;
			if (t.Passed) _totalPassed++;
			else _totalFailed++;
			Console.WriteLine("");
		}

		private void TestCompareFolders(TestCase t, SyncTask curTask)
		{
			RemoveExistingDirectories(curTask);
			Thread.Sleep(25);
			CreateTestDirectories(curTask);
			Thread.Sleep(25);

			CustomDictionary<String, String, FileUnit> srcMeta = new CustomDictionary<string, string, FileUnit>();
			CustomDictionary<String, String, FileUnit> tgtMeta = new CustomDictionary<string, string, FileUnit>();
			int srcLength = curTask.Source.Length;
			int tgtLength = curTask.Target.Length;
			char[] delimiters = new char[] { ',' };

			String createScenario = t.Param2;
			String[] createFiles = createScenario.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

			CreateOldState(curTask, t, createFiles, srcMeta, srcLength, tgtMeta, tgtLength);
			Thread.Sleep(25);

			String scenario = t.Param3;
			String[] performChanges = scenario.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

			SimulateChanges(curTask, performChanges);

			Detector tester;
			TestCompareFolders(curTask, t, srcMeta, tgtMeta, out tester);

			TestCompareFoldersOutput(t, tester);
		}

		private void TestCompareFoldersOutput(TestCase t, Detector tester)
		{
			Console.WriteLine("Comparing Output...");
			t.Actual = "";
			int M = 0;
			int C = 0;
			int D = 0;

			// Add sCleanFiles output
			t.Actual += "sCleanFiles[" + tester.SCleanFiles.Primary.Count + "], ";

			#region Add sDirtyFiles output
			foreach (var info in tester.SDirtyFiles.PriSub)
			{
				String start = info.Value.Substring(0, 1);
				switch (start)
				{
					case "M":
						M++;
						break;
					case "C":
						C++;
						break;
					case "D":
						D++;
						break;
				}
			}

			t.Actual += "sDirtyFiles[" + tester.SDirtyFiles.PriSub.Count + ", " + M + "M" + C + "C" + D + "D], ";
			#endregion

			// Add tCleanFiles output
			t.Actual += "tCleanFiles[" + tester.TCleanFiles.Primary.Count + "], ";

			#region Add tDirtyFiles output
			M = 0;
			C = 0;
			D = 0;

			foreach (var info in tester.TDirtyFiles.PriSub)
			{
				String start = info.Value.Substring(0, 1);
				switch (start)
				{
					case "M":
						M++;
						break;
					case "C":
						C++;
						break;
					case "D":
						D++;
						break;
				}
			}

			t.Actual += "tDirtyFiles[" + tester.TDirtyFiles.PriSub.Count + ", " + M + "M" + C + "C" + D + "D], ";
			#endregion

			// Add sCleanFolders output
			t.Actual += "sCleanFolders[" + tester.SCleanDirs.Primary.Count + "], ";

			#region Add sDirtyFolders output
			C = 0;
			D = 0;

			foreach (var info in tester.SDirtyDirs.PriSub)
			{
				String start = info.Value.Substring(0, 1);
				switch (start)
				{
					case "C":
						C++;
						break;
					case "D":
						D++;
						break;
				}
			}

			t.Actual += "sDirtyFolders[" + tester.SDirtyDirs.PriSub.Count + ", " + C + "C" + D + "D], ";
			#endregion

			// Add tCleanFolders output
			t.Actual += "tCleanFolders[" + tester.TCleanDirs.Primary.Count + "], ";

			#region Add tDirtyFolders output
			C = 0;
			D = 0;

			foreach (var info in tester.TDirtyDirs.PriSub)
			{
				String start = info.Value.Substring(0, 1);
				switch (start)
				{
					case "C":
						C++;
						break;
					case "D":
						D++;
						break;
				}
			}

			t.Actual += "tDirtyFolders[" + tester.TDirtyDirs.PriSub.Count + ", " + C + "C" + D + "D]";
			#endregion

			t.Passed = (t.Actual.Equals(t.Param4.Trim())) ? true : false;
			if (t.Passed) _totalPassed++;
			else _totalFailed++;

			Console.WriteLine();
		}

		private void TestCompareFolders(SyncTask curTask, TestCase t, CustomDictionary<string, string, FileUnit> srcMeta, CustomDictionary<string, string, FileUnit> tgtMeta, out Detector tester)
		{
			Console.WriteLine("Performing compareFolders()...");
			tester = new Detector("TestCases", curTask);
			if (t.Param1.Equals("Y"))
			{
				tester.SMetaData = srcMeta;
				tester.TMetaData = tgtMeta;
			}
			else
			{
				tester.SMetaData = null;
				tester.TMetaData = null;
			}

			tester.CompareFolders();
		}

		private void SimulateChanges(SyncTask curTask, string[] performChanges)
		{
			Console.WriteLine("Simulating changes to files/directories...");
			Thread.Sleep(15);
			foreach (string changes in performChanges)
			{
				String type = changes.Trim().Substring(0, 3);
				FileInfo updateFile;
				StreamWriter sw;

				#region Determine and perform changes
				switch (type)
				{
					case "FCR":
						updateFile = new FileInfo(curTask.Target + changes.Trim().Substring(4));
						if (!updateFile.Directory.Exists)
							Directory.CreateDirectory(updateFile.DirectoryName);
						sw = updateFile.CreateText();
						sw.Close();
						Console.WriteLine("Creating file " + changes.Trim().Substring(4) + " on _target...");
						break;
					case "FCL":
						updateFile = new FileInfo(curTask.Source + changes.Trim().Substring(4));
						if (!updateFile.Directory.Exists)
							Directory.CreateDirectory(updateFile.DirectoryName);
						sw = updateFile.CreateText();
						sw.Close();
						Console.WriteLine("Creating file " + changes.Trim().Substring(4) + " on _source...");
						break;
					case "FMR":
						sw = File.AppendText(curTask.Target + changes.Trim().Substring(4));
						sw.WriteLine("New info");
						sw.Close();
						Console.WriteLine("Modifying file " + changes.Trim().Substring(4) + " on _target...");
						break;
					case "FML":
						sw = File.AppendText(curTask.Source + changes.Trim().Substring(4));
						sw.WriteLine("New info");
						sw.Close();
						Console.WriteLine("Modifying file " + changes.Trim().Substring(4) + " on _source...");
						break;
					case "FDR":
						File.Delete(curTask.Target + changes.Trim().Substring(4));
						Console.WriteLine("Deleting file " + changes.Trim().Substring(4) + " on _target...");
						break;
					case "FDL":
						File.Delete(curTask.Source + changes.Trim().Substring(4));
						Console.WriteLine("Deleting file " + changes.Trim().Substring(4) + " on _source...");
						break;
					case "FRR":
						File.Move(curTask.Target + changes.Trim().Substring(4), curTask.Target + @"\Rename" + changes.Trim().Substring(5));
						Console.WriteLine("Renaming file " + changes.Trim().Substring(4) + " on _target...");
						break;
					case "FRL":
						File.Move(curTask.Source + changes.Trim().Substring(4), curTask.Source + @"\Rename" + changes.Trim().Substring(5));
						Console.WriteLine("Renaming file " + changes.Trim().Substring(4) + " on _source...");
						break;
					case "DCR":
						Directory.CreateDirectory(curTask.Target + changes.Trim().Substring(4));
						Console.WriteLine("Creating directory " + changes.Trim().Substring(4) + " on _target...");
						break;
					case "DCL":
						Directory.CreateDirectory(curTask.Source + changes.Trim().Substring(4));
						Console.WriteLine("Creating directory " + changes.Trim().Substring(4) + " on _source...");
						break;
					case "DDR":
						Directory.Delete(curTask.Target + changes.Trim().Substring(4), true);
						Console.WriteLine("Deleting directory " + changes.Trim().Substring(4) + " on _target...");
						break;
					case "DDL":
						Directory.Delete(curTask.Source + changes.Trim().Substring(4), true);
						Console.WriteLine("Deleting directory " + changes.Trim().Substring(4) + " on _source...");
						break;
					case "DRR":
						Directory.Move(curTask.Target + changes.Trim().Substring(4), curTask.Target + @"\Rename" + changes.Trim().Substring(5));
						Console.WriteLine("Renaming directory " + changes.Trim().Substring(4) + " on _target...");
						break;
					case "DRL":
						Directory.Move(curTask.Source + changes.Trim().Substring(4), curTask.Source + @"\Rename" + changes.Trim().Substring(5));
						Console.WriteLine("Renaming directory " + changes.Trim().Substring(4) + " on _source...");
						break;
				}
				#endregion
			}
		}

		private void CreateOldState(SyncTask curTask, TestCase t, string[] createFiles, CustomDictionary<string, string, FileUnit> srcMeta, int srcLength, CustomDictionary<string, string, FileUnit> tgtMeta, int tgtLength)
		{
			foreach (String file in createFiles)
			{
				FileInfo newFile = new FileInfo(curTask.Source + file.Trim());

				if (!newFile.Directory.Exists)
				{
					#region Create directories
					Directory.CreateDirectory(newFile.DirectoryName);
					FileInfo newFileTgt = new FileInfo(curTask.Target + file.Trim());
					Directory.CreateDirectory(newFileTgt.DirectoryName);
					if (t.Param1.Equals("Y"))
					{
						String tempSrc = newFile.DirectoryName + @"\";
						String tempTgt = newFileTgt.DirectoryName + @"\";

						srcMeta.Add(tempSrc.Substring(srcLength), new FileUnit(newFile.DirectoryName));
						tgtMeta.Add(tempTgt.Substring(tgtLength), new FileUnit(newFileTgt.DirectoryName));
					}
					#endregion
				}

				#region Create files
				StreamWriter sw = newFile.CreateText();
				sw.Close();
				File.Copy(curTask.Source + file.Trim(), curTask.Target + file.Trim());
				Console.WriteLine("Creating file " + file.Trim() + " on _source and _target directory...");
				#endregion

				if (t.Param1.Equals("Y"))
				{
					#region Add to metadata list
					FileUnit srcFileUnit = new FileUnit(curTask.Source + file.Trim());
					FileUnit tgtFileUnit = new FileUnit(curTask.Target + file.Trim());
					String temp = null;
					if (file.Trim().StartsWith(@"\"))
						temp = file.Trim().Substring(1);
					else
						temp = file;
					srcMeta.Add(temp.Trim(), Utility.ComputeMyHash(srcFileUnit), srcFileUnit);
					tgtMeta.Add(temp.Trim(), Utility.ComputeMyHash(tgtFileUnit), tgtFileUnit);
					#endregion
				}
			}
		}

		private void CreateTestDirectories(SyncTask curTask)
		{
			Console.WriteLine("Creating Source and Target directories...");
			Directory.CreateDirectory(curTask.Source);
			Directory.CreateDirectory(curTask.Target);
		}

		private void RemoveExistingDirectories(SyncTask curTask)
		{
			bool srcNotDeleted = true;
			bool tgtNotDeleted = true;

			while (srcNotDeleted || tgtNotDeleted)
			{
				try
				{
					#region Remove source directories
					if (Directory.Exists(curTask.Source))
					{
						Directory.Delete(curTask.Source, true);
						Console.WriteLine("Source Exists...Deleting...");
						srcNotDeleted = false;
					}
					else
					{
						srcNotDeleted = false;
					}
					#endregion

					#region Remove target directories
					if (Directory.Exists(curTask.Target))
					{
						Directory.Delete(curTask.Target, true);
						Console.WriteLine("Target Exists...Deleting...");
						tgtNotDeleted = false;
					}
					else
					{
						tgtNotDeleted = false;
					}
					#endregion

				}
				catch
				{
				}
			}
		}

		private void Cleanup()
		{
			Console.WriteLine("Cleaning up test files/directories...\n");
			if (Directory.Exists(_source))
				Directory.Delete(_source, true);
			if (Directory.Exists(_target))
				Directory.Delete(_target, true);
			if (Directory.Exists(_taskSource))
				Directory.Delete(_taskSource, true);
			if (Directory.Exists(_taskTarget))
				Directory.Delete(_taskTarget, true);
			if (File.Exists(@".\srcmetatest"))
				File.Delete(@".\srcmetatest");
			if (File.Exists(@".\tgtmetatest"))
				File.Delete(@".\tgtmetatest");
			if(Directory.Exists(@".\Profiles"))
				Directory.Delete(@".\Profiles",true);
		}

		#endregion
	}
}