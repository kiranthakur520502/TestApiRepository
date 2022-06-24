using Everest.EFCore;
using Everest.EFCore.Interface;
using Everest.EFCore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverestSmartEVServices
{

    public class ConnectorService
    {
        private readonly IRepository<ChargeStation> _chargeStation;
        private readonly IRepository<Group> _group;
        private readonly IRepository<Connector> _connector;
        EverestSmartEvContext _context;
        public ConnectorService(EverestSmartEvContext context)
        {
            _group = new RepositoryGroup(context);
            _chargeStation = new RepositoryChargeStation(context); //chargeStation();
            _connector = new RepositoryConnector(context);
            _context = context;
        }
        public Connector GetConnectorsById(int connectorId)
        {
            return _connector.GetAll().Where(x => x.ConnectorId == connectorId).FirstOrDefault();
        }
        public IEnumerable<Connector> GetAllConnectors()
        {
            return _connector.GetAll().ToList();
        }

        public async Task<Connector> AddConnector(Connector connector)
        {
           decimal capacity=  _group.GetById( _chargeStation.GetById(connector.ChargeStationId).GroupId ).Capacity;
          if( _connector.GetAll().Where(e=>e.ChargeStationId == connector.ChargeStationId).ToList().Count >= 5)
            {
                throw new ArgumentException(String.Format("Maximum connector's count reached"));
            }

            if (connector.Capacity < capacity)
            {
                throw new ArgumentException(String.Format("Capacity should be greater or equal to ", capacity.ToString()));
            }
            return await _connector.Create(connector);

        }
        //Delete Connector   
        public bool DeleteConnector(int ConnectorId)
        {

            try
            {
                var DataList = _connector.GetAll().Where(x => x.ConnectorId == ConnectorId).ToList();
                foreach (var item in DataList)
                {
                    _connector.Delete(item);
                }
                return true;
            }
            catch (Exception)
            {
                return true;
            }

        }
        //Update Connector Details  
        public decimal GetTotalCapacity(Connector connector)
        {

            var cs = _connector.GetById(connector.ConnectorId).Capacity;
            var query = from g in _context.Set<Group>()
                        join p in _context.Set<ChargeStation>()
                            on g.GroupId equals p.GroupId
                        select new
                        {
                            p.ChargeStationId
                        };
            var _cons = from co in _context.Set<Connector>()
                        where query.Select(x => x.ChargeStationId).Contains(co.ChargeStationId)
                        select new { co.Capacity };
            var capacity = _cons.Sum(e => e.Capacity) - _connector.GetById(connector.ConnectorId).Capacity + connector.Capacity;
            return capacity;
        }
        public bool UpdateConnector(Connector connector)
        {
            try
            {

                decimal _capacity = _group.GetById(_chargeStation.GetById(connector.ChargeStationId).GroupId).Capacity;
                var capacity= GetTotalCapacity(connector); 

                if ( capacity<_capacity)
                {
                    throw new ArgumentException(String.Format("Capacity should be greater or equal to ", capacity.ToString()));
                }

                if (_connector.GetAll().Where(e => e.ChargeStationId == connector.ChargeStationId && e.ConnectorId!=connector.ConnectorId).ToList().Count >= 5)
                {
                    throw new ArgumentException(String.Format("Maximum connector's level reached"));
                }

                var DataList = _connector.GetAll().Where(x => x.ConnectorId==connector.ConnectorId).ToList();
               
                    _connector.Update(connector);
               
                return true;
            }
            catch (Exception)
            {
                return true;
            }
        }

    }
   
}
