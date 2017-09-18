using GameStatusAlert.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStatusAlert.Queue {
    //Specific interface implementation for Cache object.
    partial class MessageQueue : ICacheCleanup {
        public void CleanUp() {
            Stop();
        }
    }
}
