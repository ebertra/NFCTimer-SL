using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace NFCTimer_SLTest
{
    [TestClass]
    public class UnitTest1
    {
        //[TestMethod]
        //public async void TestMethod1()
        //{
        //    await Windows.System.Launcher.LaunchUriAsync(new System.Uri("nfctimer:action=startstop"));
        //}

        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsTrue(System.Uri.IsWellFormedUriString("/Protocol?encodedLaunchUri=nfctimer%3Aaction%3Dstartstop", System.UriKind.RelativeOrAbsolute));
        }

        [TestMethod]
        public void TestMethod2()
        {
            Assert.IsTrue(System.Uri.IsWellFormedUriString("/Protocol?encodedLaunchUri=nfctimer%3Aaction%3Dstartstop", System.UriKind.Absolute));
        }

        [TestMethod]
        public void TestMethod3()
        {
            var uri = new Uri("/Protocol?encodedLaunchUri=nfctimer%3Aaction%3Dstartstop");
            Assert.IsTrue(true);
        }
    }
}
