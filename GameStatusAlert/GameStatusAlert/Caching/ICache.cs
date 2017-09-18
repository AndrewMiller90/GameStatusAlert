using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStatusAlert.Caching {
    internal interface ICache {
        bool ContainsKey(string key);
        object GetValue(string key);
        void SetValue(string key, object value);
        object GetValueOrCreateEntry(string key, Func<object> constructor);
    }
}
