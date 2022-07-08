using System.Text.Json;
using Mapster;
using MapsterMapper;

namespace PhoneBook.Api.Mapping;

public static class MapperRegistrationExtension
{
    private static readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    };

    public static void AddMapper(this IServiceCollection services)
    {
        var config = new TypeAdapterConfig();

        config.ForType<Data.Models.PhoneBookEntry, DTOs.PhoneBookEntry>()
              .Map(d => d.PhoneNumbers, src => JsonSerializer.Deserialize<string[]>(src.PhoneNumber!, _jsonSerializerOptions));

        services.AddSingleton(config);
        services.AddTransient<IMapper, ServiceMapper>();
    }
}