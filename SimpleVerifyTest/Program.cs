using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConstructorIO;

namespace SimpleVerifyTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //TODO: Add environment args

            ConstructorIOAPI api = new ConstructorIOAPI("UuXqlIafeKvwop6DaRwP", "5tHZR5xflF6bNLgvpa60");

            bool result = api.VerifyAsync().Result;

            ListItem testItem = new ListItem("Test Item", ListItemAutocompleteType.SearchSuggestions);

            Console.WriteLine("Sending Add");
            bool addResult = api.Add(testItem);
            Console.WriteLine("Add Result: " + addResult);

            Task.Delay(2000).Wait();

            Console.WriteLine("Sending Remove");
            bool removeResult = api.Remove(testItem);
            Console.WriteLine("Remove Result: " + removeResult);

            Task.Delay(2000).Wait();

            List<ListItem> batchTestSet = new List<ListItem>();

            for (int i = 0; i < 5; i++)
                batchTestSet.Add(new ListItem("Test Item " + i, ListItemAutocompleteType.SearchSuggestions));

            Console.WriteLine("Sending Batch Add");
            bool batchAddResult = api.AddBatch(batchTestSet, ListItemAutocompleteType.SearchSuggestions);
            Console.WriteLine("Batch Add Result: " + batchAddResult);

            Task.Delay(5000).Wait();

            foreach (var listItem in batchTestSet)
            {
                Console.WriteLine("Removing Batch Item: " + listItem.Name);
                bool removeBatchResult = api.Remove(listItem);
                Console.WriteLine("Remove result: " + removeBatchResult);

                Task.Delay(1000).Wait();
            }

            Console.ReadLine();
        }
    }
}
