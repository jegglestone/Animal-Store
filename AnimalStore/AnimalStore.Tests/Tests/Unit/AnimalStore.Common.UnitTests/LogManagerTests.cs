using NUnit.Framework;

namespace AnimalStore.Common.UnitTests
{
    [TestFixture]
    public class LogManagerTests
    {
        [Test]
        public void GetLoggerReturnsLogger()
        {
            //arrange
            var logManager = new Logging.LogManager();

            //act
            var log = logManager.GetLogger(typeof(LogManagerTests));

            //assert
            Assert.That(log, Is.TypeOf<Logging.LoggerAdapter>());
        }
    }
}
