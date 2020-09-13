using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPIDotNetCore.Api.Models;

namespace WebAPIDotNetCore.Api.Clients
{
    public interface IPotterAPIClient
    {
        Task<IList<HouseModel>> GetHousesAsync(int errorCount = 1);

        Task<HouseModel> GetHouseAsync(string id, int errorCount = 1);
    }
}
