using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisExample.Models
{
    public class Vote
    {
        public int Yes { get; set; }

        public int No { get; set; }

        public int Undecided { get; set; }

    }
}
