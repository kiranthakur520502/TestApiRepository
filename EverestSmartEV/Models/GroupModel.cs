using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Everest.EverestSmartEV
{
   

    public class GroupModel
    {
        
        
        public int GroupId { get; set; }
       
        public string GroupName { get; set; }
       
        public decimal Capacity { get; set; }

       
    }
}
