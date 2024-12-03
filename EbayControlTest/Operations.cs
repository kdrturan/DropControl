using EbayControl;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbayControlTest
{
    public  class Operations
    {
        public static void GetProducts(ChromeDriver dsDriver,ChromeDriver amazonDriver)
        {
            Ebay_Control product = new Ebay_Control();
            string temp_asin;
            int numbers = Operations.CheckList(dsDriver);
            if (numbers > 0)
            {
                int i = 1;
                while (i <= numbers)
                {
                    Thread.Sleep(1000);
                    temp_asin = PriceScraper.GetAsinCode(dsDriver, Paths.DsProductColumn(i));
                    if (OperationDb.Get(x => x.ASINCode == temp_asin) == null)
                    {
                        product = PriceScraper.GetData(dsDriver, amazonDriver, Paths.DsProductColumn(i));
                        OperationDb.Add(product);
                        Console.WriteLine("Listed: " + product.ASINCode);
                    }
                    else
                    {
                        Console.WriteLine("Already Listed: " + temp_asin);
                    }
                    i++;
                }
                Console.WriteLine("All products listed");
            }
        }


        public static void CheckDbProducts(ChromeDriver amazonDriver)
        {
            var data = OperationDb.GetList();
            foreach (var item in data)
            {
                bool cntrl = PriceScraper.CheckAmazonPrice(amazonDriver, item);
                if (cntrl)
                {
                    Console.WriteLine("Checked item:" + item.ASINCode);
                }
                item.LastControl = DateTime.Now;
                OperationDb.Update(item);
            }
        }


        public static int CheckList(ChromeDriver dsDriver)
        {
            //autods de kaç listelenmiş ürün olduğunu söyler
            var storeNumber = dsDriver.FindElement(By.XPath(Paths.productNumbers)).Text;
            var split = storeNumber.Split(' ');
            int number = int.Parse(split[3]);
            return (number);
        }
    }
}
