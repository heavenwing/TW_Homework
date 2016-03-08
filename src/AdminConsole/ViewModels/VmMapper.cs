using AdminConsole.Models;
using AutoMapper;

namespace AdminConsole.ViewModels
{
    public class VmMapper
    {
        public static void Config(IMapperConfiguration cfg)
        {
            cfg.CreateMap<Product, ProductVm>().
                AfterMap((s, d) =>
                {
                    if (s.Promotions != null)
                    {
                        foreach (var pp in s.Promotions)
                        {
                            if (pp.Promotion != null)
                                d.PromotionNames.Add(pp.Promotion.Name);
                        }
                    }
                });
            cfg.CreateMap<Promotion, PromotionVm>();

            cfg.CreateMap<ProductVm, Product>();
            cfg.CreateMap<PromotionVm, Promotion>();
        }
    }
}
