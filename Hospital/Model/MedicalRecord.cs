// File:    MedicalRecord.cs
// Author:  ShowRunner
// Created: Tuesday, March 23, 2021 10:16:14 PM
// Purpose: Definition of Class MedicalRecord

using System;

public class MedicalRecord
{
   public String healthCardNumber;
   public String parentName;
   public Boolean isInsured;
   public int medicalRecordID;
   
   public System.Collections.Generic.List<Examination> examination;
   
   /// <summary>
   /// Property for collection of Examination
   /// </summary>
   /// <pdGenerated>Default opposite class collection property</pdGenerated>
   public System.Collections.Generic.List<Examination> Examination
   {
      get
      {
         if (examination == null)
            examination = new System.Collections.Generic.List<Examination>();
         return examination;
      }
      set
      {
         RemoveAllExamination();
         if (value != null)
         {
            foreach (Examination oExamination in value)
               AddExamination(oExamination);
         }
      }
   }
   
   /// <summary>
   /// Add a new Examination in the collection
   /// </summary>
   /// <pdGenerated>Default Add</pdGenerated>
   public void AddExamination(Examination newExamination)
   {
      if (newExamination == null)
         return;
      if (this.examination == null)
         this.examination = new System.Collections.Generic.List<Examination>();
      if (!this.examination.Contains(newExamination))
      {
         this.examination.Add(newExamination);
         newExamination.MedicalRecord = this;
      }
   }
   
   /// <summary>
   /// Remove an existing Examination from the collection
   /// </summary>
   /// <pdGenerated>Default Remove</pdGenerated>
   public void RemoveExamination(Examination oldExamination)
   {
      if (oldExamination == null)
         return;
      if (this.examination != null)
         if (this.examination.Contains(oldExamination))
         {
            this.examination.Remove(oldExamination);
            oldExamination.MedicalRecord = null;
         }
   }
   
   /// <summary>
   /// Remove all instances of Examination from the collection
   /// </summary>
   /// <pdGenerated>Default removeAll</pdGenerated>
   public void RemoveAllExamination()
   {
      if (examination != null)
      {
         System.Collections.ArrayList tmpExamination = new System.Collections.ArrayList();
         foreach (Examination oldExamination in examination)
            tmpExamination.Add(oldExamination);
         examination.Clear();
         foreach (Examination oldExamination in tmpExamination)
            oldExamination.MedicalRecord = null;
         tmpExamination.Clear();
      }
   }
   public Patient patient;

}