using BMS.Core.Helpers;
using BMS.Core.Interfaces.Registration;
using BMS.DTO.Common;
using BMS.DTO.Registration;
using BMS.Infrastructure;
using DataModels;
using LinqToDB;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static LinqToDB.Common.Configuration;

namespace BMS.Core.Service.Registration
{
    public class Registration : IRegistration
    {
        public async Task<MessageHelper> CreateBuildingRegistration(BuildingInformationDTO obj)
        {
            CancellationToken cancellationToken = default;
            using var _context = new AppDB();
            var buildingExist = await _context.TblBuildingInformation.Where(x => x.Name.Trim().ToLower() == obj.BuildingName.Trim().ToLower()).Select(x => x).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
            if (buildingExist != null)
            {
                throw new Exception("Building name already exist.");
            }

            var building = new TblBuildingInformation
            {
                Address = obj.BuildingAddress,
                Attatchment = obj.Attachment,
                CityName = obj.CityName,
                CityId = obj.CityId,
                DteLastActionDateTime = DateTime.UtcNow,
                IntActionBy = obj.ActionById,
                Name = obj.BuildingName,
                NoOfFloor = obj.NoOfFloor,
                Remarks = obj.Remarks,
                Status = (int)(Status.Active),
                DteServerDateTime = DateTime.UtcNow,

            };
            var buildingId = await _context.InsertWithInt32IdentityAsync(building).ConfigureAwait(false);

            var buildingUser = new TblUserBuildingInfo
            {
                BuildingId = buildingId,
                IntUserId = obj.ActionById ?? 0,
                IntActionBy = obj.ActionById ?? 0,
                IsActive = true,
                DteServerDateTime = DateTime.UtcNow,
            };
            await _context.InsertWithInt32IdentityAsync(buildingUser).ConfigureAwait(false);
            return new MessageHelper
            {
                Message = "Created successfully",
                StatusCode = 200
            };
        }

        public async Task<MessageHelper> CreateFlatRegistration(FlatInformationDTO obj)
        {
            using var _context = new AppDB();

            var flatReg = new TblFlatInformation
            {
                Attatchment = obj.Attatchment,
                BuildingId = obj.BuildingId,
                BuildingName = obj.BuildingName,
                Contract = obj.Contract,
                DteLastActionDateTime = DateTime.UtcNow,
                Email = obj.Email,
                FlatHolderTypeId = obj.FlatHolderTypeId,
                FlatHolderTypeName = obj.FlatHolderTypeName,
                FlatNo = obj.FlatNo,
                FlatUserName = obj.FlatUserName,
                FloorNumber = obj.FloorNumber,
                IntActionBy = obj.IntActionBy,
                NumberOfRoom = obj.NumberOfRoom,
                Remarks = obj.Remarks,
                RentAmount = obj.RentAmount,
                Size = obj.Size,
                Status = (int)Status.Active,

            };
            await _context.InsertWithInt32IdentityAsync(flatReg).ConfigureAwait(false);
            return new MessageHelper
            {
                Message = "Created successfully",
                StatusCode = 200
            };
        }

        public async Task<BuildingInformationDTO> GetBuildingRegistrationInfo(long id)
        {
            CancellationToken cancellationToken = default;
            using var _context = new AppDB();
            var data = await (from h in _context.TblBuildingInformation
                              join u in _context.TblUsers on h.IntActionBy equals u.IntUserId
                              where h.Id == id
                              select new BuildingInformationDTO
                              {
                                  Status = h.Status,
                                  ActionById = h.IntActionBy,
                                  ActionByName = u.StrUserName,
                                  Attachment = h.Attatchment,
                                  BuildingAddress = h.Address,
                                  BuildingId = h.Id,
                                  BuildingName = h.Name,
                                  CityId = h.CityId,
                                  CityName = h.CityName,
                                  NoOfFloor = h.NoOfFloor,
                                  Remarks = h.Remarks,

                              }).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
            return data == null ? new BuildingInformationDTO() : data;
        }

        public async Task<BuildingInformationLandingPaginationDTO> GetBuildingRegistrationLandingPagination(string search, DateTime? fromDate, DateTime? toDate, string viewOrder, int pageNo, int pageSize)
        {
            CancellationToken cancellationToken = default;
            using var _context = new AppDB();
            var totalData = await _context.TblBuildingInformation.Where(x => x.Status == 1).Select(x => x.Id).ToListAsync(cancellationToken).ConfigureAwait(false);
            IQueryable<BuildingInformationDTO> data = await Task.FromResult(from h in _context.TblBuildingInformation
                                                                            join u in _context.TblUsers on h.IntActionBy equals u.IntUserId
                                                                            where h.Status == 1
                                                                            && (search == null || h.Name.Contains(search)
                                                                            || h.CityName.Contains(search)
                                                                            || h.Address.Contains(search))
                                                                            select new BuildingInformationDTO
                                                                            {
                                                                                Status = h.Status,
                                                                                ActionById = h.IntActionBy,
                                                                                ActionByName = u.StrUserName,
                                                                                Attachment = h.Attatchment,
                                                                                BuildingAddress = h.Address,
                                                                                BuildingId = h.Id,
                                                                                BuildingName = h.Name,
                                                                                CityId = h.CityId,
                                                                                CityName = h.CityName,
                                                                                NoOfFloor = h.NoOfFloor,
                                                                                Remarks = h.Remarks,

                                                                            });
            if (pageNo <= 0)
                pageNo = 1;
            var itemData = data.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

            long index = (1 + ((pageNo - 1) * pageSize));
            foreach (var item in itemData)
            {
                item.Sl = index;
                index++;
            }

            var itm = new BuildingInformationLandingPaginationDTO
            {
                Data = itemData,
                CurrentPage = pageNo,
                TotalCount = totalData.Count(),
                PageSize = pageSize
            };
            return itm;
        }

