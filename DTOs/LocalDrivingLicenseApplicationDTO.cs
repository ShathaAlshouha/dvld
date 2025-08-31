using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class LocalDLApp
    {
        public int ID { get; set; }
        public int ApplicationID { get; set; }
        public int LicenseClassID { get; set; }

        public LocalDLApp()
        {
            this.ID = 0; 
            this.ApplicationID = 0;
            this.LicenseClassID = 0;
        }
        public LocalDLApp(int ID, int ApplicationID, int LicenseClassID)
        {
            this.ID = ID;
            this.ApplicationID = ApplicationID;
            this.LicenseClassID = LicenseClassID;
        }
    }

    public class LocalDLApp_View
    {
        public int LocalDrivingLisenceID { get; set; }
        public string NationalNo { get; set; }
        public string ClassName { get; set; }
        public string FullName { get; set; }
        public DateTime ApplicationDate { get; set; }
        public int PassedTestCount { get; set; }
        public string Status { get; set; }

        public LocalDLApp_View()
        {
            this.LocalDrivingLisenceID = 0;
            this.NationalNo = "";
            this.ClassName = "";
            this.FullName = "";
            this.ApplicationDate = new DateTime();
            this.PassedTestCount = 0;
            this.Status = "";
        }
        public LocalDLApp_View(int id, string nationalID, string Class, string fullname, DateTime date, int passedtest, string status)
        {
            this.LocalDrivingLisenceID = id;
            this.NationalNo = nationalID;
            this.ClassName = Class;
            this.FullName = fullname;
            this.ApplicationDate = date;
            this.PassedTestCount = passedtest;
            this.Status = status;
        }
    }
}
