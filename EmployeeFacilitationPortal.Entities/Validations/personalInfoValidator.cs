using EmployeeFacilitationPortal.Entities.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeFacilitationPortal.Entities.Validations
{
    public class personalInfoValidator: AbstractValidator<PersonalInfo>
    {

       public personalInfoValidator()
        {
            //name length check
            RuleFor(emp => emp.FullName)
                 .Cascade(CascadeMode.StopOnFirstFailure)
                 .NotEmpty()
                 .Length(3, 40);

            //DOB should be checked that age is greater than 18 
            RuleFor(emp => emp.DateOfBirth)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithMessage("DOB can not be null")
                .Must(emp =>
                (Convert.ToInt32(DateTime.Now.Year) - Convert.ToInt32(emp.Year)) > 18)
                .WithMessage("Age and year mismatch");
        }
    }
}
