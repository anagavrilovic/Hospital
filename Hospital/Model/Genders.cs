
using System;
using System.ComponentModel.DataAnnotations;

public enum Genders
{
    [Display(Name = "muško")] male,
    [Display(Name = "žensko")] female,
    [Display(Name = "ostalo")] other
}