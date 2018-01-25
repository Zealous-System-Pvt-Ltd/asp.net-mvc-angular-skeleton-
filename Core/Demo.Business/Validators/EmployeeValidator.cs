using System;
using Demo.Business.Contracts;
using Demo.DomainModel;
using Demo.Shared.Resources;
using FluentValidation;

namespace Demo.Business.Validators
{
    /// <summary>
    /// EmployeeValidator is responsible for validate Employee data
    /// </summary>
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        private readonly IEmployeeManager _employeeManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeValidator"/> class.
        /// </summary>
        /// <param name="employeeManager">
        /// The employee manager.
        /// </param>
        /// <param name="mode">Add or Edit mode</param>
        public EmployeeValidator(IEmployeeManager employeeManager, string mode)
        {
            _employeeManager = employeeManager;
            RuleFor(e => e.FirstName).NotEmpty().WithLocalizedMessage(() => ErrorMessages.FirstNameRequired).Length(2, 10).WithLocalizedMessage(() => ErrorMessages.FirstNameRequired);
            RuleFor(e => e.LastName).NotEmpty().WithLocalizedMessage(() => ErrorMessages.LastNameRequired);
            RuleFor(e => e.DateOfBirth).LessThan(DateTime.Today).WithLocalizedMessage(() => ErrorMessages.DateOfBirthLessThanCurrentDate).When(e => e.DateOfBirth != DateTime.MinValue);
            RuleFor(e => e.Email).EmailAddress().WithLocalizedMessage(() => ErrorMessages.InvalidEmail).When(e => !string.IsNullOrEmpty(e.Email) && mode.Equals("Add")).Must(email => !this._employeeManager.IsEmailUnique(email)).WithLocalizedMessage(() => ErrorMessages.EmailAlreadyExists).When(e => e.EmployeeId != null);
            RuleFor(e => e.Salary).NotEmpty().Must(salary => salary > 0).WithLocalizedMessage(() => ErrorMessages.ZeroSalary);
            RuleFor(e => e.DesignationId).NotEmpty().WithLocalizedMessage(() => ErrorMessages.DesignationRequired);
        }
    }
}
