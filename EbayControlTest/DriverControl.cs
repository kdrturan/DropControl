using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbayControlTest
{
    public class DriverControl
    {
        public static ChromeDriver CreateAutoDsDriver(string url)
        {
            ChromeDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();
            return (driver);
        }


        public static ChromeDriver CreateAmazonDriver(string url)
        {
            var options = new ChromeOptions();
            string userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/119.0.0.0 Safari/537.37";
            options.AddArgument($"--user-agent={userAgent}");
            ChromeDriver driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();
            Thread.Sleep(4000);
            AmazonMiami(driver);
            return (driver);
        }


        public static  ChromeDriver CreateDriver(string url)
        {
            var options = new ChromeOptions();
            string userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/119.0.0.0 Safari/537.37";
            options.AddArgument($"--user-agent={userAgent}");
            ChromeDriver driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();
            return (driver);
        }

        public static void AmazonMiami(ChromeDriver driver)
        {
            driver.FindElement(By.XPath("//*[@id=\"nav-global-location-popover-link\"]")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//*[@id=\"GLUXZipUpdateInput\"]")).SendKeys("33133");
            driver.FindElement(By.XPath("//*[@id=\"GLUXZipUpdate\"]/span/input")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//*[@id=\"a-popover-1\"]/div/div[2]/span/span")).Click();
            Thread.Sleep(4000);
        }

        public static void LoginAutods(ChromeDriver driver,string mail,string password)
        {
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div[3]/div/div[1]/div/div[2]/form/div[2]/input")).SendKeys(mail);
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div[3]/div/div[1]/div/div[2]/form/div[3]/div/input")).SendKeys(password);
            driver.FindElement(By.XPath("//*[@id=\"root\"]/div[3]/div/div[1]/div/div[2]/form/button[2]")).Click();
        }
    }
}
