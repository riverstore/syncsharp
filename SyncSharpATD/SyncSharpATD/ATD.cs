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
	class ATD
	{
		#region Attributes
		private List<TestCase> _cases = new List<TestCase>();
		private String _inputFile, _outputFile, _source, _target;
		private SyncTask _curTask;
		private TaskSettings _curSettings = new TaskSettings();
		private Filters _curFilters = new Filters();
		private Detector _detectorTester;
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
			Console.WriteLine("ATD ready for testing, press any key to continue, press q to quit testing...");
			char userKey = (char)Console.ReadKey().KeyChar;

			while (!userKey.Equals('q'))
			{
				ATD myTester = new ATD(inputFile, outputFile);
				myTester.ReadTestCases();
				myTester._curTask = new SyncTask("TestCases", myTester._source, myTester._target, true, false, false, myTester._curSettings, myTester._curFilters);
				myTester.ExecuteTests(myTester._curTask);
				myTester.WriteResult();
				System.Diagnostics.Process.Start(outputFile);
				Console.WriteLine("ATD ready for testing, press any key to continue, press q to quit testing...");
				userKey = (char)Console.ReadKey().KeyChar;
			}
		}

		#region Read/Execute/Write
		private void ReadTestCases()
		{
			List<String> allFiles = new List<String>();
			StreamReader re = File.OpenText(_inputFile);
			String line;
			while((line = re.ReadLine()) != null)
			{
				if(line.StartsWith("*")) continue;
				allFiles.Add(line);
			}
			foreach (var testFile in allFiles)
			{
				re = File.OpenText(testFile);
				while ((line = re.ReadLine()) != null)
				{
					if (line.Trim().StartsWith("SETUPSOURCE"))
					{
						_source = line.Substring(11).Trim();
						continue;
					}
					if (line.Trim().StartsWith("SETUPTARGET"))
					{
						_target = line.Substring(11).Trim();
						continue;
					}
					if (line.Trim().StartsWith("ADDFILEEXCLUDE"))
					{
						CurFilters.FileExcludeList.Add(line.Substring(14).Trim());
						continue;
					}
					if (line.Trim().StartsWith("ADDFILEINCLUDE"))
					{
						CurFilters.FileIncludeList.Add(line.Substring(14).Trim());
						continue;
					}
					if (line.Trim().StartsWith("SRCFOLDEREXCLUDE"))
					{
						CurFilters.SourceFolderExcludeList.Add(line.Substring(16).Trim());
						continue;
					}
					if (line.Trim().StartsWith("TGTFOLDEREXCLUDE"))
					{
						CurFilters.TargetFolderExcludeList.Add(line.Substring(16).Trim());
						continue;
					}
					if (!line.Trim().Equals("") && !line.Trim().StartsWith("*"))
					{
						_cases.Add(new TestCase(line));
						continue;
					}
				}
			}
			re.Close();
		}

		public void ExecuteTests(SyncTask curTask)
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
				else
					throw new Exception("unsupported method " + t.Method);
			}
		}

		private void WriteResult()
		{
			FileInfo file = new FileInfo(_outputFile);
			StreamWriter sr = file.CreateText();
			foreach (TestCase t in _cases)
			{
				sr.Write(t.ToOutputString());
				sr.WriteLine();
			}
			sr.Close();
		}
		#endregion

		private void TestSync(TestCase t, SyncTask curTask)
		{
			RemoveExistingDirectories(curTask);
			Thread.Sleep(15);
			CreateTestDirectories(curTask);
			Thread.Sleep(15);

			CustomDictionary<String, String, FileUnit> srcMeta = new CustomDictionary<string, string, FileUnit>();
			CustomDictionary<String, String, FileUnit> tgtMeta = new CustomDictionary<string, string, FileUnit>();
			int srcLength = curTask.Source.Length;
			int tgtLength = curTask.Target.Length;
			char[] delimiters = new char[] { ',' };

			String createScenario = t.OldState;
			String[] createFiles = createScenario.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

			CreateOldState(curTask, t, createFiles, srcMeta, srcLength, tgtMeta, tgtLength);
			Thread.Sleep(15);

			String scenario = t.NewState;
			String[] performChanges = scenario.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

			SimulateChanges(curTask, performChanges);

			Detector tester;
			TestCompareFolders(curTask, t, srcMeta, tgtMeta, out tester);

			Reconciler reconciler = new Reconciler(tester.SourceList, tester.TargetList, curTask, "");
			Console.WriteLine("Performing Sync...");
			reconciler.Sync();

			tester = null;
			tester = new Detector("", curTask);
			SyncMetaData.WriteMetaData(@".\srcmetatest", reconciler._updatedList);
			SyncMetaData.WriteMetaData(@".\tgtmetatest", reconciler._updatedList);
			tester.SMetaData = SyncMetaData.ReadMetaData(@".\srcmetatest");
			tester.TMetaData = SyncMetaData.ReadMetaData(@".\tgtmetatest");
			Console.WriteLine("Comparing Output...");
			tester.CompareFolders();
			tester.IsSynchronized();

			t.Actual = tester.IsSynchronized() ? true.ToString() : false.ToString();
			t.Passed = (t.Actual.Equals(t.Expected.Trim())) ? true : false;
			Console.WriteLine("");

		}

		private void TestCompareFolders(TestCase t, SyncTask curTask)
		{
			RemoveExistingDirectories(curTask);
			Thread.Sleep(15);
			CreateTestDirectories(curTask);
			Thread.Sleep(15);

			CustomDictionary<String, String, FileUnit> srcMeta = new CustomDictionary<string, string, FileUnit>();
			CustomDictionary<String, String, FileUnit> tgtMeta = new CustomDictionary<string, string, FileUnit>();
			int srcLength = curTask.Source.Length;
			int tgtLength = curTask.Target.Length;
			char[] delimiters = new char[] { ',' };

			String createScenario = t.OldState;
			String[] createFiles = createScenario.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

			CreateOldState(curTask, t, createFiles, srcMeta, srcLength, tgtMeta, tgtLength);
			Thread.Sleep(15);

			String scenario = t.NewState;
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

			t.Actual += "sCleanFiles[" + tester.SCleanFiles.Primary.Count + "], ";

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

			t.Actual += "tCleanFiles[" + tester.TCleanFiles.Primary.Count + "], ";

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

			t.Actual += "sCleanFolders[" + tester.SCleanDirs.Primary.Count + "], ";

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

			t.Actual += "tCleanFolders[" + tester.TCleanDirs.Primary.Count + "], ";

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
			t.Passed = (t.Actual.Equals(t.Expected.Trim())) ? true : false;

			Console.WriteLine();
		}

		private void TestCompareFolders(SyncTask curTask, TestCase t, CustomDictionary<string, string, FileUnit> srcMeta, CustomDictionary<string, string, FileUnit> tgtMeta, out Detector tester)
		{
			Console.WriteLine("Performing compareFolders()...");
			tester = new Detector("TestCases", curTask);
			if (t.WithMeta.Equals("Y"))
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
			}
		}

		private void CreateOldState(SyncTask curTask, TestCase t, string[] createFiles, CustomDictionary<string, string, FileUnit> srcMeta, int srcLength, CustomDictionary<string, string, FileUnit> tgtMeta, int tgtLength)
		{
			foreach (String file in createFiles)
			{
				FileInfo newFile = new FileInfo(curTask.Source + file.Trim());
				if (!newFile.Directory.Exists)
				{
					Directory.CreateDirectory(newFile.DirectoryName);
					FileInfo newFileTgt = new FileInfo(curTask.Target + file.Trim());
					Directory.CreateDirectory(newFileTgt.DirectoryName);
					if (t.WithMeta.Equals("Y"))
					{

						srcMeta.add(newFile.DirectoryName.Substring(srcLength), new FileUnit(newFile.DirectoryName));
						tgtMeta.add(newFileTgt.DirectoryName.Substring(tgtLength), new FileUnit(newFileTgt.DirectoryName));
					}
				}
				StreamWriter sw = newFile.CreateText();
				sw.Close();
				File.Copy(curTask.Source + file.Trim(), curTask.Target + file.Trim());
				Console.WriteLine("Creating file " + file.Trim() + " on _source and _target directory...");
				if (t.WithMeta.Equals("Y"))
				{
					FileUnit srcFileUnit = new FileUnit(curTask.Source + file.Trim());
					FileUnit tgtFileUnit = new FileUnit(curTask.Target + file.Trim());
					srcMeta.add(file.Trim(), Utility.computeMyHash(srcFileUnit), srcFileUnit);
					tgtMeta.add(file.Trim(), Utility.computeMyHash(tgtFileUnit), tgtFileUnit);
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
				}
				catch
				{
				} 
			}
		}
	}
}