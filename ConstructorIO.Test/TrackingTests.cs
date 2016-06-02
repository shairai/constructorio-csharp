using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ConstructorIO.Test
{
    [TestClass]
    public class TrackingTests
    {

        [TestMethod]
        public void TestTrackSearch()
        {
            var api = TestCommon.MakeAPI();
            Assert.IsTrue(api.Tracker.TrackSearch("Test"), "Track Search");
        }

        [TestMethod]
        public void TestTrackConversion()
        {
            var api = TestCommon.MakeAPI();
            Assert.IsTrue(api.Tracker.TrackConversion("Test", "Search Suggestions"), "Track Conversion");
        }

        [TestMethod]
        public void TestTrackClickThrough()
        {
            var api = TestCommon.MakeAPI();
            Assert.IsTrue(api.Tracker.TrackClickThrough("Test", "Search Suggestions"), "Track Click Through");
        }

        [TestMethod]
        public void TestTrackClickThroughWaithRevenue()
        {
            var api = TestCommon.MakeAPI();
            Assert.IsTrue(api.Tracker.TrackClickThrough("Test", "Search Suggestions", new Dictionary<string, object>() { { "revenue", "500" } }), "Track Click Through");
        }
    }
}
