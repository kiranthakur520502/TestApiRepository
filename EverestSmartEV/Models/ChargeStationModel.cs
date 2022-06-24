using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Everest.EverestSmartEV
{
   

    public class ChargeStationModel
    {
        
       

        public int ChargeStationId { get; set; }

        [Required]
        public string ChargeStationName { get; set; }
        public int GroupId { get; set; }
      
        //[Range(1, 5, ErrorMessage = "Please enter a value between 1 and 5")]
        //public decimal TotalConnections { get; set; }

    }
}
