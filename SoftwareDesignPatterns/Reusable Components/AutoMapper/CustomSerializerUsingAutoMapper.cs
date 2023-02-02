namespace SoftwareDesignPatterns
{
    using AutoMapper;
    using Newtonsoft.Json;

    public static class CustomSerializerUsingAutoMapper
    {
        private static IMapper mapper = CreateMapper();

        public static string Serialize(ParentEntity entity)
        {
            var entitydto = mapper.Map<ParentEntityDto>(entity);

            return JsonConvert.SerializeObject(entitydto);
        }

        public static ParentEntity Deserialize (string jsonEntity)
        {
            var deserializedEntity = JsonConvert.DeserializeObject<ParentEntityDto>(jsonEntity);

            return mapper.Map<ParentEntity>(deserializedEntity);
        }

        private static IMapper CreateMapper()
        {
            var mapperConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ParentEntity, ParentEntityDto>();

                config.CreateMap<ParentEntityDto, ParentEntity>()
                .ConstructUsing(dto => new ParentEntity
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Description = dto.Description,
                    ParentId = dto.Id,
                    ParentName = dto.Name,
                });
            });

            return mapperConfig.CreateMapper();
        }
    }
}
