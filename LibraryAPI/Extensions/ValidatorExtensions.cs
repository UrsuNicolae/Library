
using FluentValidation.Results;

namespace LibraryAPI.Extensions
{
    public static class ValidatorExtensions
    {
        public static IEnumerable<object> GetErrorMessages(this ValidationResult result)
        {
            return result.Errors.Select(e => new
            {
                e.PropertyName,
                e.ErrorMessage
            });
        }
    }
}
