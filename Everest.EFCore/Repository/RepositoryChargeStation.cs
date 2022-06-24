using Everest.EFCore.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everest.EFCore.Repository
{
    public class RepositoryChargeStation:IRepository<ChargeStation>
    {
        EverestSmartEvContext _dbContext;
        public RepositoryChargeStation(EverestSmartEvContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }
        public async Task<ChargeStation> Create(ChargeStation _object)
        {
            var obj = await _dbContext.ChargeStations.AddAsync(_object);
            _dbContext.SaveChanges();
            return obj.Entity;
        }

        public void Delete(ChargeStation _object)
        {
            _dbContext.Remove(_object);
            _dbContext.SaveChanges();
        }

        public IEnumerable<ChargeStation> GetAll()
        {
            try
            {
                return _dbContext.ChargeStations.ToList();
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        public ChargeStation GetById(int Id)
        {
            return _dbContext.ChargeStations.Where(x =>  x.ChargeStationId == Id).FirstOrDefault();
        }

        public void Update(ChargeStation _object)
        {
            _dbContext.ChargeStations.Update(_object);
            _dbContext.SaveChanges();
        }
    }
}

