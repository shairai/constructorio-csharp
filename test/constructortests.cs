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
      paramDict.Add("moinka", "foinka");
      string serialized = ConstructorIO.SerializeParams(paramDict);
      Assert.AreEqual(serialized, "something something", "Serializes params properly");
    }

    [Test]
    public void TestMakeUrl() {
      ////////////
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
