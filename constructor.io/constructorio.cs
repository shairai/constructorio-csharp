using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Reflection;

namespace ConstructorIOClient
{
    /// <summary>
    ///  StringValueAttribute
    /// </summary>

    public class StringValueAttribute : Attribute
    {
        private string m_sValue;

        public StringValueAttribute(string value)
        {
            m_sValue = value;
        }

        public string Value
        {
            get { return m_sValue; }
        }
    }

    /// <summary>
    /// StringEnum
    /// </summary>
    public class StringEnum
    {
        private static Hashtable m_hsStringValues = new Hashtable();

        public static string GetStringValue(Enum value)
        {
            string output = null;
            
            Type type = value.GetType();

            if (m_hsStringValues.ContainsKey(value))
                output = (m_hsStringValues[value] as StringValueAttribute).Value;
            else
            {
                FieldInfo fi = type.GetField(value.ToString());
                StringValueAttribute[] attrs = fi.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];
                if (attrs.Length > 0)
                {
                    m_hsStringValues.Add(value, attrs[0]);
                    output = attrs[0].Value;
                }
            }
            
            return output;
        }
    }

    /// <summary>
    /// Constructor.io Client
    /// Constructor.io provides a lightning-fast, typo-tolerant autocomplete service that ranks your users' queries by popularity 
    /// to let them find what they're looking for as quickly as possible.
    /// </summary>
    public class ConstructorIO
    {
        public string apiToken;
        public string autocompleteKey;
        public string protocol;
        public string host;

        /// <summary>
        /// Built in types for sections names
        /// </summary>
        public enum AutoCompleteListType
        {
            [StringValue("Products")]
            Product,
            [StringValue("Search Suggestions")]
            SearchSuggestions,
        };


        /// <summary>
        ///  Creates a constructor.io client. 
        /// </summary>
        /// <param name="apiToken"> API Token, gotten from your <a href="https://constructor.io/dashboard">Constructor.io Dashboard</a>, and kept secret.</param>
        /// <param name="autocompleteKey"> Autocomplete key, also gotten from your <a href="https://constructor.io/dashboard">Constructor.io Dashboard</a> and used publicly in your in-site javascript client.</param>
        /// <param name="protocol">It is highly recommended that you use HTTPS.</param>
        /// <param name="host">The host of the autocomplete service that you are using. It is recommended that you let this value be null, in which case the host defaults to the Constructor.io autocomplete service at ac.cnstrc.com.</param>
        public ConstructorIO(string apiToken, string autocompleteKey, string protocol = "https", string host = "ac.cnstrc.com")
        {
            this.apiToken = apiToken;
            this.autocompleteKey = autocompleteKey;
            this.protocol = protocol;
            this.host = host;
        }

        /// <summary>
        /// Serializes url params in a rudimentary way, and you must write other helper methods to serialize other things.
        /// </summary>
        /// <param name="paramDict"> params HashMap of the parameters to encode.</param>
        /// <returns> The encoded parameters, as a String.</returns>
        public static string SerializeParams(IDictionary<string, object> paramDict)
        {
            var list = new List<string>();
            foreach (var item in paramDict)
            {
                list.Add(System.Uri.EscapeDataString(item.Key) + "=" + System.Uri.EscapeDataString((string)item.Value));
            }
            return string.Join("&", list);
        }


        /// <summary>
        /// Makes a URL to issue the requests to. 
        /// Note that the URL will automagically have the autocompleteKey embedded.
        /// </summary>
        /// <param name="endpoint"> endpoint Endpoint of the autocomplete service.</param>
        /// <param name="keys">IDictionary of the parameters you're encoding in the URL</param>
        /// <returns> The created URL</returns>
        public string MakeUrl(string endpoint, IDictionary<string, object> keys)
        {
            Dictionary<string, object> paramDict = new Dictionary<string, object>(keys);
            paramDict.Add("autocomplete_key", this.autocompleteKey);
            string[] urlMembers = new string[] 
            {
                this.protocol,
                this.host,
                endpoint,
                SerializeParams(paramDict)
            };

            return String.Format("{0}://{1}/{2}?{3}", urlMembers);
        }

        /// <summary>
        /// Makes a URL to issue the requests to. 
        /// Note that the URL will automagically have the autocompleteKey embedded.
        /// </summary>
        /// <param name="endpoint"> Endpoint of the autocomplete service.</param>
        /// <returns> The created URL</returns>
        public string MakeUrl(string endpoint)
        {
            // empty keys
            Dictionary<string, object> keys = new Dictionary<string, object>();
            return this.MakeUrl(endpoint, keys);
        }

        /// <summary>
        /// Creates Dictionary of parameters
        /// </summary>
        /// <param name="itemName">Name of the item</param>
        /// <param name="autocompleteSection">the section of the autocomplete that you're adding the item to.</param>
        /// <param name="isTracking"></param>
        /// <param name="otherParams"></param>
        /// <returns>Dictionary<string, object>IDictionary of parameters</returns>
        public static Dictionary<string, object> CreateItemParams(string itemName, string autocompleteSection, bool isTracking, IDictionary<string, object> otherParams)
        {
            Dictionary<string, object> paramDict = new Dictionary<string, object>();
            if (isTracking)
            {
                paramDict.Add("term", itemName);
            }
            else
            {
                paramDict.Add("item_name", itemName);
            }
            if (autocompleteSection != null)
            {
                paramDict.Add("autocomplete_section", autocompleteSection);
            }
            if (otherParams != null)
            {
                foreach (var otherParam in otherParams)
                {
                    paramDict.Add(otherParam.Key, otherParam.Value);
                }
            }
            return paramDict;
        }

        /// <summary>
        /// Sends POST HTTP request
        /// </summary>
        /// <param name="url">the URL for HTTP request</param>
        /// <param name="values">IDictionary of optional parameters</param>
        /// <returns>the string of response</returns>
        private string MakePostReq(string url, IDictionary<string, object> values)
        {
            using (WebClient wc = new WebClient())
            {
                try
                {
                    JObject jobj = JObject.FromObject(values);
                    string jsonParams = jobj.ToString();
                    jsonParams = jsonParams.Replace("\\\"", "\"");
                    jsonParams = jsonParams.Replace("\"[","[");
                    jsonParams = jsonParams.Replace("]\"", "]");

                    wc.Headers[HttpRequestHeader.ContentType] = "application/json";
                    string creds = Convert.ToBase64String(
                        Encoding.ASCII.GetBytes(this.apiToken + ":"));
                    wc.Headers[HttpRequestHeader.Authorization] = String.Format("Basic {0}", creds);
                    return wc.UploadString(url, jsonParams);
                }
                catch (WebException we)
                {
                    using (Stream stream = we.Response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            throw new WebException(reader.ReadToEnd());
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Sends GET HTTP request
        /// </summary>
        /// <param name="url">the URL for HTTP request</param>
        /// <returns>the string of response</returns>
        private string MakeGetReq(string url)
        {
            String sResponse = String.Empty;

            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Method = "GET";
                sResponse = String.Empty;

                string creds = Convert.ToBase64String(Encoding.ASCII.GetBytes(this.apiToken + ":"));
                request.Headers[HttpRequestHeader.Authorization] = String.Format("Basic {0}", creds);
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    sResponse = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();
                }
            }
            catch (Exception ex)
            {
            }

            return sResponse;
        }

        /// <summary>
        /// Sends custom HTTP request
        /// </summary>
        /// <param name="url">the URL for HTTP request</param>
        /// <param name="verb">the method of HTTP request</param>
        /// <param name="valueDict">IDictionary of optional parameters</param>
        /// <returns>true if working</returns>
        public bool MakeOtherReq(string url, string verb, IDictionary<string, object> valueDict)
        {
            try
            {
                string creds = "Basic " + Convert.ToBase64String(
                    Encoding.ASCII.GetBytes(this.apiToken + ":"));
                JObject values = JObject.FromObject(valueDict);
                byte[] buf = Encoding.UTF8.GetBytes(values.ToString());
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = verb;
                req.ContentType = "application/json";
                req.Headers.Add("Authorization", creds);
                req.ContentLength = buf.Length;

                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(buf, 0, buf.Length);
                }
                using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
                {
                    return (int)resp.StatusCode == 204;
                }
            }
            catch (WebException we)
            {
                using (Stream stream = we.Response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        throw new WebException(reader.ReadToEnd());
                    }
                }
            }
        }

        /// <summary>
        /// Queries an autocomplete service. 
        /// </summary>
        /// <param name="queryStr">queryStr The string that you will be autocompleting.</param>
        /// <returns>An ArrayList of suggestions for querying</returns>
        public List<string> Query(string queryStr)
        {
            List<string> res = new List<string>();
            string url = this.MakeUrl("autocomplete/" + queryStr);
            using (WebClient wc = new WebClient())
            {
                string response = wc.DownloadString(url);
                var encoding = ASCIIEncoding.ASCII;
                JObject responseJson = JObject.Parse(response);
                JArray suggestions = (JArray)responseJson.GetValue("suggestions");
                List<JObject> objRes = suggestions.ToObject<List<JObject>>();
                foreach (JObject obj in objRes)
                {
                    res.Add((string)obj.GetValue("value"));
                }
            }
            return res;
        }

        /// <summary>
        ///  Verifies that an autocomplete service is working.
        /// </summary>
        /// <returns> true if working.</returns>
        public bool Verify()
        {
            string url = this.MakeUrl("v1/verify");
            string sResponse = this.MakeGetReq(url);

            if (sResponse.IndexOf("successful authentication") > 0)
                return true;

            return false;
        }

        /// <summary>
        ///  Adds an item to your autocomplete.
        /// </summary>
        /// <param name="itemName">the item that you're adding.</param>
        /// <param name="autocompleteSection">the section of the autocomplete that you're adding the item to.</param>
        /// <returns>true if working</returns>
        public bool Add(string itemName, string autocompleteSection)
        {
            return this.Add(itemName, autocompleteSection, new Dictionary<string, object>());
        }

        /// <summary>
        /// Adds an item to your autocomplete.
        /// </summary>
        /// <param name="itemName">itemName the item that you're adding.</param>
        /// <param name="autocompleteSection">the section of the autocomplete that you're adding the item to.</param>
        /// <param name="paramDict">Optional parameters are in the <a href="https://constructor.io/docs/#add-an-item">API documentation</a></param>
        /// <returns> true on success</returns>
        public bool Add(string itemName, string autocompleteSection, IDictionary<string, object> paramDict)
        {
            string url = this.MakeUrl("v1/item");

                Dictionary<string, object> values = CreateItemParams(itemName, autocompleteSection, false, paramDict);
                string response = this.MakePostReq(url, values);

            return response == "";
        }


        /// <summary>
        /// Adds an item to your autocomplete.
        /// </summary>
        /// <param name="itemName">itemName the item that you're adding.</param>
        /// <param name="autocompleteSection">the section of the autocomplete that you're adding the item to</param>
        /// <param name="paramDict">IDictionary of optional parameters. Optional parameters are in the <a href="https://constructor.io/docs/#add-an-item">API documentation</a></param>
        /// <param name="keywords">list of keywords</param>
        /// <returns>true on success</returns>
        public bool Add(string itemName, string autocompleteSection, IDictionary<string, object> paramDict, List<string> keywords)
        {
            JArray arr = JArray.FromObject(keywords);

                paramDict.Add("keywords", arr.ToString());

            return this.Add(itemName, autocompleteSection, paramDict);
        }

        /// <summary>
        ///  Adds or updates an item to your autocomplete.
        /// </summary>
        /// <param name="itemName">the item that you're adding/updating.</param>
        /// <param name="autocompleteSection">the section of the autocomplete that you're adding/updating the item to.</param>
        /// <returns>true if working</returns>
        public bool AddOrUpdate(string itemName, string autocompleteSection)
        {
            return this.AddOrUpdate(itemName, autocompleteSection, new Dictionary<string, object>());
        }

        /// <summary>
        /// Adds or updates an item to your autocomplete.
        /// </summary>
        /// <param name="itemName">itemName the item that you're adding/updating.</param>
        /// <param name="autocompleteSection">the section of the autocomplete that you're adding/updating the item to.</param>
        /// <param name="paramDict">Optional parameters are in the <a href="https://constructor.io/docs/#add-an-item">API documentation</a></param>
        /// <returns> true on success</returns>
        public bool AddOrUpdate(string itemName, string autocompleteSection, IDictionary<string, object> paramDict)
        {
            Dictionary<string, object> queryStringDict = new Dictionary<string, object>{
                { "force", "1"}
            };
            string url = this.MakeUrl("v1/item", queryStringDict);

            Dictionary<string, object> values = CreateItemParams(itemName, autocompleteSection, false, paramDict);
            string response = this.MakePostReq(url, values);

            return response == "";
        }

        /// <summary>
        /// Adds an item to your autocomplete.
        /// </summary>
        /// <param name="itemName">itemName the item that you're adding.</param>
        /// <param name="autocompleteSection">the section of the autocomplete that you're adding the item to</param>
        /// <param name="paramDict">IDictionary of optional parameters. Optional parameters are in the <a href="https://constructor.io/docs/#add-an-item">API documentation</a></param>
        /// <param name="keywords">list of keywords</param>
        /// <returns>true on success</returns>
        public bool AddOrUpdate(string itemName, string autocompleteSection, IDictionary<string, object> paramDict, List<string> keywords)
        {
            JArray arr = JArray.FromObject(keywords);

            paramDict.Add("keywords", arr.ToString());

            return this.Add(itemName, autocompleteSection, paramDict);
        }

        /// <summary>
        /// Removes an item from your autocomplete.
        /// </summary>
        /// <param name="itemName"> the item that you're removing.</param>
        /// <param name="autocompleteSection">the section of the autocomplete that you're removing the item from.</param>
        /// <returns> true if successfully removed</returns>
        public bool Remove(string itemName, string autocompleteSection)
        {
            string url = this.MakeUrl("v1/item");

                Dictionary<string, object> values = CreateItemParams(itemName, autocompleteSection, false, null);

            return this.MakeOtherReq(url, "DELETE", values);
        }

        /// <summary>
        /// Modifies an item from your autocomplete.
        /// </summary>
        /// <param name="itemName"> the item that you're modifying.</param>
        /// <param name="newItemName">the new item name of the item you're modifying. If you don't wnat to change it, just put in the old itemName.</param>
        /// <param name="autocompleteSection">the section of the autocomplete that you're modifying the item from.</param>
        /// <returns>true if successfully modified</returns>
        public bool Modify(string itemName, string newItemName, string autocompleteSection)
        {
            return this.Modify(itemName, newItemName, autocompleteSection, new Dictionary<string, object>());
        }

        /// <summary>
        /// Modifies an item from your autocomplete.
        /// </summary>
        /// <param name="itemName">the item that you're modifying.</param>
        /// <param name="newItemName">the new item name of the item you're modifying. If you don't wnat to change it, just put in the old itemName.</param>
        /// <param name="autocompleteSection">the section of the autocomplete that you're modifying the item from.</param>
        /// <param name="paramDict">IDictionary of optional parameters. Optional parameters are in the <a href="https://constructor.io/docs/#modify-an-item">API documentation</a></param>
        /// <returns>true if successfully modified</returns>
        public bool Modify(string itemName, string newItemName, string autocompleteSection, IDictionary<string, object> paramDict)
        {
            string url = this.MakeUrl("v1/item");

                // new_item_name is mandatory param!
                paramDict.Add("new_item_name", newItemName);
                Dictionary<string, object> values = CreateItemParams(itemName, autocompleteSection, false, paramDict);

            return this.MakeOtherReq(url, "PUT", values);
        }

        /// <summary>
        /// Modifies an item from your autocomplete.
        /// </summary>
        /// <param name="itemName">the item that you're modifying.</param>
        /// <param name="newItemName">the new item name of the item you're modifying. If you don't wnat to change it, just put in the old itemName.</param>
        /// <param name="autocompleteSection">the section of the autocomplete that you're modifying the item from.</param>
        /// <param name="paramDict">IDictionary of optional parameters. Optional parameters are in the <a href="https://constructor.io/docs/#modify-an-item">API documentation</a></param>
        /// <param name="keywords">list of keywords</param>
        /// <returns>true if successfully modified</returns>
        public bool Modify(string itemName, string newItemName, string autocompleteSection, IDictionary<string, object> paramDict, List<string> keywords)
        {
            JArray arr = JArray.FromObject(keywords);

                paramDict.Add("keywords", arr.ToString());

            return this.Modify(itemName, newItemName, autocompleteSection, paramDict);
        }

        /// <summary>
        /// Tracks the fact that someone converted on your site.
        ///  Can be for any definition of conversion, whether someone buys a product or signs up or does something important to your site.
        /// </summary>
        /// <param name="term">the term that someone converted from</param>
        /// <param name="autocompleteSection">the autocomplete section that they converted from</param>
        /// <returns>true if successfully tracked</returns>
        public bool TrackConversion(string term, string autocompleteSection)
        {
            return this.TrackConversion(term, autocompleteSection, new Dictionary<string, object>());
        }

        /// <summary>
        /// Tracks the fact that someone converted on your site.
        /// Can be for any definition of conversion, whether someone buys a product or signs up or does something important to your site.
        /// </summary>
        /// <param name="term">the term that someone converted from</param>
        /// <param name="autocompleteSection">the autocomplete section that they converted from</param>
        /// <param name="paramDict">IDictionary of optional parameters. Optional parameters are in the <a href="https://constructor.io/docs/#add-an-item">API documentation</a></param>
        /// <returns>true if successfully tracked</returns>
        public bool TrackConversion(string term, string autocompleteSection, IDictionary<string, object> paramDict)
        {
            string url = this.MakeUrl("v1/conversion");

                Dictionary<string, object> values = CreateItemParams(term, autocompleteSection, true, paramDict);
                string response = this.MakePostReq(url, values);

            return response == "";
        }

        /// <summary>
        /// Tracks the fact that someone clicked through a search result on the site.
        /// </summary>
        /// <param name="term">the term that someone clicked.</param>
        /// <param name="autocompleteSection">the autocomplete section of the term that they clicked.</param>
        /// <returns>true if successfully tracked.</returns>
        public bool TrackClickThrough(string term, string autocompleteSection)
        {
            return this.TrackClickThrough(term, autocompleteSection, new Dictionary<string, object>());
        }

        /// <summary>
        /// Tracks the fact that someone clicked through a search result on the site.
        /// </summary>
        /// <param name="term"> the term that someone clicked.</param>
        /// <param name="autocompleteSection">the autocomplete section of the term that they clicked.</param>
        /// <param name="paramDict">IDictionary of optional parameters. Optional parameters are in the <a href="https://constructor.io/docs/#add-an-item">API documentation</a></param>
        /// <returns>true if successfully tracked.</returns>
        public bool TrackClickThrough(string term, string autocompleteSection, IDictionary<string, object> paramDict)
        {
            string url = this.MakeUrl("v1/click_through");

                Dictionary<string, object> values = CreateItemParams(term, autocompleteSection, true, paramDict);
                string response = this.MakePostReq(url, values);

            return response == "";
        }

        /// <summary>
        /// Tracks the fact that someone searched on your site.
        ///There's no autocompleteSection parameter because if you're searching, you aren't using an autocomplete.
        /// </summary>
        /// <param name="term">the term that someone searched.</param>
        /// <returns>true if successfully tracked.</returns>
        public bool TrackSearch(string term)
        {
            return this.TrackSearch(term, new Dictionary<string, object>());
        }

        /// <summary>
        /// Tracks the fact that someone searched on your site.
        /// There's no autocompleteSection parameter because if you're searching, you aren't using an autocomplete.
        /// </summary>
        /// <param name="term">the term that someone searched.</param>
        /// <param name="paramDict">IDictionary of the optional parameters. Optional parameters are in the <a href="https://constructor.io/docs/#track-a-search">API documentation</a></param>
        /// <returns>true if working</returns>
        public bool TrackSearch(string term, IDictionary<string, object> paramDict)
        {
            string url = this.MakeUrl("v1/search");

                Dictionary<string, object> values = CreateItemParams(term, null, true, paramDict);
                string response = this.MakePostReq(url, values);

            return response == "";
        }

        /// <summary>
        ///Adds multiple items
        /// </summary>
        /// <param name="items">IDictionary of the parameters you're encoding in the URL</param>
        /// <param name="autocompleteSection">the section of the autocomplete that you're adding the item to.</param>
        /// <returns>true on success</returns>
        public bool AddBatch(IDictionary<string, object> items, string autocompleteSection)
        {
            string url = this.MakeUrl("v1/batch_items");

                Dictionary<string, object> values = CreateItemsParams(items, autocompleteSection);
                string response = this.MakePostReq(url, values);

            return response == "";
        }

        /// <summary>
        ///Adds or updates multiple items
        /// </summary>
        /// <param name="items">IDictionary of the parameters you're encoding in the URL</param>
        /// <param name="autocompleteSection">the section of the autocomplete that you're adding/update the item to.</param>
        /// <returns>true on success</returns>
        public bool AddOrUpdateBatch(IDictionary<string, object> items, string autocompleteSection)
        {
            Dictionary<string, object> queryStringDict = new Dictionary<string, object>{
                { "force", "1"}
            };
            string url = this.MakeUrl("v1/batch_items", queryStringDict);

            Dictionary<string, object> values = CreateItemsParams(items, autocompleteSection);
            string response = this.MakePostReq(url, values);

            return response == "";
        }

        /// <summary>
        /// CreateItemsParams
        /// </summary>
        /// <param name="otherParams">Optional parameters are in the <a href="https://constructor.io/docs/#add-an-item">API documentation</a></param>
        /// <param name="autocompleteSection">Name of section</param>
        /// <returns> Dictionary<string, object> of created parameters</returns>
        public static Dictionary<string, object> CreateItemsParams(IDictionary<string, object> otherParams, string autocompleteSection)
        {
            Dictionary<string, object> paramDict = new Dictionary<string, object>();

                if (otherParams != null)
                {
                    foreach (var otherParam in otherParams)
                    {
                        paramDict.Add(otherParam.Key, otherParam.Value);
                    }
                }
                paramDict.Add("autocomplete_section", autocompleteSection);

            return paramDict;
        }
    }
}
