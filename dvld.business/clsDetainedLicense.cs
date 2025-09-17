using DTOs;
using dvld.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvld.business
{
    public class clsDetainedLicense
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;


        public int DetainID { set; get; }
        public int LicenseID { set; get; }
        public DateTime DetainDate { set; get; }

        public float FineFees { set; get; }
        public int CreatedByUserID { set; get; }
        public clsUser CreatedByUserInfo { set; get; }
        public bool IsReleased { set; get; }
        public DateTime ReleaseDate { set; get; }
        public int ReleasedByUserID { set; get; }
        public clsUser ReleasedByUserInfo { set; get; }
        public int ReleaseApplicationID { set; get; }

        public clsDetainedLicense()

        {
            this.DetainID = -1;
            this.LicenseID = -1;
            this.DetainDate = DateTime.Now;
            this.FineFees = 0;
            this.CreatedByUserID = -1;
            this.IsReleased = false;
            this.ReleaseDate = DateTime.MaxValue;
            this.ReleasedByUserID = 0;
            this.ReleaseApplicationID = -1;



            Mode = enMode.AddNew;

        }

        public clsDetainedLicense(int DetainID,
            int LicenseID, DateTime DetainDate,
            float FineFees, int CreatedByUserID,
            bool IsReleased, DateTime ReleaseDate,
            int ReleasedByUserID, int ReleaseApplicationID)

        {
            this.DetainID = DetainID;
            this.LicenseID = LicenseID;
            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUserInfo = clsUser.FindByUserID(this.CreatedByUserID);
            this.IsReleased = IsReleased;
            this.ReleaseDate = ReleaseDate;
            this.ReleasedByUserID = ReleasedByUserID;
            this.ReleaseApplicationID = ReleaseApplicationID;
            this.ReleasedByUserInfo = clsUser.FindByPersonID(this.ReleasedByUserID);
            Mode = enMode.Update;
        }

        private bool _AddNewDetainedLicense()
        {
            DetainedLicenseDTO dto = new DetainedLicenseDTO
            {
                
                LicenseID = this.LicenseID,
                DetainDate = this.DetainDate,
                FineFees = this.FineFees,
                CreatedByUserID = this.CreatedByUserID,
            };
            this.DetainID = DetainedLicenseData.AddNewDetainedLicense(dto); 

            return (this.DetainID != -1);
        }

        private bool _UpdateDetainedLicense()
        {

            DetainedLicenseDTO dto = new DetainedLicenseDTO
            {
                LicenseID = this.LicenseID,
                DetainDate = this.DetainDate,
                FineFees = this.FineFees,
                CreatedByUserID = this.CreatedByUserID,

            };
            return DetainedLicenseData.UpdateDetainedLicense(dto);
        }

        public static clsDetainedLicense Find(int DetainID)
        {
            DetainedLicenseDTO detainedLicenseDTO = new DetainedLicenseDTO();

            if (DetainedLicenseData.GetDetainedLicenseInfoByID(DetainID,ref detainedLicenseDTO))

                return new clsDetainedLicense(DetainID,
                     detainedLicenseDTO.LicenseID, detainedLicenseDTO.DetainDate,
                     detainedLicenseDTO.FineFees, detainedLicenseDTO.CreatedByUserID,
                     detainedLicenseDTO.IsReleased, detainedLicenseDTO.ReleaseDate,
                     detainedLicenseDTO.ReleasedByUserID, detainedLicenseDTO.ReleaseApplicationID);
            else
                return null;

        }

        public static List<DetaiendViewDTO> GetAllDetainedLicenses()
        {
            return DetainedLicenseData.GetAllDetainedLicenses();

        }

        public static clsDetainedLicense FindByLicenseID(int LicenseID)
        {

            DetainedLicenseDTO dto = new DetainedLicenseDTO();

            if (DetainedLicenseData.GetDetainedLicenseInfoByLicenseID(LicenseID, ref dto))

                return new clsDetainedLicense(dto.DetainID,
                     dto.LicenseID, dto.DetainDate,
                     dto.FineFees, dto.CreatedByUserID,
                     dto.IsReleased, dto.ReleaseDate,
                     dto.ReleasedByUserID, dto.ReleaseApplicationID);
            else
                return null;

        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDetainedLicense())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateDetainedLicense();

            }

            return false;
        }

        public static bool IsLicenseDetained(int LicenseID)
        {
            return DetainedLicenseData.IsLicenseDetained(LicenseID);
        }

        public bool ReleaseDetainedLicense(int ReleasedByUserID, int ReleaseApplicationID)
        {
            return DetainedLicenseData.ReleaseDetainedLicense(this.DetainID,
                   ReleasedByUserID, ReleaseApplicationID);
        }

    }
}
