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


    }
    public class TestAppointmentDetailsDTO
    {
        public int TestAppointmentID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int TestTypeID { get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; }
        public float PaidFees { set; get; }
        public string FullName { set; get; }
        public bool IsLocked { set; get; }
    

    }
    public class TestAppointmentViewDTO
    {
        public int TestAppointmentID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public float PaidFees { get; set; }
        public bool IsLocked { get; set; }
    }
}
