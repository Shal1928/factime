using System.IO;
using UseAbilities.IoC.Stores;

namespace Factime.Stores
{
    public class FileStore : StoreBase<string>, IFileStore<string>
    {
        public virtual string FileName
        {
            get;
            set;
        }

        public override string Load()
        {
            var file = new StreamReader(FileName);
            var fileData = file.ReadLine();

            file.Close();
            return fileData;
        }
    }

    public class FileStore<T> : StoreBase<T>, IFileStore<T>
    {
        public virtual string FileName
        {
            get;
            set;
        }
    }
}
