using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class SensorRepository : RepositoryBase<Sensor>, ISensorRepository
    {
        public SensorRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {    
        }

        public async Task<IEnumerable<Sensor>> GetAllSensorsAsync(Guid equipmentId, bool trackChanges) =>
            await FindByCondition(s => s.EquipmentId.Equals(equipmentId), trackChanges)
            .OrderBy(s => s.Name)
            .ToListAsync();

        public async Task<Sensor> GetSensorAsync(Guid equipmentId, Guid sensorId, bool trackChange) =>
            await FindByCondition(s => s.EquipmentId.Equals(equipmentId) && s.Id.Equals(sensorId), trackChange)
            .SingleOrDefaultAsync();

        public void CreateSensorForEquipment(Guid equipmentId, Sensor sensor)
        {
            sensor.EquipmentId = equipmentId;
            Create(sensor);
        }

        public void DeleteSensor(Sensor sensor) => Delete(sensor);
    }
}
