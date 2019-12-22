using Microsoft.VisualStudio.TestTools.UnitTesting;
using Price_Calculator;
namespace Price_Calculator_Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            
            MyPriceCalculator case1 = new MyPriceCalculator(1,500,"DIS10");
            case1.checkPromotionRules();
            Assert.AreEqual(case1.getPromotedPrice(), 450);

            MyPriceCalculator case2 = new MyPriceCalculator(1,2000,"STARCARD");
            case2.checkPromotionRules();
            Assert.AreEqual(case2.getPromotedPrice(), 1800);

            MyPriceCalculator case3 = new MyPriceCalculator(2,1500,"DIS10");
            case3.checkPromotionRules();
            Assert.AreEqual(case3.getPromotedPrice(), 2250);

            MyPriceCalculator case4 = new MyPriceCalculator(4,500,"STARCARD");
            case4.checkPromotionRules();
            Assert.AreEqual(case4.getPromotedPrice(), 1500);

            MyPriceCalculator case5 = new MyPriceCalculator(2,1500,"STARCARD");
            case5.checkPromotionRules();
            Assert.AreEqual(case5.getPromotedPrice(), 2100);

            MyPriceCalculator case6 = new MyPriceCalculator(4,3000,"STARCARD");
            case6.checkPromotionRules();
            Assert.AreEqual(case6.getPromotedPrice(), 9000);

            MyPriceCalculator case7 = new MyPriceCalculator(4,3000,"NONE");
            case7.checkPromotionRules();
            Assert.AreEqual(case7.getPromotedPrice(), 9000);
            
            MyPriceCalculator case8 = new MyPriceCalculator(4,3000,"FFDSAFDS");
            case8.checkPromotionRules();
            Assert.AreEqual(case8.getPromotedPrice(), 9000);

            MyPriceCalculator case9 = new MyPriceCalculator(4,3000,"FFDSAFDS");
            case9.checkPromotionRules();
            Assert.AreEqual(case9.getPromotedPrice(), 9000);

            MyPriceCalculator case10 = new MyPriceCalculator(4,500,"STARCARD1");
            case10.checkPromotionRules();
            Assert.AreEqual(case10.getPromotedPrice(), 1200);


        }
    }
}
