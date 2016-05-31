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
        public void TestModify()
        {
            var api = MakeAPI();

            ListItem testItem = new ListItem("Modify Test Item", ListItemAutocompleteType.Products)
            {
                Url = "http://test.com",
                PrivateID = "12345"
            };

            Assert.IsTrue(api.Add(testItem), "Add Item");
            Task.Delay(500).Wait();

            testItem.Description = "Extra details";

            Assert.IsTrue(api.Modify(testItem), "Modify Existing Item");
            Task.Delay(500).Wait();
            Assert.IsTrue(api.Remove(testItem), "Remove Item");
        }

        [TestMethod]
        public void TestBatchAddIndividualRemove()
        {
            var api = MakeAPI();

            List<ListItem> batchTestSet = new List<ListItem>();

            for (int i = 0; i < 5; i++)
                batchTestSet.Add(new ListItem("Batch Add Ind Remove Test Item " + i, ListItemAutocompleteType.SearchSuggestions));

            Assert.IsTrue(api.AddBatch(batchTestSet, ListItemAutocompleteType.SearchSuggestions), "Batch Add");

            Task.Delay(500).Wait();

            foreach (var listItem in batchTestSet)
            {
                Assert.IsTrue(api.Remove(listItem), "Remove Item");
                Task.Delay(500).Wait();
            }
        }

        [TestMethod]
        public void TestBatchAddBatchRemove()
        {
            var api = MakeAPI();

            List<ListItem> batchTestSet = new List<ListItem>();

            for (int i = 0; i < 5; i++)
                batchTestSet.Add(new ListItem("Batch Add Batch Remove Test Item " + i, ListItemAutocompleteType.SearchSuggestions));

            Assert.IsTrue(api.AddBatch(batchTestSet, ListItemAutocompleteType.SearchSuggestions));

            Task.Delay(500).Wait();

            Assert.IsTrue(api.RemoveBatch(batchTestSet, ListItemAutocompleteType.SearchSuggestions), "Batch Remove");
        }

        [TestMethod]
        public void TestIndividualAddBatchRemove()
        {
            var api = MakeAPI();

            List<ListItem> batchTestSet = new List<ListItem>();

            for (int i = 0; i < 5; i++)
                batchTestSet.Add(new ListItem("Ind Add Batch Remove Test Item " + i, ListItemAutocompleteType.SearchSuggestions));
            
            foreach (var listItem in batchTestSet)
            {
                Assert.IsTrue(api.Add(listItem), "Remove Item");
                Task.Delay(500).Wait();
            }

            Task.Delay(500).Wait();

            Assert.IsTrue(api.RemoveBatch(batchTestSet, ListItemAutocompleteType.SearchSuggestions), "Batch Remove");
        }
    }
}