        public async Task<object> GetCityList()
        {
            CancellationToken cancellationToken = default;
            using var _context = new AppDB();
            var cityList = await _context.TblCities.Select(x => new
            {
                Value = x.Id,
                Label = x.Name
            }).ToListAsync(cancellationToken).ConfigureAwait(false);
            return cityList;
        }

        public async Task<FlatInformationDTO> GetFlatRegistrationInfo(long id)
        {
            CancellationToken cancellationToken = default;
            using var _context = new AppDB();
            var data = await _context.TblFlatInformation.Where(x => x.Id == id).Select(x => new FlatInformationDTO
            {
                Id = x.Id,
                Attatchment = x.Attatchment,
                BuildingId = x.BuildingId,
                BuildingName = x.BuildingName,
                Contract = x.Contract,
                DteLastActionDateTime = x.DteLastActionDateTime,
                Email = x.Email,
                FlatHolderTypeId = x.FlatHolderTypeId,
                DteServerDateTime = x.DteServerDateTime,
                FlatHolderTypeName = x.FlatHolderTypeName,
                FlatNo = x.FlatNo,
                FlatUserName = x.FlatUserName,
                FloorNumber = x.FloorNumber,
                IntActionBy = x.IntActionBy,
                NumberOfRoom = x.NumberOfRoom,
                Remarks = x.Remarks,
                RentAmount = x.RentAmount,
                Size = x.Size,
                Status = x.Status,
            }).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);

            return data == null ? new FlatInformationDTO() : data;
        }

        public async Task<FlatInformationDTOLandingPaginationDTO> GetFlatRegistrationLandingPagination(string search, DateTime? fromDate, DateTime? toDate, string viewOrder, int pageNo, int pageSize)
        {
            CancellationToken cancellationToken = default;
            using var _context = new AppDB();
            var totalData = await _context.TblFlatInformation.Where(x => x.Status == 1).Select(x => x.Id).ToListAsync(cancellationToken).ConfigureAwait(false);
            IQueryable<FlatInformationDTO> data = await Task.FromResult(from h in _context.TblFlatInformation
                                                                        join u in _context.TblUsers on h.IntActionBy equals u.IntUserId
                                                                        where h.Status == 1
                                                                        && (search == null || h.BuildingName.Contains(search)
                                                                        || h.FlatUserName.Contains(search)
                                                                        || h.FlatHolderTypeName.Contains(search))
                                                                        select new FlatInformationDTO
                                                                        {
                                                                            Status = h.Status,
                                                                            Remarks = h.Remarks,
                                                                            FlatHolderTypeName = h.FlatHolderTypeName,
                                                                            FlatUserName = h.FlatUserName,
                                                                            BuildingName = h.BuildingName,
                                                                            Size = h.Size,
                                                                            Attatchment = h.Attatchment,
                                                                            BuildingId = h.BuildingId,
                                                                            Contract = h.Contract,
                                                                            DteLastActionDateTime = h.DteLastActionDateTime,
                                                                            DteServerDateTime = h.DteServerDateTime,
                                                                            Email = h.Email,
                                                                            FlatHolderTypeId = h.FlatHolderTypeId,
                                                                            FlatNo = h.FlatNo,
                                                                            FloorNumber = h.FloorNumber,
                                                                            Id = h.Id,
                                                                            IntActionBy = h.IntActionBy,
                                                                            NumberOfRoom = h.NumberOfRoom,
                                                                            RentAmount = h.RentAmount,
                                                                            ActionByName = u.StrUserName
                                                                        });
            if (pageNo <= 0)
                pageNo = 1;
            var itemData = data.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

            long index = (1 + ((pageNo - 1) * pageSize));
            foreach (var item in itemData)
            {
                item.Sl = index;
                index++;
            }

            var itm = new FlatInformationDTOLandingPaginationDTO
            {
                Data = itemData,
                CurrentPage = pageNo,
                TotalCount = totalData.Count(),
                PageSize = pageSize
            };
            return itm;
        }

        public async Task<List<BuildingInformationDTO>> GetUserWiseBuildingList(long userId)
        {
            CancellationToken cancellationToken = default;
            using var _context = new AppDB();
            var buildingList = await (from h in _context.TblUserBuildingInfo
                                      join r in _context.TblBuildingInformation on h.BuildingId equals r.Id
                                      where h.IntUserId == userId
                                      && r.Status == 1
                                      select new BuildingInformationDTO
                                      {
                                          Status = r.Status,
                                          BuildingAddress = r.Address,
                                          BuildingId = r.Id,
                                          BuildingName = r.Name,
                                          NoOfFloor = r.NoOfFloor,
                                          CityId = r.CityId,
                                          CityName = r.CityName,
                                          Remarks = r.Remarks,
                                      }).ToListAsync(cancellationToken).ConfigureAwait(false);
            return buildingList;
        }
    }
}
