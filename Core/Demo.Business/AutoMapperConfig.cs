using System.Collections.Generic;
using Demo.DomainModel;
using Demo.ViewModel.Web;

namespace Demo.Business
{
    /// <summary>
    /// The autoMapperConfig is responsible for mapping entities of ViewModel to Model or vise varsa.
    /// </summary>
    public static class AutoMapperConfig
    {
        /// <summary>
        /// The register mappings.
        /// </summary>
        public static void RegisterMappings()
        {
            AutoMapper.Mapper.CreateMap<EmployeeViewModel, Employee>();
            AutoMapper.Mapper.CreateMap<Employee, EmployeeViewModel>();
            AutoMapper.Mapper.CreateMap<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>();
        } 
    }
}