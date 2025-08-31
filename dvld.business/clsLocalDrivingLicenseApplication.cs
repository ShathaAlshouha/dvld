using dvld.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs;
using static System.Net.Mime.MediaTypeNames;

namespace dvld.business
{
    public class clsLocalDrivingLicenseApplication : clsApplication
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int LocalDrivingLicenseApplicationID { set; get; }
        public int LicenseClassID { set; get; }
        public clsLicenseClass LicenseClassInfo;
        public string PersonFullName
        {
            get
            {
                return base.PersonInfo.FullName;
            }

        }

        public clsLocalDrivingLicenseApplication()

        {
            this.LocalDrivingLicenseApplicationID = -1;
            this.LicenseClassID = -1;


            Mode = enMode.AddNew;

        }
        private clsLocalDrivingLicenseApplication(int localDrivingLicenseApplicationID, int applicationID, ApplicationDTO applicationDTO,
            int licenseClassID)
        {
            this.LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            this.ApplicationID = applicationID;

            this.ApplicantPersonID = applicationDTO.ApplicantPersonID;
            this.ApplicationDate = applicationDTO.ApplicationDate;
            this.ApplicationTypeID = applicationDTO.ApplicationTypeID;
            this.ApplicationStatus = (enApplicationStatus)applicationDTO.ApplicationStatus;
            this.LastStatusDate = applicationDTO.LastStatusDate;
            this.PaidFees = applicationDTO.PaidFees;
            this.CreatedByUserID = applicationDTO.CreatedByUserID;

            this.LicenseClassID = licenseClassID;
            this.LicenseClassInfo = clsLicenseClass.Find(licenseClassID);

            this.Mode = enMode.Update;
        }


        private bool _AddNewLocalDrivingLicenseApplication()
        {

            LocalDLApp newDTO = new LocalDLApp
            {

                ApplicationID = this.ApplicationID,
                LicenseClassID = this.LicenseClassID
            };

            this.LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationData.AddNewLocalDrivingLicenseApplication(newDTO);
            return (this.LocalDrivingLicenseApplicationID != -1);
        }

        private bool _UpdateLocalDrivingLicenseApplication()
        {

            LocalDLApp DTO = new LocalDLApp
            {
                ApplicationID = this.ApplicationID,
                LicenseClassID = this.LicenseClassID
            };
            return clsLocalDrivingLicenseApplicationData.UpdateLocalDrivingLicenseApplication
                (
                this.LocalDrivingLicenseApplicationID, DTO);

        }

        public static clsLocalDrivingLicenseApplication? FindByLocalDrivingAppLicenseID(int LocalDrivingLicenseApplicationID)
        {
            LocalDLApp? DTO = clsLocalDrivingLicenseApplicationData
                                .GetLocalDrivingLicenseApplicationInfoByID(LocalDrivingLicenseApplicationID);

            if (DTO != null)
            {
                clsApplication? application = clsApplication.FindBaseApplication(DTO.ApplicationID);

                if (application == null)
                    return null;

                ApplicationDTO applicationDTO = new ApplicationDTO
                {
                    ApplicantPersonID = application.ApplicantPersonID,
                    ApplicationDate = application.ApplicationDate,
                    ApplicationTypeID = application.ApplicationTypeID,
                    ApplicationStatus = (byte)application.ApplicationStatus,
                    LastStatusDate = application.LastStatusDate,
                    PaidFees = application.PaidFees,
                    CreatedByUserID = application.CreatedByUserID
                };

                return new clsLocalDrivingLicenseApplication(
                    LocalDrivingLicenseApplicationID,
                    DTO.ApplicationID,
                    applicationDTO,
                    DTO.LicenseClassID
                );
            }

            return null;
        }



        public static clsLocalDrivingLicenseApplication FindByApplicationID(int ApplicationID)
        {

            LocalDLApp DTO = new LocalDLApp();

            bool IsFound = clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationInfoByApplicationID
                (ApplicationID, ref DTO);


            if (IsFound)
            {

                clsApplication application = clsApplication.FindBaseApplication(ApplicationID);

                ApplicationDTO applicationDTO = new ApplicationDTO
                {
                    ApplicantPersonID = application.ApplicantPersonID,
                    ApplicationDate = application.ApplicationDate,
                    ApplicationTypeID = application.ApplicationTypeID,
                    ApplicationStatus = (byte)application.ApplicationStatus,
                    LastStatusDate = application.LastStatusDate,
                    PaidFees = application.PaidFees,
                    CreatedByUserID = application.CreatedByUserID
                };
                return new clsLocalDrivingLicenseApplication(
                   DTO.ID,
                   DTO.ApplicationID,
                   applicationDTO,
                   DTO.LicenseClassID
               );
            }
            else
                return null;


        }

        public bool Save()
        {
            var applicationFound = clsApplication.IsApplicationExist(this.ApplicationID);
            if (!applicationFound)
                return false;

            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLocalDrivingLicenseApplication())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateLocalDrivingLicenseApplication();

            }

            return false;
        }

        public static List<LocalDLApp_View> GetAllLocalDrivingLicenseApplications()
        {
            return clsLocalDrivingLicenseApplicationData.GetAllLocalDrivingLicenseApplications();
        }

        public bool Delete()
        {
            bool IsLocalDrivingApplicationDeleted = false;
            bool IsBaseApplicationDeleted = false;
            //First we delete the Local Driving License Application
            IsLocalDrivingApplicationDeleted = clsLocalDrivingLicenseApplicationData.DeleteLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID);

            if (!IsLocalDrivingApplicationDeleted)
                return false;
            //Then we delete the base Application
            IsBaseApplicationDeleted = base.Delete();
            return IsBaseApplicationDeleted;

        }

        public bool DoesPassTestType(clsTestType.enTestType TestTypeID)

        {
            return clsLocalDrivingLicenseApplicationData.DoesPassTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

     
        public bool DoesPassPreviousTest(clsTestType.enTestType CurrentTestType)
        {

            switch (CurrentTestType)
            {
                case clsTestType.enTestType.VisionTest:
                    //in this case no required prvious test to pass.
                    return true;

                case clsTestType.enTestType.WrittenTest:
                    //Written Test, you cannot sechdule it before person passes the vision test.
                    //we check if pass visiontest 1.

                    return this.DoesPassTestType(clsTestType.enTestType.VisionTest);


                case clsTestType.enTestType.StreetTest:

                    //Street Test, you cannot sechdule it before person passes the written test.
                    //we check if pass Written 2.
                    return this.DoesPassTestType(clsTestType.enTestType.WrittenTest);

                default:
                    return false;
            }
        }

        public static bool DoesPassTestType(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)

        {
            return clsLocalDrivingLicenseApplicationData.DoesPassTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool DoesAttendTestType(clsTestType.enTestType TestTypeID)

        {
            return clsLocalDrivingLicenseApplicationData.DoesAttendTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }




    }
}
