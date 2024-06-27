using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Equipment
    {
        [Column("EquipmentId")]
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "Equipment name is a required field.")]
        [MaxLength(20, ErrorMessage = "Maximum length for the Name is 20 characters.")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Status is a required field.")]
        public string Status { get; set; }

        [ForeignKey(nameof(Company))]
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
        
        public ICollection<Sensor> Sensors { get; set; }    
    }
}
