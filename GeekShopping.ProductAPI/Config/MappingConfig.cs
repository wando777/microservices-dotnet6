using AutoMapper;

namespace GeekShopping.ProductAPI.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Model.Product, Data.DTOs.ProductDTO>().ReverseMap();
            });
        }
    }
}
