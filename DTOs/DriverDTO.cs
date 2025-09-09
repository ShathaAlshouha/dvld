using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class DriverDTO
    {
        public int DriverID { set; get; }
        public int PersonID { set; get; }
        public int CreatedByUserID { set; get; }
        public DateTime CreatedDate { get; set; }

        public DriverDTO()
        {
            this.DriverID = -1;
            this.PersonID = -1;
            this.CreatedByUserID = -1;
            this.CreatedDate = DateTime.Now;
        }
        public DriverDTO(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            this.DriverID = DriverID;
            this.PersonID = PersonID;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedDate = CreatedDate;
        }
    }


    public class Driver_ViewDTO
    {
        public int DriverID { set; get; }
        public int PersonID { set; get; }
        public string NationalNo { set; get; }
        public string FullName { set; get; }
        public DateTime CreatedDate { get; set; }
        public int NumberOfActiveLicenses { get; set; }
            
    }
}
