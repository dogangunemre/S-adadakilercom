using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace adadakiler.Models
{
    public class Brand
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool State { get; set; }
        public string Address { get; set; }
        public int Source { get; set; }
        public string ShopPHPID { get; set; }
        public string ShopPHPName { get; set; }
        public bool ShopPHPState { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
