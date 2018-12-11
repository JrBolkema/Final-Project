using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Final_Project
{
//use hashing the password to make cryptography work
	class Program
	{
		static void Main(string[] args)
		{



			
			// Obtaining a file path
			// For storing and retrieving user information
			string filePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
			string formattedFilePath = $"{filePath}\\User Info.xml";

			// Verifying file path is correct
			// Problems could arise when different computers are used
			Console.WriteLine(formattedFilePath);
			Console.WriteLine(@"C:\Users\JrBol\source\repos\Final Project\Final Project\User Info.xml");

			
			// Deserializing the users
			var myXml = new MyXMLSerializer();
			List<User> users = myXml.Deserialize<List<User>>(formattedFilePath);

			//Logging in a user and making sure they are valid
			Console.WriteLine("Enter Your Username");
			string Username = Console.ReadLine();
			Console.WriteLine("Enter Your Password");
			string Password = Console.ReadLine();

			User CurrentUser = Authenticate(users,Username,Password);

			//  TODO make it so it is a bool and if its true then they can log in 
			// then just log them in
			// if false do another option to make a new account
			
			
			
			bool menu = Menu();
			do
			{
				
			} while (menu);


		}

		public static bool Authenticate(List<User> users,string Username,string Password)
		{
			foreach (User user in users)
			{

				if (user.Username == Username && user.Password == Password)
				{
					return true;
				}
			}

			while (CreateNewAccountChoice())
			{
				User user = CreateNewAccount();
				return user;
			}
			User returnUser = new User("Not", "Working");
			return returnUser;

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

		public static bool Menu()
		{
			Console.WriteLine("Would you like to:{0}1.)Log a New Workout{0}2.)View previous workouts{0}3.)Create new workout{0}4.)Quit",Environment.NewLine);
			string choice = Console.ReadLine();
			// some regex stuff
			// convert choice to int
			int intChoice = Convert.ToInt32(choice);
			if (intChoice == 1)
			{
				return true;
			}
			else if(intChoice == 2)
			{
				return true;
			}
			else if (intChoice == 3)
			{
				return true;
			}
			else if (intChoice == 4)
			{
				return false;
			}
			else
			{
				Console.WriteLine("dont see this");
				return false;
			}


		}
	}
}
