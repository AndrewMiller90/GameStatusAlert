using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStatusAlert.Queue {
    interface IMessageQueue {
        Task Enqueue(Action action);
    }
}
