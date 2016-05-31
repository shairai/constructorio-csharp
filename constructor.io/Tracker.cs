using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConstructorIO
{
    using System.Threading.Tasks;
    using HashArgs = Dictionary<string, object>;

    public class Tracker
    {
        ConstructorIOAPI _parentAPI;

        internal Tracker(ConstructorIOAPI ParentAPI)
        {
            _parentAPI = ParentAPI;
        }

        private ConstructorIORequest CreateTrackingRequest(APIRequestType APIPath, string Method, ListItem ForItem = null)
        {
            var trackingRequest = new ConstructorIORequest(APIPath, Method);

            if (ForItem != null)
            {
                if (ForItem.Name != null) trackingRequest.RequestBody.Add("term", ForItem.Name);
                if (ForItem.Category != null) trackingRequest.RequestBody.Add("autocomplete_section", ForItem.Category);
            }
            
            return trackingRequest;
        }

        /// <summary>
        /// Tracks the fact that someone clicked through a search result on the site.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <returns>true if successfully tracked.</returns>
        public bool TrackClickThrough(ListItem item)
        {
            return TrackClickThroughAsync(item).Result;
        }

        /// <summary>
        /// Tracks the fact that someone clicked through a search result on the site.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <returns>true if successfully tracked.</returns>
        public async Task<bool> TrackClickThroughAsync(ListItem item)
        {
            var trackingRequest = CreateTrackingRequest(APIRequestType.V1_ClickThrough, "POST", item);
            
            var response = await _parentAPI.Requestor.MakeRequest(trackingRequest);
            return response.Item1;
        }

        /// <summary>
        /// Tracks the fact that someone converted on your site.
        /// Can be for any definition of conversion, whether someone buys a product or signs up or does something important to your site.
        /// </summary>
        /// <param name="term">the term that someone converted from</param>
        /// <param name="autocompleteSection">the autocomplete section that they converted from</param>
        /// <param name="paramDict">IDictionary of optional parameters. Optional parameters are in the <a href="https://constructor.io/docs/#add-an-item">API documentation</a></param>
        /// <returns>true if successfully tracked</returns>
        public bool TrackConversion(ListItem item, dynamic extra_params = null)
        {
            return TrackConversionAsync(item, extra_params).Result;
        }

        /// <summary>
        /// Tracks the fact that someone converted on your site.
        /// Can be for any definition of conversion, whether someone buys a product or signs up or does something important to your site.
        /// </summary>
        /// <param name="term">the term that someone converted from</param>
        /// <param name="autocompleteSection">the autocomplete section that they converted from</param>
        /// <param name="paramDict">IDictionary of optional parameters. Optional parameters are in the <a href="https://constructor.io/docs/#add-an-item">API documentation</a></param>
        /// <returns>true if successfully tracked</returns>
        public async Task<bool> TrackConversionAsync(ListItem item, dynamic extra_params = null)
        {
            var trackingRequest = CreateTrackingRequest(APIRequestType.V1_Conversion, "POST", item);
            
            var response = await _parentAPI.Requestor.MakeRequest(trackingRequest);
            return response.Item1;
        }

        /// <summary>
        /// Tracks the fact that someone searched on your site.
        /// There's no autocompleteSection parameter because if you're searching, you aren't using an autocomplete.
        /// </summary>
        /// <param name="term">the term that someone searched.</param>
        /// <param name="paramDict">IDictionary of the optional parameters. Optional parameters are in the <a href="https://constructor.io/docs/#track-a-search">API documentation</a></param>
        /// <returns>true if working</returns>
        public bool TrackSearch(string term, dynamic extra_params = null)
        {
            return TrackSearchAsync(term, extra_params).Result;
        }

        /// <summary>
        /// Tracks the fact that someone searched on your site.
        /// There's no autocompleteSection parameter because if you're searching, you aren't using an autocomplete.
        /// </summary>
        /// <param name="term">the term that someone searched.</param>
        /// <param name="paramDict">IDictionary of the optional parameters. Optional parameters are in the <a href="https://constructor.io/docs/#track-a-search">API documentation</a></param>
        /// <returns>true if working</returns>
        public async Task<bool> TrackSearchAsync(string term, dynamic extra_params = null)
        {
            var trackingRequest = CreateTrackingRequest(APIRequestType.V1_Search, "POST");

            trackingRequest.RequestBody["term"] = term;

            var response = await _parentAPI.Requestor.MakeRequest(trackingRequest);
            return response.Item1;
        }
    }
}