using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Everest.EFCore
{
   

    [Table("ChargeStations")]
    public class ChargeStation
    {
      
        [Key]
        public int ChargeStationId { get; set; }

        [Required]
        public string ChargeStationName { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        

    }
}
