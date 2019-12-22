using System;

namespace Price_Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(priceCal.getName());
            while (true)
            {
                Console.WriteLine("Number of customers: ");
                int numberOfCustomers = Convert.ToInt32(Console.ReadLine());
                if (numberOfCustomers == -1)
                { 
                    break;
                }

                Console.WriteLine("Price per person: ");
                int pricePerPerson = Convert.ToInt32(Console.ReadLine());
                if (pricePerPerson == -1)
                { 
                    break;
                }

                Console.WriteLine("coupon code: ");
                string couponCode = Console.ReadLine();
                if (couponCode.ToUpper() == "EXIT")
                {
                    break;
                }
                MyPriceCalculator priceCal = new MyPriceCalculator(numberOfCustomers, pricePerPerson, couponCode.ToUpper());
                priceCal.checkPromotionRules();
                priceCal.print();
            }
        }
    }
}
