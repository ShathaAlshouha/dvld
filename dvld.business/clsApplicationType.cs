using DTOs;
using dvld.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace dvld.business
{
    public class clsApplicationType
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;


        public int ID { set; get; }
        public string Title { set; get; }
        public float Fees { set; get; }

        public clsApplicationType()

        {
            this.ID = -1;
            this.Title = "";
            this.Fees = 0;
            Mode = enMode.AddNew;

        }

        public clsApplicationType(int ID, string ApplicationTypeTitel, float ApplicationTypeFees)

        {
            this.ID = ID;
            this.Title = ApplicationTypeTitel;
            this.Fees = ApplicationTypeFees;
            Mode = enMode.Update;
        }

        private bool _AddNewApplicationType()
        {
       
            ApplicationTypeDTO DTO = new ApplicationTypeDTO
            {
                Title = this.Title,
                Fees = this.Fees
            };
            this.ID = clsApplicationTypeData.AddNewApplicationType(DTO); 


            return (this.ID != -1);
        }

        private bool _UpdateApplicationType()
        {
    

            return clsApplicationTypeData.UpdateApplicationType(this.ID, this.Title, this.Fees);
        }

        public static clsApplicationType Find(int ID)
        {

           ApplicationTypeDTO DTO = new ApplicationTypeDTO();

            if (clsApplicationTypeData.GetApplicationTypeInfoByID((int)ID, ref DTO))

                return new clsApplicationType(DTO.ID, DTO.Title, DTO.Fees);
            else
                return null;

        }

        public static List<ApplicationTypeDTO> GetAllApplicationTypes()
        {
            return clsApplicationTypeData.GetAllApplicationTypes();

        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplicationType())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateApplicationType();

            }

            return false;
        }

    }
}
