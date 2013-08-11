using System;
using System.Configuration;
using System.Linq;
using System.Data;
using System.Data.Entity;
using AnimalStore.Model;

namespace AnimalStore.Data
{
    public class DataContext : DbContext
    {
        public DataContext()
        { }
    }
}
