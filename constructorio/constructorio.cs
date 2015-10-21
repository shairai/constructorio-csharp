using System;

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

    public static string makeUrl(string endpoint) {
    }

    public static string makeUrl(string endpoint, IDictionary<string, string> params) {
    }

    public static string createItemParams(string itemName, string autocompleteSection, bool isTracking) {
    }

    public static string createItemParams(string itemName, string autocompleteSection, IDictionary<string, string> otherJsonParams, bool isTracking) {
    }

    private static boolean checkResponse(resp, int expectedStatus) {
    }

    public string[] query(string queryStr) {
    }

    public bool verify() {
    }

    public bool addItem(string itemName, string autocompleteSection) {
    }

    public bool removeItem(string itemName, string autocompleteSection) {
    }

    public bool modifyItem(string itemName, string autocompleteSection) {
    }

    public bool trackConversion(string term, string autocompleteSection) {
    }

    public bool trackClickThrough(string term, string autocompleteSection) {
    }

    public bool trackSearch(string term) {
    }

  }
}
