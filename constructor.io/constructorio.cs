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
    /// ConstructorIO
    /// </summary>
    public class ConstructorIO
    {
        public string apiToken;
        public string autocompleteKey;
        public string protocol;
        public string host;

        /// <summary>
        /// AutoCompleListType - built in types
        /// </summary>
        public enum AutoCompleListType
        {
            [StringValue("Products")]
            Product,
            [StringValue("Search Suggestions")]
            SearchSuggestions,
        };

        /// <summary>
        ///  Creates a constructor.io client. 
        /// </summary>
        /// <param name="apiToken">apiToken API Token, gotten from your <a href="https://constructor.io/dashboard">Constructor.io Dashboard</a>, and kept secret.</param>
        /// <param name="autocompleteKey">autocompleteKey Autocomplete key, used publically in your in-site javascript client.</param>
        /// <param name="protocol">It is highly recommended that you use HTTPS.</param>
        /// <param name="host">The host of the autocomplete service that you are using. It is recommended that you let this value be null, in which case the host defaults to the Constructor.io autocomplete servic at ac.cnstrc.com.</param>
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
        /// <param name="keys">keys IDictionary of the parameters you're encoding in the URL</param>
        /// <returns> The created URL. Now you can use it to issue requests and things!</returns>
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
        /// <param name="endpoint"> endpoint Endpoint of the autocomplete service.</param>
        /// <returns> The created URL. Now you can use it to issue requests and things!</returns>
        public string MakeUrl(string endpoint)
        {
            // empty keys
            Dictionary<string, object> keys = new Dictionary<string, object>();
            return this.MakeUrl(endpoint, keys);
        }

        /// <summary>
        /// CreateItemParams
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="autocompleteSection"></param>
        /// <param name="isTracking"></param>
        /// <param name="otherParams"></param>
        /// <returns>Dictionary<string, object></returns>
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
        /// MakePostReq
        /// </summary>
        /// <param name="url"></param>
        /// <param name="values"></param>
        /// <returns>string</returns>
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
        /// MakeGetReq
        /// </summary>
        /// <param name="url"></param>
        /// <returns>bool</returns>
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
        /// MakeOtherReq
        /// </summary>
        /// <param name="url"></param>
        /// <param name="verb"></param>
        /// <param name="valueDict"></param>
        /// <returns>bool</returns>
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
        /// Note that if you're making an autocomplete service on a website, you should definitely use our javascript client instead of doing it server-side!
        /// That's important. That will be a solid latency difference.
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
        /// <param name="autocompleteSection"> the section of the autocomplete that you're adding the item to.</param>
        /// <returns>true if working</returns>
        public bool Add(string itemName, string autocompleteSection)
        {
            return this.Add(itemName, autocompleteSection, new Dictionary<string, object>());
        }

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="autocompleteSection"></param>
        /// <param name="paramDict"></param>
        /// <returns>bool</returns>
        public bool Add(string itemName, string autocompleteSection, IDictionary<string, object> paramDict)
        {
            string url = this.MakeUrl("v1/item");

                Dictionary<string, object> values = CreateItemParams(itemName, autocompleteSection, false, paramDict);
                string response = this.MakePostReq(url, values);

            return response == "";
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="autocompleteSection"></param>
        /// <param name="paramDict"></param>
        /// <param name="keywords"></param>
        /// <returns></returns>
        public bool Add(string itemName, string autocompleteSection, IDictionary<string, object> paramDict, List<string> keywords)
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
        /// <param name="paramDict"></param>
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
        /// <param name="paramDict"></param>
        /// <param name="keywords"></param>
        /// <returns>true if successfully modified</returns>
        public bool Modify(string itemName, string newItemName, string autocompleteSection, IDictionary<string, object> paramDict, List<string> keywords)
        {
            JArray arr = JArray.FromObject(keywords);

                paramDict.Add("keywords", arr.ToString());

            return this.Modify(itemName, newItemName, autocompleteSection, paramDict);
        }

        /// <summary>
        /// TrackConversion
        /// </summary>
        /// <param name="term"></param>
        /// <param name="autocompleteSection"></param>
        /// <returns>bool</returns>
        public bool TrackConversion(string term, string autocompleteSection)
        {
            return this.TrackConversion(term, autocompleteSection, new Dictionary<string, object>());
        }

        /// <summary>
        /// TrackConversion
        /// </summary>
        /// <param name="term"></param>
        /// <param name="autocompleteSection"></param>
        /// <param name="paramDict"></param>
        /// <returns>bool</returns>
        public bool TrackConversion(string term, string autocompleteSection, IDictionary<string, object> paramDict)
        {
            string url = this.MakeUrl("v1/conversion");

                Dictionary<string, object> values = CreateItemParams(term, autocompleteSection, true, paramDict);
                string response = this.MakePostReq(url, values);

            return response == "";
        }

        /// <summary>
        /// TrackClickThrough
        /// </summary>
        /// <param name="term"></param>
        /// <param name="autocompleteSection"></param>
        /// <returns>bool</returns>
        public bool TrackClickThrough(string term, string autocompleteSection)
        {
            return this.TrackClickThrough(term, autocompleteSection, new Dictionary<string, object>());
        }

        /// <summary>
        /// TrackClickThrough
        /// </summary>
        /// <param name="term"></param>
        /// <param name="autocompleteSection"></param>
        /// <param name="paramDict"></param>
        /// <returns>bool</returns>
        public bool TrackClickThrough(string term, string autocompleteSection, IDictionary<string, object> paramDict)
        {
            string url = this.MakeUrl("v1/click_through");

                Dictionary<string, object> values = CreateItemParams(term, autocompleteSection, true, paramDict);
                string response = this.MakePostReq(url, values);

            return response == "";
        }

        /// <summary>
        /// TrackSearch
        /// </summary>
        /// <param name="term"></param>
        /// <returns>bool</returns>
        public bool TrackSearch(string term)
        {
            return this.TrackSearch(term, new Dictionary<string, object>());
        }

        /// <summary>
        /// TrackSearch
        /// </summary>
        /// <param name="term"></param>
        /// <param name="paramDict"></param>
        /// <returns></returns>
        public bool TrackSearch(string term, IDictionary<string, object> paramDict)
        {
            string url = this.MakeUrl("v1/search");

                Dictionary<string, object> values = CreateItemParams(term, null, true, paramDict);
                string response = this.MakePostReq(url, values);

            return response == "";
        }

        /// <summary>
        /// BatchAdd
        /// </summary>
        /// <param name="items"></param>
        /// <param name="autocompleteSection"></param>
        /// <returns>bool</returns>
        public bool BatchAdd(IDictionary<string, object> items, AutoCompleListType autocompleteSection)
        {
            string url = this.MakeUrl("v1/batch_items");

                Dictionary<string, object> values = CreateItemsParams(items, StringEnum.GetStringValue(autocompleteSection));
                string response = this.MakePostReq(url, values);

            return response == "";
        }

        /// <summary>
        /// CreateItemsParams
        /// </summary>
        /// <param name="otherParams"></param>
        /// <param name="autocompleteSection"></param>
        /// <returns> Dictionary<string, object></returns>
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
