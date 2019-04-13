using Pexeso.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pexeso.Models
{
    class Box
    {
        public int Value { get; }
        public BoxStatus Status { get; set; }

        public Box(int value)
        {
            Value = value;
            Status = BoxStatus.Hidden;
        }
    }
}
