using AutoMapper;
using ProductsCrudApi.Dto;
using ProductsCrudApi.Products.Model;

namespace ProductsCrudApi.Mappings
{
    public class MappingProfiles:Profile
    {

        public MappingProfiles()
        {
            CreateMap<CreateProductRequest, Product>();
            CreateMap<UpdateProductRequest, Product>();
        }

    }
}
