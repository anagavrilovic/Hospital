
using System;
using System.ComponentModel.DataAnnotations;

public enum Genders
{
    [Display(Name = "mu�ko")] male,
    [Display(Name = "�ensko")] female,
    [Display(Name = "ostalo")] other
}