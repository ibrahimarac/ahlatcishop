using Ahlatci.Shop.Application.Exceptions;
using ArxOne.MrAdvice.Advice;
using FluentValidation.Results;

namespace Ahlatci.Shop.Application.Behaviors
{
    public class ValidationBehavior : Attribute, IMethodAdvice
    {
        private readonly Type _validatorType; //CreateCategoryValidator

        public ValidationBehavior(Type validatorType)
        {
            _validatorType = validatorType;
        }

        public void Advise(MethodAdviceContext context)
        {
            //Metod çalışmadan önce devreye girecek kodlar.

            if (context.Arguments.Any())
            {
                var requestModel = context.Arguments[0]; //CreateCategoryVM

                //Request model doğrulaması - Fluent Validation
                var validateMethod = _validatorType.GetMethod("Validate", new Type[] { requestModel.GetType() });
                var validatorInstance = Activator.CreateInstance(_validatorType); // new CreateCategoryValidator()
                if (validateMethod != null)
                {
                    var validationResult = (ValidationResult)validateMethod.Invoke(validatorInstance, new object[] { requestModel });
                    if (!validationResult.IsValid)
                    {
                        throw new ValidateException(validationResult);
                    }
                }
            }

            context.Proceed(); //Metod tetikleniyor


            //Metod çalıştıktan sonra devreye girecek kodlar.
        }

    }
}
