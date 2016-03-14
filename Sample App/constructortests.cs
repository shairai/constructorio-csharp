using System;
using System.Collections.Generic;
using NUnit.Framework;
using ConstructorIOClient;

namespace ConstructorTest {
  [TestFixture]
  public class ConstructorTest {
    static Random random;

    static ConstructorTest() {
      random = new Random();
    }

    [Test]
    public void TestSerializeParams() {
      Dictionary<string, object> paramDict = new Dictionary<string, object>();
      paramDict.Add("boinka", "woinka");
      paramDict.Add("moinka", "foink a");
      string serialized = ConstructorIO.SerializeParams(paramDict);
      Assert.AreEqual("boinka=woinka&moinka=foink%20a", serialized, "Serializes params properly");
    }

    [Test]
    public void TestMakeUrl() {
      ConstructorIO client = new ConstructorIO("moinka", "foinka");
      string url = client.MakeUrl("v1/woinka");
      Assert.AreEqual("https://ac.cnstrc.com/v1/woinka?autocomplete_key=foinka", url, "Creates URL correctly");
      Dictionary<string, object> paramDict = new Dictionary<string, object>();
      paramDict.Add("boinka", "woinka");
      paramDict.Add("moinka", "foinka");
      string paramUrl = client.MakeUrl("v1/woinka", paramDict);
      Assert.AreEqual("https://ac.cnstrc.com/v1/woinka?boinka=woinka&moinka=foinka&autocomplete_key=foinka", paramUrl, "Creates URL correctly with parameters");
    }

    [Test]
    public void TestParamSetting() {
      ConstructorIO client = new ConstructorIO("moinka", "foinka");
      Assert.AreEqual("moinka", client.apiToken, "Sets API Key correctly");
      Assert.AreEqual("foinka", client.autocompleteKey, "Sets AC Key correctly");
    }

    /*
     * Testing keys, which are really actually for testing, honest to god:
     * "YSOxV00F0Kk2R0KnPQN8", "ZqXaOfXuBWD4s3XzCI1q"
     */

    [Test]
    public void TestQuery() {
      ConstructorIO client = new ConstructorIO("YSOxV00F0Kk2R0KnPQN8", "ZqXaOfXuBWD4s3XzCI1q");
      List<string> queryRes = client.Query("a");
      Assert.NotNull(queryRes, "Query gets something");
    }

    public static string GenerateRandString() {
      string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
      char[] stringChars = new char[10];
      for (int i = 0; i < stringChars.Length; i++) { stringChars[i] = chars[random.Next(chars.Length)];
      }
      return new String(stringChars);
    }

    [Test]
    public void TestAdd() {
      ConstructorIO client = new ConstructorIO("YSOxV00F0Kk2R0KnPQN8", "ZqXaOfXuBWD4s3XzCI1q");
      string itemName1 = GenerateRandString();
      string itemName2 = GenerateRandString();
      Dictionary<string, object> paramDict1 = new Dictionary<string, object>();
      paramDict1.Add("suggested_score", 1337);
      Assert.IsTrue(client.Add(itemName1, "Search Suggestions"), "Addition without params returns alright");
      Assert.IsTrue(client.Add(itemName2, "Search Suggestions", paramDict1), "Addition with params returns alright");
    }
    
    [Test]
    public void TestRemove() {
      ConstructorIO client = new ConstructorIO("YSOxV00F0Kk2R0KnPQN8", "ZqXaOfXuBWD4s3XzCI1q");
      string itemName = GenerateRandString();
      Assert.IsTrue(client.Add(itemName, "Search Suggestions"), "Creation of item for removal returns alright");
      Assert.IsTrue(client.Remove(itemName, "Search Suggestions"), "Removes item alright");
    }
    
    [Test]
    public void TestModify() {
      ConstructorIO client = new ConstructorIO("YSOxV00F0Kk2R0KnPQN8", "ZqXaOfXuBWD4s3XzCI1q");
      string itemName1 = GenerateRandString();
      string itemName2 = GenerateRandString();
      string newItemName1 = GenerateRandString();
      string newItemName2 = GenerateRandString();
      Assert.IsTrue(client.Add(itemName1, "Search Suggestions"), "Creation of first item for modification returns alright");
      Assert.IsTrue(client.Add(itemName2, "Search Suggestions"), "Creation of second item for modification returns alright");
      Dictionary<string, object> paramDict2 = new Dictionary<string, object>();
      paramDict2.Add("suggested_score", 1337);
      Assert.IsTrue(client.Modify(itemName1, newItemName1, "Search Suggestions"), "Modification without params returns alright");
      Assert.IsTrue(client.Modify(itemName2, newItemName2, "Search Suggestions", paramDict2), "Modification with one param returns alright");
    }
    
    [Test]
    public void TestTrackConversion() {
      ConstructorIO client = new ConstructorIO("YSOxV00F0Kk2R0KnPQN8", "ZqXaOfXuBWD4s3XzCI1q");
      string itemName = GenerateRandString();
      Assert.IsTrue(client.Add(itemName, "Search Suggestions"), "Creation of first item for tracking returns alright");
      Assert.IsTrue(client.TrackConversion(itemName, "Search Suggestions"), "Track conversion without parameters returns alright");
      Dictionary<string, object> paramDict2 = new Dictionary<string, object>();
      paramDict2.Add("item", itemName);
      Assert.IsTrue(client.TrackConversion(itemName, "Search Suggestions", paramDict2), "Track conversion with one parameter returns alright");
    }
    
    [Test]
    public void TestTrackSearch() {
      ConstructorIO client = new ConstructorIO("YSOxV00F0Kk2R0KnPQN8", "ZqXaOfXuBWD4s3XzCI1q");
      string itemName = GenerateRandString();
      Assert.IsTrue(client.Add(itemName, "Search Suggestions"), "Creation of first item for tracking returns alright");
      Assert.IsTrue(client.TrackSearch(itemName), "Track searching without parameters returns alright");
      Dictionary<string, object> paramDict2 = new Dictionary<string, object>();
      paramDict2.Add("item", itemName);
      Assert.IsTrue(client.TrackSearch(itemName, paramDict2), "Track search with one parameter returns alright");
    }
    
    [Test]
    public void TestTrackClickThrough() {
      ConstructorIO client = new ConstructorIO("YSOxV00F0Kk2R0KnPQN8", "ZqXaOfXuBWD4s3XzCI1q");
      string itemName = GenerateRandString();
      Assert.IsTrue(client.Add(itemName, "Search Suggestions"), "Creation of first item for tracking returns alright");
      Assert.IsTrue(client.TrackClickThrough(itemName, "Search Suggestions"), "Track clickthrough without parameters returns alright");
      Dictionary<string, object> paramDict2 = new Dictionary<string, object>();
      paramDict2.Add("item", itemName);
      Assert.IsTrue(client.TrackClickThrough(itemName, "Search Suggestions", paramDict2), "Track clickthrough with one parameter returns alright");
    }

   /*
    public static void Main() {
      // null
    }

    */
  }
}
