using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.DTO.Registration
{
    public class BuildingInformationDTO
    {
        public long Sl { get; set; }
        public long BuildingId { get; set; }
        public string BuildingName { get; set; }
        public string BuildingAddress { get; set; }
        public long? CityId { get; set; }
        public string CityName { get; set; }
        public int? NoOfFloor { get; set; }
        public string Remarks { get; set; }
        public string Attachment { get; set; }
        public long? ActionById { get; set; }
        public string ActionByName { get; set; }
        public int Status { get; set; }
    }


}
