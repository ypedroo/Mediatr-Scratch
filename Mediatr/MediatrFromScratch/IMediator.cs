using System.Threading.Tasks;

namespace MediatrFromScratch
{
    public interface IMediator
    {
        Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request);
    }
}