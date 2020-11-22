using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StajBul.WebUI.Infrastructure
{
    public class MustBeTrueAttribute : Attribute, IModelValidator //Bunu artik [MustBeTrueAttribute(errorMessage = "istediginiz bir mesaj yazabiirsin")] olarak kullanabilirim.
    {
        public bool isRequired => true;

        public string ErrorMessage { get; set; } = "Kullanım Koşullarını Kabul Etmelisiniz.";

        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            var value = context.Model as bool?;

            if(!value.HasValue || value.Value == false)
            {
                return new List<ModelValidationResult> { 
                    new ModelValidationResult("", ErrorMessage)
                };
            }
            else
            {
                return Enumerable.Empty<ModelValidationResult>();
            }
        }
    }
}
