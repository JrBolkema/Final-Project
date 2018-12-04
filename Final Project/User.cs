using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project
{
	class User
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public List<Workout> Workouts { get; set; }

		public User(string Username, string Password)
		{
			this.Username = Username;
			this.Password = Password;
		}
		public void AddWorkout(Workout workout)
		{
			Workouts.Add(workout);
		}
	}
}
