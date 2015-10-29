using System;
using System.IO;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;

namespace ConstructorIOClient {
  public class ConstructorIO {
    public string apiToken;
    public string autocompleteKey;
    public string protocol;
    public string host;

    public ConstructorIO(string apiToken, string autocompleteKey, string protocol="https", string host="ac.cnstrc.com") {
      this.apiToken = apiToken;
      this.autocompleteKey = autocompleteKey;
      this.protocol = protocol;
      this.host = host;
    }

    public static string SerializeParams(IDictionary<string, object> paramDict) {
      var list = new List<string>();
      foreach (var item in paramDict) {
        list.Add(System.Uri.EscapeDataString(item.Key) + "=" + System.Uri.EscapeDataString((string)item.Value));
      }
      return string.Join("&", list);
    }

    public string MakeUrl(string endpoint, IDictionary<string, object> keys) {
      Dictionary<string, object> paramDict = new Dictionary<string, object>(keys);
      paramDict.Add("autocomplete_key", this.autocompleteKey);
      string[] urlMembers = new string[] {
        this.protocol,
        this.host,
        endpoint,
        SerializeParams(paramDict)
      };
      return String.Format("{0}://{1}/{2}?{3}", urlMembers);
    }

    public string MakeUrl(string endpoint) {
      // empty keys
      Dictionary<string, object> keys = new Dictionary<string, object>();
      return this.MakeUrl(endpoint, keys);
    }

    public static Dictionary<string, object> CreateItemParams(string itemName, string autocompleteSection, bool isTracking, IDictionary<string, object> otherParams) {
      Dictionary<string, object> paramDict = new Dictionary<string, object>();
      if (isTracking) {
        paramDict.Add("term", itemName);
      } else {
        paramDict.Add("item_name", itemName);
      }
      if (autocompleteSection != null) {
        paramDict.Add("autocomplete_section", autocompleteSection);
      }
      if (otherParams != null) {
        foreach (var otherParam in otherParams) {
          paramDict.Add(otherParam.Key, otherParam.Value);
        }
      }
      return paramDict;
    }

    private string MakePostReq(string url, IDictionary<string, object> values) {
      using (WebClient wc = new WebClient()) {
        try {
        JObject jobj = JObject.FromObject(values);
        string jsonParams = jobj.ToString();
        wc.Headers[HttpRequestHeader.ContentType] = "application/json";
        string creds = Convert.ToBase64String(
            Encoding.ASCII.GetBytes(this.apiToken + ":"));
        wc.Headers[HttpRequestHeader.Authorization] = String.Format("Basic {0}", creds);
        return wc.UploadString(url, jsonParams);
        } catch (WebException we) {
          using (Stream stream = we.Response.GetResponseStream()) {
            using (StreamReader reader = new StreamReader(stream)) {
              throw new WebException(reader.ReadToEnd());
            }
          }
        }
      }
    }

    private bool MakeOtherReq(string url, string verb, IDictionary<string, object> valueDict) {
      try {
        string creds = "Basic " + Convert.ToBase64String(
            Encoding.ASCII.GetBytes(this.apiToken + ":"));
        JObject values = JObject.FromObject(valueDict);
        byte[] buf = Encoding.UTF8.GetBytes(values.ToString());
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
        req.Method = verb;
        req.ContentType = "application/json";
        req.Headers.Add("Authorization", creds);
        req.ContentLength = buf.Length;
        using (Stream reqStream = req.GetRequestStream()) {
          reqStream.Write(buf, 0, buf.Length);
        }
        using (HttpWebResponse resp = (HttpWebResponse) req.GetResponse()) {
          return (int)resp.StatusCode == 204;
        }
      } catch (WebException we) {
        using (Stream stream = we.Response.GetResponseStream()) {
          using (StreamReader reader = new StreamReader(stream)) {
            throw new WebException(reader.ReadToEnd());
          }
        }
      }
    }

