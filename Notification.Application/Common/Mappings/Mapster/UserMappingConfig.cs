namespace Notification.Application.Common.Mappings.Mapster;


public static class UserMappingConfig
{
    public static void Register()
    {
        //TypeAdapterConfig<User, UserDto>
        //    .NewConfig()
        //    .Map(dest => dest.Profile, src => src.Profile)
        //    .Map(static dest => dest.Profile.City, src => src.Profile.City)
        //    .Map(static dest => dest.Profile.City.Province, src => src.Profile.City.Province)
        //    .Map(dest => dest.InstallerProfile, src => src.InstallerProfile)
        //    .Map(dest => dest.CustomerProfile, src => src.CustomerProfile)
        //    .Map(dest => dest.AgentProfile, src => src.AgentProfile);
    }
}