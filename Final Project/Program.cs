using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Final_Project
{
	class Program
	{
		static void Main(string[] args)
		{
			//DELEGATE
			Action<string> write = Console.WriteLine;

			//THREADED CLOCK	
			var startTimeSpan = TimeSpan.Zero;
			var periodTimeSpan = TimeSpan.FromMinutes(.25);
			var timer = new System.Threading.Timer((e) =>
			{
				string Text = DateTime.Now.ToString();
				write(Text);
			}, null, startTimeSpan, periodTimeSpan);

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
			+ $"'{filePath}\\Final Project file.mdf';"
			+ "Integrated Security = True; Connect Timeout = 90";
			SqlConnection myConnection = new SqlConnection(m_ConnectionString);
			myConnection.Open();


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
				Menu(currentUser, myConnection);

			}
			else
			{
				// asking if they want to create a new account
				if (CreateNewAccountChoice())
				{
					// Create the account
					User currentUser = CreateNewAccount();
					users.Add(currentUser);
					Database database = new Database(myConnection);
					database.AddUserToDatabase(currentUser);

					Menu(currentUser, myConnection);

				}
				else
				{
					User errorUser = new User("Err", "err");
					User currentUser = errorUser;
					Menu(currentUser, myConnection);

				}
			}			

			// Saving/Reserializing the list
			myXml.Serialize(formattedFilePath, users);
			//Closing the connection
			myConnection.Close();


		}
		/// <summary>
		/// Will return a user from the list
		/// </summary>
		/// <param name="users">List of known Users</param>
		/// <param name="Username"></param>
		/// <param name="Password"></param>
		/// <returns>User</returns>
		public static User AssignUser(List<User> users, string Username, string Password)
		{
			foreach (User user in users)
			{
				if (user.Username == Username && user.Password == Password)
				{
					return user;
				}
			}
			User errorUser = new User("Err", "err");
			return errorUser;
		}

		/// <summary>
		/// Will verify a given username and password against all known 
		/// usernames an passwords
		/// </summary>
		/// <param name="users">List of all known users</param>
		/// <param name="Username"></param>
		/// <param name="Password"></param>
		/// <returns></returns>
		public static bool Authenticate(List<User> users, string Username, string Password)
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

		/// <summary>
		/// Prompts the user to determine if they wish to create a new account
		/// </summary>
		/// <returns></returns>
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

		/// <summary>
		/// Presents user with series of prompts to create
		/// a User object
		/// </summary>
		/// <returns></returns>
		public static User CreateNewAccount()
		{
			Console.WriteLine("*****Create New Account*****");

			Console.WriteLine("What is your username?");
			string newUsername = Console.ReadLine();
			Console.WriteLine("What is your new password");
			string newPassword = Console.ReadLine();

			User newUser = new User(newUsername, newPassword);
			return newUser;
		}

		/// <summary>
		/// presents the use with a series of prompts to create
		/// and return a workout object
		/// also gives user option to add Exercises
		/// </summary>
		/// <returns></returns>
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

		/// <summary>
		/// asks the user if they wish to create an exercise
		/// returns true if yes false if no
		/// </summary>
		/// <returns></returns>
		public static bool CreateNewExerciseChoice()
		{
			Console.WriteLine("Would you like to add an exercise? (y/n)");
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

		/// <summary>
		/// Presents user with series of prompts in order to create and
		/// return an exercise object 
		/// </summary>
		/// <returns></returns>
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
			Exercise newExercise = new Exercise(exerciseName, exerciseMuscleGroup, intExerciseSets, intExerciseReps, intExerciseWeight);

			return newExercise;


		}

		/// <summary>
		/// Will present user with some either logging a workout or
		/// making a new one and manipulates the database approprately
		/// </summary>
		/// <param name="currentUser">given user after authentication</param>
		/// <param name="connection">database connection to local database</param>
		public static void Menu(User currentUser, SqlConnection connection)
		{
			Database database = new Database(connection);

			Console.WriteLine("Would you like to:{0}1.)Log a New Workout{0}2.)Create new workout{0}3.)Quit", Environment.NewLine);
			string choice = Console.ReadLine();
			int intChoice = Convert.ToInt32(choice);

			if (intChoice == 1)
			{
				currentUser.ShowWorkouts();

				//Choosing the workout
				Console.WriteLine("Which workout would you like to log?");
				string logChoice = Console.ReadLine();

				//REGEX
				Regex regex = new Regex(@"^\d+$/");
				Match match = regex.Match(logChoice);
				if (match.Success)
				{
					Environment.Exit(1);
				}
				int intLogChoice = Convert.ToInt32(logChoice);
				int counter = 1;

				foreach (Workout workout in currentUser.Workouts)
				{
					if (intLogChoice == counter)
					{
						database.AddWorkoutToDatabase(workout, currentUser);
					}
					counter++;
				}
			}
			else if (intChoice == 2)
			{
				Workout createdWorkout = CreateNewWorkout();
				currentUser.AddWorkout(createdWorkout);
				database.AddWorkoutToDatabase(createdWorkout, currentUser);
			}
			else
			{

			}

			}
		}
	}

