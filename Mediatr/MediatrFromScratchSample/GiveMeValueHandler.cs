using System.Threading.Tasks;
using MediatrFromScratch;

namespace MediatrFromScratchSample
{
    public class GiveMeValueHandler : IHandler<GiveMeValueRequest, string>
    {
        public Task<string> HandleAsync(GiveMeValueRequest request)
        {
            return Task.FromResult("Hello from pedro");
        }
    }
}