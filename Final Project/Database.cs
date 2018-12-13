using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project
{
	class Database
	{
		public SqlConnection Connection { get; set; }

		public Database(SqlConnection connection)
		{
			this.Connection = connection;
		}

		public Database()
		{


		}

		public void AddUserToDatabase(User user)
		{
			string insertQuery = @"INSERT INTO Users
												([Username]
												,[Password])
											VALUES
												(@username
												,@password);";
			using (SqlCommand cmd = new SqlCommand(insertQuery, this.Connection))
			{
				cmd.Parameters.AddWithValue("@username", user.Username);
				cmd.Parameters.AddWithValue("@password", user.Password);
				cmd.ExecuteNonQuery();
				//this.Connection.Close();
			};
		}

		public void AddWorkoutToDatabase(Workout workout,User user)
		{
			string insertQuery = @"INSERT INTO Workouts
												([UserID]
												,[WorkoutName]
												,[Sets]
												,[Reps]
												,[Weight])
											VALUES
												(@UserID
												,@WorkoutName
												,@Sets
												,@Reps
												,@Weight);";
			using (SqlCommand cmd = new SqlCommand(insertQuery, this.Connection))
			{
				//this.Connection.Open();
				int UserID = SelectUserFromDatabase(user);

				foreach (Exercise exercise in workout.Exercises)
				{
					cmd.Parameters.Clear();
					cmd.Parameters.AddWithValue("@UserID", UserID);
					cmd.Parameters.AddWithValue("@WorkoutName", workout.WorkoutName);

					cmd.Parameters.AddWithValue("@Sets", exercise.Sets);
					cmd.Parameters.AddWithValue("@Reps",exercise.Reps);
					cmd.Parameters.AddWithValue("@Weight",exercise.Weight);
					
					cmd.ExecuteNonQuery();
					System.Threading.Thread.Sleep(5);


				}
			};
		}

		public int SelectUserFromDatabase(User user)
		{
			
			string insertQuery = $"SELECT UserID FROM Users WHERE Users.Username = '{user.Username}'";

			using (SqlCommand cmd = new SqlCommand(insertQuery, Connection))
			{
				int returnValue = 1;
				//Connection.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				//cmd.Parameters.AddWithValue("@Username", user.Username);

				if (reader.HasRows)
				{
					while (reader.Read())
					{

						returnValue = (int)reader[0];
						
					}

				}
				reader.Close();
				return returnValue;

			}


		}

		
	}
}		
