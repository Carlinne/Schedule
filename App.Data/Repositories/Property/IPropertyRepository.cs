using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Data.Repositories.Property
{
    public interface IPropertyRepository
    {
        Task<DTO.Schedule.Property> GetByIdAsync(object id);
        Task<IEnumerable<DTO.Schedule.Property>> GetAllAsync();
        int Add(DTO.Schedule.Property property);
        void Edit(DTO.Schedule.Property property, int id);
        Task<int> SaveChanges();
    }
}
