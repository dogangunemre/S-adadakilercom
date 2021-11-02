using System;
using adadakiler.Context;
using adadakiler.Models;
using System.Collections.Generic;
using System.Linq;

namespace adadakiler
{
    class Program
    {
        static void Main(string[] args)
        {

            Brands brands = new Brands();
            //brands.brand_added();
            PullCategoryChild pullCategoryChild = new PullCategoryChild();
            //pullCategoryChild.category_added();
            PullingProductAddressInCategory pullingProductAddressInCategory = new PullingProductAddressInCategory();
            //try
            //{
            //    using (var contex = new ProductContext())
            //    {
            //        List<Category> GetAllKategoriAddresses2 = contex.Categories.Where(s => s.State == false).ToList();
            //        foreach (var item in GetAllKategoriAddresses2)
            //        {

            //            pullingProductAddressInCategory.katpro(item.Address);
            //            item.State = true;
            //            contex.SaveChanges();

            //        }
            //    }
            //}
            //catch (Exception ex)
            //{

            //    throw ex;
            //}

            urun urun = new urun();

            try
            {
                using (var contex = new ProductContext())
                {
                    List<ProductAddress> GetAllKategoriAddresses2 = contex.ProductAddresses.Where(s => s.State == true).ToList();
                    foreach (var item in GetAllKategoriAddresses2)
                    {

                        urun.urun_added(item.Path);
                        item.State = false;
                        contex.SaveChanges();

                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
    }
}
