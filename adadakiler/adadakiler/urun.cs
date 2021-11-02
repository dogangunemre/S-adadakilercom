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
    public class urun
    {
        public void urun_added(string proURL)
        {
            //var options = new ChromeOptions();
            //options.AddArguments("headless");
            //IWebDriver driver = new ChromeDriver(options);

            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(proURL);
            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("Siteye Gidildi!");
            Thread.Sleep(2000);
            var contex = new ProductContext();

            List<Product> pro = new List<Product>();
            Product Producta = new Product();

            try
            {


                //Barcode
                try
                {
                    IWebElement BarcodePath = driver.FindElement(By.XPath("//*[@id='product-detail-container']/div[2]/div/div[2]/div/div[4]/div[3]/div[2]"));
                    string BarcodePathi = BarcodePath.Text;
                    Producta.Barcode = BarcodePathi;
                }
                catch (Exception)
                {

                    IWebElement BarcodePath2 = driver.FindElement(By.XPath("//*[@id='product-detail-container']/div[2]/div/div[2]/div/div[3]/div[2]/div[2]"));
                    string BarcodePathi2 = BarcodePath2.Text;
                    Producta.Barcode = BarcodePathi2;
                }
                

                Product oprobul = contex.Products.FirstOrDefault(o => o.Barcode == Producta.Barcode);
                //Brand

                if (oprobul == null)
                {

                    try
                    {
                        IWebElement brandPath = driver.FindElement(By.ClassName("product-brand"));

                        string brandbul = brandPath.FindElement(By.TagName("A")).GetAttribute("innerHTML");
                        Brand brandbul2 = contex.Brands.FirstOrDefault(o => o.Name == brandbul);
                        if (brandbul2 != null)
                        {
                            Producta.BrandID = brandbul2.ID;

                        }
                    }
                    catch (Exception)
                    {

                        Producta.BrandID = 948;
                    }




                    //Unit
                    bool bbunit = true;
                    try
                    {
                        driver.FindElement(By.ClassName("product-qty-wrapper"));
                    }
                    catch (Exception)
                    {

                        bbunit = false;
                    }
                    if (bbunit == true)
                    {


                        IWebElement UnitPath2 = driver.FindElement(By.ClassName("product-qty-wrapper"));
                        IWebElement UnitPath = UnitPath2.FindElement(By.XPath("div"));

                        string Unitbul = UnitPath.Text;
                        Unit Unitbul2 = contex.Unit.FirstOrDefault(o => o.Name == Unitbul);
                        if (Unitbul2 != null)
                        {
                            Producta.UnitID = Unitbul2.ID;

                        }
                        else
                        {
                            Unit unita = new Unit();
                            unita.Name = Unitbul;
                            unita.State = true;
                            unita.Source = 4;
                            unita.Description = Unitbul;
                            unita.Code = Unitbul;
                            using (var context = new ProductContext())
                            {
                                context.Unit.AddRange(unita);

                                context.SaveChanges();
                            }
                            Producta.UnitID = unita.ID;

                        }



                    }
                    else
                    {
                        Producta.UnitID =1;
                    }


                    //Name
                    IWebElement NamePath = driver.FindElement(By.ClassName("product-title"));
                    string NamePathi = NamePath.FindElement(By.TagName("h1")).GetAttribute("innerHTML");
                    string value1 = NamePathi;
                    string value2 = value1.Replace("&amp;", "&");
                    NamePathi = value2;
                    Producta.Name = NamePathi;


                    //State
                    Producta.State = true;

                    //Category
                    IWebElement CategoryPath = driver.FindElement(By.XPath("//*[@id='breadcrumbs']/ol/li[last()-1]"));

                    string katURLbul = CategoryPath.FindElement(By.TagName("A")).GetAttribute("href");

                    Regex r4 = new Regex(@".*\/(?<katCode>.*$)");

                    string CatgoryCode = null;
                    if (r4.Match(katURLbul).Success)
                    {
                        CatgoryCode = r4.Match(katURLbul).Groups["katCode"].Value;
                    }

                    Category bul = contex.Categories.FirstOrDefault(o => o.Description == CatgoryCode);
                    if (bul != null)
                    {
                        Producta.CategoryID = bul.ID;
                        Console.WriteLine(CatgoryCode);
                    }
                    else
                    {
                        Producta.CategoryID = 460;

                    }

                    //Address 
                    Producta.Address = proURL;

                    //Source 
                    Producta.Source = 4;//adadakiler

                    //Price
                    IWebElement PricePath = driver.FindElement(By.ClassName("product-price-old"));
                    string PricePathi = PricePath.Text;
                    string value11 = PricePathi;
                    string value21 = value11.Replace(" TL", "");
                    PricePathi = value21;
                    Producta.Price = Convert.ToDecimal(PricePathi);


                    //Code
                    Regex r5 = new Regex(@".*\/(?<katCode>.*$)");

                    string productCode = null;
                    if (r5.Match(proURL).Success)
                    {
                        productCode = r5.Match(proURL).Groups["katCode"].Value;
                    }
                    Producta.Code = productCode;


                    //File
                    IWebElement File2Path = driver.FindElement(By.XPath("//*[@id='product-thumb-image']/div/div/div/div/a/img"));
                    string File2Pathi = File2Path.GetAttribute("SRC");

                    Regex r6 = new Regex(@".*\/(?<katCode>.*$)");


                    using (var context = new ProductContext())
                    {
                        context.Products.AddRange(Producta);
                        context.SaveChanges();
                    }

                    File file = new File();
                    file.Path = File2Pathi;
                    file.State = true;
                    file.RelativePath = productCode + ".jpg";
                    file.ProductID = Producta.ID;

                    System.Net.WebClient wc = new System.Net.WebClient();
                    wc.DownloadFile(File2Pathi, String.Concat(@"C:\Users\Emre\source\repos\adadakiler\adadakiler\images\", productCode, ".jpg"));


                    using (var context = new ProductContext())
                    {
                        context.Files.Add(file);
                        context.SaveChanges();
                    }
                    driver.Close();

                }
                else
                {
                    Console.WriteLine("Ürün daha önce eklenmiş");
                    driver.Close();

                }
            }
            catch (Exception ex)
            {


                throw;
            }
            }

        }
    }
