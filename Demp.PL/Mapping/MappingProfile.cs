using AutoMapper;
using Demo.BLL.DTOs.DepartmentDTOs;
using Demo.BLL.DTOs.EmployeeDTOs;
using Demp.PL.ViewModels;

namespace Demp.PL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Department Module

            CreateMap<DepartmentDetailsDto, DepartmentViewModel>();
            CreateMap<DepartmentViewModel, UpdatedDepartmentDto>();
            CreateMap<DepartmentViewModel, CreatedDepartmentDto>();

            #endregion

            #region Employee Module
            CreateMap <CreatedEmployeeDto, UpdatedEmployeeDto>();
            #endregion

        }
    }
}
