using adadakiler.Models;
using OpenQA.Selenium;
using adadakiler.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using adadakiler.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Linq;

namespace adadakiler
{
    public class Brands
    {
        public void brand_added()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.adadakiler.com/arama/markalar");
            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("Siteye Gidildi!");
            Thread.Sleep(2000);

            IReadOnlyCollection<IWebElement> Brandis = driver.FindElements(By.XPath("//*[@id='main']/div/div/section/div[2]/div/div/form/div[1]/div[3]/div/div/select/option"));

            List<Brand> brand = new List<Brand>();

            foreach (IWebElement Brandii2 in Brandis)
            {

                string brandname = Brandii2.GetAttribute("innerHTML");

                Console.WriteLine(brandname);

                Brand branda = new Brand();
                branda.Name = brandname;
                branda.State = true;
                branda.Source = 4;//adadakiler

                using (var context = new ProductContext())
                {
                    context.Brands.AddRange(branda);

                    context.SaveChanges();
                }

            }
        }
    }

}

