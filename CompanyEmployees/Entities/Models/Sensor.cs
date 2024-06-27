using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Sensor
    {
        [Column("SensorId")]
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "Sensor name is a required field.")]
        [MaxLength(20, ErrorMessage = "Maximum length for the Name is 20 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Status is a required field.")]
        public string Type { get; set; }

        [ForeignKey(nameof(Equipment))]
        public Guid EquipmentId { get; set; }

        public Equipment? Equipment { get; set; }
    }
}
