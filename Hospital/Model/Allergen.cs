// File:    Allergen.cs
// Author:  Ana Gavrilovic
// Created: Monday, April 5, 2021 7:28:17 PM
// Purpose: Definition of Class Allergen

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hospital
{
    public class Allergen
    {
       private string other;
       private ObservableCollection<string> medicineNames = new ObservableCollection<string>();
       private ObservableCollection<string> ingredientNames = new ObservableCollection<string>();

        public string Other
        {
            get { return other; }
            set { other = value; }
        }

        public ObservableCollection<string> MedicineNames
        {
            get { return medicineNames; }
            set { medicineNames = value; }
        }
        public ObservableCollection<string> IngredientNames
        {
            get { return ingredientNames; }
            set { ingredientNames = value; }
        }


    }
}
