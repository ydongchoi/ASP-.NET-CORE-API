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
    public class EquipmentConfiguration : IEntityTypeConfiguration<Equipment>
    {
        public void Configure(EntityTypeBuilder<Equipment> builder)
        {
            builder.HasData(
                new Equipment
                {
                    Id = new Guid("d2d4c053-49b6-410c-bc78-2d54a9991825"),
                    Name = "Equip1",
                    Status = "InOperation",
                    CompanyId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
                }, 
                new Equipment
                {
                    Id = new Guid("a3e4c053-49b6-410c-bc78-2d54a9991823"),
                    Name = "Equip2",
                    Status = "Stop",
                    CompanyId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
                }
            );
        }
    }
}
