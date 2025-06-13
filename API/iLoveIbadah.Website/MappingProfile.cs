using AutoMapper;
using iLoveIbadah.Website.Models;
using iLoveIbadah.Website.Services.Base;
using Microsoft.AspNetCore.Identity.Data;

namespace iLoveIbadah.Website
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<UserAccountDto, UserAccountVM>().ReverseMap();
            CreateMap<BlogDto, BlogVM>()
                .ForMember(q => q.CreatedAt, opt => opt.MapFrom(x => x.CreatedAt.Value.DateTime))
                .ReverseMap();
            CreateMap<BlogListDto, BlogListVM>()
                .ForMember(q => q.CreatedAt, opt => opt.MapFrom(x => x.CreatedAt.Value.DateTime))
                .ReverseMap();
            CreateMap<CreateBlogDto, CreateBlogVM>().ReverseMap();
            CreateMap<CommentDto, CommentVM>().ReverseMap();
            CreateMap<CommentListDto, CommentListVM>().ReverseMap();
            CreateMap<CreateCommentDto, CreateCommentVM>().ReverseMap();
            CreateMap<UpdateCommentDto, UpdateCommentVM>().ReverseMap();
            CreateMap<TagListDto, TagListVM>().ReverseMap();
            CreateMap<CategoryListDto, CategoryListVM>().ReverseMap();
            CreateMap<RegisterVM, RegistrationRequest>().ReverseMap();
            //CreateMap<LoginVM, AuthRequest>().ReverseMap();
            //CreateMap<UpdateBlog, UpdateBlogVM>().ReverseMap();

        }
    }
}
