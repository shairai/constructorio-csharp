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
        /// This is contracutor for class ConstructorIO. More details 
        /// </summary>
        /// <param name="apiToken">your API token</param>
        /// <param name="autocompleteKey">autocomplete key</param>
        /// <param name="protocol">protocol</param>
        /// <param name="host">host</param>
        public ConstructorIO(string apiToken, string autocompleteKey, string protocol = "https", string host = "ac.cnstrc.com")
        {
            this.apiToken = apiToken;
            this.autocompleteKey = autocompleteKey;
            this.protocol = protocol;
            this.host = host;
        }

        /// <summary>
        /// SerializeParams
        /// </summary>
        /// <param name="paramDict"></param>
        /// <returns></returns>
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
        /// MakeUrl
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
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
        /// MakeUrl
        /// </summary>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public string MakeUrl(string endpoint)
        {
            // empty keys
            Dictionary<string, object> keys = new Dictionary<string, object>();
            return this.MakeUrl(endpoint, keys);
        }

        /// <summary>
        /// CreateItemParams1
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="autocompleteSection"></param>
        /// <param name="isTracking"></param>
        /// <param name="otherParams"></param>
        /// <returns></returns>
        public Dictionary<string, object> CreateItemParams1(string itemName, string autocompleteSection, bool isTracking, IDictionary<string, object> otherParams)
        {
            return CreateItemParams(itemName, autocompleteSection, isTracking, otherParams);
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
        private bool MakeGetReq(string url)
        {
            try
            {
                string creds = "Basic " + Convert.ToBase64String(
                Encoding.ASCII.GetBytes(this.apiToken + ":"));
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Headers.Add("auth_token", creds);

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
            }
            catch (Exception ex)
            { }
            return false;
        }

        /// <summary>
        /// MakeOtherReq
        /// </summary>
        /// <param name="url"></param>
        /// <param name="verb"></param>
        /// <param name="valueDict"></param>
        /// <returns>bool</returns>
        private bool MakeOtherReq(string url, string verb, IDictionary<string, object> valueDict)
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
        /// Query
        /// </summary>
        /// <param name="queryStr"></param>
        /// <returns></returns>
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
        /// Verify
        /// </summary>
        /// <returns>bool</returns>
        public bool Verify()
        {
            string url = this.MakeUrl("v1/verify");
            Dictionary<string, object> keys = new Dictionary<string, object>();
            return this.MakeOtherReq(url, "GET", keys);
        }

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="autocompleteSection"></param>
        /// <returns>bool</returns>
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

        public bool Add(string itemName, string autocompleteSection, IDictionary<string, object> paramDict, List<string> keywords)
        {
            JArray arr = JArray.FromObject(keywords);
            paramDict.Add("keywords", arr.ToString());
            return this.Add(itemName, autocompleteSection, paramDict);
        }

        public bool Remove(string itemName, string autocompleteSection)
        {
            string url = this.MakeUrl("v1/item");
            Dictionary<string, object> values = CreateItemParams(itemName, autocompleteSection, false, null);
            return this.MakeOtherReq(url, "DELETE", values);
        }

        public bool Modify(string itemName, string newItemName, string autocompleteSection)
        {
            return this.Modify(itemName, newItemName, autocompleteSection, new Dictionary<string, object>());
        }

        public bool Modify(string itemName, string newItemName, string autocompleteSection, IDictionary<string, object> paramDict)
        {
            string url = this.MakeUrl("v1/item");
            // new_item_name is mandatory param!
            paramDict.Add("new_item_name", newItemName);
            Dictionary<string, object> values = CreateItemParams(itemName, autocompleteSection, false, paramDict);
            return this.MakeOtherReq(url, "PUT", values);
        }

        public bool Modify(string itemName, string newItemName, string autocompleteSection, IDictionary<string, object> paramDict, List<string> keywords)
        {
            JArray arr = JArray.FromObject(keywords);
            paramDict.Add("keywords", arr.ToString());
            return this.Modify(itemName, newItemName, autocompleteSection, paramDict);
        }

        public bool TrackConversion(string term, string autocompleteSection)
        {
            return this.TrackConversion(term, autocompleteSection, new Dictionary<string, object>());
        }

        public bool TrackConversion(string term, string autocompleteSection, IDictionary<string, object> paramDict)
        {
            string url = this.MakeUrl("v1/conversion");
            Dictionary<string, object> values = CreateItemParams(term, autocompleteSection, true, paramDict);
            string response = this.MakePostReq(url, values);
            return response == "";
        }

        public bool TrackClickThrough(string term, string autocompleteSection)
        {
            return this.TrackClickThrough(term, autocompleteSection, new Dictionary<string, object>());
        }

        public bool TrackClickThrough(string term, string autocompleteSection, IDictionary<string, object> paramDict)
        {
            string url = this.MakeUrl("v1/click_through");
            Dictionary<string, object> values = CreateItemParams(term, autocompleteSection, true, paramDict);
            string response = this.MakePostReq(url, values);
            return response == "";
        }

        public bool TrackSearch(string term)
        {
            return this.TrackSearch(term, new Dictionary<string, object>());
        }

        public bool TrackSearch(string term, IDictionary<string, object> paramDict)
        {
            string url = this.MakeUrl("v1/search");
            Dictionary<string, object> values = CreateItemParams(term, null, true, paramDict);
            string response = this.MakePostReq(url, values);
            return response == "";
        }

        public bool BatchAdd(IDictionary<string, object> items, AutoCompleListType autocompleteSection)
        {
            string url = this.MakeUrl("v1/batch_items");

            Dictionary<string, object> values = CreateItemsParams(items, StringEnum.GetStringValue(autocompleteSection));
            string response = this.MakePostReq(url, values);
            return response == "";
        }

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
