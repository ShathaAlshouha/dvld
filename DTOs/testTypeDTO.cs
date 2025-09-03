using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class testTypeDTO
    {
        public int TestTypeID { get; set; }
        public string TestTypeTitle { get; set; }
        public string Description { get; set; }
        public float TestFees { get; set; }


        public testTypeDTO ()
        {
            TestTypeID = -1;
            TestTypeTitle = ""; 
            Description = "";
            TestFees = 1.0f; 

        }
        public testTypeDTO(int testTypeID, string TestTypeTitle, string TestTypeDescription, float TestTypeFees)
        {
            this.TestTypeID = testTypeID;
            this.TestTypeTitle = TestTypeTitle;
            this.TestFees = TestTypeFees;
            this.Description = TestTypeDescription;
        }
    }
}