namespace CompanyManager.Logic.Contracts
{
    public interface ISettings
    {
        string? this[string key] { get; }
    }
}