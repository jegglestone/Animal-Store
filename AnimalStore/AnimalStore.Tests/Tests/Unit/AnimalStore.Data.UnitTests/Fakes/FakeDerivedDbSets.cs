using AnimalStore.Model;
using System.Linq;

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

    internal class FakeDogDbSet : FakeDbSet<Dog>
    {
        public override Dog Find(params object[] keyValues)
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
