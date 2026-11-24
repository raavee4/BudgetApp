namespace BudgetApp.Common.Exceptions;

public class AppValidationException : Exception
{
    public IDictionary<string, string[]> Errors { get; }
    public AppValidationException(IDictionary<string, string[]> errors)
    {
        Errors = errors;
    }
}
