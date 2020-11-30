namespace _Support
{
    public interface IDomainValidation
    {
        bool IsValid { get; }
        void AddError(string message);
    }
}