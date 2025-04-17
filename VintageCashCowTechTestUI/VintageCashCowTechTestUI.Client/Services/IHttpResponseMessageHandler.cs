
namespace VintageCashCowTechTestUI.Client.Services
{
    public interface IHttpResponseMessageHandler
    {
        Task<T?> Handle<T>(HttpResponseMessage? httpResponseMessage);
    }
}