using AutoMapper;
using iLoveIbadah.Application.DTOs.BlobFile;
using iLoveIbadah.Application.Models.Identity;
using iLoveIbadah.Domain;
using iLoveIbadah.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Identity.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, UserAccount>().ReverseMap();
            CreateMap<ApplicationRole, RoleType>().ReverseMap();
            CreateMap<ApplicationUserRole, UserAccountRoleTypeMapping>().ReverseMap();
            CreateMap<ApplicationUserClaim, UserAccountClaimTypeMapping>().ReverseMap();
            CreateMap<ApplicationRoleClaim, RoleTypeClaimTypeMapping>().ReverseMap();
            CreateMap<ApplicationUserToken, UserAccountAuthenticationToken>().ReverseMap();
            CreateMap<ApplicationUserLogin, UserAccountExternalLogin>().ReverseMap();
        }
    }
}
