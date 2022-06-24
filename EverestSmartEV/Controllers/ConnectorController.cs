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
    public class ConnectorController : ControllerBase
    {


        private ConnectorService _ConnectorService;
        public ConnectorController(EverestSmartEvContext _context)
        {
            //_context = context;
            _ConnectorService = new ConnectorService(_context);

        }

        // GET: api/<ConnectorController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<Connector> grps = _ConnectorService.GetAllConnectors();
            if (grps.Count() == 0)
            {
                return NotFound();
            }
            else
                return Ok(grps);

        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            Connector grp = _ConnectorService.GetConnectorsById(id);
            if (grp == null)
                return NotFound();

            // Process thingFromDB, blah blah blah
            return Ok(grp); // This will be JSON by default
        }


        [HttpPost]
        public HttpResponseMessage CreateConnector(Everest.EverestSmartEV.ConnectorModel Connector)
        {
            HttpResponseMessage res = new HttpResponseMessage();
            var newConnector = new Connector();
            newConnector.ChargeStationId = Connector.ChargeStationId;
            newConnector.Capacity = Connector.Capacity;
            newConnector.ConnectorId=Connector.ConnectorId;
            if (newConnector.ConnectorId > 0)
            {
                var result = _ConnectorService.UpdateConnector(newConnector);
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
                var result = _ConnectorService.AddConnector(newConnector);
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
        // POST api/<ConnectorController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}



        // DELETE api/<ConnectorController>/5
        [HttpDelete("{id}")]
        public HttpResponseMessage Delete(int id)
        {
            HttpResponseMessage res = new HttpResponseMessage();

            if (id > 0)
            {
                var result = _ConnectorService.DeleteConnector(id);
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
