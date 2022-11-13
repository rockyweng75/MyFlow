using MyFlow.Test.Cases;
using System.Diagnostics;

namespace MyFlow.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var cases = new TestCase1();
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(cases);
            Console.Write(json);
            Debug.Write(json);
            Assert.Pass();
        }
    }
}