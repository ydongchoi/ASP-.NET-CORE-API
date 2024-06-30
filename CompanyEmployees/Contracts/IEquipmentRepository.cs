using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IEquipmentRepository
    {
        Task<IEnumerable<Equipment>> GetAllEquipmentsAsync(bool trackChange);

        Task<Equipment> GetEquipmentAsync(Guid equipmentId, bool trackChange);

        Task<IEnumerable<Equipment>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        void CreateEquipment(Equipment equipment);
    }
}
