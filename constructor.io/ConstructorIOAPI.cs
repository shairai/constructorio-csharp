﻿using System;
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

        public async Task<bool> AddAsync(ListItem Item)
        {
            string requestMethod = "POST";
            var addRequest = new ConstructorIORequest(APIRequestType.V1_Item, requestMethod);

            Item.GetAsHash().ToList().ForEach((kvp) => addRequest.RequestBody.Add(kvp.Key, kvp.Value));

            var addResponse = await Requestor.MakeRequest(addRequest);
            return addResponse.Item1;
        }

        public async Task<bool> AddOrUpdateAsync(ListItem Item)
        {
            string requestMethod = "PUT";
            var addRequest = new ConstructorIORequest(APIRequestType.V1_Item, requestMethod);

            Item.GetAsHash().ToList().ForEach((kvp) => addRequest.RequestBody.Add(kvp.Key, kvp.Value));

            addRequest["force"] = "1";

            var addResponse = await Requestor.MakeRequest(addRequest);
            return addResponse.Item1;
        }

        public async Task<bool> AddBatchAsync(IEnumerable<ListItem> Items, string AutocompleteSection)
        {
            string requestMethod = "POST";
            var addBatchRequest = new ConstructorIORequest(APIRequestType.V1_BatchItems, requestMethod);

            addBatchRequest.RequestBody["items"] = Items.ToArray().Select(li => li.GetAsHash());
            addBatchRequest.RequestBody["autocomplete_section"] = AutocompleteSection;

            var addBatchResponse = await Requestor.MakeRequest(addBatchRequest);
            return addBatchResponse.Item1;
        }

        public async Task<bool> AddOrUpdateBatchAsync(IEnumerable<ListItem> Items, string AutocompleteSection)
        {
            string requestMethod = "PUT";
            var addBatchRequest = new ConstructorIORequest(APIRequestType.V1_BatchItems, requestMethod);

            addBatchRequest.RequestBody["items"] = Items.ToArray().Select(li => li.GetAsHash());
            addBatchRequest.RequestBody["autocomplete_section"] = AutocompleteSection;
            
            addBatchRequest["force"] = "1";

            var addBatchResponse = await Requestor.MakeRequest(addBatchRequest);
            return addBatchResponse.Item1;
        }

        public async Task<bool> ModifyAsync(ListItem ItemToUpdate)
        {
            var modifyRequest = new ConstructorIORequest(APIRequestType.V1_Item, "PUT");

            ItemToUpdate.GetAsHash().ToList().ForEach(kvp => modifyRequest.RequestBody[kvp.Key] = kvp.Value);

            var modifyResponse = await Requestor.MakeRequest(modifyRequest);
            return modifyResponse.Item1;
        }

        public async Task<bool> RemoveAsync(ListItem ItemToRemove)
        {
            var removeRequest = new ConstructorIORequest(APIRequestType.V1_Item, "DELETE");

            removeRequest.RequestBody["item_name"] = ItemToRemove.Name;
            removeRequest.RequestBody["autocomplete_section"] = ItemToRemove.AutocompleteSection;
            removeRequest.RequestBody["id"] = ItemToRemove.PrivateID ?? "";

            var removeResponse = await Requestor.MakeRequest(removeRequest);
            return removeResponse.Item1;
        }

        public async Task<bool> RemoveBatchAsync(IEnumerable<ListItem> ItemsToRemove, string AutocompleteSection)
        {
            var removeBatchRequest = new ConstructorIORequest(APIRequestType.V1_BatchItems, "DELETE");

            removeBatchRequest.RequestBody["items"] = ItemsToRemove.ToArray().Select(item => item.GetAsHash(true));
            removeBatchRequest.RequestBody["autocomplete_section"] = AutocompleteSection;

            var removeBatchResponse = await Requestor.MakeRequest(removeBatchRequest);
            return removeBatchResponse.Item1;
        }

        #endregion

        #region Non Async Implementations

        public bool Verify()
        {
            return VerifyAsync().Result;
        }

        public bool Add(ListItem Item)
        {
            return AddAsync(Item).Result;
        }

        public bool AddOrUpdate(ListItem Item)
        {
            return AddOrUpdateAsync(Item).Result;
        }

        public bool AddBatch(IEnumerable<ListItem> Items, ListItemAutocompleteType AutocompleteSection)
        {
            return AddBatch(Items, StringEnum.GetStringValue(AutocompleteSection));
        }

        public bool AddBatch(IEnumerable<ListItem> Items, string AutocompleteSection)
        {
            return AddBatchAsync(Items, AutocompleteSection).Result;
        }

        public bool AddOrUpdateBatch(IEnumerable<ListItem> Items, ListItemAutocompleteType AutocompleteSection)
        {
            return AddOrUpdateBatch(Items, StringEnum.GetStringValue(AutocompleteSection));
        }

        public bool AddOrUpdateBatch(IEnumerable<ListItem> Items, string AutocompleteSection)
        {
            return AddOrUpdateBatchAsync(Items, AutocompleteSection).Result;
        }

        public bool Modify(ListItem ItemToModify)
        {
            return ModifyAsync(ItemToModify).Result;
        }

        public bool Remove(ListItem Item)
        {
            return RemoveAsync(Item).Result;
        }

        public bool RemoveBatch(IEnumerable<ListItem> Items, ListItemAutocompleteType AutocompleteSection)
        {
            return RemoveBatch(Items, StringEnum.GetStringValue(AutocompleteSection));
        }

        public bool RemoveBatch(IEnumerable<ListItem> Items, string AutocompleteSection)
        {
            return RemoveBatchAsync(Items, AutocompleteSection).Result;
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