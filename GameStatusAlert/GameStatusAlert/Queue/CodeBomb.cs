using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameStatusAlert.Queue {
    //Not thread safe
    internal sealed class CodeBomb {
        private bool Enabled = false;
        private object lockObj = new object();
        private List<Task> _bombThreads = new List<Task>();
        public CodeBomb() { }
        public void AddBombThread(Action thread) {
            AddBombThread(thread, 1);
        }
        public void AddBombThread(Action thread, int executions) {
            if (executions <= 0) throw new ArgumentException(nameof(executions));
            _bombThreads.Add(Task.Run(() => ExecuteBombThread(thread, executions)));
        }
        private void ExecuteBombThread(Action thread, int executions) {
            while (!Enabled) { }
            for (int i = 0; i < executions; i++) {
                thread();
                Thread.Sleep(1);
            }
        }
        public void Execute() {
            lock (lockObj) {
                Enabled = true;
                foreach(var task in _bombThreads) {
                    task.Wait();
                }
                _bombThreads.Clear();
                Enabled = false;
            }
        }
    }
}
