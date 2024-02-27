using AutoMapper;
using Demo.DAL.Models;
using Demo.PL.ViewModels;

namespace Demo.PL.Mapping_Profiles
{
    public class EmployeeProfile :Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
                //.ForMember(d =>d.Name , o =>o.MapFrom(s=>s.EmpName));
        }
    }
}
