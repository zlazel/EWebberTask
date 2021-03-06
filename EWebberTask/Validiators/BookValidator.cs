﻿using FluentValidation;
using EWebberTask.DAL;
namespace EWebberTask.Validation
{
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator ()//ApplicationContext context)
        {
            // Name Validations
            RuleFor(x => x.Name).NotEmpty().WithMessage("*Required")
                .Length(4,100).WithMessage("Minimum Length Is 4 Characters And Maximum Length Is 100 Characters")
                .Matches(@"^[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z]+[\s\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z]*")
                .WithMessage("Only Arabic and English Letters Allowed");

            // ISBN Validations
            RuleFor(x => x.ISBN).NotEmpty().WithMessage("*Required")
                .Length(4, 20).WithMessage("Minimum Length Is 4 Digits And Maximum Length Is 10 Digits")
                .Matches("^\\d+$").WithMessage("Only Digits are Allowed");
        }
    }
}  
