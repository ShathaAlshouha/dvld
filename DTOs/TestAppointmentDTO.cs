using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class TestAppointmentDTO
    {
        public int TestAppointmentID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int TestTypeID { get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; }
        public float PaidFees { set; get; }
        public int CreatedByUserID { set; get; }
        public bool IsLocked { set; get; }
        public int RetakeTestApplicationID { set; get; }

        public TestAppointmentDTO ()
        {
            TestAppointmentID = -1;
            AppointmentDate = DateTime.Now;
            TestTypeID = -1;
            LocalDrivingLicenseApplicationID = -1;  
            PaidFees = 0;
            CreatedByUserID = -1;
            IsLocked = false;
            RetakeTestApplicationID = -1;

        }
        public TestAppointmentDTO(int testAppointmentID, DateTime appointmentDate, int testTypeID, int localDrivingLicenseApplicationID, float paidFees, int createdByUserID, bool isLocked, int retakeTestApplicationID)
        {
            TestAppointmentID = testAppointmentID;
            AppointmentDate = appointmentDate;
            TestTypeID = testTypeID;
            LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            PaidFees = paidFees;
            CreatedByUserID = createdByUserID;
            IsLocked = isLocked;
            RetakeTestApplicationID = retakeTestApplicationID;
        }

    }
    public class TestAppointmentDetailsDTO
    {
        public int TestAppointmentID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string TestTypeTitle { get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; }
        public float PaidFees { set; get; }
        public string FullName { set; get; }
        public bool IsLocked { set; get; }
    
        public string ClassName { set; get; }
        public TestAppointmentDetailsDTO ()
        {
            TestAppointmentID = -1;
            AppointmentDate = DateTime.Now;
            TestTypeTitle = "";
            LocalDrivingLicenseApplicationID = -1;  
            PaidFees = 0;
            FullName = string.Empty;
            IsLocked = false;
            ClassName = "";

        }
        public TestAppointmentDetailsDTO(int testAppointmentID, DateTime appointmentDate, string testTypeTitle, int localDrivingLicenseApplicationID, float paidFees,string className, string fullName, bool isLocked)
        {
            TestAppointmentID = testAppointmentID;
            AppointmentDate = appointmentDate;
            TestTypeTitle = testTypeTitle; 
            LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            PaidFees = paidFees;
            FullName = fullName;
            IsLocked = isLocked;
            ClassName = className; 
        }

    }
    public class TestAppointmentViewDTO
    {
        public int TestAppointmentID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public float PaidFees { get; set; }
        public bool IsLocked { get; set; }

        public TestAppointmentViewDTO ()
        {
            TestAppointmentID = -1;
            AppointmentDate = DateTime.Now;
            PaidFees = 0;
            IsLocked = false;

        }
        public TestAppointmentViewDTO(int testAppointmentID, DateTime appointmentDate, float paidFees, bool isLocked)
        {
            TestAppointmentID = testAppointmentID;
            AppointmentDate = appointmentDate;
            PaidFees = paidFees;
            IsLocked = isLocked;
        }

    }
}
