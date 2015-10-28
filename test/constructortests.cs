using System;
using System.Collections.Generic;
using NUnit.Framework;
using ConstructorIOClient;

namespace ConstructorTest {
  [TestFixture]
  public class ConstructorTest {
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
      Random random = new Random();
      string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
      char[] stringChars = new char[10];
      for (int i = 0; i < stringChars.Length; i++) { stringChars[i] = chars[random.Next(chars.Length)];
      }
      return new String(stringChars);
    }

    [Test]
    public void TestAdd() {
      ConstructorIO client = new ConstructorIO("YSOxV00F0Kk2R0KnPQN8", "ZqXaOfXuBWD4s3XzCI1q");
      string itemName = GenerateRandString();
      Dictionary<string, string> paramDict1 = new Dictionary<string, object>();
      paramDict1.Add("suggested_score", 1337);
      Assert.IsTrue(client.AddItem(itemName, "Search Suggestions"), "Addition without params returns alright");
      Assert.IsTrue(client.AddItem(itemName, "Search Suggestions", paramDict1), "Addition with params returns alright");
    }
    
    [Test]
    public void TestRemove() {
      ConstructorIO client = new ConstructorIO("YSOxV00F0Kk2R0KnPQN8", "ZqXaOfXuBWD4s3XzCI1q");
      string itemName = GenerateRandString();
      Assert.IsTrue(client.AddItem(itemName, "Search Suggestions"), "Creation of item for removal returns alright");
      Assert.IsTrue(client.RemoveItem(itemName, "Search Suggestions"), "Removes item alright");
    }
    
    [Test]
    public void TestModify() {
      ConstructorIO client = new ConstructorIO("YSOxV00F0Kk2R0KnPQN8", "ZqXaOfXuBWD4s3XzCI1q");
      string itemName1 = GenerateRandString();
      string itemName2 = GenerateRandString();
      string itemName3 = GenerateRandString();
      string newItemName1 = GenerateRandString();
      string newItemName2 = GenerateRandString();
      string newItemName3 = GenerateRandString();
      Assert.IsTrue(client.AddItem(itemName1, "Search Suggestions"), "Creation of first item for modification returns alright");
      Assert.IsTrue(client.AddItem(itemName2, "Search Suggestions"), "Creation of second item for modification returns alright");
      Assert.IsTrue(client.AddItem(itemName3, "Search Suggestions"), "Creation of third item for modification returns alright");
      Assert.IsTrue(client.ModifyItem(some shit, some shit));
      make the paramdicts
      Assert.AreEqual(1,1,"modify without params returns alright");
      Assert.AreEqual(1,1,"modify with one param returns alright");
      Assert.AreEqual(1,1,"modify with all params returns alright");
      ////////////
    }
    
    [Test]
    public void TestTrackConversion() {
      ConstructorIO client = new ConstructorIO("YSOxV00F0Kk2R0KnPQN8", "ZqXaOfXuBWD4s3XzCI1q");
      string itemName = GenerateRandString();
      Assert.IsTrue(client.AddItem(itemName1, "Search Suggestions"), "Creation of first item for tracking returns alright");
      Assert.IsTrue(client.TrackConversion(itemName1, "Search Suggestions"), "Track conversion without parameters returns alright");
      Assert.AreEqual(1,1,"track conversion with one param returns alright");
      Assert.AreEqual(1,1,"track conv with all params returns alright");
      ////////////
    }
    
    [Test]
    public void TestTrackSearch() {
      ConstructorIO client = new ConstructorIO("YSOxV00F0Kk2R0KnPQN8", "ZqXaOfXuBWD4s3XzCI1q");
      string itemName = GenerateRandString();
      Assert.IsTrue(client.AddItem(itemName1, "Search Suggestions"), "Creation of first item for tracking returns alright");
      Assert.IsTrue(client.TrackSearch(itemName1), "Track searching without parameters returns alright");
      Assert.AreEqual(1,1,"track search with one param returns alright");
      Assert.AreEqual(1,1,"track search with all params returns alright");
      ////////////
    }
    
    [Test]
    public void TestTrackClickThrough() {
      ConstructorIO client = new ConstructorIO("YSOxV00F0Kk2R0KnPQN8", "ZqXaOfXuBWD4s3XzCI1q");
      string itemName = GenerateRandString();
      Assert.IsTrue(client.AddItem(itemName1, "Search Suggestions"), "Creation of first item for tracking returns alright");
      Assert.IsTrue(client.TrackClickThrough(itemName1, "Search Suggestions"), "Track clickthrough without parameters returns alright");
      Assert.AreEqual(1,1,"track clickthrough with one param returns alright");
      Assert.AreEqual(1,1,"track clickthrough with all params returns alright");
      // put in stuff about keywords, tho
      ////////////
    }

    public static void Main() {
      // null
    }
  }
}
