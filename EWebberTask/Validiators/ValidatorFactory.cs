using FluentValidation;
using EWebberTask.DAL;
using EWebberTask.ViewModels;
using System;
using System.Collections.Generic;

namespace EWebberTask.Validation
{
    public class ValidatorFactory : ValidatorFactoryBase  
    {  
        private static Dictionary<Type, IValidator> validators = new Dictionary<Type, IValidator>();

        static ValidatorFactory()
        {
            validators.Add(typeof(IValidator<Author>), new AuthorValidator());
            validators.Add(typeof(IValidator<Student>), new StudentValidator());
            validators.Add(typeof(IValidator<Book>), new BookValidator());
        }

        public override IValidator CreateInstance(Type validatorType)  
        {  
            IValidator validator;  
            if (validators.TryGetValue(validatorType, out validator))  
            {  
                return validator;  
            }  
            return validator;  
        }  
    }  
}  
