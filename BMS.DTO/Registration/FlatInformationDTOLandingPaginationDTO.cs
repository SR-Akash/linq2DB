using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.DTO.Registration
{
    public class FlatInformationDTOLandingPaginationDTO
    {
        public List<FlatInformationDTO> Data { get; set; }
        public long CurrentPage { get; set; }
        public long TotalCount { get; set; }
        public long PageSize { get; set; }
    }
}
