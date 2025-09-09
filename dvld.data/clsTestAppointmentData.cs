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
    public class clsTestAppointmentData
    {
        public static bool GetTestAppointmentInfoByID(int TestAppointmentID, ref TestAppointmentDTO DTO)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM TestAppointments WHERE TestAppointmentID = @TestAppointmentID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;
                    DTO.TestTypeID = (int)reader["TestTypeID"];
                    DTO.LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                    DTO.AppointmentDate = (DateTime)reader["AppointmentDate"];
                    DTO.CreatedByUserID = (int)reader["CreatedByUserID"];
                    DTO.PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    DTO.IsLocked = (bool)reader["IsLocked"];

                    if (reader["RetakeTestApplicationID"] == DBNull.Value)
                        DTO.RetakeTestApplicationID = -1;
                    else
                        DTO.RetakeTestApplicationID = (int)reader["RetakeTestApplicationID"];

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

        public static bool GetLastTestAppointment( int LocalDrivingLicenseApplicationID, int TestTypeID,
            ref TestAppointmentDTO DTO)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT       top 1 *
                FROM            TestAppointments
                WHERE        (TestTypeID = @TestTypeID) 
                AND (LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID) 
                order by TestAppointmentID Desc";


            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;

                    DTO.TestAppointmentID = (int)reader["TestAppointmentID"];
                    DTO.AppointmentDate = (DateTime)reader["AppointmentDate"];
                    DTO.PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    DTO.CreatedByUserID = (int)reader["CreatedByUserID"];
                    DTO.IsLocked = (bool)reader["IsLocked"];

                    if (reader["RetakeTestApplicationID"] == DBNull.Value)
                        DTO.RetakeTestApplicationID = -1;
                    else
                        DTO.RetakeTestApplicationID = (int)reader["RetakeTestApplicationID"];


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

        public static List<TestAppointmentDetailsDTO> GetAllTestAppointments()
        {

            List<TestAppointmentDetailsDTO> list = new List<TestAppointmentDetailsDTO> ();

             SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select * from TestAppointments_View
                                  order by AppointmentDate Desc";


            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())

                {
                    list.Add(new TestAppointmentDetailsDTO
                    {
                        TestAppointmentID = (int)reader["TestAppointmentID"],
                        AppointmentDate = (DateTime)reader["AppointmentDate"],
                        TestTypeTitle = (string)reader["TestTypeTitle"],
                        LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"],
                        PaidFees = Convert.ToSingle(reader["PaidFees"]),
                        FullName = reader["FullName"].ToString(),
                        IsLocked = (bool)reader["IsLocked"],
                        ClassName = (string)reader["ClassName"] 
                      

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

        public static List<TestAppointmentViewDTO> GetApplicationTestAppointmentsPerTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            var list = new List<TestAppointmentViewDTO>();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"SELECT TestAppointmentID, AppointmentDate, PaidFees, IsLocked
                         FROM TestAppointments
                         WHERE TestTypeID = @TestTypeID
                           AND LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                         ORDER BY TestAppointmentID DESC;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                    command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new TestAppointmentViewDTO
                        {
                            TestAppointmentID = (int)reader["TestAppointmentID"],
                            AppointmentDate = (DateTime)reader["AppointmentDate"],
                            PaidFees = reader["PaidFees"] == DBNull.Value ? 0 : Convert.ToSingle(reader["PaidFees"]),
                            IsLocked = reader["IsLocked"] != DBNull.Value && (bool)reader["IsLocked"]
                        });
                    }
                }
            }

            return list;
        }


        public static int AddNewTestAppointment(TestAppointmentDTO DTO)
        {
            int TestAppointmentID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Insert Into TestAppointments (TestTypeID,LocalDrivingLicenseApplicationID,AppointmentDate,PaidFees,CreatedByUserID,IsLocked,RetakeTestApplicationID)
                            Values (@TestTypeID,@LocalDrivingLicenseApplicationID,@AppointmentDate,@PaidFees,@CreatedByUserID,0,@RetakeTestApplicationID);
                
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("@TestTypeID", DTO.TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID",DTO.LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", DTO.AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", DTO.PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", DTO.CreatedByUserID);

            if (DTO.RetakeTestApplicationID == -1)

                command.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
            else
                command.Parameters.AddWithValue("@RetakeTestApplicationID", DTO.RetakeTestApplicationID);





            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TestAppointmentID = insertedID;
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


            return TestAppointmentID;

        }

        public static bool UpdateTestAppointment(TestAppointmentDTO DTO)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update  TestAppointments  
                            set TestTypeID = @TestTypeID,
                                LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID,
                                AppointmentDate = @AppointmentDate,
                                PaidFees = @PaidFees,
                                CreatedByUserID = @CreatedByUserID,
                                IsLocked=@IsLocked,
                                RetakeTestApplicationID=@RetakeTestApplicationID
                                where TestAppointmentID = @TestAppointmentID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", DTO.TestAppointmentID);
            command.Parameters.AddWithValue("@TestTypeID", DTO.TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", DTO.LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate",DTO. AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", DTO.PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", DTO.CreatedByUserID);
            command.Parameters.AddWithValue("@IsLocked", DTO.IsLocked);

            if (DTO.RetakeTestApplicationID == -1)

                command.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
            else
                command.Parameters.AddWithValue("@RetakeTestApplicationID", DTO.RetakeTestApplicationID);





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


        public static int GetTestID(int TestAppointmentID)
        {
            int TestID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select TestID from Tests where TestAppointmentID=@TestAppointmentID;";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TestID = insertedID;
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


            return TestID;

        }

    }

}