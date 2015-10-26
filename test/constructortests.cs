using System;
using NUnit.Framework;
using ConstructorIO;

namespace ConstructorTests {
  [TestFixture]
  public class ConstructorTest {
    [Test]
    public void TestSerializeParams() {
      ConstructorIO constructor = new ConstructorIO();
      give it something to serialize
      string serialized = constructor.SerializeParams();
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
  }
}
