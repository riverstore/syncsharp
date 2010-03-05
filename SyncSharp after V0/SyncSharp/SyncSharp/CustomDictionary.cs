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
		private Dictionary<K2, K1> subPri = new Dictionary<K2, K1>();

		public Dictionary<K1, V> Primary
		{
			get { return primary; }
		}
		public Dictionary<K1, K2> PriSub
		{
			get { return priSub; }
		}
		public Dictionary<K2, K1> SubPri
		{
			get { return subPri; }
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
			subPri.Remove(priSub[primaryKey]);
			priSub.Remove(primaryKey);
			primary.Remove(primaryKey);
		}

		public void removeBySecondary(K2 secondaryKey)
		{
			primary.Remove(subPri[secondaryKey]);
			priSub.Remove(subPri[secondaryKey]);
			subPri.Remove(secondaryKey);
		}

		private void associate(K2 secondaryKey, K1 primaryKey)
		{
			if (!primary.ContainsKey(primaryKey))
				throw new KeyNotFoundException(string.Format("The primary dictionary does not contain the key '{0}'", primaryKey));
			if (subPri.ContainsKey(secondaryKey))
			{
				subPri[secondaryKey] = primaryKey;
				priSub[primaryKey] = secondaryKey;
			}
			else
			{
				subPri.Add(secondaryKey, primaryKey);
				priSub.Add(primaryKey, secondaryKey);
			}
		}

		public V getByPrimary(K1 primaryKey)
		{
			if (primary.ContainsKey(primaryKey))
			{
				return primary[primaryKey];
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

		public V getBySecondary(K2 secondaryKey)
		{
			if (subPri.ContainsKey(secondaryKey))
			{
				return primary[subPri[secondaryKey]];
			}
			else
			{
				throw new KeyNotFoundException(secondaryKey + " not found");
			}
		}

		public void setBySecondary(K2 secondaryKey, V value)
		{
			if(!subPri.ContainsKey(secondaryKey))
				throw new KeyNotFoundException(string.Format("The primary dictionary does not contain the key '{0}'", secondaryKey));
			primary[subPri[secondaryKey]] = value;
		}

		public bool containsPriKey(K1 primaryKey)
		{
			return primary.ContainsKey(primaryKey);
		}

		public bool containsSecKey(K2 secondaryKey)
		{
			return subPri.ContainsKey(secondaryKey);
		}
			
	}
}