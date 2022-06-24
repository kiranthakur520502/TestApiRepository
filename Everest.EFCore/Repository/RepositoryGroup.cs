using Everest.EFCore.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everest.EFCore.Repository
{
    public class RepositoryGroup:IRepository<Group>
    {
        EverestSmartEvContext _dbContext;
       
        public RepositoryGroup(EverestSmartEvContext applicationDbContext
            )
        {
            _dbContext = applicationDbContext;
        }
        public async Task<Group> Create(Group _object)
        {
            var obj = await _dbContext.Groups.AddAsync(_object);
            _dbContext.SaveChanges();
            return obj.Entity;
        }

        public void Delete(Group _object)
        {
            _dbContext.Remove(_object);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Group> GetAll()
        {
            try
            {
                return _dbContext.Groups.ToList();
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        public Group GetById(int Id)
        {
            return _dbContext.Groups.Where(x =>  x.GroupId == Id).FirstOrDefault();
        }

        public void Update(Group _object)
        {
            _dbContext.Groups.Update(_object);
            _dbContext.SaveChanges();
        }
    }
}

