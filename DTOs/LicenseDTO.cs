using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class LicenseDTO
    {
        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public int LicenseClass { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public float PaidFees { get; set; }
        public bool IsActive { get; set; }

        public int IssueReason { get; set; }
        public int CreatedByUserID { get; set; }

        public LicenseDTO()
        {
            LicenseID = 0;
            ApplicationID = 0;
            DriverID = 0;
            LicenseClass = 0;
            IssueDate = DateTime.Now;
            ExpirationDate = DateTime.Now;
            Notes = string.Empty;
            PaidFees = 0;
            IsActive = true;
            IssueReason = 1; 
            CreatedByUserID = 0;

        }


        public LicenseDTO(int licenseID, int applicationID, int driverID, int licenseClass, DateTime issueDate, DateTime expirationDate, string notes, float paidFees, bool isActive, int issueReason, int createdByUserID)
        {
            LicenseID = licenseID;
            ApplicationID = applicationID;
            DriverID = driverID;
            LicenseClass = licenseClass;
            IssueDate = issueDate;
            ExpirationDate = expirationDate;
            Notes = notes;
            PaidFees = paidFees;
            IsActive = isActive;
            IssueReason = issueReason;
            CreatedByUserID = createdByUserID;
        }
    }

    public class LicenseViewDTO
    {
        public int licenseID { get; set; }
        public int applicationID { get; set; }

        public string licenseClassName { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime IssueDate { get; set; }
        public bool IsActive { get; set; }

        public LicenseViewDTO()
        {
            licenseID = 0;
            applicationID = 0;
            licenseClassName = string.Empty;
            ExpirationDate = DateTime.Now;
            IssueDate = DateTime.Now;
            IsActive = true;
        }
        public LicenseViewDTO(int licenseID, int applicationID, string licenseClassName, DateTime expirationDate, DateTime issueDate, bool isActive)
        {
            this.licenseID = licenseID;
            this.applicationID = applicationID;
            this.licenseClassName = licenseClassName;
            ExpirationDate = expirationDate;
            IssueDate = issueDate;
            IsActive = isActive;
        }

    }

}
