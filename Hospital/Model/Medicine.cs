using Newtonsoft.Json;
using System.ComponentModel;

namespace Hospital
{
    public class Medicine
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public double DosageInMg { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }
       
        [JsonIgnore]
        public string Description { get; set; }

        public override string ToString()
        {
            return Name + " " + ID;
        }
        private System.Collections.Generic.List<Ingredient> ingredient;

        public System.Collections.Generic.List<Ingredient> Ingredient
        {
            get
            {
                if (ingredient == null)
                    ingredient = new System.Collections.Generic.List<Ingredient>();
                return ingredient;
            }
            set
            {
                RemoveAllIngredient();
                if (value != null)
                {
                    foreach (Ingredient oIngredient in value)
                        AddIngredient(oIngredient);
                }
            }
        }

        public void AddIngredient(Ingredient newIngredient)
        {
            if (newIngredient == null)
                return;
            if (this.ingredient == null)
                this.ingredient = new System.Collections.Generic.List<Ingredient>();
            if (!this.ingredient.Contains(newIngredient))
                this.ingredient.Add(newIngredient);
        }

        public void RemoveIngredient(Ingredient oldIngredient)
        {
            if (oldIngredient == null)
                return;
            if (this.ingredient != null)
                if (this.ingredient.Contains(oldIngredient))
                    this.ingredient.Remove(oldIngredient);
        }

        public void RemoveAllIngredient()
        {
            if (ingredient != null)
                ingredient.Clear();
        }

        private System.Collections.Generic.List<string> replacementMedicineIDs;

        public System.Collections.Generic.List<string> ReplacementMedicineIDs
        {
            get
            {
                if (replacementMedicineIDs == null)
                    replacementMedicineIDs = new System.Collections.Generic.List<string>();
                return replacementMedicineIDs;
            }
            set
            {
                RemoveAllMedicineID();
                if (value != null)
                {
                    foreach (string id in value)
                        AddMedicineID(id);
                }
            }
        }

        public void AddMedicineID(string medicineID)
        {
            if (medicineID == null)
                return;
            if (this.ReplacementMedicineIDs == null)
                this.ReplacementMedicineIDs = new System.Collections.Generic.List<string>();
            if (!this.ReplacementMedicineIDs.Contains(medicineID))
                this.ReplacementMedicineIDs.Add(medicineID);
        }

        public void RemoveMedicineID(string medicineID)
        {
            if (medicineID == null)
                return;
            if (this.ReplacementMedicineIDs != null)
                if (this.ReplacementMedicineIDs.Contains(medicineID))
                    this.ReplacementMedicineIDs.Remove(medicineID);
        }

        public void RemoveAllMedicineID()
        {
            if (ReplacementMedicineIDs != null)
                ReplacementMedicineIDs.Clear();
        }
    }
}

