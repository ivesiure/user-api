using AutoMapper;
using User.API.Models;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AutoMapperExtensions
    {
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserViewModel, User.API.Entities.User>();
                cfg.CreateMap<User.API.Entities.User, UserViewModel>();
            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}