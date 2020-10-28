using AutoMapper;
using Domain.Commands;
using Domain.DataModels;
using Domain.ViewModel;
using System.Security.Cryptography.X509Certificates;

namespace WebApi.AutoMapper
{
    public class MemberProfile : Profile
    {
        public MemberProfile()
        {
            CreateMap<CreateMemberCommand, Member>();
            CreateMap<UpdateMemberCommand, Member>();
            CreateMap<Member, MemberVm>();

            CreateMap<CreateTaskCommand, Task>();
            CreateMap<UpdateTaskCommand, Task>();
            CreateMap<Task, TaskVm>()
                .ForMember(dest=>dest.Avatar, src=>src.MapFrom(x=>x.AssignMember.Avatar));
        }
    }
}
