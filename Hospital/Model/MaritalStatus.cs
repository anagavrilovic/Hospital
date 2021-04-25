using System;
using System.ComponentModel.DataAnnotations;

namespace Hospital
{
   public enum MaritalStatus
   {
        [Display(Name = "neoženjen - neudata")] neozenjen,
        [Display(Name = "oženjen - udata")] ozenjen,
        [Display(Name = "udovac - udovica")] udovac,
        [Display(Name = "razveden - razvedena")] razveden
   }
}