// ReSharper disable once CheckNamespace
namespace System.ComponentModel.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    internal class NotEmptyAttribute : ValidationAttribute
    {
        private const string DefaultErrorMessage = "{0} must not be empty";
    
        public NotEmptyAttribute()
            : base(DefaultErrorMessage) { }
    
        public override bool IsValid(object value)
        {
            return value switch
            {
                Guid guid => guid != Guid.Empty,
                string text => !string.IsNullOrWhiteSpace(text),
                _ => true
            };
        }
    }
}
