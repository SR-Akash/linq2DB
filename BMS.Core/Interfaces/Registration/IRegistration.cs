using BMS.DTO.Common;
using BMS.DTO.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Core.Interfaces.Registration
{
    public interface IRegistration
    {
        Task<object> GetCityList();
        Task<MessageHelper> CreateBuildingRegistration(BuildingInformationDTO obj);
        Task<BuildingInformationLandingPaginationDTO> GetBuildingRegistrationLandingPagination(string search, DateTime? fromDate, DateTime? toDate, string viewOrder, int pageNo, int pageSize);
        Task<BuildingInformationDTO> GetBuildingRegistrationInfo(long id);

        Task<List<BuildingInformationDTO>> GetUserWiseBuildingList(long userId);

        Task<MessageHelper> CreateFlatRegistration(FlatInformationDTO obj);
        Task<FlatInformationDTOLandingPaginationDTO> GetFlatRegistrationLandingPagination(string search, DateTime? fromDate, DateTime? toDate, string viewOrder, int pageNo, int pageSize);
        Task<FlatInformationDTO> GetFlatRegistrationInfo(long id);

    }
}
