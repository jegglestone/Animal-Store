using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimalStore.Data;

namespace AnimalStore.Data.UnitTests
{
    [TestFixture]
    public class ConnectionStringHelper
    {
        [Test]
        public void ConnectionStringName_returns_default_connection_string_when_AnimalsContextConnectionString_cannot_be_found()
        {
            // act
            var connectionString = Helpers.ConnectionStringHelper.ConnectionStringName;

            Assert.That(connectionString, Is.EqualTo("DefaultConnection"));
        }
    }
}
