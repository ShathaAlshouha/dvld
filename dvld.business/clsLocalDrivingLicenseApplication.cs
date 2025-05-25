using dvld.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvld.business
{
    public class clsLocalDrivingLicenseApplication:clsApplication
    {
        enum enMode { AddNew = 0, Update = 1 }
        enMode Mode = enMode.AddNew; 
        int LocalDrivingLicenseApplicationID { get; set; } 
        int ApplicationID {  get; set; }
        int LicenseClassID { get; set; }
        clsLicenseClass LicenseClassInfo   { get; set; }
        clsApplication ApplivationInfo { get; set; }
        string PersonFullName
        {
            get
            {
                return base.PersonInfo.FullName;
            }
        }
        public clsLocalDrivingLicenseApplication()
        {
            this.ApplicationID = 0; 
            this.LicenseClassID = 0;
            this.LocalDrivingLicenseApplicationID = 0;
            Mode = enMode.AddNew; 
        }
        private clsLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID, int ApplicationID, int ApplicantPersonID,
            DateTime ApplicationDate, int ApplicationTypeID,
             enApplicationStatus ApplicationStatus, DateTime LastStatusDate,
             float PaidFees, int CreatedByUserID, int LicenseClassID)
        {
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID; ;
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = (int)ApplicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.LicenseClassID = LicenseClassID;
            this.LicenseClassInfo = new clsLicenseClass(); 
            Mode = enMode.Update;
        }



        public bool _AddNewLocalDrivingLisencsApplication()
        {

            int ID = clsLocalDrivingLicenseApplicationData.AddNewLocalDrivingLicenseApplication(this.ApplicationID, this.LicenseClassID);
            return (ID != -1);
        }

        public bool _UpdateLocalDrivingLisencseApplication()
        {

            return clsLocalDrivingLicenseApplicationData.UpdateLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID, this.ApplicationID, this.LicenseClassID));
        }

    }
}
