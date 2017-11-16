using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Transport.Models;

namespace Transport.DAO.RouteDAO
{
    public interface IRouteDAO
    {
        Task<IEnumerable<RouteViewModel>> GetRoutes();
        Task CreateRoute(RouteViewModel model);
        Task EditRoute(RouteViewModel model);
        Task DeleteRoute(int routeId);
        Task<RouteViewModel> GetRouteById(int routeId);
    }
}