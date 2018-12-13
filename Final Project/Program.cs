using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Final_Project
{
	class Program
	{
		static void Main(string[] args)
		{
			Action<string> write = Console.WriteLine;



			// Obtaining a file path
			// For storing and retrieving user information
			string filePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
			string formattedFilePath = $"{filePath}\\User Info.xml";

			// Verifying file path is correct
			// Problems could arise when different computers are used
			write(formattedFilePath);
			write(@"C:\Users\JrBol\source\repos\Final Project\Final Project\User Info.xml");

			// Creating the database connection
			string m_ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = "
			+$"'{filePath}\\Final Project file.mdf';"
			+"Integrated Security = True; Connect Timeout = 90";
			SqlConnection myConnection = new SqlConnection(m_ConnectionString);
			
			//Testing the database stuff
			using (myConnection)
			{
				string insertQuery = @"SELECT * FROM Users";
				//string insertQuery = @"INSERT INTO Users
										//	([Username]
										//	,[Password])
										//VALUES
										//	(@test
										//	,@testpass);";
				using (SqlCommand cmd = new SqlCommand(insertQuery, myConnection))
				{
					myConnection.Open();
					//cmd.Parameters.AddWithValue("@test", "test");
					//cmd.Parameters.AddWithValue("@testpass", "TestPassword");
					//cmd.ExecuteNonQuery();

					SqlDataReader reader = cmd.ExecuteReader();
					if (reader.HasRows)
					{
						while (reader.Read())
						{
							Console.WriteLine($"{reader[0]} {reader[1]} {reader[2]}");
						}

					}
					reader.Close();
				}
			}
			
			
			// Deserializing the users
			var myXml = new MyXMLSerializer();
			List<User> users = myXml.Deserialize<List<User>>(formattedFilePath);

			//Logging in a user and making sure they are valid
			write("Enter Your Username");
			string Username = Console.ReadLine();
			write("Enter Your Password");
			string Password = Console.ReadLine();

			// Authenticating a user
			if (Authenticate(users, Username, Password))
			{
				// "Logging" them in
				User currentUser = AssignUser(users, Username, Password);
				Menu(currentUser);

			}
			else
			{
				// asking if they want to create a new account
				if (CreateNewAccountChoice())
				{
					// Create the account
					User currentUser = CreateNewAccount();
					users.Add(currentUser);
					Menu(currentUser);

				}
				else
				{
					User errorUser = new User("Err", "err");
					User currentUser = errorUser;
					Menu(currentUser);

				}
			}

			// TODO: append new user to the existing list if they dont already exist and serialize it
			// add regex

			// Saving/Reserializing the list
			myXml.Serialize(formattedFilePath, users);


		}

		public static User AssignUser (List<User> users, string Username, string Password)
		{
			foreach (User user in users)
			{
				if (user.Username == Username && user.Password == Password)
				{
					return user;
				}
			}
			User errorUser = new User("Err","err");
			return errorUser;
		}

		public static bool Authenticate(List<User> users,string Username,string Password)
		{
			Hashing hasher = new Hashing();
			
			foreach (User user in users)
				{

				
				if (user.Username == Username && hasher.GetHash(user.Password) == hasher.GetHash(Password))
				{
					return true;
				}
			}
			return false;
		}

		private static bool CreateNewAccountChoice()
		{
			Console.WriteLine("Account not found, Would you like to make a new acount? (y/n)");
			string choice = Console.ReadLine();
			if (choice == "y")
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public static User CreateNewAccount()
		{
			Console.WriteLine("*****Create New Account*****");

			Console.WriteLine("What is your username?");
			string newUsername = Console.ReadLine();
			Console.WriteLine("What is your new password");
			string newPassword = Console.ReadLine();

			User newUser = new User (newUsername, newPassword);
			return newUser;
		}
		public static Workout CreateNewWorkout()
		{
			Console.WriteLine("What is the name of your new workout?");
			string workoutName = Console.ReadLine();
			Console.WriteLine("What is the primary muscle group of this workout?");
			string muscleGroup = Console.ReadLine();
			Workout newWorkout = new Workout(muscleGroup, workoutName);

			while (CreateNewExerciseChoice())
			{
				newWorkout.AddExercise(CreateNewExercise());
			}


			return newWorkout;
		}

		public static bool CreateNewExerciseChoice()
		{
			Console.WriteLine("Would you like to add and exercise? (y/n)");
			string choice = Console.ReadLine();
			if (choice == "y")
			{
				return true;
			}
			else
			{
				return false;
			}

		}

		public static Exercise CreateNewExercise()
		{
			
			int intExerciseReps = 0;
			int intExerciseSets = 0;
			int intExerciseWeight = 0;
			Console.WriteLine("What is the name of this exercise?");
			string exerciseName = Console.ReadLine();
			Console.WriteLine("What is the muscle worked in the exercise");
			string exerciseMuscleGroup = Console.ReadLine();
			Console.WriteLine("How many sets did you do?");
			string exerciseSets = Console.ReadLine();
			Console.WriteLine("How many reps did you do?");
			string exerciseReps = Console.ReadLine();
			Console.WriteLine("How much weight did you do?");
			string exerciseWeight = Console.ReadLine();

			try
			{
				intExerciseReps = Convert.ToInt32(exerciseReps);
				intExerciseSets = Convert.ToInt32(exerciseSets);
				intExerciseWeight = Convert.ToInt32(exerciseWeight);

			}
			catch (Exception)
			{
				Console.WriteLine("ERROR");
				throw;
			}
			//finally
			//{
				//newExercise = CurrentExercise;
			//}
			Exercise newExercise = new Exercise(exerciseName,exerciseMuscleGroup, intExerciseSets, intExerciseReps, intExerciseWeight);

			return newExercise;


		}
		public static void Menu(User currentUser)
		{
			Console.WriteLine("Would you like to:{0}1.)Log a New Workout{0}2.)View previous workouts{0}3.)Create new workout{0}4.)Quit",Environment.NewLine);
			string choice = Console.ReadLine();
			// some regex stuff
			// convert choice to int
			int intChoice = Convert.ToInt32(choice);
			if (intChoice == 1)
			{
				currentUser.ShowWorkouts();
			}
			else if(intChoice == 2)
			{
			
			}
			else if (intChoice == 3)
			{
				Workout createdWorkout = CreateNewWorkout();
				currentUser.AddWorkout(createdWorkout);
			}
			else if (intChoice == 4)
			{
			
			}
			else
			{
				Console.WriteLine("dont see this");
			}


		}
	}
}
