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
            await _edmx.Routes.Select(x => new RouteViewModel
            {
                RouteId = x.RouteId,
                FirstStop = x.FirstStop,
                LastSport = x.LastStop,
                Transport = new TransportViewModel
                {
                    TransportId = x.TransportId,
                    TransportName = x.Transport.Name
                },
                Person = new PersonViewModel
                {
                    PersonId = x.Transport.People.FirstOrDefault(y => y.TransportId == x.TransportId).PersonId,
                    Name = x.Transport.People.FirstOrDefault(y => y.TransportId == x.TransportId).Name,
                    Surname = x.Transport.People.FirstOrDefault(y => y.TransportId == x.TransportId).Surname,
                    PersonType = new PersonTypeViewModel
                    {
                        PersonTypeId = x.Transport.People.FirstOrDefault(y => y.TransportId == x.TransportId).PersonTypeId,
                        PersonTypeName = x.Transport.People.FirstOrDefault(y => y.TransportId == x.TransportId).PersonType.Name
                    }
                },
                TicketCount = x.TicketCount
            })
            .ToListAsync();



        public async Task CreateRoute(RouteViewModel model)
        {
            try
            {
                var routeEntity = new Route
                {
                    FirstStop = model.FirstStop,
                    LastStop = model.LastSport,
                    TicketCount = model.TicketCount,
                    TransportId = model.Transport.TransportId.Value
                };
                _edmx.Routes.Add(routeEntity);
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
                var routeEntity = await _edmx.Routes
                .FirstOrDefaultAsync(x => x.RouteId == routeId);
                _edmx.Routes.Remove(routeEntity);
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
                var routeEntity = await _edmx.Routes.FirstOrDefaultAsync(x => x.RouteId == model.RouteId);
                routeEntity.FirstStop = model.FirstStop;
                routeEntity.LastStop = model.LastSport;
                routeEntity.TransportId = model.Transport.TransportId.Value > 0 ? model.Transport.TransportId.Value : routeEntity.TransportId;
                //routeEntity.Transport.People.PersonId = model.Person.PersonId > 0 ? model.Person.PersonId : routeEntity.Transport.PersonId;
                routeEntity.TicketCount = model.TicketCount;
                await _edmx.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }

        public async Task<RouteViewModel> GetRouteById(int routeId)
        =>
            (await GetRoutes()).FirstOrDefault(x => x.RouteId == routeId);

    }
}