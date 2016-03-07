using AdminConsole.Models;
using AutoMapper;

namespace AdminConsole.ViewModels
{
    public class VmMapper
    {
        public static void Config(IMapperConfiguration cfg)
        {
            cfg.CreateMap<Product, ProductVm>();

            cfg.CreateMap<ProductVm, Product>();
        }
    }
}
