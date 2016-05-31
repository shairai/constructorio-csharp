using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            var item = new ListItem("Test Conversion", ListItemAutocompleteType.SearchSuggestions);

            var api = TestCommon.MakeAPI();
            Assert.IsTrue(api.Tracker.TrackConversion(item), "Track Conversion");
        }

        [TestMethod]
        public void TestTrackClickThrough()
        {
            var item = new ListItem("Test Click Through", ListItemAutocompleteType.SearchSuggestions);

            var api = TestCommon.MakeAPI();
            Assert.IsTrue(api.Tracker.TrackClickThrough(item), "Track Click Through");
        }
    }
}
