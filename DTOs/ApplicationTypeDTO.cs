using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class ApplicationTypeDTO
    {
        public int ID { set; get; }
        public string Title { set; get; }
        public float Fees { set; get; }

        public ApplicationTypeDTO()
        {
            this.ID = -1;
            this.Title = "";
            this.Fees = 0;
        }
        public ApplicationTypeDTO(int id, string title, float fees)
        {
            this.ID = id;
            this.Title = title;
            this.Fees = fees;
        }
    }
}
