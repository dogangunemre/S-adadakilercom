using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace adadakiler.Models
{
    public class ProductAddress
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Path { get; set; }
        public bool State { get; set; } = true;
        public int Source { get; set; }
        public string ShopPHPID { get; set; }
        public string ShopPHPName { get; set; }
        public bool ShopPHPState { get; set; }
    }
}
