using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.DTO.Registration
{
    public class FlatInformationDTO
    {
        public long Sl { get; set; }
        public long Id { get; set; }
        public long BuildingId { get; set; }
        public string BuildingName { get; set; }
        public string FlatNo { get; set; }
        public int FloorNumber { get; set; }
        public double Size { get; set; }
        public int NumberOfRoom { get; set; }
        public string Attatchment { get; set; }
        public int FlatHolderTypeId { get; set; }
        public string FlatHolderTypeName { get; set; }
        public string FlatUserName { get; set; }
        public string Contract { get; set; }
        public string Email { get; set; }
        public double? RentAmount { get; set; }
        public string Remarks { get; set; }
        public long IntActionBy { get; set; }
        public string ActionByName { get; set; }
        public DateTime DteLastActionDateTime { get; set; }
        public DateTime? DteServerDateTime { get; set; }
        public int Status { get; set; }

    }
}
