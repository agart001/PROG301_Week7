using PROG301_Week7.Models;

namespace UnitTests
{
    [TestClass]
    public class EngineTests
    {
        [TestMethod]
        public void EngineIsStarted()
        {
            //Arrange
            Engine e = new Engine();
            //Act 
            bool defaultEngineStarted = e.IsStarted;
            e.Start();
            bool startEngineStarted = e.IsStarted;
            e.Stop();
            bool stopEngineStarted = e.IsStarted;
            //Assert
            Assert.AreEqual(defaultEngineStarted, false);
            Assert.AreEqual(startEngineStarted, true);
            Assert.AreEqual(stopEngineStarted, false);
        }

        [TestMethod]
        public void EngineAbout()
        {
            //Arrange
            Engine e = new Engine();
            //Act 
            string defaultEngineAbout = e.About();
            e.Start();
            string startedAbout = e.About();
            e.Stop();
            string stoppedAbout = e.About();
            //Assert
            Assert.AreEqual(defaultEngineAbout, $"{e} is not started.");
            Assert.AreEqual(startedAbout, $"{e} is started.");
            Assert.AreEqual(stoppedAbout, $"{e} is not started.");
        }
    }
}
