using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoCycle.Core
{
    public class Task
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string UserId { get; set; }

        /// <summary>
        /// The order of the task. Lower order tasks (eg. order = 0) appear before higher-order tasks (eg. order = 13).
        /// If you have tasks (T1 order=13, T2 order=17, T3 order=9), you'll see them in the order T3, T1, T2.
        /// </summary>
        public int Order { get; set; }
    }
}
