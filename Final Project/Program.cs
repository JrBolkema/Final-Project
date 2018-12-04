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

			//Logging in a user and making sure they are valid
			User CurrentUser = Authenticate();



			var myXml = new MyXMLSerializer();
			myXml.Serialize(formattedFilePath, CurrentUser);
			bool menu = Menu();
			do
			{
				
			} while (menu);
		}

		public static User Authenticate()
		{
			AesCryptoServiceProvider aesCSP = new AesCryptoServiceProvider();
			SymmetricEncryption Encryptor = new SymmetricEncryption();
			//aesCSP.GenerateKey();
			//aesCSP.GenerateIV();


			Console.WriteLine("Enter Your Username");
			string Username = Console.ReadLine();
			Console.WriteLine("Enter Your Password");
			string Password = Console.ReadLine();
			User currentUser = new User(Username,Password);

			byte[] cipher = Encryptor.EncryptData(aesCSP, Password);


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
