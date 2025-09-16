using DTOs;
using dvld.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvld.business
{
    public class clsLisence
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public enum enIssueReason { FirstTime = 1, Renew = 2, DamagedReplacement = 3, LostReplacement = 4 };

        public clsDriver DriverInfo;
        public int LicenseID { set; get; }
        public int ApplicationID { set; get; }
        public int DriverID { set; get; }
        public int LicenseClass { set; get; }
        public clsLicenseClass LicenseClassIfo;
        public DateTime IssueDate { set; get; }
        public DateTime ExpirationDate { set; get; }
        public string Notes { set; get; }
        public float PaidFees { set; get; }
        public bool IsActive { set; get; }
        public enIssueReason IssueReason { set; get; }

        public int CreatedByUserID { set; get; }


        public clsLisence()
        {
            this.LicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.LicenseClass = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.Notes = "";
            this.PaidFees = 0;
            this.IsActive = true;
            this.IssueReason = enIssueReason.FirstTime;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;

        }
        public clsLisence(int LicenseID, int ApplicationID, int DriverID, int LicenseClass,
           DateTime IssueDate, DateTime ExpirationDate, string Notes,
           float PaidFees, bool IsActive, enIssueReason IssueReason, int CreatedByUserID)

        {
            this.LicenseID = LicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.LicenseClass = LicenseClass;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.IssueReason = IssueReason;
            this.CreatedByUserID = CreatedByUserID;

            this.DriverInfo = clsDriver.FindByDriverID(this.DriverID);
            this.LicenseClassIfo = clsLicenseClass.Find(this.LicenseClass);


            Mode = enMode.Update;
        }

        private bool _AddNewLisence()
        {
            var license = new LicenseDTO
            {
                ApplicationID = this.ApplicationID,
                DriverID = this.DriverID,
                LicenseClass = this.LicenseClass,
                IssueDate = this.IssueDate,
                ExpirationDate = this.ExpirationDate,
                Notes = this.Notes,
                PaidFees = this.PaidFees,
                IsActive = this.IsActive,
                IssueReason = (byte)this.IssueReason,
                CreatedByUserID = this.CreatedByUserID
            };
            this.LicenseID = clsLisenceData.AddNewLicense(license);
            return this.LicenseID != -1;
        }

        private bool _UpdateLisence()
        {
            var license = new LicenseDTO
            {
                ApplicationID = this.ApplicationID,
                DriverID = this.DriverID,
                LicenseClass = this.LicenseClass,
                IssueDate = this.IssueDate,
                ExpirationDate = this.ExpirationDate,
                Notes = this.Notes,
                PaidFees = this.PaidFees,
                IsActive = this.IsActive,
                IssueReason = (byte)this.IssueReason,
                CreatedByUserID = this.CreatedByUserID
            };

            return clsLisenceData.UpdateLicense(license);
        }


        public static List<LicenseDTO> GetAllLicenses()
        {
            return clsLisenceData.GetAllLicenses();
        }
        public static List<LicenseViewDTO> GetDriverLicenses(int DriverID)
        {
            return clsLisenceData.GetDriverLicenses(DriverID);
        }
       
        public static int GetActiveLicenseIDByPersonID (int PersonID ,int LicenseClassID)
        {
            return clsLisenceData.GetActiveLicenseIDByPersonID(PersonID, LicenseClassID);
        }
   
    
        public static bool DeactivateLicense (int LicenseID)
        {
            return clsLisenceData.DeactivateLicense(LicenseID);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLisence())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateLisence();

            }

            return false;
        }
    }
}

