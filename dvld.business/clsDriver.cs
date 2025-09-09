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
    public class clsDriver
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public clsPerson PersonInfo;

        public int DriverID { set; get; }
        public int PersonID { set; get; }
        public int CreatedByUserID { set; get; }
        public DateTime CreatedDate { get; }

        public clsDriver()

        {
            this.DriverID = -1;
            this.PersonID = -1;
            this.CreatedByUserID = -1;
            this.CreatedDate = DateTime.Now;
            Mode = enMode.AddNew;

        }

        public clsDriver(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)

        {
            this.DriverID = DriverID;
            this.PersonID = PersonID;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedDate = CreatedDate;
            this.PersonInfo = clsPerson.Find(PersonID);

            Mode = enMode.Update;
        }

        private bool _AddNewDriver()
        {
            //call DataAccess Layer 

            this.DriverID = clsDriverData.AddNewDriver(PersonID, CreatedByUserID);


            return (this.DriverID != -1);
        }

        private bool _UpdateDriver()
        {
            //call DataAccess Layer 

            return clsDriverData.UpdateDriver(this.DriverID, this.PersonID, this.CreatedByUserID);
        }

        public static clsDriver FindByDriverID(int DriverID)
        {

        DriverDTO driverDTO = new DriverDTO();

            if (clsDriverData.GetDriverInfoByDriverID(DriverID, ref driverDTO))

                return new clsDriver(DriverID, driverDTO.PersonID, driverDTO.CreatedByUserID, driverDTO.CreatedDate);
            else
                return null; 

        }

        public static clsDriver FindByPersonID(int PersonID)
        {

            DriverDTO driverDTO = new DriverDTO();

            if (clsDriverData.GetDriverInfoByPersonID(PersonID,ref driverDTO))

                return new clsDriver(driverDTO.DriverID, PersonID, driverDTO.CreatedByUserID, driverDTO.CreatedDate);
            else
                return null;

        }

        public static List<DriverDTO> GetAllDrivers()
        {
            return clsDriverData.GetAllDrivers();

        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDriver())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateDriver();

            }

            return false;
        }

        //public static DataTable GetLicenses(int DriverID)
        //{
        //    return clsLicense.GetDriverLicenses(DriverID);
        //}

        //public static DataTable GetInternationalLicenses(int DriverID)
        //{
        //    return clsInternationalLicense.GetDriverInternationalLicenses(DriverID);
        //}


    }
    
}
