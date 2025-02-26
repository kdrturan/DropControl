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

        OperationDb.DeleteAll();
        ChromeDriver dsDriver = DriverControl.CreateAutoDsDriver(Paths.dsUrl);
        DriverControl.LoginAutods(dsDriver, LoginData.Mail, LoginData.password);
        ChromeDriver amazonDriver = DriverControl.CreateAmazonDriver(Paths.amazonUrl);
        dsDriver.Navigate().GoToUrl(Paths.dsProductUrl);
        PriceScraper.SetShowProduct(dsDriver);
        Operations.GetProducts(dsDriver);
        Operations.CheckDbProducts(amazonDriver);
        dsDriver.Quit();
        amazonDriver.Quit();
        Console.WriteLine("Veritabanı hazır!");
    }
}
