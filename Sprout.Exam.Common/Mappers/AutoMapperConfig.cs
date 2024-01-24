using AutoMapper;
using Sprout.Exam.Common.Mappers.Profiles;

namespace Sprout.Exam.Common.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<EmployeeProfile>();
            });

            return config.CreateMapper();
        }
    }
}
