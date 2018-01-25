using System;
using FluentValidation;
using Microsoft.Practices.Unity;

namespace Demo.Web
{
    public class UnityValidatorFactory : ValidatorFactoryBase
    {
        /// <summary>
        /// The unity container.
        /// </summary>
        private readonly IUnityContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityValidatorFactory"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public UnityValidatorFactory(IUnityContainer container)
        {
            _container = container;
        }

        /// <summary>
        /// The create instance.
        /// </summary>
        /// <param name="validatorType">Validator Type</param>
        /// <returns>
        /// The <see cref="IValidator"/>.
        /// </returns>
        public override IValidator CreateInstance(Type validatorType)
        {
            return _container.Resolve(validatorType) as IValidator;
        }
    }
}