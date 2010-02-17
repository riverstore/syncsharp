using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SyncSharp.DataModel;

namespace SyncSharp.Storage
{
    [Serializable]
    class SyncMetaData
    {
        private Dictionary<string, FileUnit> _metaData;

        public Dictionary<string, FileUnit> MetaData
        {
            set { _metaData = value; }
            get { return _metaData; }
        }

        public bool isMetaDataExists(string name)
        {
            return File.Exists(name);
        }

        public void WriteMetaData(string path)
        {
            /*
            var stmCar   = new FileStream("Car3.car", FileMode.Create);
            var bfmCar = new BinaryFormatter();
            bfmCar.Serialize(stmCar, vehicle);
            */
        }

        public SyncMetaData ReadMetaData(string path)
        {
            /*
            var stmCar   = new FileStream("Car3.car", FileMode.Open);
            var bfmCar = new BinaryFormatter();
            var vehicle = (Car)bfmCar.Deserialize(stmCar);
             */
            return null;
        }
    }
}
