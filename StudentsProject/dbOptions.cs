using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

class dbOptions
{

	public void ShowAll()
	{
		string connectionString = "Data Source=DESKTOP-IBERPBJ;Initial Catalog=TEST;Integrated Security=True";

		string move = "SELECT * FROM Students";

		using (SqlConnection conn = new SqlConnection(connectionString))
		{

			conn.Open();

			using (SqlCommand cmd = new SqlCommand(move, conn))
			{
				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						Console.WriteLine(reader["Students_NAME"] + " " + reader["Students_SURNAME"] + " " + reader["Students_COEF"] + " " + reader["Students_GRANT"]);

					}

				}



			}
		}



	}
	public void showNames()
	{
		string connectionString = "Data Source=DESKTOP-IBERPBJ;Initial Catalog=TEST;Integrated Security=True";

		string move = "SELECT Students_NAME, Students_SURNAME FROM Students";

		using (SqlConnection conn = new SqlConnection(connectionString))
		{

			conn.Open();

			using (SqlCommand cmd = new SqlCommand(move, conn))
			{
				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						Console.WriteLine(reader["Students_NAME"] + " " + reader["Students_SURNAME"]);

					}

				}



			}
		}




	}
		public void CalculateGrant(int coef)
		{
			string connectionString = "Data Source=DESKTOP-IBERPBJ;Initial Catalog=TEST;Integrated Security=True";


			string addCoef = "UPDATE Students SET Students_GRANT = @Grant WHERE Students_COEF = @Coef";

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				conn.Open();

				

				using(SqlCommand addCoefCmd = new SqlCommand(addCoef, conn))
				{
					
						int grantValue = GetGrantValue(coef);


						addCoefCmd.Parameters.Clear();
						addCoefCmd.Parameters.AddWithValue("@Grant", grantValue);
						addCoefCmd.Parameters.AddWithValue("Coef", coef);
						addCoefCmd.ExecuteNonQuery();
					
					
				}

			}
		}
	public void AddStudent(string studentName,string studentSurname,int Coef)
	{
		string connectionString = "Data Source=DESKTOP-IBERPBJ;Initial Catalog=TEST;Integrated Security=True";

		string move = "INSERT INTO Students (Students_NAME,Students_SURNAME,Students_COEF) VALUES (@Name,@Surname,@Coef)";

		using(SqlConnection conn = new SqlConnection(connectionString))
		{
			conn.Open();

			using(SqlCommand addStudent = new SqlCommand(move, conn))
			{
				if (checkValue(Coef))
				{

					addStudent.Parameters.AddWithValue("@Name", studentName);
					addStudent.Parameters.AddWithValue("@Surname", studentSurname);
					addStudent.Parameters.AddWithValue("@Coef", Coef);



					int rowsAffected = addStudent.ExecuteNonQuery();

					if (rowsAffected > 0)
					{
						Console.WriteLine("Student Added Succesfully");

						CalculateGrant(Coef);
					}
					else
					{
						Console.WriteLine("Error!");
					}
				}
				
			}
			


		}
	

	}
private int GetGrantValue(int coef)
	{
		if (coef >= 3000)
		{
			return 100;
		}
		if (coef >= 2500 && coef < 3000)
		{
			return 75;
		}
		if(coef>=2000 && coef < 2500)
		{
			return 50;
		}
		else
		{
			return 0;
		}




	}

	

private bool checkValue(int val)
	{
		if(val > 3500)
		{
			return false;
		}
		return true;
	}

public void FindStudent(string studentName,string studentSurname)
	{
		
		string connectionString = "Data Source=DESKTOP-IBERPBJ;Initial Catalog=TEST;Integrated Security=True";

		string move = "SELECT Students_ID, Students_NAME, Students_Surname FROM Students as Your_Student WHERE Students_NAME = @Name AND Students_SURNAME = @Surname";
		using(SqlConnection conn = new SqlConnection(connectionString)) {
			
			conn.Open();
			using(SqlCommand findStud = new SqlCommand(move, conn))
			{
				findStud.Parameters.AddWithValue("@Name", studentName);
				findStud.Parameters.AddWithValue("@Surname", studentSurname);

				using (SqlDataReader r = findStud.ExecuteReader()) {

					while (r.Read())
					{
						
						string SearchingName = r["Students_NAME"].ToString();
						string SearchingSurname = r["Students_SURNAME"].ToString();

						if (studentName == SearchingName  && studentSurname == SearchingSurname)
						{
							Console.WriteLine("Your studends id is" + " " +r["Students_ID"]);
							return;
						}
						
					}
					Console.WriteLine("Students not Found");
				
				
				}



			}

		}


	}



}
