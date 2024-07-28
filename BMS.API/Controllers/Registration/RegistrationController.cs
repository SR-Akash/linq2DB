using BMS.Core.Helpers;
using BMS.Core.Interfaces.Registration;
using BMS.Core.Interfaces.Security;
using BMS.Core.Service.Security;
using BMS.DTO.Response;
using BMS.Localization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;
using System;
using BMS.DTO.Registration;

namespace BMS.API.Controllers.Registration
{
    [Route("bms/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {

        //private readonly OpenIddictApplicationManager<ApplicationClient> _applicationClientManager;
        private readonly IRegistration _IRepository;


        public RegistrationController(IRegistration IRepository)
        {
            _IRepository = IRepository;
        }

        [HttpGet]
        [Route("GetCityList")]
        public async Task<IActionResult> GetCityList()
        {
            try
            {
                var msg = await _IRepository.GetCityList();
                return Ok((msg));
            }
            catch (Exception ex)
            {
                return new ObjectResult(SetResponses.SetErrorResponse(ex.Message));
            }

        }

        [HttpPost]
        [Route("CreateBuildingRegistration")]
        public async Task<IActionResult> CreateBuildingRegistration(BuildingInformationDTO obj)
        {
            try
            {
                var msg = await _IRepository.CreateBuildingRegistration(obj);
                return Ok((msg));
            }
            catch (Exception ex)
            {
                return new ObjectResult(SetResponses.SetErrorResponse(ex.Message));
            }

        }

        [HttpGet]
        [Route("GetBuildingRegistrationLandingPagination")]
        public async Task<IActionResult> GetBuildingRegistrationLandingPagination(string search, DateTime? fromDate, DateTime? toDate, string viewOrder, int pageNo, int pageSize)
        {
            try
            {
                var msg = await _IRepository.GetBuildingRegistrationLandingPagination(search, fromDate, toDate, viewOrder, pageNo, pageSize);
                return Ok((msg));
            }
            catch (Exception ex)
            {
                return new ObjectResult(SetResponses.SetErrorResponse(ex.Message));
            }

        }

        [HttpGet]
        [Route("GetBuildingRegistrationInfo")]
        public async Task<IActionResult> GetBuildingRegistrationInfo(long id)
        {
            try
            {
                var msg = await _IRepository.GetBuildingRegistrationInfo(id);
                return Ok((msg));
            }
            catch (Exception ex)
            {
                return new ObjectResult(SetResponses.SetErrorResponse(ex.Message));
            }

        }

        [HttpGet]
        [Route("GetUserWiseBuildingList")]
        public async Task<IActionResult> GetUserWiseBuildingList(long userId)
        {
            try
            {
                var msg = await _IRepository.GetUserWiseBuildingList(userId);
                return Ok((msg));
            }
            catch (Exception ex)
            {
                return new ObjectResult(SetResponses.SetErrorResponse(ex.Message));
            }

        }


        [HttpPost]
        [Route("CreateFlatRegistration")]
        public async Task<IActionResult> CreateFlatRegistration(FlatInformationDTO obj)
        {
            try
            {
                var msg = await _IRepository.CreateFlatRegistration(obj);
                return Ok((msg));
            }
            catch (Exception ex)
            {
                return new ObjectResult(SetResponses.SetErrorResponse(ex.Message));
            }

        }

        [HttpGet]
        [Route("GetFlatRegistrationLandingPagination")]
        public async Task<IActionResult> GetFlatRegistrationLandingPagination(string search, DateTime? fromDate, DateTime? toDate, string viewOrder, int pageNo, int pageSize)
        {
            try
            {
                var msg = await _IRepository.GetFlatRegistrationLandingPagination(search, fromDate, toDate, viewOrder, pageNo, pageSize);
                return Ok((msg));
            }
            catch (Exception ex)
            {
                return new ObjectResult(SetResponses.SetErrorResponse(ex.Message));
            }

        }

        [HttpGet]
        [Route("GetFlatRegistrationInfo")]
        public async Task<IActionResult> GetFlatRegistrationInfo(long id)
        {
            try
            {
                var msg = await _IRepository.GetFlatRegistrationInfo(id);
                return Ok((msg));
            }
            catch (Exception ex)
            {
                return new ObjectResult(SetResponses.SetErrorResponse(ex.Message));
            }

        }

    }
}
