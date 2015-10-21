using System;
using System.Collections.Generic;

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
      return "boinkadoinka";
    }

    public static string createItemParams(string itemName, string autocompleteSection, bool isTracking, IDictionary<string, string> paramDict) {
      return "boinkamoinka";
    }

    private static bool checkResponse(int resp, int expectedStatus) {
      return true;
    }

    public string[] query(string queryStr) {
      return new string[] {"boinka", "woinka", "moinka"};
    }

    public bool verify() {
      return true;
    }

    public bool addItem(string itemName, string autocompleteSection) {
      return true;
    }

    public bool removeItem(string itemName, string autocompleteSection) {
      return true;
    }

    public bool modifyItem(string itemName, string autocompleteSection) {
      return true;
    }

    public bool trackConversion(string term, string autocompleteSection) {
      return true;
    }

    public bool trackClickThrough(string term, string autocompleteSection) {
      return true;
    }

    public bool trackSearch(string term) {
      return true;
    }

    public static void Main() {
      Console.WriteLine("boinka doinka");
    }

  }
}
