using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class UserDTO
    {
        public int UserID { set; get; }
        public int PersonID { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
        public bool IsActive { set; get; }

        public UserDTO()
        {
            UserID = -1;
            PersonID = -1;
            UserName = "";
            Password = "";
            IsActive = true;
        }
        public UserDTO(int userID, int personID, string userName, string password, bool isActive)
        {
            UserID = userID;
            PersonID = personID;
            UserName = userName;
            Password = password;
            IsActive = isActive;
        }
    }


    public class UserDetailsDTO
    {
        public int UserID { set; get; }
        public int PersonID { set; get; }
        public string UserName { set; get; }
        public string FullName { set; get; }
        public bool IsActive { set; get; }

        public UserDetailsDTO()
        {
            UserID = -1;
            PersonID = -1;
            UserName = "";
            FullName = "";
            IsActive = true;
        }
        public UserDetailsDTO(int userID, int personID, string userName, string fullName, bool isActive)
        {
            UserID = userID;
            PersonID = personID;
            UserName = userName;
            FullName = fullName;
            IsActive = isActive;
        }
    }
}
