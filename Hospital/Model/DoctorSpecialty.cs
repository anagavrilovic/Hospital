// File:    DoctorSpecialty.cs
// Author:  Ana Gavrilovic
// Created: Monday, April 5, 2021 5:52:17 PM
// Purpose: Definition of Enum DoctorSpecialty

using System;
using System.ComponentModel.DataAnnotations;

namespace Hospital
{
    public enum DoctorSpecialty
    {
        general,
        cardiologist,
        dermatologist,
        neurologist,
        cardiosurgeon,
        neurosurgeon,
        generalsurgeon,
        anesthesiologist,
        orthopedist,
        pediatrician,
        gynecologist,
        urologist,
        psyciatrist,
        none
    }
}

