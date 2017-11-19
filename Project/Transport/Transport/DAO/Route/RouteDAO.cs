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
            try
            {
                var routeEntity = new Route
                {
                    FirstStop = model.FirstStop,
                    LastStop = model.LastSport
                };
                _edmx.Route.Add(routeEntity);
            }
            catch (Exception ex)
            {

            }
            await _edmx.SaveChangesAsync();
        }

        public async Task DeleteRoute(int routeId)
        {
            try
            {
                var routeEntity = await _edmx.Route
                .FirstOrDefaultAsync(x => x.RouteId == routeId);
                _edmx.Route.Remove(routeEntity);
            }
            catch (Exception ex)
            {

            }
            await _edmx.SaveChangesAsync();
        }

        public async Task EditRoute(RouteViewModel model)
        {
            try
            {
                var routeEntity = await _edmx.Route.FirstOrDefaultAsync(x => x.RouteId == model.RouteId);
                routeEntity.FirstStop = model.FirstStop;
                routeEntity.LastStop = model.LastSport;
            }
            catch (Exception ex)
            {

            }
            await _edmx.SaveChangesAsync();
        }

        public async Task<RouteViewModel> GetRouteById(int routeId)
        =>
            await
            (
            from route in _edmx.Route
            where route.RouteId == routeId
            select new RouteViewModel
            {
                RouteId = route.RouteId,
                FirstStop = route.FirstStop,
                LastSport = route.LastStop
            }
            )
            .FirstOrDefaultAsync();

    }
}