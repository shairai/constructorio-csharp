using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructorIO
{
    public class ConstructorIOAPI
    {
        private WebRequestor _webRequestor;
        private Tracker _tracker;

        public ConstructorIOAPI(string APIKey, string AutocompleteKey)
        {
            _webRequestor = new WebRequestor(APIKey, AutocompleteKey);
            _tracker = new Tracker(this);
        }

        #region Methods

        public async Task<bool> VerifyAsync()
        {
            var verifyRequest = new ConstructorIORequest(APIRequestType.V1_Verify, "GET");

            var verifyResponse = await Requestor.MakeRequest(verifyRequest);

            if (verifyResponse.Item2.IndexOf("successful authentication") != -1)
            {
                //TODO: beter way to do this?
                return true;
            }
            return false;
        }

        public async Task<bool> AddAsync(ListItem Item, bool UpdateExisting = false)
        {
            string requestMethod = UpdateExisting ? "PUT" : "POST";
            var addRequest = new ConstructorIORequest(APIRequestType.V1_Item, requestMethod);

            Item.GetAsHash().ToList().ForEach((kvp) => addRequest.RequestBody.Add(kvp.Key, kvp.Value));

            if (UpdateExisting)
                addRequest["force"] = "1";

            var addResponse = await Requestor.MakeRequest(addRequest);
            return addResponse.Item1;
        }

        public async Task<bool> AddBulkAsync(IEnumerable<ListItem> Items,
            ListItemAutocompleteType AutocompleteSection, bool UpdateExisting = false)
        {
            return await AddBulkAsync(Items, StringEnum.GetStringValue(AutocompleteSection), UpdateExisting);
        }

        public async Task<bool> AddBulkAsync(IEnumerable<ListItem> Items, string AutocompleteSection,
            bool UpdateExisting = false)
        {
            string requestMethod = UpdateExisting ? "PUT" : "POST";
            var addBulkRequest = new ConstructorIORequest(APIRequestType.V1_BatchItems, requestMethod);

            addBulkRequest.RequestBody["items"] = Items.ToArray().Select(li => li.GetAsHash());
            addBulkRequest.RequestBody["autocomplete_section"] = AutocompleteSection;

            if (UpdateExisting)
                addBulkRequest["force"] = "1";

            var addBulkResponse = await Requestor.MakeRequest(addBulkRequest);
            return addBulkResponse.Item1;
        }

        public async Task<bool> RemoveAsync(ListItem ItemToRemove)
        {
            var removeRequest = new ConstructorIORequest(APIRequestType.V1_Item, "DELETE");

            removeRequest.RequestBody["item_name"] = ItemToRemove.Name;
            removeRequest.RequestBody["autocomplete_section"] = ItemToRemove.Category;
            removeRequest.RequestBody["id"] = ItemToRemove.PrivateID ?? "";

            var removeResponse = await Requestor.MakeRequest(removeRequest);
            return removeResponse.Item1;
        }

        public async Task<bool> RemoveBulkAsync(IEnumerable<ListItem> ItemsToRemove, string AutocompleteSection)
        {
            var removeBulkRequest = new ConstructorIORequest(APIRequestType.V1_BatchItems, "DELETE");

            removeBulkRequest.RequestBody["items"] = ItemsToRemove.ToArray().Select(item => item.GetAsHash(true));
            removeBulkRequest.RequestBody["autocomplete_section"] = AutocompleteSection;

            var removeBulkResponse = await Requestor.MakeRequest(removeBulkRequest);
            return removeBulkResponse.Item1;
        }

        #endregion

        #region Non Async Implementations

        public bool Verify()
        {
            return VerifyAsync().Result;
        }

        public bool Add(ListItem Item, bool UpdateExisting = false)
        {
            return AddAsync(Item, UpdateExisting).Result;
        }

        public bool AddBulk(IEnumerable<ListItem> Items,
            ListItemAutocompleteType AutocompleteSection, bool UpdateExisting = false)
        {
            return AddBulkAsync(Items, AutocompleteSection, UpdateExisting).Result;
        }

        public bool AddBulk(IEnumerable<ListItem> Items,
            string AutocompleteSection, bool UpdateExisting = false)
        {
            return AddBulkAsync(Items, AutocompleteSection, UpdateExisting).Result;
        }

        public bool Remove(ListItem Item)
        {
            return RemoveAsync(Item).Result;
        }

        public bool RemoveBulk(IEnumerable<ListItem> Items, ListItemAutocompleteType AutocompleteSection)
        {
            return RemoveBulk(Items, StringEnum.GetStringValue(AutocompleteSection));
        }

        public bool RemoveBulk(IEnumerable<ListItem> Items, string AutocompleteSection)
        {
            return RemoveBulkAsync(Items, AutocompleteSection).Result;
        }

        #endregion



        internal WebRequestor Requestor
        {
            get
            {
                return _webRequestor;
            }
        }

        public string LastResponseBody
        {
            get
            {
                return _webRequestor.GetLastBody();
            }
        }

        public Tracker Tracker
        {
            get
            {
                return _tracker;
            }
        }
    }
}