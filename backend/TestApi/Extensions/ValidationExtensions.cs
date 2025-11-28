using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FitTracker.Api.Controllers.Extensions
{
    /// <summary>
    /// Provides extension methods for converting FluentValidation results to ASP.NET Core model state.
    /// </summary>
    internal static class ValidationExtensions
    {
        /// <summary>
        /// Converts a <see cref="ValidationResult"/> into a <see cref="ModelStateDictionary"/>
        /// that can be returned from ASP.NET Core controllers.
        /// </summary>
        /// <param name="validationResult">The validation result to convert.</param>
        /// <returns>The populated <see cref="ModelStateDictionary"/> instance.</returns>
        public static ModelStateDictionary ToModelState(this ValidationResult validationResult)
        {
            var modelState = new ModelStateDictionary();

            foreach (var error in validationResult.Errors)
            {
                modelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            return modelState;
        }
    }
}
