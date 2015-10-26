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
      Assert.AreEqual("https://ac.cnstrc.com/v1/woinka?autocomplete_key=moinka", paramUrl, "Creates URL correctly with parameters");
    }

    [Test]
    public void TestParamSetting() {
      ////////////
    }

    [Test]
    public void TestQuery() {
      ////////////
    }

    [Test]
    public void TestAdd() {
      ////////////
    }
    
    [Test]
    public void TestRemove() {
      ////////////
    }
    
    [Test]
    public void TestModify() {
      ////////////
    }
    
    [Test]
    public void TestTrackConversion() {
      ////////////
    }
    
    [Test]
    public void TestTrackSearch() {
      ////////////
    }
    
    [Test]
    public void TestTrackClickThrough() {
      ////////////
    }

    public static void Main() {
      // null
    }
  }
}
