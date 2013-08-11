using AnimalStore.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalStore.Model
{
    public class Animal : IAuditInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public int Age { get; set; }
        public bool isLitter { get; set; }
        public bool isSold { get; set; }
        public int Price { get; set; }
        public Breed Breed { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
