using System;
using System.ComponentModel.DataAnnotations;
using SalesWebMVC.Models.Enums;

namespace SalesWebMVC.Models
{
    public class SalesRecord
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Sale Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        [Range(1.0, 1000000000.0, ErrorMessage = "{0} must be from {1} to {2}")]
        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Sale Amount")]
        [DisplayFormat(DataFormatString = "$ {0:F2}")]
        public double Amount { get; set; }

        [Range(1, 3, ErrorMessage = "{0} must be from {1} to {2}")]
        [Required(ErrorMessage = "{0} is required")]
        public SalesStatus Status { get; set; }
        
        public Seller Seller { get; set; }
        public int SellerId { get; set; }
    
        public SalesRecord()
        {

        }

        public SalesRecord(int id, DateTime date, double amount, SalesStatus status, Seller seller)
        {
            Id = id;
            Date = date;
            Amount = amount;
            Status = status;
            Seller = seller;
        }
    }
}
