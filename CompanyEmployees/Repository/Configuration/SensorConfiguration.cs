using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Configuration
{
    public class SensorConfiguration : IEntityTypeConfiguration<Sensor>
    {
        public void Configure(EntityTypeBuilder<Sensor> builder)
        {
            builder.HasData(
                new Sensor
                {
                    Id = Guid.NewGuid(),
                    Name = "Sensor1",
                    Type = "Pressure",
                    EquipmentId = new Guid("d2d4c053-49b6-410c-bc78-2d54a9991825")
                }
            );
        }
    }
}
