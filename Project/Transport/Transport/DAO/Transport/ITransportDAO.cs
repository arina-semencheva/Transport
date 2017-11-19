using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Transport.Models;

namespace Transport.DAO.Transport
{
    public interface ITransportDAO
    {
        Task<IEnumerable<TransportViewModel>> GetTransports();
        Task CreateTransport(TransportViewModel model);
        Task EditTransport(TransportViewModel model);
        Task DeleteTransport(int transportId);
        Task<TransportViewModel> GetTransportById(int transportId);
    }
}