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
        public bool Executing {
            get {
                return CancellationSource != null;
            }
        }
        private ConcurrentQueue<Task> Queue = new ConcurrentQueue<Task>();
        private CancellationTokenSource CancellationSource;
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
            if (!Executing) {
                CancellationSource = new CancellationTokenSource();
                StartWatchDogTask();
            }
        }
        public void Stop() {
            if (Executing) {
                CancellationSource.Cancel();
                CancellationSource = null;
            }
        }
        private Task StartTask(Action action) {
            var token = CancellationSource.Token;
            return Task.Run(action);
        }
        private Task StartExecuteTask() {
            return StartTask(() => Execute());
        }
        private Task StartWatchDogTask() {
            return StartTask(() => WatchDog());
        }
        private void Execute() {
            while (true) {
                Task action;
                lock (LockObj) {
                    if (Queue.TryDequeue(out action)) {
                        action.Start();
                    }
                }
                Thread.Sleep(RefreshRate);
            }
        }
        private void WatchDog() {
            while(true) {
                StartExecuteTask().Wait();
            }
        }
        public Task Enqueue(Action action) {
            var task = new Task(action);
            lock (LockObj) {
                Queue.Enqueue(task);
            }
            return task;
        }
    }
}
