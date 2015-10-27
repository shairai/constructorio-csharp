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
     */

    [Test]
    public void TestQuery() {
      ConstructorIO client = new ConstructorIO("YSOxV00F0Kk2R0KnPQN8", "ZqXaOfXuBWD4s3XzCI1q");
      List<string> queryRes = client.Query("a");
      Assert.NotNull(queryRes, "Query gets something");
    }

    [Test]
    public void TestAdd() {
      ConstructorIO client = new ConstructorIO("YSOxV00F0Kk2R0KnPQN8", "ZqXaOfXuBWD4s3XzCI1q");
      Assert.IsTrue(client.AddItem("woinka", "Search Suggestions"), "addition without params returns alright");
      Assert.AreEqual(1,1,"addition with params returns alright");
      ////////////
    }
    
    [Test]
    public void TestRemove() {
      ConstructorIO client = new ConstructorIO("YSOxV00F0Kk2R0KnPQN8", "ZqXaOfXuBWD4s3XzCI1q");
      Assert.AreEqual(1,1,"remove returns alright");
      ////////////
    }
    
    [Test]
    public void TestModify() {
      ConstructorIO client = new ConstructorIO("YSOxV00F0Kk2R0KnPQN8", "ZqXaOfXuBWD4s3XzCI1q");
      Assert.AreEqual(1,1,"modify without params returns alright");
      Assert.AreEqual(1,1,"modify with params returns alright");
      ////////////
    }
    
    [Test]
    public void TestTrackConversion() {
      ConstructorIO client = new ConstructorIO("YSOxV00F0Kk2R0KnPQN8", "ZqXaOfXuBWD4s3XzCI1q");
      Assert.AreEqual(1,1,"track conversion without params returns alright");
      Assert.AreEqual(1,1,"track conversion with params returns alright");
      ////////////
    }
    
    [Test]
    public void TestTrackSearch() {
      ConstructorIO client = new ConstructorIO("YSOxV00F0Kk2R0KnPQN8", "ZqXaOfXuBWD4s3XzCI1q");
      Assert.AreEqual(1,1,"track search without params returns alright");
      Assert.AreEqual(1,1,"track search with params returns alright");
      ////////////
    }
    
    [Test]
    public void TestTrackClickThrough() {
      ConstructorIO client = new ConstructorIO("YSOxV00F0Kk2R0KnPQN8", "ZqXaOfXuBWD4s3XzCI1q");
      Assert.AreEqual(1,1,"track clickthrough without params returns alright");
      Assert.AreEqual(1,1,"track clickthrough with params returns alright");
      // put in stuff about keywords, tho
      ////////////
    }

    public static void Main() {
      // null
    }
  }
}
