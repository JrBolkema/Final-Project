using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project
{
	class Exercise
	{
		public string MuscleGroup { get; set; }
		public int Sets { get; set; }
		public int Reps { get; set; }
		public int Weight { get; set; }
		public Exercise(string MuscleGroup,int Sets,int Reps, int Weight)
		{
			this.MuscleGroup = MuscleGroup;
			this.Sets = Sets;
			this.Reps = Reps;
			this.Weight = Weight;
		}

	}
}
