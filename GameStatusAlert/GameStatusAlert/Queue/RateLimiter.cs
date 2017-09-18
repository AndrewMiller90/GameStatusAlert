using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStatusAlert.Queue {
    //TODO: Fix this
    internal sealed class RateLimiter {
        private RateLimit[] Limits;
        private List<DateTime> accesses = new List<DateTime>();
        public RateLimiter(params RateLimit[] limits) {
            Limits = limits;
        }
        public TimeSpan? GetRate() {
            PruneAccesses();
            accesses.Add(DateTime.Now);

            var query = Limits.Where(LimitValid).Select(CalculateRate);
            if (query.Count().Equals(0)) {
                return null;
            }
            return query.Min();
        }
        private void PruneAccesses() {
            accesses = accesses.Where(
                x => DateTime.Now - x < Limits.Select(y => y.Duration).Max()
            ).ToList();
        }
        private bool LimitValid(RateLimit limit) {
            return TotalAccessesDuringLimitPeriod(limit) <= limit.Limit;
        }
        private TimeSpan CalculateRate(RateLimit limit) {
            return TimeSpan.FromMilliseconds(
                (limit.Duration - TimeSinceInitialAccess(limit)).TotalMilliseconds / 
                (limit.Limit - TotalAccessesDuringLimitPeriod(limit) + 1)
            );
        }
        private TimeSpan TimeSinceInitialAccess(RateLimit limit) {
            var accessesDuringLimitPeriod = AllAccessesDuringLimitPeriod(limit);
            return accessesDuringLimitPeriod.Count() > 0 ? DateTime.Now - accessesDuringLimitPeriod.Min() : new TimeSpan();
        }
        private int TotalAccessesDuringLimitPeriod(RateLimit limit) {
            return AllAccessesDuringLimitPeriod(limit).Count();
        }
        private IEnumerable<DateTime> AllAccessesDuringLimitPeriod(RateLimit limit) {
            return accesses.Where(x => DateTime.Now - x < limit.Duration);
        }
    }
    internal sealed class RateLimit {
        public TimeSpan Duration { get; set; }
        public int Limit { get; set; }
        public TimeSpan MinimumRate { get {
                return TimeSpan.FromMilliseconds(Duration.TotalMilliseconds / (double)Limit);
            }
        }
        public RateLimit(TimeSpan duration, int limit) {
            Duration = duration;
            Limit = limit;
        }
    }
}
