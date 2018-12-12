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
			this.Exercises = new List<Exercise>();

		}

		public Workout(string MainMuscleGroup, string Name)
		{
			this.MainMuscleGroup = MainMuscleGroup;
			this.WorkoutName = Name;
			this.Exercises = new List<Exercise>();

		}

		// Here because XML serializer requires a parameterless constructor
		public Workout()
		{
		}

		public void AddExercise(Exercise exercise)
		{
			Exercises.Add(exercise);
		}

		public void ShowWorkout()
		{
			int counter = 1;
			foreach (Exercise exercise in Exercises)
			{
				Console.WriteLine($"{counter}.) {exercise.ShowExercise()}");
				counter++;
			}

		}
	}
}
