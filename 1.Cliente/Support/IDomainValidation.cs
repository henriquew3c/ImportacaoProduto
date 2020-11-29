namespace _1.Cliente.Support
{
    public interface IDomainValidation
    {
        bool IsValid { get; }
        void AddError(string message);
    }
}