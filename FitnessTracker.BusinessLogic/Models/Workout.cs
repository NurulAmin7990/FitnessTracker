using System;
using System.Collections.Generic;

namespace FitnessTracker.BusinessLogic.Models
{
    public partial class Workout
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public int ExerciseId { get; set; }
        public int BodyPartId { get; set; }
        public int? Sets { get; set; }
        public string? Reps { get; set; }
        public decimal? Weights { get; set; }
        public byte[]? TimeLength { get; set; }
        public int? UnitId { get; set; }
        public string? Notes { get; set; }

        public virtual BodyPart BodyPart { get; set; } = null!;
        public virtual Exercise Exercise { get; set; } = null!;
        public virtual Unit? Unit { get; set; }
    }
}
