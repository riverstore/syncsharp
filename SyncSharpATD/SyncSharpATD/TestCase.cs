using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATDSyncSharp
{
	class TestCase
	{
		#region Attributes
		private String _line, _id, _method, _oldState, _newState, _expected, _actual, _withMeta;
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
		public String OldState
		{
			get { return _oldState; }
			set { _oldState = value; }
		}
		public String NewState
		{
			get { return _newState; }
			set { _newState = value; }
		}
		public String Expected
		{
			get { return _expected; }
			set { _expected = value; }
		}
		public String Actual
		{
			get { return _actual; }
			set { _actual = value; }
		}
		public String WithMeta
		{
			get { return _withMeta; }
			set { _withMeta = value; }
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
			char[] delimiters = new char[] { ':' };
			String[] caseInfo = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
			if (caseInfo.Length < 6) throw new Exception("Wrong test case");
			this._line = line;
			this._id = caseInfo[0].Trim();
			this._method = caseInfo[1].Trim();
			this._withMeta = caseInfo[2].Trim();
			this._oldState = caseInfo[3].Trim();
			this._newState = caseInfo[4].Trim();
			this._expected = caseInfo[5].Trim();
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