    public List<string> Query(string queryStr) {
      List<string> res = new List<string>();
      string url = this.MakeUrl("autocomplete/" + queryStr);
      using (WebClient wc = new WebClient()) {
        string response = wc.DownloadString(url);
        var encoding = ASCIIEncoding.ASCII;
        JObject responseJson = JObject.Parse(response);
        JArray suggestions = (JArray) responseJson.GetValue("suggestions");
        List<JObject> objRes = suggestions.ToObject<List<JObject>>();
        foreach (JObject obj in objRes) {
          res.Add((string) obj.GetValue("value"));
        }
      }
      return res;
    }

    public bool Verify() {
      string url = this.MakeUrl("v1/verify");
      string response = this.MakePostReq(url, new Dictionary<string, object>());
      return response == "";
    }

    public bool AddItem(string itemName, string autocompleteSection) {
      return this.AddItem(itemName, autocompleteSection, new Dictionary<string, object>());
    }

    public bool AddItem(string itemName, string autocompleteSection, IDictionary<string, object> paramDict) {
      string url = this.MakeUrl("v1/item");
      Dictionary<string, object> values = CreateItemParams(itemName, autocompleteSection, false, paramDict);
      string response = this.MakePostReq(url, values);
      return response == "";
    }

    public bool AddItem(string itemName, string autocompleteSection, IDictionary<string, object> paramDict, List<string> keywords) {
      JArray arr = JArray.FromObject(keywords);
      paramDict.Add("keywords", arr.ToString());
      return this.AddItem(itemName, autocompleteSection, paramDict);
    }

    public bool RemoveItem(string itemName, string autocompleteSection) {
      string url = this.MakeUrl("v1/item");
      Dictionary<string, object> values = CreateItemParams(itemName, autocompleteSection, false, null);
      return this.MakeOtherReq(url, "DELETE", values);
    }

    public bool ModifyItem(string itemName, string newItemName, string autocompleteSection) {
      return this.ModifyItem(itemName, newItemName, autocompleteSection, new Dictionary<string, object>());
    }

    public bool ModifyItem(string itemName, string newItemName, string autocompleteSection, IDictionary<string, object> paramDict) {
      string url = this.MakeUrl("v1/item");
      // new_item_name is mandatory param!
      paramDict.Add("new_item_name", newItemName);
      Dictionary<string, object> values = CreateItemParams(itemName, autocompleteSection, false, paramDict);
      return this.MakeOtherReq(url, "PUT", values);
    }

    public bool ModifyItem(string itemName, string newItemName, string autocompleteSection, IDictionary<string, object> paramDict, List<string> keywords) {
      JArray arr = JArray.FromObject(keywords);
      paramDict.Add("keywords", arr.ToString());
      return this.ModifyItem(itemName, newItemName, autocompleteSection, paramDict);
    }

    public bool TrackConversion(string term, string autocompleteSection) {
      return this.TrackConversion(term, autocompleteSection, new Dictionary<string, object>());
    }
    
    public bool TrackConversion(string term, string autocompleteSection, IDictionary<string, object> paramDict) {
      string url = this.MakeUrl("v1/conversion");
      Dictionary<string, object> values = CreateItemParams(term, autocompleteSection, true, paramDict);
      string response = this.MakePostReq(url, values);
      return response == "";
    }

    public bool TrackClickThrough(string term, string autocompleteSection) {
      return this.TrackClickThrough(term, autocompleteSection, new Dictionary<string, object>());
    }
    
    public bool TrackClickThrough(string term, string autocompleteSection, IDictionary<string, object> paramDict) {
      string url = this.MakeUrl("v1/click_through");
      Dictionary<string, object> values = CreateItemParams(term, autocompleteSection, true, paramDict);
      string response = this.MakePostReq(url, values);
      return response == "";
    }

    public bool TrackSearch(string term) {
      return this.TrackSearch(term, new Dictionary<string, object>());
    }

    public bool TrackSearch(string term, IDictionary<string, object> paramDict) {
      string url = this.MakeUrl("v1/search");
      Dictionary<string, object> values = CreateItemParams(term, null, true, paramDict);
      string response = this.MakePostReq(url, values);
      return response == "";
    }

    public static void Main() {
      // null
    }

  }
}
