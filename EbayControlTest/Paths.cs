using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbayControlTest
{
    public class Paths
    {
        public static string amazonUrl = "https://www.amazon.com/ref=nav_bb_logo";
        public static string dsUrl = "https://platform.autods.com/login";
        public static string dsProductUrl = "https://platform.autods.com/products";
        public static string ebayPricePath = "td[10]/div[2]";
        public static string ebayAmazonPricePath = "td[10]/div[1]";
        public static string amazonLinkPath = "td[12]/div[1]/a";
        public static string asinCodePath = "td[12]/div[1]/a/div";
        public static string wholePriceCss = "span.a-price-whole";
        public static string fractionPriceCss = "span.a-price-fraction";
        public static string productNumbers = "//*[@id=\"root\"]/div[3]/div/main/div/div[6]/div/p[2]";
        public static string ariaSelected = "//div[@class='ant5-select-item ant5-select-item-option' and @title='200']";
        public static string showsProduct = "//*[@id=\"root\"]/div[3]/div/main/div/div[6]/div/div/div";
        public static string scrollScript = "window.scrollTo(0, document.body.scrollHeight);";


        public static string DsProductColumn(int column)
        {
            string dsProductColumn = $"//*[@id=\"products-table\"]/table/tbody/tr[{column}]/";
            return dsProductColumn;
        }
    }
}
