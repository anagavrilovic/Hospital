using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hospital
{
    public class Allergen
    {
       private string other;
       private List<string> medicineNames = new List<string>();
       private List<string> ingredientNames = new List<string>();

        public string Other
        {
            get { return other; }
            set { other = value; }
        }

        public List<string> MedicineNames
        {
            get { return medicineNames; }
            set { medicineNames = value; }
        }
        public List<string> IngredientNames
        {
            get { return ingredientNames; }
            set { ingredientNames = value; }
        }


    }
}
