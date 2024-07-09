using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public abstract record SensorForManipulationDto
    {
        [Required(ErrorMessage = "Sensor name is a required field.")]]
        public string Name { get; init; }

        [Required(ErrorMessage = "Sensor Type is a required field.")]
        public string Type { get; init; }
    }
}
