using NUnit.Framework;
using AnimalStore.Web.API.Helpers;

namespace AnimalStore.Services.UnitTests
{
    [TestFixture]
    public class LocationSearchCheckerTests
    {
        [Test]
        public void IsLocationSearch_returns_true_when_given_a_place_id()
        {
            Assert.That(LocationSearchChecker.IsLocationSearch(1), Is.EqualTo(true));
        }

        [Test]
        public void IsLocationSearch_returns_false_when_given_a_place_id_of_zero()
        {
            Assert.That(LocationSearchChecker.IsLocationSearch(0), Is.EqualTo(false));
        }

    }
}
