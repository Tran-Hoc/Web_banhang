using AutoMapper;
using Web_banhang.Data;
using Web_banhang.Models;

namespace Web_banhang.Mapper
{
    public class MappingProduct: Profile
    {
        public MappingProduct()
        {
            CreateMap<TbProductCategory, ProdCategoryVM>();
            CreateMap<ProdCategoryVM,TbProductCategory>();
            CreateMap<ProductVM, TbProduct >();
            CreateMap<TbProduct, ProductVM>();
        }
    }
}
