using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConstructorIO.Test
{
    [TestClass]
    public class TrackingTests
    {
        public ConstructorIOAPI MakeAPI()
        {
            //TODO: Switch to environment variables
            return new ConstructorIOAPI("UuXqlIafeKvwop6DaRwP", "5tHZR5xflF6bNLgvpa60");
        }

        [TestMethod]
        public void TestTrackSearch()
        {
            var api = MakeAPI();
            Assert.IsTrue(api.Tracker.TrackSearch("Test"), "Track Search");
        }

        [TestMethod]
        public void TestTrackConversion()
        {
            var item = new ListItem("Test Conversion", ListItemAutocompleteType.SearchSuggestions);

            var api = MakeAPI();
            Assert.IsTrue(api.Tracker.TrackConversion(item), "Track Conversion");
        }

        [TestMethod]
        public void TestTrackClickThrough()
        {
            var item = new ListItem("Test Click Through", ListItemAutocompleteType.SearchSuggestions);

            var api = MakeAPI();
            Assert.IsTrue(api.Tracker.TrackClickThrough(item), "Track Click Through");
        }
    }
}
