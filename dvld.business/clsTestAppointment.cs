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
    public class clsTestAppointment
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int TestAppointmentID { set; get; }
        public clsTestType.enTestType TestTypeID { set; get; }
        public int LocalDrivingLicenseApplicationID { set; get; }
        public DateTime AppointmentDate { set; get; }
        public float PaidFees { set; get; }
        public int CreatedByUserID { set; get; }
        public bool IsLocked { set; get; }
        public int RetakeTestApplicationID { set; get; }
        public clsApplication RetakeTestAppInfo { set; get; }

        public int TestID
        {
            get { return _GetTestID(); }

        }

        public clsTestAppointment()

        {
            this.TestAppointmentID = -1;
            this.TestTypeID = clsTestType.enTestType.VisionTest;
            this.AppointmentDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;
            this.RetakeTestApplicationID = -1;
            Mode = enMode.AddNew;

        }

        public clsTestAppointment(int TestAppointmentID, clsTestType.enTestType TestTypeID,
           int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, float PaidFees,
           int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID)

        {
            this.TestAppointmentID = TestAppointmentID;
            this.TestTypeID = TestTypeID;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsLocked = IsLocked;
            this.RetakeTestApplicationID = RetakeTestApplicationID;
            this.RetakeTestAppInfo = clsApplication.FindBaseApplication(RetakeTestApplicationID);
            Mode = enMode.Update;
        }

        private bool _AddNewTestAppointment()
        {

            TestAppointmentDTO testAppointmentDTO = new TestAppointmentDTO
            {
                TestAppointmentID = this.TestAppointmentID,
                TestTypeID = (int)this.TestTypeID,
                LocalDrivingLicenseApplicationID = this.LocalDrivingLicenseApplicationID,
                AppointmentDate = this.AppointmentDate,
                PaidFees = this.PaidFees,
                CreatedByUserID = this.CreatedByUserID,
                IsLocked = this.IsLocked,
                RetakeTestApplicationID = this.RetakeTestApplicationID
            };

            this.TestAppointmentID = clsTestAppointmentData.AddNewTestAppointment(testAppointmentDTO); 

            return (this.TestAppointmentID != -1);
        }

        private bool _UpdateTestAppointment()
        {

            TestAppointmentDTO testAppointmentDTO = new TestAppointmentDTO
            {
                TestAppointmentID = this.TestAppointmentID,
                TestTypeID = (int)this.TestTypeID,
                LocalDrivingLicenseApplicationID = this.LocalDrivingLicenseApplicationID,
                AppointmentDate = this.AppointmentDate,
                PaidFees = this.PaidFees,
                CreatedByUserID = this.CreatedByUserID,
                IsLocked = this.IsLocked,
                RetakeTestApplicationID = this.RetakeTestApplicationID

            };

            return clsTestAppointmentData.UpdateTestAppointment(testAppointmentDTO); 
        }

        public static clsTestAppointment Find(int TestAppointmentID)
        {
            TestAppointmentDTO testAppointmentDTO = new TestAppointmentDTO();

            if (clsTestAppointmentData.GetTestAppointmentInfoByID(TestAppointmentID, ref testAppointmentDTO))

                return new clsTestAppointment(TestAppointmentID, (clsTestType.enTestType)testAppointmentDTO.TestTypeID, testAppointmentDTO.LocalDrivingLicenseApplicationID,
             testAppointmentDTO.AppointmentDate, testAppointmentDTO.PaidFees, testAppointmentDTO.CreatedByUserID, testAppointmentDTO.IsLocked, testAppointmentDTO.RetakeTestApplicationID);
            else
                return null;

        }

        public static clsTestAppointment GetLastTestAppointment(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            TestAppointmentDTO dto = new TestAppointmentDTO(); 

            if (clsTestAppointmentData.GetLastTestAppointment(LocalDrivingLicenseApplicationID, (int)TestTypeID, ref dto ))

                return new clsTestAppointment(dto.TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID,
             dto.AppointmentDate, dto.PaidFees, dto.CreatedByUserID, dto.IsLocked, dto.RetakeTestApplicationID);
            else
                return null;

        }

        public static List<TestAppointmentDetailsDTO> GetAllTestAppointments()
        {
            return clsTestAppointmentData.GetAllTestAppointments();

        }

        public List<TestAppointmentViewDTO> GetApplicationTestAppointmentsPerTestType(clsTestType.enTestType TestTypeID)
        {
            return clsTestAppointmentData.GetApplicationTestAppointmentsPerTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);

        }

        public static List<TestAppointmentViewDTO> GetApplicationTestAppointmentsPerTestType(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            return clsTestAppointmentData.GetApplicationTestAppointmentsPerTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);

        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestAppointment())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateTestAppointment();

            }

            return false;
        }

        private int _GetTestID()
        {
            return clsTestAppointmentData.GetTestID(TestAppointmentID);
        }
    }


}
