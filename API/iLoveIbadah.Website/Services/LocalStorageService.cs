using Hanssens.Net;
using iLoveIbadah.Website.Contracts;

namespace iLoveIbadah.Website.Services
{
    public class LocalStorageService : ILocalStorageService
    {
        private LocalStorage _localStorage;

        public LocalStorageService()
        {
            var config = new LocalStorageConfiguration()
            {
                AutoLoad = true,
                AutoSave = true,
                Filename = "iLoveIbadah"
            };

            _localStorage = new LocalStorage(config);
        }
        public void ClearStorage(List<string> keys)
        {
            foreach (var key in keys)
            {
                _localStorage.Remove(key);
            }
        }
        public void SetStorageValue<T>(string key, T value)
        {
            _localStorage.Store(key, value);
            _localStorage.Persist();
        }
        public T GetStorageValue<T>(string key)
        {
            return _localStorage.Get<T>(key);
        }
        public bool Exists(string key)
        {

            return _localStorage.Exists(key);
        }
    }
}
