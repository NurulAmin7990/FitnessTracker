using System;
using System.Collections.Generic;

namespace FitnessTracker.BusinessLogic.Models
{
    public partial class BodyPart
    {
        public BodyPart()
        {
            Workouts = new HashSet<Workout>();
        }

        public int Id { get; set; }
        public string BodyPart1 { get; set; } = null!;

        public virtual ICollection<Workout> Workouts { get; set; }
    }
}
