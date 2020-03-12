﻿using FluentValidation;
using EWebberTask.ViewModels;
using System.Linq;

namespace EWebberTask.Validation
{
    public class StudentVMValidator : AbstractValidator<StudentVM>
    {
        public StudentVMValidator()
        {
            // Name Validations
            RuleFor(x => x.Name).NotEmpty().WithMessage("*Required")
                .MinimumLength(3).WithMessage("Minimum Length Is 3 Characters")
                .MaximumLength(30).WithMessage("Maximum Length Is 30 Characters")
                .Matches(@"^[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z]+[\s\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z]*")
                .WithMessage("Only Arabic and English Letters Allowed");
        }
    }

}  
