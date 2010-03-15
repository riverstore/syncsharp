using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SyncSharp.Storage
{
	[Serializable]
	public class CustomDictionary<K1, K2, V>
	{
		private Dictionary<K1, V> primary = new Dictionary<K1, V>();
		private Dictionary<K1, K2> priSub = new Dictionary<K1, K2>();
		private Dictionary<K2, List<K1>> subPri = new Dictionary<K2, List<K1>>();

		public Dictionary<K1, V> Primary
		{
			get { return primary; }
			set { primary = value; }
		}
		public Dictionary<K1, K2> PriSub
		{
			get { return priSub; }
			set { priSub = value; }
		}
		public Dictionary<K2, List<K1>> SubPri
		{
			get { return subPri; }
			set { subPri = value; }
		}

		public void add(K1 primaryKey, V value)
		{
			primary.Add(primaryKey, value);
		}

		public void add(K1 primaryKey, K2 secondaryKey, V value)
		{
			primary.Add(primaryKey, value);
			associate(secondaryKey, primaryKey);
		}

		public void removeByPrimary(K1 primaryKey)
		{
			if (priSub.ContainsKey(primaryKey))
			{
				List<K1> temp = SubPri[priSub[primaryKey]];
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

		//public void removeBySecondary(K2 secondaryKey)
		//{
		//  Primary.Remove(subPri[secondaryKey]);
		//  PriSub.Remove(subPri[secondaryKey]);
		//  SubPri.Remove(secondaryKey);
		//}

		private void associate(K2 secondaryKey, K1 primaryKey)
		{
			if (!primary.ContainsKey(primaryKey))
				throw new KeyNotFoundException(string.Format("The primary dictionary does not contain the key '{0}'", primaryKey));
			if (subPri.ContainsKey(secondaryKey))
			{
				List<K1> temp;
				temp = SubPri[secondaryKey];
				temp.Add(primaryKey);
				SubPri[secondaryKey] = temp;
				PriSub.Add(primaryKey,secondaryKey);
			}
			else
			{
				List<K1> temp = new List<K1>();
				temp.Add(primaryKey);
				SubPri.Add(secondaryKey, temp);
				PriSub.Add(primaryKey, secondaryKey);
			}
		}

		public V getByPrimary(K1 primaryKey)
		{
			if (primary.ContainsKey(primaryKey))
			{
				return Primary[primaryKey];
			}
			else
			{
				throw new KeyNotFoundException(primaryKey + " not found");
			}
		}

		public void setByPrimary(K1 primaryKey, V value)
		{
			primary[primaryKey] = value;
		}

		public List<K1> getBySecondary(K2 secondaryKey)
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

		public bool containsPriKey(K1 primaryKey)
		{
			return Primary.ContainsKey(primaryKey);
		}

		public bool containsSecKey(K2 secondaryKey)
		{
			return SubPri.ContainsKey(secondaryKey);
		}

	}
}