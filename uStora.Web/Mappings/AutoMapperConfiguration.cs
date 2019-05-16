using AutoMapper;
using uStora.Model.Models;
using uStora.Web.API;
using uStora.Web.Models;

namespace uStora.Web.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.CreateMap<Post, PostViewModel>();
                x.CreateMap<ProductCategory, ProductCategoryViewModel>();
                x.CreateMap<PostCategory, PostCategoryViewModel>();
                x.CreateMap<PostTag, PostTagViewModel>();
                x.CreateMap<Tag, TagViewModel>();
                x.CreateMap<Product, ProductViewModel>();
                x.CreateMap<Product, ExportedProduct>();
                x.CreateMap<ProductTag, ProductTagViewModel>();
                x.CreateMap<Brand, BrandViewModel>();
                x.CreateMap<ContactDetail, ContactDetailViewModel>();
                x.CreateMap<Slide, SlideViewModel>();
                x.CreateMap<Order, OrderViewModel>();
                x.CreateMap<OrderDetail, OrderDetailViewModel>();
                x.CreateMap<Feedback, FeedbackViewModel>();
                x.CreateMap<Footer, FooterViewModel>();
                x.CreateMap<Page, PageViewModel>();
                x.CreateMap<Wishlist, WishlistViewModel>();
                x.CreateMap<Vehicle, VehicleViewModel>();
                x.CreateMap<TrackOrder, TrackOrderViewModel>();
                x.CreateMap<ApplicationGroup, ApplicationGroupViewModel>();
                x.CreateMap<ApplicationRole, ApplicationRoleViewModel>();
                x.CreateMap<ApplicationUser, ApplicationUserViewModel>();
            });
        }
    }
}