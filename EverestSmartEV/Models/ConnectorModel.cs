using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Everest.EverestSmartEV
{
   

    public class ConnectorModel
    {
        public int ConnectorId { get; set; }

        public int ChargeStationId { get; set; }
      

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value greater than 0")]
        public decimal Capacity { get; set; }


    }
}
