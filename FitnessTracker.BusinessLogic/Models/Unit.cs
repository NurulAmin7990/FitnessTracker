using System;
using System.Collections.Generic;

namespace FitnessTracker.BusinessLogic.Models
{
    public partial class Unit
    {
        public Unit()
        {
            Workouts = new HashSet<Workout>();
        }

        public int Id { get; set; }
        public string UnitType { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<Workout> Workouts { get; set; }
    }
}
