using EmployeeFacilitationPortal.Entities.Models;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeFacilitationPortal.Entities.Validations
{
    public class EmployeeValidator: AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            //  designation cannot be null, empty, or whitespace and must be at least 3 but no more than 40 characters long
            RuleFor(emp => emp.Designation)
                .NotEmpty()
                .Length(2, 5);

            //  for email related to ciklum 

            RuleFor(emp => emp.Username)
                .NotEmpty()
                .MatchEmailRule()
                .WithMessage("Email is not related to ciklum");

            // date of joining should not be greater than today date 
            RuleFor(emp => emp.DateOfJoining)
               .NotEmpty()
               .Must(emp =>
               (DateTime.Now.Date).Equals(emp.Date))
               .WithMessage("Date of joining should be today");

            //rule for the ProjectAssigned
            RuleFor(emp => emp.ProjectAssigned)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(emp => emp.Substring(0, 1).Equals(emp.Substring(0, 1).ToUpper()))
                .Must(emp => emp.Contains("ciklum"));
                ;

            RuleFor(emp => emp.PersonalInfo).SetValidator(new personalInfoValidator());
        }




    }

    public static class CustomRule
    {
        public static IRuleBuilderOptions<T, string> MatchEmailRule<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new RegularExpressionValidator(@"^[A-Za-z0-9](\.?|_?[A-Za-z0-9]){5,}@(ciklum)\.com$"));
        }
    }
}
