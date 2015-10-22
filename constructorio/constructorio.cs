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

    public static string createItemParams(string itemName, string autocompleteSection, bool isTracking, IDictionary<string, string> paramDict) {
      return "boinkamoinka";
      /////////////////////
      /////////////////////
      /////////////////////
    }

    private static bool checkResponse(int resp, int expectedStatus) {
      return true;
      /////////////////////
      /////////////////////
      /////////////////////
    }

    private static bool makePostReq(IDictionary<string, string> values) {
      using (var client = new HttpClient()) {
        var content = new FormUrlEncodedContent(values); // no! jsonified!
        // gotta do auth
        // need to send the created item / term params, and they have to be in text and json, basically

      }
    }

    public List<string> query(string queryStr) {
      List<string> res = new List<string>();
      return res;
      /////////////////////
      /////////////////////
      /////////////////////
    }

    public bool verify() {
      return true;
      /////////////////////
      /////////////////////
      /////////////////////
    }

    public bool addItem(string itemName, string autocompleteSection) {
      return true;
      /////////////////////
      /////////////////////
      /////////////////////
    }

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
