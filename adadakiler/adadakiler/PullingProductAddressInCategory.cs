using adadakiler.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using adadakiler.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;


namespace adadakiler
{
    public class PullingProductAddressInCategory
    {
        public void katpro(string katurl)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(katurl);
            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("Siteye Gidildi!");
            Thread.Sleep(2000);

            IReadOnlyCollection<IWebElement> LİstProduct = driver.FindElements(By.XPath("//*[@id='product-list-container']/div[4]/div/div"));

            foreach (IWebElement LİstProductone in LİstProduct)
            {
                IWebElement katname = LİstProductone.FindElement(By.ClassName("showcase-image"));

                string urunurl = katname.FindElement(By.TagName("A")).GetAttribute("href");

                ProductAddress productAddress = new ProductAddress();
                productAddress.State = true;
                productAddress.Path = urunurl;
                Console.WriteLine(urunurl);

                using (var context = new ProductContext())
                {
                    context.ProductAddresses.AddRange(productAddress);

                    context.SaveChanges();
                }
            }

            IWebElement nextpage2= driver.FindElement(By.ClassName("paginate"));
            IWebElement nextpage = nextpage2.FindElement(By.XPath("div[3]/a"));


            string kanextpage = nextpage.GetAttribute("href");


            if (kanextpage!= "javascript:void(0)")
            {
                driver.Close();
                this.katpro(kanextpage);
            }

            
            else
            {
                Console.WriteLine("biitti");
            }
        }

    }
}
        
    
