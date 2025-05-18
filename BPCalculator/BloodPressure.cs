using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace BPCalculator
{
    // BP categories
    public enum BPCategory
    {
        [Display(Name = "Low Blood Pressure")] Low,
        [Display(Name = "Ideal Blood Pressure")] Ideal,
        [Display(Name = "Pre-High Blood Pressure")] PreHigh,
        [Display(Name = "High Blood Pressure")] High
    };

    public class BloodPressure
    {
        public const int SystolicMin = 70;
        public const int SystolicMax = 190;
        public const int DiastolicMin = 40;
        public const int DiastolicMax = 100;

        [Range(SystolicMin, SystolicMax, ErrorMessage = "Invalid Systolic Value")]
        public int Systolic { get; set; }                       // mmHG

        [Range(DiastolicMin, DiastolicMax, ErrorMessage = "Invalid Diastolic Value")]
        public int Diastolic { get; set; }                      // mmHG

        // calculate BP category
        public BPCategory Category
        {
            get
            {
                BPCategory category = BPCategory.Ideal; // Default category

                // High BP takes precedence if either value is in the high range
                if (Systolic >= 140 || Diastolic >= 90)
                {
                    category = BPCategory.High;
                }
                // Low BP takes precedence if both values are in the low range
                else if (Systolic < 90 && Diastolic < 60)
                {
                    category = BPCategory.Low;
                }
                // Pre-High BP if systolic or diastolic falls into the pre-high range
                else if ((Systolic >= 121 && Systolic <= 139) || (Diastolic >= 81 && Diastolic <= 89))
                {
                    category = BPCategory.PreHigh;
                }
                // Ideal BP if systolic and diastolic values are within the ideal range
                else if (Systolic >= 90 && Systolic <= 120 && Diastolic >= 60 && Diastolic <= 80)
                {
                    category = BPCategory.Ideal;
                }

                return category;

            }
        }
    }


}
