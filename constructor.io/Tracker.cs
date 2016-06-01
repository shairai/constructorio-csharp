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

        private ConstructorIORequest CreateTrackingRequest(APIRequestType APIPath, string Method, HashArgs ExtraArgs)
        {
            var trackingRequest = new ConstructorIORequest(APIPath, Method);

            if (ExtraArgs != null)
                Util.Merge(ExtraArgs, trackingRequest.RequestBody);

            return trackingRequest;
        }

        /// <summary>
        /// Tracks the fact that someone searched on your site.
        /// There's no autocompleteSection parameter because if you're searching, you aren't using an autocomplete.
        /// </summary>
        /// <param name="Term">the term that someone searched.</param>
        /// <param name="paramDict">IDictionary of the optional parameters. Optional parameters are in the <a href="https://constructor.io/docs/#track-a-search">API documentation</a></param>
        /// <returns>true if working</returns>
        public bool TrackSearch(string Term, HashArgs ExtraParams = null)
        {
            return TrackSearchAsync(Term, ExtraParams).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Tracks the fact that someone searched on your site.
        /// There's no autocompleteSection parameter because if you're searching, you aren't using an autocomplete.
        /// </summary>
        /// <param name="Term">the term that someone searched.</param>
        /// <param name="paramDict">IDictionary of the optional parameters. Optional parameters are in the <a href="https://constructor.io/docs/#track-a-search">API documentation</a></param>
        /// <returns>true if working</returns>
        public async Task<bool> TrackSearchAsync(string Term, HashArgs ExtraParams = null)
        {
            var trackingRequest = CreateTrackingRequest(APIRequestType.V1_Search, "POST", ExtraParams);

            trackingRequest.RequestBody["term"] = Term;

            var response = await _parentAPI.Requestor.MakeRequest(trackingRequest);
            return response.Item1;
        }

        /// <summary>
        /// Tracks the fact that someone clicked through a search result on the site.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <param name="ExtraParams">Any extra parameters to be included in the body of the </param>
        /// <returns>true if successfully tracked.</returns>
        public bool TrackClickThrough(string Term, string AutocompleteSection, HashArgs ExtraParams = null)
        {
            return TrackClickThroughAsync(Term, AutocompleteSection, ExtraParams).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Tracks the fact that someone clicked through a search result on the site.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <returns>true if successfully tracked.</returns>
        public async Task<bool> TrackClickThroughAsync(string Term, string AutocompleteSection, HashArgs ExtraParams = null)
        {
            var trackingRequest = CreateTrackingRequest(APIRequestType.V1_ClickThrough, "POST", ExtraParams);

            trackingRequest.RequestBody["term"] = Term;
            trackingRequest.RequestBody["autocomplete_section"] = AutocompleteSection;

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
        public bool TrackConversion(string Term, string AutocompleteSection, string ItemName = null, HashArgs ExtraParams = null)
        {
            return TrackConversionAsync(Term, AutocompleteSection, ItemName, ExtraParams).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Tracks the fact that someone converted on your site.
        /// Can be for any definition of conversion, whether someone buys a product or signs up or does something important to your site.
        /// </summary>
        /// <param name="item">The item/term that someone converted from</param>
        /// <param name="paramDict">IDictionary of optional parameters. Optional parameters are in the <a href="https://constructor.io/docs/#add-an-item">API documentation</a></param>
        /// <returns>true if successfully tracked</returns>
        public async Task<bool> TrackConversionAsync(string Term, string AutocompleteSection, string ItemName = null, HashArgs ExtraParams = null)
        {

            var trackingRequest = CreateTrackingRequest(APIRequestType.V1_Conversion, "POST", ExtraParams);

            trackingRequest.RequestBody["term"] = Term;
            trackingRequest.RequestBody["autocomplete_section"] = AutocompleteSection;
            if (ItemName != null) trackingRequest.RequestBody["item"] = ItemName;

            var response = await _parentAPI.Requestor.MakeRequest(trackingRequest);
            return response.Item1;
        }
    }
}