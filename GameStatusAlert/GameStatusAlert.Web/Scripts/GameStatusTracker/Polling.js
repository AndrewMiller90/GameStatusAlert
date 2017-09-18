function Poll() {
    var Polling = false;

    this.StartPoll = function (pollPromise, callback, interval) {
        this.Polling = true;
        var sleep = (time) => new Promise(resolve => setTimeout(resolve, time));
        var poll = (promiseFn, time) => promiseFn()
            .then(() => {
                if (this.Polling) {
                    sleep(time).then(() => poll(promiseFn, time))
                }
            }).catch(() => {
                this.EndPoll();
                callback();
            });
        poll(() => new Promise(pollPromise), interval);
    }
    this.EndPoll = function () {
        this.Polling = false;
    }
    this.IsPolling = function () {
        return this.Polling;
    }
}