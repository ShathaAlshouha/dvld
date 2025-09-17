using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class DetainedLicenseDTO
    {
        public int DetainID { get; set; }
        public int LicenseID { get; set; }
        public DateTime DetainDate { get; set; }
        public float FineFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsReleased { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int ReleasedByUserID { get; set; }
        public int ReleaseApplicationID { get; set; }
        public DetainedLicenseDTO()
        {
            DetainID = 0;
            LicenseID = 0;
            DetainDate = DateTime.Now;
            FineFees = 0;
            CreatedByUserID = 0;
            IsReleased = false;
            ReleaseDate = DateTime.Now;
            ReleasedByUserID = 0;
            ReleaseApplicationID = 0;

        }
        public DetainedLicenseDTO(int detainID, int licenseID, DateTime detainDate, float fineFees, int createdByUserID, bool isReleased, DateTime releaseDate, int releasedByUserID, int releaseApplicationID)
        {
            DetainID = detainID;
            LicenseID = licenseID;
            DetainDate = detainDate;
            FineFees = fineFees;
            CreatedByUserID = createdByUserID;
            IsReleased = isReleased;
            ReleaseDate = releaseDate;
            ReleasedByUserID = releasedByUserID;
            ReleaseApplicationID = releaseApplicationID;
        }
    }





    public class DetaiendViewDTO
    {

        public int DetainID { get; set; }
        public int LicenseID { get; set; }
        public DateTime DetainDate { get; set; }
        public float FineFees { get; set; }

        public bool IsReleased { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int ReleaseApplicationID { get; set; }
        public string NationalNo { get; set; }
        public string FullName { get; set; }

        public DetaiendViewDTO()
        {
            DetainID = 0;
            LicenseID = 0;
            DetainDate = DateTime.Now;
            FineFees = 0;
            IsReleased = false;
            ReleaseDate = DateTime.Now;
            ReleaseApplicationID = 0;
            NationalNo = string.Empty;
            FullName = string.Empty;

        }

        public DetaiendViewDTO(int detainID, int licenseID, DateTime detainDate, float fineFees, bool isReleased, DateTime releaseDate, int releaseApplicationID, string nationalNo, string fullName)
        {
            DetainID = detainID;
            LicenseID = licenseID;
            DetainDate = detainDate;
            FineFees = fineFees;
            IsReleased = isReleased;
            ReleaseDate = releaseDate;
            ReleaseApplicationID = releaseApplicationID;
            NationalNo = nationalNo;
            FullName = fullName;
        }
    }


}
