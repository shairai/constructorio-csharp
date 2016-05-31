using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ConstructorIO.Test
{
    [TestClass]
    public class ListItemTests
    {

        [TestMethod]
        public void TestVerify()
        {
            var api = TestCommon.MakeAPI();

            Assert.IsTrue(api.Verify());
        }

        [TestMethod]
        public void TestAddRemove()
        {
            var api = TestCommon.MakeAPI();

            ListItem testItem = new ListItem("AddRemove Test Item", ListItemAutocompleteType.Products)
            {
                PrivateID = "AddRemoveTestItem",
                Url = "http://test.com",
                Description = "ExampleItem",
                ImageUrl = "http://test.com/test.jpg"
            };

            Assert.IsTrue(api.AddOrUpdate(testItem), "Add Item");
            Task.Delay(500).Wait();
            Assert.IsTrue(api.Remove(testItem), "Remove Item");
        }

        [TestMethod]
        public void TestModifyByName()
        {
            var api = TestCommon.MakeAPI();

            ListItem testItem = new ListItem("Modify Test Item", ListItemAutocompleteType.Products)
            {
                Url = "http://test.com",
                Description = "Example Item",
                ImageUrl = "http://test.com/test.jpg"
            };

            Assert.IsTrue(api.AddOrUpdate(testItem), "Add Item");
            Task.Delay(500).Wait();

            testItem.Description = "Extra details";

            Assert.IsTrue(api.Modify(testItem), "Modify Existing Item");
            Task.Delay(500).Wait();
            Assert.IsTrue(api.Remove(testItem), "Remove Item");
        }

        [TestMethod]
        public void TestModifyChangeName()
        {
            var api = TestCommon.MakeAPI();

            ListItem testItem = new ListItem("Modify Test Item", ListItemAutocompleteType.Products)
            {
                Url = "http://test.com",
                Description = "Example Item",
                ImageUrl = "http://test.com/test.jpg"
            };

            Assert.IsTrue(api.AddOrUpdate(testItem), "Add Item");
            Task.Delay(500).Wait();

            testItem.Name = "Modify Test Item Rename";

            Assert.IsTrue(api.Modify(testItem), "Modify Existing Item");
            Task.Delay(500).Wait();
            Assert.IsTrue(api.Remove(testItem), "Remove Item");
        }

        [TestMethod]
        public void TestModifyByID()
        {
            var api = TestCommon.MakeAPI();

            ListItem testItem = new ListItem("ModifyTestItem", ListItemAutocompleteType.Products)
            {
                PrivateID = "ModifyTestItemID",
                Url = "http://test.com",
                Description = "Example Item",
                ImageUrl = "http://test.com/test.jpg"
            };

            Assert.IsTrue(api.AddOrUpdate(testItem), "Add Item");
            Task.Delay(500).Wait();

            testItem.Name = "ModifyTestItemNewName";

            Assert.IsTrue(api.Modify(testItem), "Modify Existing Item");
            Task.Delay(500).Wait();
            Assert.IsTrue(api.Remove(testItem), "Remove Item");
        }

        [TestMethod]
        public void TestBatchAddIndividualRemove()
        {
            var api = TestCommon.MakeAPI();

            List<ListItem> batchTestSet = new List<ListItem>();

            for (int i = 0; i < 5; i++)
                batchTestSet.Add(new ListItem("Batch Add Ind Remove Test Item " + i, ListItemAutocompleteType.SearchSuggestions));

            Assert.IsTrue(api.AddOrUpdateBatch(batchTestSet, ListItemAutocompleteType.SearchSuggestions), "Batch Add");

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
            var api = TestCommon.MakeAPI();

            List<ListItem> batchTestSet = new List<ListItem>();

            for (int i = 0; i < 5; i++)
                batchTestSet.Add(new ListItem("Batch Add Batch Remove Test Item " + i, ListItemAutocompleteType.SearchSuggestions));

            Assert.IsTrue(api.AddOrUpdateBatch(batchTestSet, ListItemAutocompleteType.SearchSuggestions));

            Task.Delay(500).Wait();

            Assert.IsTrue(api.RemoveBatch(batchTestSet, ListItemAutocompleteType.SearchSuggestions), "Batch Remove");
        }

        [TestMethod]
        public void TestIndividualAddBatchRemove()
        {
            var api = TestCommon.MakeAPI();

            List<ListItem> batchTestSet = new List<ListItem>();

            for (int i = 0; i < 5; i++)
                batchTestSet.Add(new ListItem("Ind Add Batch Remove Test Item " + i, ListItemAutocompleteType.SearchSuggestions));
            
            foreach (var listItem in batchTestSet)
            {
                Assert.IsTrue(api.AddOrUpdate(listItem), "Remove Item");
                Task.Delay(500).Wait();
            }

            Task.Delay(500).Wait();

            Assert.IsTrue(api.RemoveBatch(batchTestSet, ListItemAutocompleteType.SearchSuggestions), "Batch Remove");
        }
    }
}
