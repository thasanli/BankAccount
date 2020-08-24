using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BankAccounts.Models
{
    public class LoginUser
    {
        [Required(ErrorMessage="*** Email is required to Log in ***")]
        [EmailAddress(ErrorMessage = "*** Enter a valid Email address ***")]
        public string LoginEmail{get;set;}
        

        [Required(ErrorMessage="*** Password is required to Log in ***")]
        [MinLength(8, ErrorMessage = "*** Password must contain at least 8 characters ***")]
        [DataType(DataType.Password, ErrorMessage = "*** Invalid Password ***")]
        public string LoginPassword{get;set;}
    }
}