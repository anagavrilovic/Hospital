using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
    public enum BloodType
    {
        [Display(Name = "A+")] Aplus,
        [Display(Name = "A-")] Aneg,
        [Display(Name = "B+")] Bplus,
        [Display(Name = "B-")] Bneg,
        [Display(Name = "AB+")] ABplus,
        [Display(Name = "AB-")] ABneg,
        [Display(Name = "O+")] Oplus,
        [Display(Name = "O-")] Oneg
    }
}
