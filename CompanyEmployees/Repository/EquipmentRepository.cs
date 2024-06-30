﻿using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class EquipmentRepository : RepositoryBase<Equipment>, IEquipmentRepository
    {
        public EquipmentRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {   
        }

        public async Task<IEnumerable<Equipment>> GetAllEquipmentsAsync(bool trackChanges) =>
            await FindAll(trackChanges)
            .OrderBy(e => e.Name)
            .ToListAsync();

        public async Task<Equipment> GetEquipmentAsync(Guid equipmentId, bool trackChanges) =>
            await FindByCondition(e => e.Id.Equals(equipmentId), trackChanges)
            .SingleOrDefaultAsync();

    }
}
