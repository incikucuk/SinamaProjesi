using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;


namespace Sinama
{
    [TestClass]
    public class UnitTest1
    {
        private Calculate KartHesapla;

        [TestInitialize]

        public void Init()
        {
            KartHesapla = new Calculate();
        }
        [TestMethod]
        public void Sum_5_and_5_Return_10()
        {
            Assert.AreEqual(10, KartHesapla.Sum(5, 5));
        }


    }
    public class Calculate
    {
        public int Sum(int x,int y)
        {
            return x + y;
        }
    }
}
