using Everest.EFCore.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everest.EFCore.Repository
{
    public class RepositoryConnector:IRepository<Connector>
    {
        EverestSmartEvContext _dbContext;
        public RepositoryConnector(EverestSmartEvContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }
        public async Task<Connector> Create(Connector _object)
        {
            var obj = await _dbContext.Connectors.AddAsync(_object);
            _dbContext.SaveChanges();
            return obj.Entity;
        }

        public void Delete(Connector _object)
        {
            _dbContext.Remove(_object);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Connector> GetAll()
        {
            try
            {
                return _dbContext.Connectors.ToList();
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        public Connector GetById(int Id)
        {
            return _dbContext.Connectors.Where(x =>  x.ConnectorId == Id).FirstOrDefault();
        }

        public void Update(Connector _object)
        {
            _dbContext.Connectors.Update(_object);
            _dbContext.SaveChanges();
        }
    }
}

