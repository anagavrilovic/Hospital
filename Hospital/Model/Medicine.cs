// File:    Medicine.cs
// Author:  Ana Gavrilovic
// Created: Monday, April 5, 2021 6:57:36 PM
// Purpose: Definition of Class Medicine

using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Hospital
{
    public class Medicine : INotifyPropertyChanged
    {
       private string id;
        public string ID
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged("ID");
            }
        }
        private int durationInDays;
        [JsonIgnore]
        public int DurationInDays
        {
            get => durationInDays;
            set
            {
                durationInDays = value;
                OnPropertyChanged("durationInDays");
            }
        }
        private int timesPerDay;
        [JsonIgnore]
        public int TimesPerDay
        {
            get => timesPerDay;
            set
            {
                timesPerDay = value;
                OnPropertyChanged("timesPerDay");
            }
        }
        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
      
        private double dosageInMg;
        public double DosageInMg
        {
            get => dosageInMg;
            set
            {
                dosageInMg = value;
                OnPropertyChanged("DosageInMg");
            }
        }

        private int quantity;
        public int Quantity
        {
            get => quantity;
            set
            {
                quantity = value;
                OnPropertyChanged("Quuantity");
            }
        }

        private double price;
        public double Price
        {
            get => price;
            set
            {
                price = value;
                OnPropertyChanged("Price");
            }
        }
       
        private string description;
        [JsonIgnore]
        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public override string ToString()
        {
            return Name + " " + ID;
        }
        private System.Collections.Generic.List<Ingredient> ingredient;


        /// <summary>
        /// Property for collection of Ingredient
        /// </summary>
        /// <pdGenerated>Default opposite class collection property</pdGenerated>
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

                OnPropertyChanged("Ingredient");
            }
        }

        /// <summary>
        /// Add a new Ingredient in the collection
        /// </summary>
        /// <pdGenerated>Default Add</pdGenerated>
        public void AddIngredient(Ingredient newIngredient)
        {
            if (newIngredient == null)
                return;
            if (this.ingredient == null)
                this.ingredient = new System.Collections.Generic.List<Ingredient>();
            if (!this.ingredient.Contains(newIngredient))
                this.ingredient.Add(newIngredient);
        }

        /// <summary>
        /// Remove an existing Ingredient from the collection
        /// </summary>
        /// <pdGenerated>Default Remove</pdGenerated>
        public void RemoveIngredient(Ingredient oldIngredient)
        {
            if (oldIngredient == null)
                return;
            if (this.ingredient != null)
                if (this.ingredient.Contains(oldIngredient))
                    this.ingredient.Remove(oldIngredient);
        }

        /// <summary>
        /// Remove all instances of Ingredient from the collection
        /// </summary>
        /// <pdGenerated>Default removeAll</pdGenerated>
        public void RemoveAllIngredient()
        {
            if (ingredient != null)
                ingredient.Clear();
        }

        private System.Collections.Generic.List<string> replacementMedicineIDs;


        /// <summary>
        /// Property for collection of Ingredient
        /// </summary>
        /// <pdGenerated>Default opposite class collection property</pdGenerated>
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

        /// <summary>
        /// Add a new Ingredient in the collection
        /// </summary>
        /// <pdGenerated>Default Add</pdGenerated>
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
    class PretragaM : ObservableCollection<Medicine> { }
}

