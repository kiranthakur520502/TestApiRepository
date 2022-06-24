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
    public class ChargeStationController : ControllerBase
    {


        private ChargeStationService _chargeStationService;
        public ChargeStationController(EverestSmartEvContext _context)
        {
            //_context = context;
            _chargeStationService = new ChargeStationService(_context);

        }

        // GET: api/<ChargeStationController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<ChargeStation> grps = _chargeStationService.GetAllChargeStations();
            if (grps.Count() == 0)
            {
                return NotFound();
            }
            else
                return Ok(grps);
            // return _context.ChargeStations.ToList();
            // return new string[] { "value1", "value2" };
        }

        // GET api/<ChargeStationController>/5
        //[HttpGet("{id}")]
        //public ChargeStation  Get(int id)
        //{
        //    ChargeStation grp= _chargeStationService.GetChargeStationsById(id);
        //    return grp;
        //}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            ChargeStation grp = _chargeStationService.GetChargeStationsById(id);
            if (grp == null)
                return NotFound();

            // Process thingFromDB, blah blah blah
            return Ok(grp); // This will be JSON by default
        }


        [HttpPost]
        public HttpResponseMessage CreateChargeStation(Everest.EverestSmartEV.ChargeStationModel ChargeStation)
        {
            HttpResponseMessage res = new HttpResponseMessage();
            var newChargeStation = new ChargeStation();
            newChargeStation.ChargeStationName = ChargeStation.ChargeStationName;
            newChargeStation.ChargeStationId = ChargeStation.ChargeStationId;
            newChargeStation.GroupId=ChargeStation.GroupId;
            if (newChargeStation.ChargeStationId > 0)
            {
                var result = _chargeStationService.UpdateChargeStation(newChargeStation);
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
                var result = _chargeStationService.AddChargeStation(newChargeStation);
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

        // DELETE api/<ChargeStationController>/5
        [HttpDelete("{id}")]
        public HttpResponseMessage Delete(int id)
        {
            HttpResponseMessage res = new HttpResponseMessage();

            if (id > 0)
            {
                var result = _chargeStationService.DeleteChargeStation(id);
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
