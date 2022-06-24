using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Everest.EFCore
{
   

    [Table("Groups")]
    public class Group
    {
        
        [Key,Required]
        public int GroupId { get; set; }
        [Required]
        public string GroupName { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public decimal Capacity { get; set; }

       
    }
}
