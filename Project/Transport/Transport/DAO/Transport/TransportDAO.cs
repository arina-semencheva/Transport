using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Transport.DAO.Transport;
using Transport.DataModel;
using Transport.Models;

namespace Transport.DAO
{
    public class TransportDAO : ITransportDAO
    {

        private TransportDBEntities _edmx = new TransportDBEntities();

        public TransportDAO()
        {

        }

        public TransportDAO(TransportDBEntities edm)
        {
            _edmx = edm;
        }

        public async Task<IEnumerable<TransportViewModel>> GetTransports()
        {
            var transports = await (from transport in _edmx.Transport
                                    select new TransportViewModel
                                    {
                                        TransportId = transport.TransportId,
                                        TransportName = transport.Name,
                                        EngineNumber = transport.EngineNumber,
                                        FuelId = transport.FuelId,
                                        Fuel = _edmx.Fuel.FirstOrDefault(y => y.FueldId == transport.FuelId).FuelName                                       
                                    })
                              .ToListAsync();
            return transports;
        }

        public async Task CreateTransport(TransportViewModel model)
        {
            DataModel.Transport transportEntity = new DataModel.Transport
            {
                Name = model.TransportName,
                EngineNumber = model.EngineNumber,
                FuelId = model.FuelId,
                TransportTypeId = model.TransportType.TransportTypeId
            };
            _edmx.Transport.Add(transportEntity);
            await _edmx.SaveChangesAsync();
        }

        public async Task DeleteTransport(int transportId)
        {
            var transportentity = await _edmx.Transport.FirstOrDefaultAsync(x => x.TransportId == transportId);
            if (transportentity != null)
                _edmx.Transport.Remove(transportentity);
            else
                throw new NotSupportedException("Ouuos!!!");
            await _edmx.SaveChangesAsync();
        }

        public async Task EditTransport(TransportViewModel model)
        {
            var transportEntity = await _edmx.Transport.FirstOrDefaultAsync(x => x.TransportId == model.TransportId);
            if (transportEntity == null)
                throw new Exception("Ouups!!!");
            transportEntity.Name = model.TransportName;
            transportEntity.FuelId = model.FuelId;
            transportEntity.TransportTypeId = model.TransportType.TransportTypeId;
            transportEntity.EngineNumber = model.EngineNumber;
            transportEntity.RouteId = model.RouteId;
            await _edmx.SaveChangesAsync();
        }


        public async Task<TransportViewModel> GetTransportById(int transportId)
        {
            var transportEntity = await _edmx.Transport.Where(x => x.TransportId == transportId)
                .Select(x => new TransportViewModel
                {
                    TransportId = x.TransportId,
                    TransportName = x.Name,
                    EngineNumber = x.EngineNumber,
                    FuelId = x.FuelId,
                    RouteId = x.RouteId
                })
                .FirstOrDefaultAsync();
            if (transportEntity == null)
                throw new Exception("Ouuupss!!!");
            return transportEntity;
        }

    }
}