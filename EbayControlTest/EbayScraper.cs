using EbayControl;
using EbayControlTest;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using static OpenQA.Selenium.BiDi.Modules.BrowsingContext.Locator;

public class PriceScraper
{

    private static decimal GetEbayPrice(ChromeDriver autodsDriver, string xpath)
    {
        var ebayPrice = autodsDriver.FindElement(By.XPath(xpath + Paths.ebayPricePath));
        string ebayPrice_Tetx = ebayPrice.Text.Replace("SELL\r\n$", "").Trim().Split('-')[0];
        decimal price = decimal.Parse(ebayPrice_Tetx, CultureInfo.InvariantCulture);
        return price;
    }

    private static  decimal  GetAmazonPrice(ChromeDriver amazonDriver, string url)
    {
        amazonDriver.Navigate().GoToUrl(url);
        Thread.Sleep(2000);
        var whole = amazonDriver.FindElement(By.CssSelector(Paths.wholePriceCss)).Text.Trim();
        var fraction = amazonDriver.FindElement(By.CssSelector(Paths.fractionPriceCss)).Text.Trim();
        decimal price = decimal.Parse(whole + '.' + fraction, CultureInfo.InvariantCulture);
        return price;
    }

    public static string GetAsinCode(ChromeDriver autodsDriver, string xpath)
    {
        return autodsDriver.FindElement(By.XPath(xpath + Paths.asinCodePath)).Text.Trim();
    }

    public static  Ebay_Control GetData(ChromeDriver autodsDriver,ChromeDriver amazonDriver,string xpath)
    {
        Ebay_Control data = new Ebay_Control();

        data.EbayPrice = GetEbayPrice(autodsDriver, xpath);
        data.ASINCode = GetAsinCode(autodsDriver,xpath);
        data.AmazonLink = autodsDriver.FindElement(By.XPath(xpath + Paths.amazonLinkPath)).GetAttribute("href");
        data.AmazonPrice = GetAmazonPrice(amazonDriver, data.AmazonLink);
        data.InStock = true;
        data.LastControl = DateTime.Now;
        return (data);
    }


    public static bool CheckAmazonPrice(ChromeDriver amazonDriver,Ebay_Control product)
    {
        decimal price = PriceScraper.GetAmazonPrice(amazonDriver,product.AmazonLink);
        if (price != product.AmazonPrice)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("---------  ");
            Console.WriteLine(product.ASINCode);
            Console.WriteLine(" ürün fiyatı değişti ------------");
            Console.ResetColor();
            return false;
        }
        return true;
    }




    public static void SetShowProduct(ChromeDriver autodsDriver)
    {
        Thread.Sleep(5500);
        IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)autodsDriver;
        jsExecutor.ExecuteScript(Paths.scrollScript);
        System.Threading.Thread.Sleep(3000);
        autodsDriver.FindElement(By.XPath(Paths.showsProduct)).Click();
        System.Threading.Thread.Sleep(1500);
        var aria_Selected = autodsDriver.FindElement(By.XPath(Paths.ariaSelected));
        var isTrue = aria_Selected.GetAttribute("aria-selected");
        if (isTrue == "false")
        {
            aria_Selected.Click();
        }
        System.Threading.Thread.Sleep(3000);
    }

}
