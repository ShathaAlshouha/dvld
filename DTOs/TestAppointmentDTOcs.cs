using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class TestAppointmentDTOcs
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
}
