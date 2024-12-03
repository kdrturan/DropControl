using EbayControl;
using EbayControlTest;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)  // 'Main' metodunu asenkron yapıyoruz
    {

        //ChromeDriver dsDriver = DriverControl.CreateAutoDsDriver(Paths.dsUrl);
        ChromeDriver amazonDriver = DriverControl.CreateAmazonDriver(Paths.amazonUrl);

        //DriverControl.LoginAutods(dsDriver, LoginData.Mail, LoginData.password);
        Thread.Sleep(5000);
        //dsDriver.Navigate().GoToUrl(Paths.dsProductUrl);
        //PriceScraper.SetShowProduct(dsDriver);
        //Operations.GetProducts(dsDriver,amazonDriver);
        Operations.CheckDbProducts(amazonDriver);
        //dsDriver.Quit();
        Console.WriteLine("Veritabanı hazır!");
    }
}
