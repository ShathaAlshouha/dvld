using DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;


namespace dvld.data
{
    public class clsUserData
    {
        public static bool GetUserInfoByUserID(int UserID,ref UserDTO userDTO)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Users WHERE UserID=@UserID";
            SqlCommand command = new SqlCommand(query, connection); 


            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    userDTO.PersonID = (int)reader["PersonID"];
                    userDTO.UserName = (string)reader["UserName"];
                    userDTO.Password = (string)reader["Password"];
                    userDTO.IsActive = (bool)reader["IsActive"];


                }
                else
                {
                    isFound = false;
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

                isFound = false;
            }

            finally
            {
                connection.Close();
            }
            return isFound; 

        }


        public static bool GetUserInfoByPersonID(int PersonID, ref UserDTO userDTO)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Users WHERE PersonID=@PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    userDTO.UserID = (int)reader["UserID"];
                    userDTO.UserName = (string)reader["UserName"];
                    userDTO.Password = (string)reader["Password"];
                    userDTO.IsActive = (bool)reader["IsActive"];

                }
                else
                {
                    isFound = false;
                }
                reader.Close(); 
                   
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

                isFound = false;
            }

            finally
            {
                connection.Close();
            }
            return isFound; 

        }

        public static bool GetUserInfoByUsernameAndPassword(string UserName, string Password,ref UserDTO userDTO)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Users WHERE Username = @Username and Password=@Password;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Username", UserName);
            command.Parameters.AddWithValue("@Password", Password);


            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;
                    userDTO.UserID = (int)reader["UserID"];
                    userDTO.PersonID = (int)reader["PersonID"];
                    userDTO.UserName = (string)reader["UserName"];
                    userDTO.Password = (string)reader["Password"];
                    userDTO.IsActive = (bool)reader["IsActive"];


                }
                else
                {
                    // The record was not found
                    isFound = false;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static int AddNewUser(UserDTO userDTO)
        {
            //this function will return the new person id if succeeded and -1 if not.
            int UserID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Users (PersonID,UserName,Password,IsActive)
                             VALUES (@PersonID, @UserName,@Password,@IsActive);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
           
            command.Parameters.AddWithValue("@PersonID", userDTO.PersonID);
            command.Parameters.AddWithValue("@UserName", userDTO.UserName);
            command.Parameters.AddWithValue("@Password", userDTO.Password);
            command.Parameters.AddWithValue("@IsActive", userDTO.IsActive);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    UserID = insertedID;
                }
            }

            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

            }

            finally
            {
                connection.Close();
            }

            return UserID;
        }


        public static bool UpdateUser(UserDTO userDTO)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update  Users  
                            set PersonID = @PersonID,
                                UserName = @UserName,
                                Password = @Password,
                                IsActive = @IsActive
                                where UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", userDTO.PersonID);
            command.Parameters.AddWithValue("@UserName", userDTO.UserName);
            command.Parameters.AddWithValue("@Password", userDTO.Password);
            command.Parameters.AddWithValue("@IsActive", userDTO.IsActive);
            command.Parameters.AddWithValue("@UserID", userDTO.UserID);

          
            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }


        public static List<UserDetailsDTO> GetAllUsers()
        {
            List<UserDetailsDTO> userDetailsDTOs = new List<UserDetailsDTO>();
              SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT  Users.UserID, Users.PersonID,
                            FullName = People.FirstName + ' ' + People.SecondName + ' ' + ISNULL( People.ThirdName,'') +' ' + People.LastName,
                            Users.UserName, Users.IsActive
                             FROM  Users INNER JOIN
                                    People ON Users.PersonID = People.PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    userDetailsDTOs.Add(new UserDetailsDTO
                    {
                        UserID = (int)reader["UserID"],
                        PersonID = (int)reader["PersonID"],
                        FullName = (string)reader["FullName"],
                        UserName = (string)reader["UserName"],
                        IsActive = (bool)reader["IsActive"]
                    });
                }

                reader.Close();
            }

            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return userDetailsDTOs; 


        }

        public static bool DeleteUser(int UserID)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Delete Users 
                                where UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return (rowsAffected > 0);
        }


        public static bool IsUserExist(int UserID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT Found=1 FROM Users WHERE UserID=@UserID"; 
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            bool isFound = false;
            try
            {
                connection.Open();
                SqlDataReader reader= command.ExecuteReader();  
                isFound =reader.HasRows;
                reader.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound; 

        }

        public static bool IsUserExist(string UserName)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM Users WHERE UserName = @UserName";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserName", UserName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool IsUserExistForPersonID(int PersonID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM Users WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool ChangePassword(int UserID, string NewPassword)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update  Users  
                            set Password = @Password
                            where UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }
        public static bool DoesPersonHaveUser44(int PersonID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM Users WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }


    }
}
