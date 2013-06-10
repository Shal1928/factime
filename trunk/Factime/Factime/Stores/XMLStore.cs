using System.IO;
using UseAbilities.IoC.Stores;
using UseAbilities.XML.Serialization;

namespace Factime.Stores
{
    public class XmlStore<T> : FileStore<T>, IXmlStore<T>
    {
        public override void Save(T storeObject)
        {
            SerializationUtility.Serialize(storeObject, FileName);
        }

        public override T Load()
        {
            if (File.Exists(FileName)) throw new FileNotFoundException(FileName);
            return SerializationUtility.Deserialize<T>(FileName);
        }
    }
}
