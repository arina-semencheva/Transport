using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Transport.DataModel;
using Transport.Models;

namespace Transport.DAO.RouteDAO
{
    public class RouteDAO : IRouteDAO
    {
        private TransportDBEntities _edmx = new TransportDBEntities();

        public RouteDAO(TransportDBEntities edmx)
        {
            _edmx = edmx;
        }

        public RouteDAO() { }

        public async Task<IEnumerable<RouteViewModel>> GetRoutes() =>
            await _edmx.Route.Select(x => new RouteViewModel
            {
                RouteId = x.RouteId,
                FirstStop = x.FirstStop,
                LastSport = x.LastStop
            })
            .ToListAsync();


       
        public async Task CreateRoute(RouteViewModel model)
        {
            var routeEntity = new Route
            {
                FirstStop = model.FirstStop,
                LastStop = model.LastSport
            };
            _edmx.Route.Add(routeEntity);
            await _edmx.SaveChangesAsync();
        }

        public async Task DeleteRoute(int routeId)
        {
            var routeEntity = await _edmx.Route
                .FirstOrDefaultAsync(x => x.RouteId == routeId);
            _edmx.Route.Remove(routeEntity);
            await _edmx.SaveChangesAsync();
        }

        public async Task EditRoute(RouteViewModel model)
        {
            var routeEntity = await _edmx.Route.FirstOrDefaultAsync(x => x.RouteId == model.RouteId);
            routeEntity.FirstStop = model.FirstStop;
            routeEntity.LastStop = model.LastSport;
            await _edmx.SaveChangesAsync();
        }

    }
}