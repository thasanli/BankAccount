using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace BankAccounts.Models
{
    public class User
    {

        [Key]
        public int UserID {get;set;}
        [Required(ErrorMessage="*** First Name is required ***")]
        [MinLength(2, ErrorMessage="*** First Name should contain at least 3 characters ***")]
        public string FirstName{get;set;}

        [Required(ErrorMessage="*** Last Name is required ***")]
        [MinLength(2, ErrorMessage="*** Last Name should contain at least 3 characters ***")]
        public string LastName{get;set;}

        [Required(ErrorMessage="*** Email is required ***")]
        [EmailAddress(ErrorMessage = "*** Enter a valid Email ***")]
        public string Email {get;set;}


        // [Required(ErrorMessage="*** Password is required ***")]
        // [MinLength(8, ErrorMessage = "*** Password must contain at least 8 characters ***")]
        // [DataType(DataType.Password, ErrorMessage = "*** Invalid Password ***")]
        // [PwdValidate]
        public string Password {get;set;}

        [NotMapped]
        // [Required(ErrorMessage="*** Password confirmation is required ***")]
        // [Compare("Password", ErrorMessage="*** Password doest match ***")]
        // [DataType(DataType.Password, ErrorMessage = "*** Invalid Password ***")]
        public string ConfirmPassword {get;set;}

        public float AccountBalace {get;set;}

       
        public DateTime CreatedAt {get;set;} = DateTime.Now;
       
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
        public List<Transactions> AllUserTransactions {get;set;}
    }


        public class PwdValidateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value != null)
        {
            Regex CompareWith = new Regex(@"^[a-zA-z]+[0-9]+[!@#$%&*]");
            if(!CompareWith.IsMatch((string)value))
            {
                return new ValidationResult("*** Password should contain 1 letter, 1 number, and 1 special character ***");
            }

        }
        return ValidationResult.Success;
    }
        
    }


}