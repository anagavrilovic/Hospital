// File:    Medicine.cs
// Author:  Ana Gavrilovic
// Created: Monday, April 5, 2021 6:57:36 PM
// Purpose: Definition of Class Medicine

using System;

namespace Hospital
{
    public class Medicine
    {
       public string id;
       public string name;
       public double dosageInMg;

        public System.Collections.Generic.List<Ingredient> ingredient;

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
    }
}

