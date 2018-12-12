using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project
{
	public class Exercise
	{
		public string ExerciseName { get; set; }
		public string MuscleGroup { get; set; }
		public int Sets { get; set; }
		public int Reps { get; set; }
		public int Weight { get; set; }
		public Exercise(string ExerciseName,string MuscleGroup,int Sets,int Reps, int Weight)
		{
			this.ExerciseName = ExerciseName;
			this.MuscleGroup = MuscleGroup;
			this.Sets = Sets;
			this.Reps = Reps;
			this.Weight = Weight;
		}

		// Here because XML serializer requires a parameterless constructor
		public Exercise()
		{

		}

		public string ShowExercise()
		{
			return $"{this.ExerciseName} ({MuscleGroup}){Environment.NewLine} {Sets}x{Reps} of {Weight}";
		}
	}
}
