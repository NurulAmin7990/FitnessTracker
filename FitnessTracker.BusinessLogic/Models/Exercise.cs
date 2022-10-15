using System;
using System.Collections.Generic;

namespace FitnessTracker.BusinessLogic.Models
{
    public partial class Exercise
    {
        public Exercise()
        {
            Workouts = new HashSet<Workout>();
        }

        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string ExerciseName { get; set; } = null!;
        public bool? IsActive { get; set; }

        public virtual ICollection<Workout> Workouts { get; set; }
    }
}
