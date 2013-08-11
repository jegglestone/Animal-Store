using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalStore.Model
{
    public class Animal
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public int Age { get; set; }
        public bool isLitter { get; set; }
        public bool isSold { get; set; }
        public Species Species { get; set; }
    }
}
