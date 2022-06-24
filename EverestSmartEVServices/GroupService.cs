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

    public class GroupService
    {
        private readonly IRepository<Group> _group;
        private readonly IRepository<ChargeStation> _chargeStation;
       // private readonly IRepository<Connector> _connector;
       // private readonly EverestSmartEvContext _context;
        public GroupService(EverestSmartEvContext _context)
        {

           
            _group = new RepositoryGroup(_context);
            _chargeStation = new RepositoryChargeStation(_context); //chargeStation();
            //_connector = new  RepositoryConnector(_context);    
        }
        public Group GetGroupsById(int GroupId)
        {
            return _group.GetAll().Where(x => x.GroupId == GroupId).FirstOrDefault();
        }
        public IEnumerable<Group> GetAllGroups()
        {
            return _group.GetAll().ToList();
        }

        public async Task<Group> AddGroup(Group group)
        {
            if (group.GroupName == "")
            {
                // throw new ArgumentException("Group Name is required");
                throw new Exception("Group Name is required");
            }
            if (group.Capacity <=0)
            {
                // throw new ArgumentException("Capacity should be greater than 0");
                throw new Exception("Capacity should be greater than 0");
            }
            return await _group.Create(group);
        }
        //Delete Group   
        public bool DeleteGroup(int groupId)
        {

            try
            {
                var DataList = _group.GetAll().Where(x => x.GroupId == groupId).ToList();
                var ChargeStationList=_chargeStation.GetAll().Where(x=>x.GroupId == groupId).ToList();
                foreach (var item in ChargeStationList)
                {
                    _chargeStation.Delete(item);
                }

                foreach (var item in DataList)
                {
                    _group.Delete(item);
                }


                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        //Update Group Details  
        public bool UpdateGroup(Group group)
        {
            try
            {
              
                if (group.Capacity <= 0)
                {
                   
                    throw new Exception("Capacity should be greater than 0");
                }
                _group.Update(group);
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
   
}
