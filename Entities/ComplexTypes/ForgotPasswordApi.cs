using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.ComplexTypes
{
   public class ForgotPasswordApi
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
    }
}
