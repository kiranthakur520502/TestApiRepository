using Everest.EFCore;
using Everest.EFCore.Interface;
using Everest.EFCore.Repository;

using EverestSmartEVServices;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EverestSmartEV
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {

        
        private GroupService _groupService;
        public GroupController(EverestSmartEvContext _context)
        {
            //_context = context;
            _groupService = new GroupService(_context);

        }

        // GET: api/<GroupController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable < Group > grps= _groupService.GetAllGroups();
            if (grps.Count() == 0)
            {
                return NotFound();
            }else 
                return Ok(grps);
           // return _context.Groups.ToList();
           // return new string[] { "value1", "value2" };
        }

        // GET api/<GroupController>/5
        //[HttpGet("{id}")]
        //public Group  Get(int id)
        //{
        //    Group grp= _groupService.GetGroupsById(id);
        //    return grp;
        //}
        [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
            Group grp = _groupService.GetGroupsById(id);
            if (grp == null)
            return NotFound();

        // Process thingFromDB, blah blah blah
        return Ok(grp); // This will be JSON by default
    }


        [HttpPost]
        public HttpResponseMessage CreateGroup(Everest.EverestSmartEV.GroupModel group)
        {
            HttpResponseMessage res = new HttpResponseMessage();
            var newGroup =new  Group();
            newGroup.GroupName = group.GroupName;
            newGroup.GroupId = group.GroupId;
            newGroup.Capacity = group.Capacity;
            if (newGroup.GroupId > 0)
            {
                var result = _groupService.UpdateGroup(newGroup);
                if (result)
                {
                    res.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    res.StatusCode = HttpStatusCode.BadRequest;
                }
            }
            else
            {
                var result = _groupService.AddGroup(newGroup);
                if (result.Status == TaskStatus.Faulted)
                {
                    res.StatusCode = HttpStatusCode.BadRequest;
                }
                else
                {
                    res.StatusCode = HttpStatusCode.OK;
                }
            }
            return res;
           
        }
        // POST api/<GroupController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        // PUT api/<GroupController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<GroupController>/5
        [HttpDelete("{id}")]
        public HttpResponseMessage Delete(int id)
        {
            HttpResponseMessage res = new HttpResponseMessage();
           
            if (id > 0)
            {
                var result = _groupService.DeleteGroup(id);
                if (result)
                {
                    res.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    res.StatusCode = HttpStatusCode.BadRequest;
                }
            }
            return res;
        }
    }
}
