using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace adadakiler.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int BrandID { get; set; }
        public Brand Brand { get; set; }
        public Unit Unit { get; set; }
        public int UnitID { get; set; }
        public string Barcode { get; set; }
        public string MaterialCode { get; set; }
        public string SKU { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool State { get; set; }
        public string Code { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }
        public ICollection<File> Files { get; set; }
        public string Address { get; set; }
        public int Source { get; set; }
        public decimal Price { get; set; }
        public string ShopPHPID { get; set; }
        public string ShopPHPName { get; set; }
        public bool ShopPHPState { get; set; }

    }
}
