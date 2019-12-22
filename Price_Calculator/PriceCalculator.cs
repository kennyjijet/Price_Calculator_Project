using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
namespace Price_Calculator
{
    public class PromotionRules
    {
        public string Coupon { get; set; }
        public int UnpaidCustomer { get; set; }

        public int DiscountPercentage { get; set; }

        public int MinimumPrice { get; set; }

        public int ConditionPeople { get; set; }
    }
    interface IPriceCalculator
    {
        void checkPromotionRules();
        void calculate(PromotionRules item);
        void print();

        int getPromotedPrice();
    }

    public class MyPriceCalculator : IPriceCalculator
    {
        public int numberOfCustomers;
        public int pricePerPerson;
        public string couponCode;
        public int totalPrice;

        public List<int> promotedPriceList;


        public MyPriceCalculator(int numberOfCustomers, int pricePerPerson, string couponCode)
        {
            this.numberOfCustomers = numberOfCustomers;
            this.pricePerPerson = pricePerPerson;
            this.couponCode = couponCode;
            this.totalPrice = this.pricePerPerson * this.numberOfCustomers;
            this.promotedPriceList = new List<int>();
        }

        public int calculatePercentage(float value, int currentPrice)
        {
            return (int)((value / 100) * currentPrice);
        }

        public int calculateUnPaidCustomers(float value)
        {
            return (int)(value * this.pricePerPerson);
        }

        public void calculate(PromotionRules item)
        {
            int promotedPrice = this.totalPrice;
            int unpaidValue = calculateUnPaidCustomers(item.UnpaidCustomer);
            if (unpaidValue > 0)
            {
                promotedPrice -= unpaidValue;
            }
            //Console.WriteLine(promotedPrice);

            int percentageValue = calculatePercentage(item.DiscountPercentage, promotedPrice);
            if (percentageValue > 0)
            {
                promotedPrice -= percentageValue;
            }
            //Console.WriteLine(promotedPrice);
            promotedPriceList.Add(promotedPrice);
        }
        public void checkPromotionRules()
        {
            using (StreamReader r = new StreamReader("PromotionRules.json"))
            {
                var json = r.ReadToEnd();
                var items = JsonConvert.DeserializeObject<List<PromotionRules>>(json);
                string none = "NONE";
                foreach (var item in items)
                {
                    if ((
                        (this.totalPrice >= item.MinimumPrice)
                        &&
                        (this.numberOfCustomers == item.ConditionPeople)
                        &&
                        (this.couponCode == item.Coupon)
                        &&
                        (
                            item.MinimumPrice > 0
                            &&
                            item.ConditionPeople > 0
                            &&
                            item.Coupon != none
                        )
                    ))
                    {
                        //Console.WriteLine("Case1");
                        calculate(item);
                    }
                    else if ((this.couponCode == item.Coupon
                        &&
                        this.numberOfCustomers == item.ConditionPeople
                        )
                        &&
                        (
                            item.MinimumPrice == 0
                            &&
                            item.ConditionPeople > 0
                            &&
                            item.Coupon != none
                        )
                        )
                    {
                        //Console.WriteLine("Case2");
                        calculate(item);
                    }
                    else if (
                        (this.totalPrice >= item.MinimumPrice
                        &&
                        this.couponCode == item.Coupon)
                        &&
                        (
                        item.MinimumPrice > 0
                        &&
                        item.Coupon != none
                        &&
                        item.ConditionPeople == 0
                        )
                        )
                    {
                        //Console.WriteLine("Case3");
                        calculate(item);
                    }
                    else if ((this.totalPrice >= item.MinimumPrice
                        &&
                        this.numberOfCustomers == item.ConditionPeople)
                        &&
                        (
                            item.MinimumPrice > 0
                            &&
                            item.ConditionPeople > 0
                            &&
                            item.Coupon == none
                        )
                        )
                    {
                        //Console.WriteLine("Case4");
                        calculate(item);
                    }

                    else if (
                    this.totalPrice >= item.MinimumPrice &&
                    (item.MinimumPrice > 0 
                    && 
                    item.ConditionPeople == 0 
                    && item.Coupon == none)
                    )
                    {
                        //Console.WriteLine("Case5");
                        calculate(item);
                    }

                    else if (this.numberOfCustomers == item.ConditionPeople 
                    &&
                    (
                    item.ConditionPeople > 0 
                    && 
                    item.Coupon == none 
                    && 
                    item.MinimumPrice == 0)
                    )
                    {
                        //Console.WriteLine("Case6");
                        calculate(item);
                    }
                    else if (this.couponCode == item.Coupon &&
                    (item.ConditionPeople == 0 
                    && 
                    item.MinimumPrice == 0 
                    && 
                    item.Coupon != none)
                    )
                    {
                        //Console.WriteLine("Case7");
                        calculate(item);
                    }
                }
                if (promotedPriceList.Count == 0)
                {
                    promotedPriceList.Add(this.totalPrice);
                }
            }
        }
        public void print()
        {
            Console.WriteLine("Output : " + this.promotedPriceList.Min());
        }

        public int getPromotedPrice()
        {
            return this.promotedPriceList.Min();
        }
    }
}

