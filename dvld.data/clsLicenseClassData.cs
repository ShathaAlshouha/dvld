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
    public class clsLicenseClassData
    {

        public static bool GetLicenseClassInfoByID(int LicenseClassID,ref LicenseClassDTO licenseClassDTO)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM LicenseClasses WHERE LicenseClassID = @LicenseClassID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                  
                    isFound = true;

                    licenseClassDTO.ClassName = (string)reader["ClassName"];
                    licenseClassDTO.ClassDescription = (string)reader["ClassDescription"];
                    licenseClassDTO.MinimumAllowedAge = (byte)reader["MinimumAllowedAge"];
                    licenseClassDTO.DefaultValidityLength = (byte)reader["DefaultValidityLength"];
                    licenseClassDTO.ClassFees = Convert.ToSingle(reader["ClassFees"]);

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


        public static bool GetLicenseClassInfoByClassName(string ClassName, ref LicenseClassDTO licenseClassDTO)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM LicenseClasses WHERE ClassName = @ClassName";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ClassName", ClassName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;
                    licenseClassDTO.LicenseClassID = (int)reader["LicenseClassID"];
                    licenseClassDTO.ClassDescription = (string)reader["ClassDescription"];
                    licenseClassDTO.MinimumAllowedAge = (byte)reader["MinimumAllowedAge"];
                    licenseClassDTO.DefaultValidityLength = (byte)reader["DefaultValidityLength"];
                    licenseClassDTO.ClassFees = Convert.ToSingle(reader["ClassFees"]);
                 
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



        public static List<LicenseClassDTO> GetAllLicenseClasses()
        {

            List<LicenseClassDTO> list = new List<LicenseClassDTO>();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM LicenseClasses order by ClassName";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new LicenseClassDTO(
                        (int)reader["LicenseClassID"],
                        (string)reader["ClassName"],
                        (string)reader["ClassDescription"],
                        (byte)reader["MinimumAllowedAge"],
                        (byte)reader["DefaultValidityLength"],
                        Convert.ToSingle(reader["ClassFees"])
                        ));
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

        public static int AddNewLicenseClass(LicenseClassDTO dto)
        {
            int LicenseClassID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Insert Into LicenseClasses 
           (
            ClassName,ClassDescription,MinimumAllowedAge, 
            DefaultValidityLength,ClassFees)
                            Values ( 
            @ClassName,@ClassDescription,@MinimumAllowedAge, 
            @DefaultValidityLength,@ClassFees)
                            where LicenseClassID = @LicenseClassID;
                            SELECT SCOPE_IDENTITY();";



            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ClassName", dto.ClassName);
            command.Parameters.AddWithValue("@ClassDescription", dto.ClassDescription);
            command.Parameters.AddWithValue("@MinimumAllowedAge", dto.MinimumAllowedAge);
            command.Parameters.AddWithValue("@DefaultValidityLength", dto.DefaultValidityLength);
            command.Parameters.AddWithValue("@ClassFees", dto.ClassFees);



            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    LicenseClassID = insertedID;
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


            return LicenseClassID;

        }

        public static bool UpdateLicenseClass( LicenseClassDTO dto)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update  LicenseClasses  
                            set ClassName = @ClassName,
                                ClassDescription = @ClassDescription,
                                MinimumAllowedAge = @MinimumAllowedAge,
                                DefaultValidityLength = @DefaultValidityLength,
                                ClassFees = @ClassFees
                                where LicenseClassID = @LicenseClassID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseClassID", dto.LicenseClassID);
            command.Parameters.AddWithValue("@ClassName", dto.ClassName);
            command.Parameters.AddWithValue("@ClassDescription",  dto.ClassDescription);
            command.Parameters.AddWithValue("@MinimumAllowedAge", dto.MinimumAllowedAge);
            command.Parameters.AddWithValue("@DefaultValidityLength", dto.DefaultValidityLength);
            command.Parameters.AddWithValue("@ClassFees", dto.ClassFees);


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



    }
}
