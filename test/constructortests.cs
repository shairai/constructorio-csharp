using System;
using NUnit.Framework;

namespace ConstructorTests {
  [TestFixture]
  public class ConstructorTest {
    [Test]
    public void TestSerializeParams() {
      Assert.AreEqual(1, 1, "walla");
    }
  }
}
