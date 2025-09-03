using DTOs;
using dvld.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace dvld.business
{
    public class clsTestType
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 };

        public clsTestType.enTestType ID { set; get; }
        public string Title { set; get; }
        public string Description { set; get; }
        public float Fees { set; get; }
        public clsTestType()

        {
            this.ID = clsTestType.enTestType.VisionTest;
            this.Title = "";
            this.Description = "";
            this.Fees = 0;
            Mode = enMode.AddNew;

        }

        public clsTestType(clsTestType.enTestType ID, string TestTypeTitel, string Description, float TestTypeFees)

        {
            this.ID = ID;
            this.Title = TestTypeTitel;
            this.Description = Description;

            this.Fees = TestTypeFees;
            Mode = enMode.Update;
        }

        private bool _AddNewTestType()
        {
            testTypeDTO newtestType = new testTypeDTO
            {
                TestFees = this.Fees,
                TestTypeTitle = this.Title,
                Description = this.Description
            };

            this.ID = (clsTestType.enTestType)clsTestTypeData.AddNewTestType(newtestType);

            return (this.Title != "");
        }

        private bool _UpdateTestType()
        {
            testTypeDTO testTypeDTO = new testTypeDTO
            {
                TestFees = this.Fees,
                TestTypeTitle = this.Title,
                Description = this.Description
            };


            return clsTestTypeData.UpdateTestType(testTypeDTO); 
        }

        public static clsTestType Find(clsTestType.enTestType TestTypeID)
        {
           testTypeDTO DTO = new testTypeDTO();

            if (clsTestTypeData.GetTestTypeInfoByID((int)TestTypeID, ref DTO))

                return new clsTestType(TestTypeID, DTO.TestTypeTitle, DTO.Description, DTO.TestFees); 
            else
                return null;

        }

        public static List<testTypeDTO> GetAllTestTypes()
        {
            return clsTestTypeData.GetAllTestTypes();

        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestType())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateTestType();
            }

            return false;
        }


    }
}
