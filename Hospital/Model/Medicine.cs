// File:    Medicine.cs
// Author:  Ana Gavrilovic
// Created: Monday, April 5, 2021 6:57:36 PM
// Purpose: Definition of Class Medicine

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

        private string description;
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

        private System.Collections.Generic.List<Medicine> medicines;


        /// <summary>
        /// Property for collection of Ingredient
        /// </summary>
        /// <pdGenerated>Default opposite class collection property</pdGenerated>
        public System.Collections.Generic.List<Medicine> Medicines
        {
            get
            {
                if (medicines == null)
                    medicines = new System.Collections.Generic.List<Medicine>();
                return medicines;
            }
            set
            {
                RemoveAllMedicine();
                if (value != null)
                {
                    foreach (Medicine oIngredient in value)
                        AddMedicine(oIngredient);
                }
            }
        }

        /// <summary>
        /// Add a new Ingredient in the collection
        /// </summary>
        /// <pdGenerated>Default Add</pdGenerated>
        public void AddMedicine(Medicine medicine)
        {
            if (medicine == null)
                return;
            if (this.medicines == null)
                this.medicines = new System.Collections.Generic.List<Medicine>();
            if (!this.medicines.Contains(medicine))
                this.medicines.Add(medicine);
        }

        /// <summary>
        /// Remove an existing Ingredient from the collection
        /// </summary>
        /// <pdGenerated>Default Remove</pdGenerated>
        public void RemoveMedicine(Medicine medicine)
        {
            if (medicine == null)
                return;
            if (this.medicines != null)
                if (this.medicines.Contains(medicine))
                    this.medicines.Remove(medicine);
        }

        /// <summary>
        /// Remove all instances of Ingredient from the collection
        /// </summary>
        /// <pdGenerated>Default removeAll</pdGenerated>
        public void RemoveAllMedicine()
        {
            if (ingredient != null)
                ingredient.Clear();
        }
    }
    class PretragaM : ObservableCollection<Medicine> { }
}

