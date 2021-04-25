using System;
using System.ComponentModel.DataAnnotations;

namespace Hospital
{
   public enum MaritalStatus
   {
        [Display(Name = "neo�enjen - neudata")] neozenjen,
        [Display(Name = "o�enjen - udata")] ozenjen,
        [Display(Name = "udovac - udovica")] udovac,
        [Display(Name = "razveden - razvedena")] razveden
   }
}