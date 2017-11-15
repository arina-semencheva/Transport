using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Transport.DataModel;

namespace Transport.DAO.Test
{
    public class TestClass : ITestClass
    {
        private TransportDBEntities _e;

        public void Smt()
        {
            var a = _e.AspNetUsers.Where(x => x != null).ToList();
        }
    }
}