using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System;

namespace Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (var vi = new Colony.Program.Main())
            {
                Console.SetOut(sw);
                Colony.Program.Main();

                var result = sw.ToString().Trim();
                Assert.AreEqual(Expected, result);
            }
        }
    }
}
