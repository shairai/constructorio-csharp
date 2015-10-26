using System;
using System.Collections.Generic;
using System.Net;

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

    public static string serializeParams(IDictionary<string, object> paramDict) {
      var list = new List<string>();
      foreach (var item in paramDict) {
        list.Add(item.Key + "=" + item.Value);
      }
      return string.Join("&", list);
    }

    public string makeUrl(string endpoint, IDictionary<string, string> paramDict) {
      paramDict.Add("autocomplete_key", this.autocompleteKey);
      string[] urlMembers = new string[] {
        this.protocol,
        this.host,
        endpoint,
        serializeParams((Dictionary<string, object>) paramDict)
      };
      return String.Format("{0}://{1}/{2}?{3}", urlMembers);
    }

    public static Dictionary<String, String> createItemParams(string itemName, string autocompleteSection, bool isTracking, IDictionary<string, string> otherParams) {
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

    private static bool checkResponse(HttpWebResponse resp, int expectedStatus) {
      // you must also catch the WebException!
      return ((int) resp.StatusCode) == expectedStatus;
    }

    private HttpWebResponse makePostReq(string url, IDictionary<string, string> values) {
      using (WebClient wc = new WebClient()) {
        try {
          JObject jobj = new JObject(values);
          string jsonparams = jobj.toString();
          wc.Headers[HttpRequestHeader.ContentType] = "application/json";
          string creds = Convert.ToBase64String(
              Encoding.ASCII.GetBytes(this.apiToken + ":"));
          wc.Headers[HttpRequestHeader.Authorization] = String.Format("Basic {0}", creds);
          wc.UploadString(url, jsonParams);
        } catch (WebException we) {
          throw new Exception();
        }
      }
    }

    private HttpWebResponse makeGetReq(string url, IDictionary<string, string> values) {
      using (WebClient wc = new WebClient()) {
        try {
          JObject jobj = new JObject(values);
          string jsonparams = jobj.toString();
          wc.Headers[HttpRequestHeader.ContentType] = "application/json";
          string creds = Convert.ToBase64String(
              Encoding.ASCII.GetBytes(this.apiToken + ":"));
          wc.Headers[HttpRequestHeader.Authorization] = String.Format("Basic {0}", creds);
          wc.DownloadString(url, jsonParams);
        } catch (WebException we) {
          throw new Exception();
        }
      }
    }

    public List<string> query(string queryStr) {
      List<string> res = new List<string>();
      HttpWebResponse response = this.makeGetReq(queryDict);
      var encoding = ASCIIEncoding.ASCII;
      using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding)) {
        string responseText = reader.ReadToEnd();
        JObject responseJson = JObject.Parse(responseText);
        JArray suggestions = responseJson.get("suggestions");
        res = JArray.toList();
      }
      return res;
    }

    public bool verify() {
      HttpWebResponse response = this.makePostReq(queryDict);
      return checkResponse(response, 200);
    }

    public bool addItem(string itemName, string autocompleteSection) {
      string url = this.makeUrl(something);
      Dictionary<string, string> values = createItemParams(itemName, autocompleteSection, false, null);
      HttpWebResponse response = this.makePostReq(url, values);
      return checkResponse(response, 200);
    }

    ///// get to here, copy and paste

    public bool removeItem(string itemName, string autocompleteSection) {
      return true;
      /////////////////////
      /////////////////////
      /////////////////////
    }

    public bool modifyItem(string itemName, string autocompleteSection) {
      return true;
      /////////////////////
      /////////////////////
      /////////////////////
    }

    public bool trackConversion(string term, string autocompleteSection) {
      return true;
      /////////////////////
      /////////////////////
      /////////////////////
    }

    public bool trackClickThrough(string term, string autocompleteSection) {
      return true;
      /////////////////////
      /////////////////////
      /////////////////////
    }

    public bool trackSearch(string term) {
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
