using System;
using System.Collections.Generic;
using System.IO;
using Factime.Models;
using UseAbilities.IoC.Stores.Impl;
using UseAbilities.XML.Serialization;

namespace Factime.Stores
{
    public class FactimeSettingsStore : XmlStoreBase<FactimeSettings>
    {
        #region Singleton implementation

        private FactimeSettingsStore()
        {
            SetFileName("FactimeSettingsStore.xml");
        }

        private static readonly FactimeSettingsStore Instance = new FactimeSettingsStore();
        public static FactimeSettingsStore GetInstance()
        {
            return Instance;
        }

        #endregion

        public override FactimeSettings Load()
        {
            if (!File.Exists(GetFileName())) base.Save(new FactimeSettings
            {
                DefaultStart = new TimeSpan(9,0,0),
                DefaultEnd = new TimeSpan(18, 0, 0),
                DefaultPreHolidaysCollection = new List<DateTime>(),
                DefaultHolidaysCollection = new List<DateTime>()
            });

            return SerializationUtility.Deserialize<FactimeSettings>(GetFileName());
        }
    }
}
