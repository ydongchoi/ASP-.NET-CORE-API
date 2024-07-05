using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ISensorRepository
    {
        Task<IEnumerable<Sensor>> GetAllSensorsAsync(Guid equipmentId, bool trackChanges);
        
        Task<Sensor> GetSensorAsync(Guid equipmentId, Guid sensorId, bool trackChange);

        void CreateSensorForEquipment(Guid equipmentId, Sensor sensor);

        void DeleteSensor(Sensor sensor);
    }
}
