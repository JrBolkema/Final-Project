using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project
{
	class Program
	{
		static void Main(string[] args)
		{
			Authenticate();
			bool menu = Menu();
			do
			{
				
			} while (menu);
		}

		public static User Authenticate()
		{
			Console.WriteLine("Enter Your Username");
			string Username = Console.ReadLine();
			Console.WriteLine("Enter Your Password");
			string Password = Console.ReadLine();
			User currentUser = new User(Username,Password);
			return currentUser;
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
