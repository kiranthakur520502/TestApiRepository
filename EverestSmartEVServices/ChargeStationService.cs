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

    public class ChargeStationService
    {
        private readonly IRepository<ChargeStation> _chargeStation;
        private readonly IRepository<Group> _group;
        private readonly IRepository<Connector> _connector;

        public ChargeStationService(EverestSmartEvContext _context)
        {            
            _group = new RepositoryGroup(_context);
            _chargeStation = new RepositoryChargeStation(_context); //chargeStation();
            _connector = new  RepositoryConnector(_context);   
        }
        public ChargeStation GetChargeStationsById(int chargeStationId)
        {
            return _chargeStation.GetAll().Where(x => x.ChargeStationId == chargeStationId).FirstOrDefault();
        }
        public IEnumerable<ChargeStation> GetAllChargeStations()
        {
            return _chargeStation.GetAll().ToList();
        }

        public async Task<ChargeStation> AddChargeStation(ChargeStation chargeStation)
        {
            if (chargeStation.ChargeStationName == "")
            {
                throw new Exception("ChargeStation Name is requird");
            }

            return await _chargeStation.Create(chargeStation);
        }
        //Delete ChargeStation   
        public bool DeleteChargeStation(int ChargeStationId)
        {

            try
            {
                var DataList = _chargeStation.GetAll().Where(x => x.ChargeStationId == ChargeStationId).ToList();
                var ConnectorList=_connector.GetAll().Where(_x => _x.ChargeStationId == ChargeStationId).ToList();
                foreach (var item in ConnectorList)
                {
                    _connector.Delete(item);
                }
                foreach (var item in DataList)
                {
                    _chargeStation.Delete(item);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        //Update ChargeStation Details  
        public bool UpdateChargeStation(ChargeStation chargeStation)
        {
            try
            {

                if (_chargeStation.GetAll().Where(_x => _x.GroupId != chargeStation.GroupId && _x.ChargeStationId==chargeStation.ChargeStationId).ToList().Count > 0)
                {
                    throw new ArgumentException(String.Format("Chargestation is in another group"));
                }

              //  var DataList = _chargeStation.GetAll().Where(x => x.ChargeStationId==chargeStation.ChargeStationId).ToList();
                
                    _chargeStation.Update(chargeStation);
               
                return true;
            }
            catch (Exception)
            {
                return true;
            }
        }

    }
   
}
