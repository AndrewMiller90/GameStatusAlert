using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStatusAlert.Queue;
using System.Threading;

namespace GameStatusAlert {
    class Program {
        static void Main(string[] args) {
            //TestRateLimiter();
            Console.ReadLine();
        }
        //private static void TestMessageQueue() {
        //    var queue = new MessageQueue();
        //    var codeBomb = new CodeBomb();
        //    for (int i = 0; i < 3; i++) {
        //        codeBomb.AddBombThread(() => queue.Enqueue(() => Console.WriteLine("1")), 5);
        //        codeBomb.AddBombThread(() => queue.Enqueue(() => Console.WriteLine("2")), 5);
        //        codeBomb.AddBombThread(() => queue.Enqueue(() => Console.WriteLine("3")), 5);
        //        codeBomb.AddBombThread(() => queue.Enqueue(() => Console.WriteLine("4")), 5);
        //    }
        //    codeBomb.Execute();
        //}
        //private static void TestMessageQueueWithRiotData() {
        //    var RiotApi = new RiotApi("na1", "RGAPI-652c60f1-cb0d-4ffb-841a-cdf96ee51855");
        //    var codeBomb = new CodeBomb();

        //    for (int i = 0; i < 3; i++) {
        //        codeBomb.AddBombThread(() => Console.WriteLine(RiotApi.GetSummonerByName("Gammaja")?["id"] ?? "failed"), 5);
        //        codeBomb.AddBombThread(() => Console.WriteLine(RiotApi.GetSummonerByName("sluppy")?["id"] ?? "failed"), 5);
        //        codeBomb.AddBombThread(() => Console.WriteLine(RiotApi.GetSummonerByName("slowburn420")?["id"] ?? "failed"), 5);
        //        codeBomb.AddBombThread(() => Console.WriteLine(RiotApi.GetSummonerByName("Slur78xpxVVY123")?["id"] ?? "failed"), 5);
        //    }
        //    codeBomb.Execute();
        //}
        //private static void TestRateLimiter() {
        //    var rateLimiter = new RateLimiter(
        //        new RateLimit(TimeSpan.FromSeconds(1), 2),
        //        new RateLimit(TimeSpan.FromSeconds(10), 10),
        //        new RateLimit(TimeSpan.FromSeconds(1), 10));
        //    double sum = 0;
        //    for (int i=0; i< 15; i++) {
        //        var rate = rateLimiter.GetRate();
        //        Console.WriteLine(string.Format("RateLimit: {0} ms", rate?.TotalMilliseconds.ToString() ?? "null"));
        //        sum += rate?.TotalMilliseconds ?? 0;
        //        Thread.Sleep((int)(rate?.TotalMilliseconds));
        //    }
        //    Console.WriteLine("Sum: {0} ms", sum);
        //}
    }
}
