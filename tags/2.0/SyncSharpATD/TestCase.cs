using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATDSyncSharp
{
	/// <summary>
	/// Written by Loh Jianxiong Christopher
	/// </summary>
	class TestCase
	{
		#region Attributes
		private String _line, _id, _method, _param2, _param3, _param4, _actual, _param1;
		private String _comment = "";
		private bool _passed; 
		#endregion

		#region Properties
		public String Line
		{
			get { return _line; }
			set { _line = value; }
		}
		public String ID
		{
			get { return _id; }
			set { _id = value; }
		}
		public String Method
		{
			get { return _method; }
			set { _method = value; }
		}
		public String Param2
		{
			get { return _param2; }
			set { _param2 = value; }
		}
		public String Param3
		{
			get { return _param3; }
			set { _param3 = value; }
		}
		public String Param4
		{
			get { return _param4; }
			set { _param4 = value; }
		}
		public String Actual
		{
			get { return _actual; }
			set { _actual = value; }
		}
		public String Param1
		{
			get { return _param1; }
			set { _param1 = value; }
		}
		public String Comment
		{
			get { return _comment; }
			set { _comment = value; }
		}
		public bool Passed
		{
			get { return _passed; }
			set { _passed = value; }
		} 
		#endregion

		#region Constructor
		public TestCase(String line)
		{
			char[] delimiters = new char[] { ';' };
			String[] caseInfo = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
			if (caseInfo.Length < 6) throw new Exception("Wrong test case");
			this._line = line;
			this._id = caseInfo[0].Trim();
			this._method = caseInfo[1].Trim();
			this._param1 = caseInfo[2].Trim();
			this._param2 = caseInfo[3].Trim();
			this._param3 = caseInfo[4].Trim();
			this._param4 = caseInfo[5].Trim();
			if (caseInfo.Length > 6) this._comment = caseInfo[6];
			this._actual = "";
		} 
		#endregion

		#region Methods
		public String ToOutputString()
		{
			String result = _passed ? "pass" : "failed";
			return result + " [ " + _line + " ] " + (_passed ? "" : _actual) + '\n';
		} 
		#endregion
	}
}