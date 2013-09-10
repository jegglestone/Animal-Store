using AnimalStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalStore.Data.UnitTests.Fakes
{
    internal class FakeAnimalDbSet : FakeDbSet<Animal>
    {
        public override Animal Find(params object[] keyValues)
        {
            var keyValue = (int)keyValues.FirstOrDefault();
            return this.SingleOrDefault(x => x.Id == keyValue);
        }
    }

    internal class FakeSpeciesDbSet : FakeDbSet<Species>
    {
        public override Species Find(params object[] keyValues)
        {
            var keyValue = (int)keyValues.FirstOrDefault();
            return this.SingleOrDefault(x => x.Id == keyValue);
        }
    }

    internal class FakeBreedDbSet : FakeDbSet<Breed>
    {
        public override Breed Find(params object[] keyValues)
        {
            var keyValue = (int)keyValues.FirstOrDefault();
            return this.SingleOrDefault(x => x.Id == keyValue);
        }
    }
}
