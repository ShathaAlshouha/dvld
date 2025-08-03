using DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvld.data
{
    public class clsPersonData
    {
        public static bool GetPersonInfoByID(int PersonID , ref PersonDTO person)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM People WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                   
                    isFound = true;
                    person.PersonID = (int)reader["PersonID"];
                    person.FirstName = (string)reader["FirstName"];
                    person.SecondName = (string)reader["SecondName"];

                   
                    if (reader["ThirdName"] != DBNull.Value)
                    {
                        person.ThirdName = (string)reader["ThirdName"];
                    }
                    else
                    {
                        person.ThirdName = "";
                    }

                    person.LastName = (string)reader["LastName"];
                   person. NationalNo = (string)reader["NationalNo"];
                   person. DateOfBirth = (DateTime)reader["DateOfBirth"];
                   person. Gendor = (byte)reader["Gendor"];
                   person. Address = (string)reader["Address"];
                   person. Phone = (string)reader["Phone"];

                    if (reader["Email"] != DBNull.Value)
                    {
                        person.Email = (string)reader["Email"];
                    }
                    else
                    {
                        person.Email = "";
                    }

                    person.NationalityCountryID = (int)reader["NationalityCountryID"];

                 
                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        person.ImagePath = (string)reader["ImagePath"];
                    }
                    else
                    {
                        person.ImagePath = "";
                    }

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


        public static bool GetPersonInfoByNationalNo(string NationalNo, ref PersonDTO person)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM People WHERE NationalNo = @NationalNo";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                   
                    isFound = true;

                    person.PersonID = (int)reader["PersonID"];
                    person.FirstName = (string)reader["FirstName"];
                    person.SecondName = (string)reader["SecondName"];

                   
                    if (reader["ThirdName"] != DBNull.Value)
                    {
                        person.ThirdName = (string)reader["ThirdName"];
                    }
                    else
                    {
                        person.ThirdName = "";
                    }

                    person.LastName = (string)reader["LastName"];
                    person.DateOfBirth = (DateTime)reader["DateOfBirth"];
                    person.Gendor = (byte)reader["Gendor"];
                    person.Address = (string)reader["Address"];
                    person.Phone = (string)reader["Phone"];

          
                    if (reader["Email"] != DBNull.Value)
                    {
                        person.Email = (string)reader["Email"];
                    }
                    else
                    {
                        person.Email = "";
                    }

                    person.NationalityCountryID = (int)reader["NationalityCountryID"];

                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        person.ImagePath = (string)reader["ImagePath"];
                    }
                    else
                    {
                        person.ImagePath = "";
                    }

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
              
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }



        public static int AddNewPerson(PersonDTO newPerson)
        {
          
            int PersonID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO People (FirstName, SecondName, ThirdName,LastName,NationalNo,
                                                   DateOfBirth,Gendor,Address,Phone, Email, NationalityCountryID,ImagePath)
                             VALUES (@FirstName, @SecondName,@ThirdName, @LastName, @NationalNo,
                                     @DateOfBirth,@Gendor,@Address,@Phone, @Email,@NationalityCountryID,@ImagePath);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@FirstName", newPerson.FirstName);
            command.Parameters.AddWithValue("@SecondName", newPerson.SecondName);

            if (newPerson.ThirdName != "" && newPerson.ThirdName != null)
                command.Parameters.AddWithValue("@ThirdName", newPerson.ThirdName);
            else
                command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);

            command.Parameters.AddWithValue("@LastName", newPerson.LastName);
            command.Parameters.AddWithValue("@NationalNo", newPerson.NationalNo);
            command.Parameters.AddWithValue("@DateOfBirth", newPerson.DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", newPerson.Gendor);
            command.Parameters.AddWithValue("@Address", newPerson.Address);
            command.Parameters.AddWithValue("@Phone", newPerson.Phone);

            if (newPerson.Email != "" && newPerson.Email != null)
                command.Parameters.AddWithValue("@Email", newPerson.Email);
            else
                command.Parameters.AddWithValue("@Email", System.DBNull.Value);

            command.Parameters.AddWithValue("@NationalityCountryID", newPerson.NationalityCountryID);

            if (newPerson.ImagePath != "" && newPerson.ImagePath != null)
                command.Parameters.AddWithValue("@ImagePath", newPerson.ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    PersonID = insertedID;
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

            return PersonID;
        }



        public static bool UpdatePerson(PersonDTO PersonDTO)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update  People  
                            set FirstName = @FirstName,
                                SecondName = @SecondName,
                                ThirdName = @ThirdName,
                                LastName = @LastName, 
                                NationalNo = @NationalNo,
                                DateOfBirth = @DateOfBirth,
                                Gendor=@Gendor,
                                Address = @Address,  
                                Phone = @Phone,
                                Email = @Email, 
                                NationalityCountryID = @NationalityCountryID,
                                ImagePath =@ImagePath
                                where PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonDTO.PersonID);
            command.Parameters.AddWithValue("@FirstName", PersonDTO.FirstName);
            command.Parameters.AddWithValue("@SecondName", PersonDTO.SecondName);

            if (PersonDTO.ThirdName != "" && PersonDTO.ThirdName != null)
                command.Parameters.AddWithValue("@ThirdName", PersonDTO.ThirdName);
            else
                command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);


            command.Parameters.AddWithValue("@LastName", PersonDTO.LastName);
            command.Parameters.AddWithValue("@NationalNo", PersonDTO.NationalNo);
            command.Parameters.AddWithValue("@DateOfBirth", PersonDTO.DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", PersonDTO.Gendor);
            command.Parameters.AddWithValue("@Address", PersonDTO.Address);
            command.Parameters.AddWithValue("@Phone", PersonDTO.Phone);

            if (PersonDTO.Email != "" && PersonDTO.Email != null)
                command.Parameters.AddWithValue("@Email", PersonDTO.Email);
            else
                command.Parameters.AddWithValue("@Email", System.DBNull.Value);

            command.Parameters.AddWithValue("@NationalityCountryID", PersonDTO.NationalityCountryID);

            if (PersonDTO.ImagePath != "" && PersonDTO.ImagePath != null)
                command.Parameters.AddWithValue("@ImagePath", PersonDTO.ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);


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


        public static List<PersonDTO> GetAllPeople()
        {

            List<PersonDTO> list = new List<PersonDTO>();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query =
              @"SELECT People.PersonID, People.NationalNo,
              People.FirstName, People.SecondName, People.ThirdName, People.LastName,
			  People.DateOfBirth, People.Gendor,  
				  CASE
                  WHEN People.Gendor = 0 THEN 'Male'

                  ELSE 'Female'

                  END as GendorCaption ,
			  People.Address, People.Phone, People.Email, 
              People.NationalityCountryID, Countries.CountryName, People.ImagePath
              FROM            People INNER JOIN
                         Countries ON People.NationalityCountryID = Countries.CountryID
                ORDER BY People.FirstName";




            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())

                {
                    PersonDTO person = new PersonDTO
                    {
                        PersonID = reader.GetInt32(reader.GetOrdinal("PersonID")),
                        NationalNo = reader.GetString(reader.GetOrdinal("NationalNo")),
                        FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                        SecondName = reader.GetString(reader.GetOrdinal("SecondName")),
                        ThirdName = reader.IsDBNull(reader.GetOrdinal("ThirdName")) ? null : reader.GetString(reader.GetOrdinal("ThirdName")),
                        LastName = reader.GetString(reader.GetOrdinal("LastName")),
                        DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                        Gendor = reader.GetByte(reader.GetOrdinal("Gendor")),
                        Address = reader.GetString(reader.GetOrdinal("Address")),
                        Phone = reader.GetString(reader.GetOrdinal("Phone")),
                        Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString(reader.GetOrdinal("Email")),
                        NationalityCountryID = reader.GetInt32(reader.GetOrdinal("NationalityCountryID")),
                        ImagePath = reader.IsDBNull(reader.GetOrdinal("ImagePath")) ? null : reader.GetString(reader.GetOrdinal("ImagePath")),
                 
                    };

                    list.Add(person);
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

            return list;

        }

        public static bool DeletePerson(int PersonID)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Delete People 
                                where PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

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

        public static bool IsPersonExist(int PersonID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM People WHERE PersonID = @PersonID";

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

        public static bool IsPersonExist(string NationalNo)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM People WHERE NationalNo = @NationalNo";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);

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
