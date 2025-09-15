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
    public class clsLisenceData
    {

        public static bool GetLisenceInfoByID(int LicenseID, ref LicenseDTO licenseDTO)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Licenses WHERE LicenseID = @LicenseID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {


                    isFound = true;
                    licenseDTO.ApplicationID = (int)reader["ApplicationID"];
                    licenseDTO.DriverID = (int)reader["DriverID"];
                    licenseDTO.LicenseClass = (int)reader["LicenseClass"];
                    licenseDTO.IssueDate = (DateTime)reader["IssueDate"];
                    licenseDTO.ExpirationDate = (DateTime)reader["ExpirationDate"];

                    if (reader["Notes"] == DBNull.Value)
                        licenseDTO.Notes = "";
                    else
                        licenseDTO.Notes = (string)reader["Notes"];

                    licenseDTO.PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    licenseDTO.IsActive = (bool)reader["IsActive"];
                    licenseDTO.IssueReason = (byte)reader["IssueReason"];
                    licenseDTO.CreatedByUserID = (int)reader["DriverID"];

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


        public static List<LicenseDTO> GetAllLicenses()
        {

            List<LicenseDTO> list = new List<LicenseDTO>();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Licenses";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())

                {
                    list.Add(new LicenseDTO
                    {
                        LicenseID = (int)reader["LicenseID"],
                        ApplicationID = (int)reader["ApplicationID"],
                        DriverID = (int)reader["DriverID"],
                        LicenseClass = (int)reader["LicenseClass"],
                        IssueDate = (DateTime)reader["IssueDate"],
                        ExpirationDate = (DateTime)reader["ExpirationDate"],
                        Notes = reader["Notes"] == DBNull.Value ? "" : (string)reader["Notes"],
                        PaidFees = Convert.ToSingle(reader["PaidFees"]),
                        IsActive = (bool)reader["IsActive"],
                        IssueReason = (byte)reader["IssueReason"],
                        CreatedByUserID = (int)reader["CreatedByUserID"]

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

            return list;

        }


        public static List<LicenseViewDTO> GetDriverLicenses(int DriverID)
        {

            List<LicenseViewDTO> list = new List<LicenseViewDTO>();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT     
                           Licenses.LicenseID,
                           ApplicationID,
		                   LicenseClasses.ClassName, Licenses.IssueDate, 
		                   Licenses.ExpirationDate, Licenses.IsActive
                           FROM Licenses INNER JOIN
                                LicenseClasses ON Licenses.LicenseClass = LicenseClasses.LicenseClassID
                            where DriverID=@DriverID
                            Order By IsActive Desc, ExpirationDate Desc";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new LicenseViewDTO
                    {
                        licenseID = (int)reader["LicenseID"],
                        applicationID = (int)reader["ApplicationID"],
                        licenseClassName = (string)reader["ClassName"],
                        IssueDate = (DateTime)reader["IssueDate"],
                        ExpirationDate = (DateTime)reader["ExpirationDate"],
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

            return dt;

        }

        public static int AddNewLicense(LicenseDTO newLicense)
        {
            int LicenseID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"
                              INSERT INTO Licenses(ApplicationID,    DriverID, LicenseClass,IssueDate,ExpirationDate,
                                        Notes,PaidFees, IsActive,IssueReason,CreatedByUserID)
                         VALUES ( @ApplicationID,@DriverID, @LicenseClass,  @IssueDate,  @ExpirationDate, @Notes,
                               @PaidFees, @IsActive,@IssueReason, @CreatedByUserID);
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", newLicense.ApplicationID);
            command.Parameters.AddWithValue("@DriverID",newLicense.DriverID);
            command.Parameters.AddWithValue("@LicenseClass",newLicense.LicenseClass);
            command.Parameters.AddWithValue("@IssueDate",newLicense.IssueDate);

            command.Parameters.AddWithValue("@ExpirationDate",newLicense.ExpirationDate);

            if (newLicense.Notes == "")
                command.Parameters.AddWithValue("@Notes", DBNull.Value);
            else
                command.Parameters.AddWithValue("@Notes",newLicense.Notes);

            command.Parameters.AddWithValue("@PaidFees",newLicense.PaidFees);
            command.Parameters.AddWithValue("@IsActive", newLicense.IsActive);
            command.Parameters.AddWithValue("@IssueReason", newLicense.IssueReason);

            command.Parameters.AddWithValue("@CreatedByUserID", newLicense.CreatedByUserID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    LicenseID = insertedID;
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

            return LicenseID;

        }

        public static bool UpdateLicense(LicenseDTO licenseDTO)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE Licenses
                           SET ApplicationID=@ApplicationID, DriverID = @DriverID,
                              LicenseClass = @LicenseClass,
                              IssueDate = @IssueDate,
                              ExpirationDate = @ExpirationDate,
                              Notes = @Notes,
                              PaidFees = @PaidFees,
                              IsActive = @IsActive,IssueReason=@IssueReason,
                              CreatedByUserID = @CreatedByUserID
                         WHERE LicenseID=@LicenseID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", licenseDTO.LicenseID);
            command.Parameters.AddWithValue("@ApplicationID", licenseDTO.ApplicationID);
            command.Parameters.AddWithValue("@DriverID", licenseDTO.DriverID);
            command.Parameters.AddWithValue("@LicenseClass", licenseDTO.LicenseClass);
            command.Parameters.AddWithValue("@IssueDate", licenseDTO.IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate",   licenseDTO.ExpirationDate);

            if (licenseDTO.Notes == "")
                command.Parameters.AddWithValue("@Notes", DBNull.Value);
            else
                command.Parameters.AddWithValue("@Notes", licenseDTO.Notes);

            command.Parameters.AddWithValue("@PaidFees", licenseDTO.PaidFees);
            command.Parameters.AddWithValue("@IsActive", licenseDTO.IsActive);
            command.Parameters.AddWithValue("@IssueReason", licenseDTO.IssueReason);
            command.Parameters.AddWithValue("@CreatedByUserID", licenseDTO.CreatedByUserID);

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



        public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {
            int LicenseID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT        Licenses.LicenseID
                            FROM Licenses INNER JOIN
                                                     Drivers ON Licenses.DriverID = Drivers.DriverID
                            WHERE  
                             
                             Licenses.LicenseClass = @LicenseClass 
                              AND Drivers.PersonID = @PersonID
                              And IsActive=1;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClassID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    LicenseID = insertedID;
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


            return LicenseID;
        }
         public static bool DeactivateLicense(int LicenseID)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE Licenses
                           SET 
                              IsActive = 0
                             
                         WHERE LicenseID=@LicenseID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);
         

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
