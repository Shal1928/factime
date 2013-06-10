using System;
using System.Collections.Generic;
using System.IO;
using Factime.Models;
using UseAbilities.XML.Serialization;

namespace Factime.Stores
{
    public class FactimeSettingsStore : XmlStore<FactimeSettings>
    {
        #region Singleton implementation

        private FactimeSettingsStore()
        {

        }

        private static readonly FactimeSettingsStore Instance = new FactimeSettingsStore();
        public static FactimeSettingsStore GetInstance()
        {
            return Instance;
        }

        #endregion

        private const string FILE_NAME = "FactimeSettingsStore.xml";
        public override string FileName
        {
            get
            {
                return FILE_NAME;
            }
        }

        public override FactimeSettings Load()
        {
            if (!File.Exists(FileName)) base.Save(new FactimeSettings
            {
                DefaultStart = new TimeSpan(9,0,0),
                DefaultEnd = new TimeSpan(18, 0, 0),
                DefaultPreHolidaysCollection = new List<DateTime>(),
                DefaultHolidaysCollection = new List<DateTime>()
            });

            return SerializationUtility.Deserialize<FactimeSettings>(FileName);
        }
    }
}
