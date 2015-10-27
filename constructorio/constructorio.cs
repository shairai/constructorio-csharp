using System;
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

    public static Dictionary<String, String> CreateItemParams(string itemName, string autocompleteSection, bool isTracking, IDictionary<string, string> otherParams) {
      Dictionary<string, string> paramDict = new Dictionary<string, string>();
      if (isTracking) {
        paramDict.Add("term", itemName);
      } else {
        paramDict.Add("item_name", itemName);
      }
      paramDict.Add("autocomplete_section", autocompleteSection);
      if (otherParams != null) {
        foreach (var otherParam in otherParams) {
          paramDict.Add(otherParam.Key, otherParam.Value);
        }
      }
      return paramDict;
    }

    private string MakePostReq(string url, IDictionary<string, string> values) {
      using (WebClient wc = new WebClient()) {
        try {
          JObject jobj = new JObject(values);
          string jsonParams = jobj.ToString();
          wc.Headers[HttpRequestHeader.ContentType] = "application/json";
          string creds = Convert.ToBase64String(
              Encoding.ASCII.GetBytes(this.apiToken + ":"));
          wc.Headers[HttpRequestHeader.Authorization] = String.Format("Basic {0}", creds);
          return wc.UploadString(url, jsonParams);
        } catch {
          throw new Exception();
        }
      }
    }

    public List<string> Query(string queryStr) {
      List<string> res = new List<string>();
      string url = this.MakeUrl("autocomplete/" + queryStr);
      using (WebClient wc = new WebClient()) {
        try {
          string response = wc.DownloadString(url);
          var encoding = ASCIIEncoding.ASCII;
          JObject responseJson = JObject.Parse(response);
          JArray suggestions = (JArray) responseJson.GetValue("suggestions");
          res = suggestions.ToObject<List<string>>();
        } catch {
          throw new Exception();
        }
      }
      return res;
    }

    public bool Verify() {
      string url = this.MakeUrl("v1/verify");
      string response = this.MakePostReq(url, new Dictionary<string, string>());
      return response == "OK";
    }

    public bool AddItem(string itemName, string autocompleteSection) {
      string url = this.MakeUrl("v1/item");
      Dictionary<string, string> values = CreateItemParams(itemName, autocompleteSection, false, null);
      string response = this.MakePostReq(url, values);
      return response == "OK";
    }

    ///// get to here, copy and paste

    public bool RemoveItem(string itemName, string autocompleteSection) {
      return true;
      /////////////////////
      /////////////////////
      /////////////////////
    }

    public bool ModifyItem(string itemName, string autocompleteSection) {
      return true;
      /////////////////////
      /////////////////////
      /////////////////////
    }

    public bool TrackConversion(string term, string autocompleteSection) {
      return true;
      /////////////////////
      /////////////////////
      /////////////////////
    }

    public bool TrackClickThrough(string term, string autocompleteSection) {
      return true;
      /////////////////////
      /////////////////////
      /////////////////////
    }

    public bool TrackSearch(string term) {
      return true;
      /////////////////////
      /////////////////////
      /////////////////////
    }

    public static void Main() {
      Console.WriteLine("boinka doinka");
    }

  }
}
