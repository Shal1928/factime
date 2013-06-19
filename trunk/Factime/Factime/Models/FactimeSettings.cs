using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Factime.Models
{
    [DataContract]
    public class FactimeSettings
    {
        [DataMember]
        public TimeSpan DefaultStart
        {
            get;
            set;
        }

        [DataMember]
        public TimeSpan DefaultEnd
        {
            get;
            set;
        }

        [DataMember]
        public TimeSpan ShortStart
        {
            get;
            set;
        }

        [DataMember]
        public TimeSpan ShortEnd
        {
            get;
            set;
        }

        [DataMember]
        public List<DateTime> DefaultHolidaysCollection
        {
            get; 
            set;
        }

        [DataMember]
        public List<DateTime> DefaultPreHolidaysCollection
        {
            get;
            set;
        }

        [DataMember]
        public string OutputPath
        {
            get; 
            set;
        }
    }
}
