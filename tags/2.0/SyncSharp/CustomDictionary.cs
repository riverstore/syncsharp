using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SyncSharp.Storage
{
    /// <summary>
    /// Written by Loh Jianxiong Christoper
    /// </summary>
    /// <typeparam name="K1"></typeparam>
    /// <typeparam name="K2"></typeparam>
    /// <typeparam name="V"></typeparam>
	[Serializable]
	public class CustomDictionary<K1, K2, V>
	{
		#region Attributes
		private Dictionary<K1, V> _primary = new Dictionary<K1, V>();
		private Dictionary<K1, K2> _priSub = new Dictionary<K1, K2>();
		private Dictionary<K2, List<K1>> _subPri = new Dictionary<K2, List<K1>>(); 
		#endregion

		#region Properties
		public Dictionary<K1, V> Primary
		{
			get { return _primary; }
			set { _primary = value; }
		}
		public Dictionary<K1, K2> PriSub
		{
			get { return _priSub; }
			set { _priSub = value; }
		}
		public Dictionary<K2, List<K1>> SubPri
		{
			get { return _subPri; }
			set { _subPri = value; }
		} 
		#endregion

		#region Methods
		public void Add(K1 primaryKey, V value)
		{
			_primary.Add(primaryKey, value);
		}

		public void Add(K1 primaryKey, K2 secondaryKey, V value)
		{
			_primary.Add(primaryKey, value);
			Associate(secondaryKey, primaryKey);
		}

		public void RemoveByPrimary(K1 primaryKey)
		{
			if (_priSub.ContainsKey(primaryKey))
			{
				List<K1> temp = SubPri[_priSub[primaryKey]];
				if (temp.Contains(primaryKey))
					temp.Remove(primaryKey);
				if (temp.Count == 0)
					SubPri.Remove(PriSub[primaryKey]);
				else
					SubPri[PriSub[primaryKey]] = temp;
				PriSub.Remove(primaryKey);
			}
			Primary.Remove(primaryKey);
		}

		private void Associate(K2 secondaryKey, K1 primaryKey)
		{
			if (!_primary.ContainsKey(primaryKey))
				throw new KeyNotFoundException(string.Format("The _primary dictionary does not contain the key '{0}'", primaryKey));
			if (_subPri.ContainsKey(secondaryKey))
			{
				List<K1> temp;
				temp = SubPri[secondaryKey];
				temp.Add(primaryKey);
				SubPri[secondaryKey] = temp;
				PriSub.Add(primaryKey, secondaryKey);
			}
			else
			{
				List<K1> temp = new List<K1>();
				temp.Add(primaryKey);
				SubPri.Add(secondaryKey, temp);
				PriSub.Add(primaryKey, secondaryKey);
			}
		}

		public V GetByPrimary(K1 primaryKey)
		{
			if (_primary.ContainsKey(primaryKey))
			{
				return Primary[primaryKey];
			}
			else
			{
				throw new KeyNotFoundException(primaryKey + " not found");
			}
		}

		public void SetByPrimary(K1 primaryKey, V value)
		{
			_primary[primaryKey] = value;
		}

		public List<K1> GetBySecondary(K2 secondaryKey)
		{
			if (SubPri.ContainsKey(secondaryKey))
			{
				return SubPri[secondaryKey];
			}
			else
			{
				throw new KeyNotFoundException(secondaryKey + " not found");
			}
		}

		public bool ContainsPriKey(K1 primaryKey)
		{
			return Primary.ContainsKey(primaryKey);
		}

		public bool ContainsSecKey(K2 secondaryKey)
		{
			return SubPri.ContainsKey(secondaryKey);
		} 
		#endregion
	}
}