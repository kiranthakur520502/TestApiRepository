using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Everest.EFCore
{
   

    [Table("Connectors")]
    public class Connector
    {
      
        [Key]
        public int ConnectorId { get; set; }

        [Required]
        public int ChargeStationId { get; set; }
        public ChargeStation ChargeStation { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value greater than 0")]
        public decimal Capacity { get; set; }

    }
}
