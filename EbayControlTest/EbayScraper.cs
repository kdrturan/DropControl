using EbayControl;
using EbayControlTest;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using static OpenQA.Selenium.BiDi.Modules.BrowsingContext.Locator;

public class PriceScraper
{
    private static decimal GetEbayPrice(ChromeDriver autodsDriver, string xpath)
    {
        try
        {
            var ebayPrice = autodsDriver.FindElement(By.XPath(xpath + Paths.ebayPricePath));
            string ebayPrice_Tetx = ebayPrice.Text.Replace("SELL\r\n$", "").Trim().Split('-')[0];
            decimal price = decimal.Parse(ebayPrice_Tetx, CultureInfo.InvariantCulture);
            return price;
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Problem getting Ebay Price: ");
            Console.WriteLine(ex.Message);
            Console.ReadLine();
            Console.ResetColor();
            return 0;
        }

    }

    private static decimal GetEbayAmazonPrice(ChromeDriver autodsDriver, string xpath)
    {
        try
        {
            var ebayPrice = autodsDriver.FindElement(By.XPath(xpath + Paths.ebayAmazonPricePath));
            string ebayPrice_Tetx = ebayPrice.Text.Replace("BUY\r\n$", "").Trim().Split('-')[0];
            decimal price = decimal.Parse(ebayPrice_Tetx, CultureInfo.InvariantCulture);
            return price;
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Problem getting Ebay Amazon Price: ");
            Console.WriteLine(ex.Message);
            Console.ReadLine();
            Console.ResetColor();
            return 0;
        }

    }

    private static  decimal  GetAmazonPrice(ChromeDriver amazonDriver, string url)
    {
        WebDriverWait wait = new WebDriverWait(amazonDriver, TimeSpan.FromSeconds(10));

        try
        {
            amazonDriver.Navigate().GoToUrl(url);
            var whole = wait.Until(driver => driver.FindElement(By.CssSelector(Paths.wholePriceCss))).Text.Trim();
            var fraction = amazonDriver.FindElement(By.CssSelector(Paths.fractionPriceCss)).Text.Trim();
            decimal price = decimal.Parse(whole + "." + fraction, CultureInfo.InvariantCulture);
            return price;
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Problem getting Amazon Price: " + url);
            Console.WriteLine(ex.Message);
            Console.ReadLine();
            Console.ResetColor();
            return 0;
        }
    }

    public static string GetAsinCode(ChromeDriver autodsDriver, string xpath)
    {
        return autodsDriver.FindElement(By.XPath(xpath + Paths.asinCodePath)).Text.Trim();
    }

    public static  Ebay_Control GetData(ChromeDriver autodsDriver,string xpath)
    {
        Ebay_Control data = new Ebay_Control();

        data.EbayPrice = GetEbayPrice(autodsDriver, xpath);
        data.ASINCode = GetAsinCode(autodsDriver,xpath);
        data.AmazonLink = autodsDriver.FindElement(By.XPath(xpath + Paths.amazonLinkPath)).GetAttribute("href");
        data.AmazonPrice = GetEbayAmazonPrice(autodsDriver, xpath);
        data.InStock = true;
        data.LastControl = DateTime.Now;
        return (data);
    }


    public static void CheckProfit(ChromeDriver amazonDriver, Ebay_Control product)
    {
        decimal price = PriceScraper.GetAmazonPrice(amazonDriver, product.AmazonLink);
        decimal Task  = price * 15 / 100;
        decimal ebayFee = product.EbayPrice * 20 / 100;
        decimal profit = product.EbayPrice - (price + Task + ebayFee + 1);
        if (price == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error: " + product.ASINCode);
            Console.ResetColor();
        }
        else if (price != product.AmazonPrice)
        {
            WriteProfit(product.ASINCode, Math.Round(profit, 2));
        }
        else
        {
            Console.WriteLine(product.ASINCode + ": $" + Math.Round(profit, 2));
        }
    }


    public static bool CheckAmazonPrice(ChromeDriver amazonDriver,Ebay_Control product)
    {
        decimal price = PriceScraper.GetAmazonPrice(amazonDriver,product.AmazonLink);
        if (price != product.AmazonPrice)
        {
            WriteChanged(product.ASINCode, (price - product.AmazonPrice));
            Console.ResetColor();
            return false;
        }
        return true;
    }


    public static void SetShowProduct(ChromeDriver autodsDriver)
    {
        try
        {
            Thread.Sleep(5500);
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)autodsDriver;
            jsExecutor.ExecuteScript(Paths.scrollScript);
            System.Threading.Thread.Sleep(300);
            autodsDriver.FindElement(By.XPath(Paths.showsProduct)).Click();
            System.Threading.Thread.Sleep(150);
            var aria_Selected = autodsDriver.FindElement(By.XPath(Paths.ariaSelected));
            var isTrue = aria_Selected.GetAttribute("aria-selected");
            if (isTrue == "false")
            {
                aria_Selected.Click();
            }
            System.Threading.Thread.Sleep(3000);

        }
        catch (Exception ex)
        {

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Problem setting Show Product: ");
            Console.WriteLine(ex.Message);
            Console.ReadLine();
            Console.ResetColor();
        }
    }

    /////////////


    public static void WriteProfit(string asin, decimal profit)
    {
        if (profit < 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("---------  ");
            Console.WriteLine(asin + " price has changed: $" + profit);
            Console.WriteLine("------------");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("---------  ");
            Console.WriteLine(asin + " price has changed: $" + profit);
            Console.WriteLine("------------");
        }
        Console.ResetColor();
    }


    public static void WriteChanged(string asin,decimal difference)
    {
        if (difference > 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("---------  ");
            Console.WriteLine(asin + " price has changed: -$" + difference);
            Console.WriteLine("------------");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("---------  ");
            Console.WriteLine(asin + " price has changed: $" + Math.Abs(difference));
            Console.WriteLine("------------");
        }
        Console.ResetColor();
    }






}
