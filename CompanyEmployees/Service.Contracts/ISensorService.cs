using Entities.Responses;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface ISensorService
    {
        Task<ApiBaseResponse> GetAllSensorsAsync(Guid equipmentId, bool trackChanges);
        
        Task<ApiBaseResponse> GetSensorAsync(Guid equipmentId, Guid id, bool trackChanges);

        Task<SensorDto> CreateSensorForEquipmentAsync(Guid equipmentId, SensorForCreationDto sensor,bool trackChanges);

        Task DeleteSensorForEquipmentAsync(Guid equipmentId, Guid id, bool trackChanges);

        Task UpdateSensorForEquipmentAsync(Guid equipmentId, Guid id, SensorForUpdateDto sensor, bool cmpTrackChanges, bool empTrackChanges);
    }
}
