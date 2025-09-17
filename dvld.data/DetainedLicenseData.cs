using DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvld.data
{
    public class DetainedLicenseData
    {
        public static bool GetDetainedLicenseInfoByID(int  ID , ref DetainedLicenseDTO dto)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM DetainedLicenses WHERE DetainID = @DetainID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DetainID", dto.DetainID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;

                    dto.LicenseID = (int)reader["LicenseID"];
                    dto.DetainDate = (DateTime)reader["DetainDate"];
                    dto.FineFees = Convert.ToSingle(reader["FineFees"]);
                    dto.CreatedByUserID = (int)reader["CreatedByUserID"];

                    dto.IsReleased = (bool)reader["IsReleased"];

                    if (reader["ReleaseDate"] == DBNull.Value)

                        dto.ReleaseDate = DateTime.MaxValue;
                    else
                        dto.ReleaseDate = (DateTime)reader["ReleaseDate"];


                    if (reader["ReleasedByUserID"] == DBNull.Value)

                        dto.ReleasedByUserID = -1;
                    else
                        dto.ReleasedByUserID = (int)reader["ReleasedByUserID"];

                    if (reader["ReleaseApplicationID"] == DBNull.Value)

                        dto.ReleaseApplicationID = -1;
                    else
                        dto.ReleaseApplicationID = (int)reader["ReleaseApplicationID"];

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


    }




}

