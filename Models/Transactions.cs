using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BankAccounts.Models
{
    public class Transactions
    {
        [Key]
        public int TransactionId{get;set;}
        public float Amount {get;set;}
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int UserId {get;set;}
        public User Creator {get;set;}
        
    }
}