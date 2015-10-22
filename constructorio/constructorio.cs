using System;
using System.Collections.Generic;
using System.Net.Http;

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

    public static string makeUrl(string endpoint, IDictionary<string, string> paramDict) {
      paramDict.put("autocomplete_key", this.autocompleteKey);
      string[] urlMembers = new int[] {
        this.protocol,
        this.host,
        endpoint,
        this.serializeParams(paramDict)
      };
      return String.Format("{0}://{1}/{2}?{3}", urlMembers);
    }

    public static string createItemParams(string itemName, string autocompleteSection, bool isTracking, IDictionary<string, string> otherParams) {
      Dictionary<string, string> paramDict = new Dictionary<string, string>();
      if (isTracking) {
        paramDict.put("term", itemName);
      } else {
        paramDict.put("item_name", itemName);
      }
      paramDict.put("autocomplete_section", autocompleteSection);
      if (otherParams != null) {
        paramDict.putAll(otherParams);
      }
      JObject jobj = new JObject(paramDict);
      string serialized = jobj.toString();
      return serialized;
    }

    private static bool checkResponse(HttpWebResponse resp, int expectedStatus) {
      // you must also catch the WebException!
      return ((int) resp.StatusCode) == expectedStatus;
    }

    private static HttpWebResponse makePostReq(string url, IDictionary<string, string> values) {
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
          //
        }
      }
    }

    private static HttpWebResponse makeGetReq(string url, IDictionary<string, string> values) {
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
          //
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
      return this.checkResponse(response, 200);
    }

    public bool addItem(string itemName, string autocompleteSection) {
      //HttpWebResponse response = this.makePostReq(values);
      return this.checkResponse(response, 200);
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
