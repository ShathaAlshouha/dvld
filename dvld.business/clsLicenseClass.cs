using DTOs;
using dvld.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace dvld.business
{
    public class clsLicenseClass
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int LicenseClassID { set; get; }
        public string ClassName { set; get; }
        public string ClassDescription { set; get; }
        public byte MinimumAllowedAge { set; get; }
        public byte DefaultValidityLength { set; get; }
        public float ClassFees { set; get; }

        public clsLicenseClass()

        {
            this.LicenseClassID = -1;
            this.ClassName = "";
            this.ClassDescription = "";
            this.MinimumAllowedAge = 18;
            this.DefaultValidityLength = 10;
            this.ClassFees = 0;

            Mode = enMode.AddNew;

        }

        public clsLicenseClass(int LicenseClassID, string ClassName,
            string ClassDescription,
            byte MinimumAllowedAge, byte DefaultValidityLength, float ClassFees)

        {
            this.LicenseClassID = LicenseClassID;
            this.ClassName = ClassName;
            this.ClassDescription = ClassDescription;
            this.MinimumAllowedAge = MinimumAllowedAge;
            this.DefaultValidityLength = DefaultValidityLength;
            this.ClassFees = ClassFees;
            Mode = enMode.Update;
        }

        private bool _AddNewLicenseClass()
        {
            LicenseClassDTO licenseClassDTO = new LicenseClassDTO
            {
                ClassDescription = this.ClassDescription,
                MinimumAllowedAge = this.MinimumAllowedAge,
                DefaultValidityLength = this.DefaultValidityLength,
                ClassFees = this.ClassFees,
                ClassName = this.ClassName

            };
            this.LicenseClassID = clsLicenseClassData.AddNewLicenseClass(licenseClassDTO);


            return (this.LicenseClassID != -1);
        }

        private bool _UpdateLicenseClass()
        {

            LicenseClassDTO licenseClassDTO = new LicenseClassDTO
            {
                ClassDescription = this.ClassDescription,
                MinimumAllowedAge = this.MinimumAllowedAge,
                DefaultValidityLength = this.DefaultValidityLength,
                ClassFees = this.ClassFees,
                ClassName = this.ClassName

            };
            return clsLicenseClassData.UpdateLicenseClass(this.LicenseClassID, licenseClassDTO); 
        }

        public static clsLicenseClass Find(int LicenseClassID)
        {
           LicenseClassDTO licenseClassDTO = new LicenseClassDTO();

            if (clsLicenseClassData.GetLicenseClassInfoByID(LicenseClassID, ref licenseClassDTO))

                return new clsLicenseClass(LicenseClassID, licenseClassDTO.ClassName, licenseClassDTO.ClassDescription,
                    licenseClassDTO.MinimumAllowedAge, licenseClassDTO.DefaultValidityLength, licenseClassDTO.ClassFees);
            else
                return null;

        }

        public static clsLicenseClass Find(string ClassName)
        {
            LicenseClassDTO licenseClassDTO = new LicenseClassDTO();

            if (clsLicenseClassData.GetLicenseClassInfoByClassName(ClassName,ref licenseClassDTO))

                return new clsLicenseClass(licenseClassDTO.LicenseClassID, ClassName,  licenseClassDTO.ClassDescription,
                    licenseClassDTO.MinimumAllowedAge, licenseClassDTO.DefaultValidityLength, licenseClassDTO.ClassFees);
            else
                return null;

        }

        public static List<LicenseClassDTO> GetAllLicenseClasses()
        {
            return clsLicenseClassData.GetAllLicenseClasses();

        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicenseClass())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateLicenseClass();

            }

            return false;
        }


    }
}
