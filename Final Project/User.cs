using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project
{
	public class User
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public List<Workout> Workouts { get; set; }

		public User(string Username, string Password)
		{
			this.Username = Username;
			this.Password = Password;
			this.Workouts = new List<Workout>();
		}
		// Here because XML serializer requires a parameterless constructor	
		private User()
		{
		}
		public void AddWorkout(Workout workout)
		{
			this.Workouts.Add(workout);
		}
		public void ShowWorkouts()
		{
			foreach (Workout workout in Workouts)
			{
				workout.ShowWorkout();
			}
		}
	}
}
