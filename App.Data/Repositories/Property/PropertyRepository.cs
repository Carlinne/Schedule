using App.Data.Repositories.BaseRepository;
using AutoMapper;

namespace App.Data.Repositories.Property
{
    public class PropertyRepository : BaseRepository<Models.Schedule.Property, DTO.Schedule.Property>, IPropertyRepository
    {
        public PropertyRepository(ApplicationDBContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
