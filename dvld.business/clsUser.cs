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
    public class clsUser
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int UserID { set; get; }
        public int PersonID { set; get; }
        public clsPerson PersonInfo;
        public string UserName { set; get; }
        public string Password { set; get; }
        public bool IsActive { set; get; }

        public clsUser()

        {
            this.UserID = -1;
            this.UserName = "";
            this.Password = "";
            this.IsActive = true;
            Mode = enMode.AddNew;
        }

        private clsUser(int UserID, int PersonID, string Username, string Password,
            bool IsActive)

        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.PersonInfo = clsPerson.Find(PersonID);
            this.UserName = Username;
            this.Password = Password;
            this.IsActive = IsActive;

            Mode = enMode.Update;
        }

        private bool _AddNewUser()
        {
            UserDTO user = new UserDTO
            {
                UserID = this.UserID,
                PersonID = this.PersonID,
                UserName = this.UserName,
                Password = this.Password,
                IsActive = this.IsActive

            }; 

            this.UserID = clsUserData.AddNewUser(user);

            return (this.UserID != -1);
        }
        private bool _UpdateUser()
        {

            UserDTO user = new UserDTO
            {
                UserID = this.UserID,
                PersonID = this.PersonID,
                UserName = this.UserName,
                Password = this.Password,
                IsActive = this.IsActive

            };
            return clsUserData.UpdateUser(user); 
        }
        public static clsUser FindByUserID(int UserID)
        {
          UserDTO user = new UserDTO();

            bool IsFound = clsUserData.GetUserInfoByUserID(UserID, ref user);

            if (IsFound)
               
                return new clsUser(UserID, user.PersonID,user. UserName, user.Password, user.IsActive);
            else
                return null;
        }
        public static clsUser FindByPersonID(int PersonID)
        {
            UserDTO user = new UserDTO();

            bool IsFound = clsUserData.GetUserInfoByPersonID
                                (PersonID, ref user);

            if (IsFound)
                
                return new clsUser(user.UserID, user.PersonID, user.UserName, user.Password, user.IsActive);
            else
                return null;
        }
        public static clsUser FindByUsernameAndPassword(string UserName, string Password)
        {
         
            UserDTO user = new UserDTO();
            bool IsFound = clsUserData.GetUserInfoByUsernameAndPassword (UserName, Password, ref user);

            if (IsFound)
                
                return new clsUser(user.UserID, user.PersonID, UserName, Password, user.IsActive);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateUser();

            }

            return false;
        }

        public static List<UserDetailsDTO> GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }

        public static bool DeleteUser(int UserID)
        {
            return clsUserData.DeleteUser(UserID);
        }

        public static bool isUserExist(int UserID)
        {
            return clsUserData.IsUserExist(UserID);
        }

        public static bool isUserExist(string UserName)
        {
            return clsUserData.IsUserExist(UserName);
        }

        public static bool isUserExistForPersonID(int PersonID)
        {
            return clsUserData.IsUserExistForPersonID(PersonID);
        }

    }
}