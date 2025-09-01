using DTOs;
using dvld.data;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvld.business
{
    public class clsTest
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int TestID { set; get; }
        public int TestAppointmentID { set; get; }
        public clsTestAppointment TestAppointmentInfo { set; get; }
        public bool TestResult { set; get; }
        public string Notes { set; get; }
        public int CreatedByUserID { set; get; }

        public clsTest()

        {
            this.TestID = -1;
            this.TestAppointmentID = -1;
            this.TestResult = false;
            this.Notes = "";
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;

        }

        public clsTest(int TestID, int TestAppointmentID,
            bool TestResult, string Notes, int CreatedByUserID)

        {
            this.TestID = TestID;
            this.TestAppointmentID = TestAppointmentID;
            this.TestAppointmentInfo = clsTestAppointment.Find(TestAppointmentID);
            this.TestResult = TestResult;
            this.Notes = Notes;
            this.CreatedByUserID = CreatedByUserID;

            Mode = enMode.Update;
        }

        private bool _AddNewTest()
        {
     
            TestDTO testDTO = new TestDTO
            {

                TestAppointmentID = this.TestAppointmentID,
                TestResult = this.TestResult,
                Notes = this.Notes,
                CreatedByUserID = this.CreatedByUserID
            };

            this.TestID = clsTestData.AddNewTest(testDTO); 


            return (this.TestID != -1);
        }

        private bool _UpdateTest()
        {
            TestDTO updatetest = new TestDTO
            {
                TestID = this.TestID,
                TestAppointmentID = this.TestAppointmentID,
                TestResult = this.TestResult,
                Notes = this.Notes,
                CreatedByUserID = this.CreatedByUserID

            };

            return clsTestData.UpdateTest(updatetest); 
        }

        public static clsTest Find(int TestID)
        {
            TestDTO DTO = new TestDTO();
            if (clsTestData.GetTestInfoByID(TestID, ref DTO))

                return new clsTest(TestID, DTO.TestAppointmentID ,DTO.TestResult,DTO.Notes , DTO.CreatedByUserID); 
            else
                return null;

        }

        public static clsTest FindLastTestPerPersonAndLicenseClass (int PersonID, int LicenseClassID, clsTestType.enTestType TestTypeID)
        {
            TestDTO testDTO = new TestDTO();

            if (clsTestData.GetLastTestByPersonAndTestTypeAndLicenseClass
                (PersonID, LicenseClassID, (int)TestTypeID, ref testDTO))

                return new clsTest(testDTO.TestID,
                        testDTO.TestAppointmentID, testDTO.TestResult,
                        testDTO.Notes, testDTO.CreatedByUserID);
            else
                return null;

        }

        public static List<TestDTO> GetAllTests()
        {
            return clsTestData.GetAllTests();

        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTest())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateTest();

            }

            return false;
        }

        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return clsTestData.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }

        public static bool PassedAllTests(int LocalDrivingLicenseApplicationID)
        {        
            return GetPassedTestCount(LocalDrivingLicenseApplicationID) == 3;
        }



    }
}
