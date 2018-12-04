using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project
{
	public class Workout
	{
		public string MainMuscleGroup { get; set; }
		public List<Exercise> Exercises { get; set; }

		public Workout(string MainMuscleGroup)
		{
			this.MainMuscleGroup = MainMuscleGroup;
		}

		// Here because XML serializer requires a parameterless constructor
		public Workout()
		{
		}

		public void AddExercise(Exercise exercise)
		{
			Exercises.Add(exercise);
		}
	}
}
