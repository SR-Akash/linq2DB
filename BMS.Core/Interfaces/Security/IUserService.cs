
using BMS.DTO.Common;
using BMS.DTO.Common.Security;
using DataModels;
using System.Threading.Tasks;

namespace BMS.Core.Interfaces.Security
{
    public interface IUserService
    {
        Task<AuthDTO> LoginWithGmail(string token);  
    }
}
