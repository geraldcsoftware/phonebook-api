using Mapster;
using MapsterMapper;

namespace PhoneBook.Api.Mapping;

public static class MapperRegistrationExtension
{
    public static void AddMapper(this IServiceCollection services)
    {
        var config = new TypeAdapterConfig();
        services.AddSingleton(config);
        services.AddTransient<IMapper, ServiceMapper>();
    }
}