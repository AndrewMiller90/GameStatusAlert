using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStatusAlert.Caching {
    internal interface ICachePolicy {
        TimeSpan? AbsoluteExpiration { get; set; }
        TimeSpan? SlidingExpiration { get; set; }
    }
}
