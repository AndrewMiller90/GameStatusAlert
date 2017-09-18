using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

//TODO: Handle killing all threads when cache invalidates
namespace GameStatusAlert.Queue {
    internal partial class MessageQueue : IMessageQueue {
        private ConcurrentQueue<Action> Queue = new ConcurrentQueue<Action>();
        private Task ExecuteTask;
        private bool Executing;
        public int RefreshRate { get; set; }
        private object LockObj = new object();
        public MessageQueue() : this(1000, true) { }
        public MessageQueue(int refreshRate) : this(refreshRate, true) { }
        public MessageQueue(int refreshRate, bool start) {
            RefreshRate = refreshRate;
            if (start) {
                Start();
            }
        }
        public void Start() {
            Executing = true;
            StartExecuteTask();
            StartWatchDog();
        }
        public void Stop() {
            Executing = false;
        }
        private void StartExecuteTask() {
            ExecuteTask = Task.Run(() => Execute());
        }
        private void Execute() {
            while (true) {
                Action action;
                lock (LockObj) {
                    if (Queue.TryDequeue(out action)) {
                        action();
                    }
                }
                Thread.Sleep(RefreshRate);
            }
        }
        private void StartWatchDog() {
            Task.Run(() => WatchDog());
        }
        private void WatchDog() {
            while (true) {
                if (TaskStopped(ExecuteTask)) {
                    StartExecuteTask();
                }
                Thread.Sleep(RefreshRate);
            }
        }
        private bool TaskStopped(Task task) {
            return task.IsCompleted || task.IsFaulted || task.IsCanceled;
        }
        public void Enqueue(Action action) {
            lock (LockObj) {
                Queue.Enqueue(action);
            }
        }
        public bool TaskCompleted(Action action) {
            lock (LockObj) {
                return !Queue.Contains(action);
            }
        }
    }
}
