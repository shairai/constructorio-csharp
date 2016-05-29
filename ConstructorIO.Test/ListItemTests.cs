using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ConstructorIO.Test
{
    [TestClass]
    public class ListItemTests
    {
        public ConstructorIOAPI MakeAPI()
        {
            return new ConstructorIOAPI("UuXqlIafeKvwop6DaRwP", "5tHZR5xflF6bNLgvpa60");
        }

        [TestMethod]
        public void TestVerify()
        {
            var api = MakeAPI();

            Assert.IsTrue(api.Verify());
        }

        [TestMethod]
        public void TestAddRemove()
        {
            var api = MakeAPI();

            ListItem testItem = new ListItem("AddRemove Test Item", ListItemAutocompleteType.SearchSuggestions);

            Assert.IsTrue(api.Add(testItem), "Add Item");
            Task.Delay(500).Wait();
            Assert.IsTrue(api.Remove(testItem), "Remove Item");
        }

        [TestMethod]
        public void TestBulkAddIndividualRemove()
        {
            var api = MakeAPI();

            List<ListItem> bulkTestSet = new List<ListItem>();

            for (int i = 0; i < 5; i++)
                bulkTestSet.Add(new ListItem("Bulk Add Ind Remove Test Item " + i, ListItemAutocompleteType.SearchSuggestions));

            Assert.IsTrue(api.AddBulk(bulkTestSet, ListItemAutocompleteType.SearchSuggestions), "Bulk Add");

            Task.Delay(500).Wait();

            foreach (var listItem in bulkTestSet)
            {
                Assert.IsTrue(api.Remove(listItem), "Remove Item");
                Task.Delay(500).Wait();
            }
        }

        [TestMethod]
        public void TestBulkAddBulkRemove()
        {
            var api = MakeAPI();

            List<ListItem> bulkTestSet = new List<ListItem>();

            for (int i = 0; i < 5; i++)
                bulkTestSet.Add(new ListItem("Bulk Add Bulk Remove Test Item " + i, ListItemAutocompleteType.SearchSuggestions));

            Assert.IsTrue(api.AddBulk(bulkTestSet, ListItemAutocompleteType.SearchSuggestions));

            Task.Delay(500).Wait();

            Assert.IsTrue(api.RemoveBulk(bulkTestSet, ListItemAutocompleteType.SearchSuggestions), "Bulk Remove");
        }

        [TestMethod]
        public void TestIndividualAddBulkRemove()
        {
            var api = MakeAPI();

            List<ListItem> bulkTestSet = new List<ListItem>();

            for (int i = 0; i < 5; i++)
                bulkTestSet.Add(new ListItem("Ind Add Bulk Remove Test Item " + i, ListItemAutocompleteType.SearchSuggestions));
            
            foreach (var listItem in bulkTestSet)
            {
                Assert.IsTrue(api.Add(listItem), "Remove Item");
                Task.Delay(500).Wait();
            }

            Task.Delay(500).Wait();

            Assert.IsTrue(api.RemoveBulk(bulkTestSet, ListItemAutocompleteType.SearchSuggestions), "Bulk Remove");
        }
    }
}
