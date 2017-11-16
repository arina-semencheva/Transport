using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Transport.DataModel;

namespace Transport.DAO.RouteDAO
{
    public class RouteDAO : IRouteDAO
    {
        private TransportDBEntities _edmx;

        public RouteDAO(TransportDBEntities edmx)
        {
            _edmx = edmx;
        }



    }
}