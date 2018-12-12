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
		public string WorkoutName { get; set; }
		public List<Exercise> Exercises { get; set; }

		public Workout(string MainMuscleGroup)
		{
			this.MainMuscleGroup = MainMuscleGroup;
			this.WorkoutName = "Unnamed Workout";
		}

		public Workout(string MainMuscleGroup, string Name)
		{
			this.MainMuscleGroup = MainMuscleGroup;
			this.WorkoutName = Name;

		}

		// Here because XML serializer requires a parameterless constructor
		public Workout()
		{
		}

		public void AddExercise(Exercise exercise)
		{
			Console.WriteLine(exercise);
			this.Exercises.Add(exercise);
		}
	}
}
