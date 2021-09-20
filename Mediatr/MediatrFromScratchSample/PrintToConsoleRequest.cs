using MediatrFromScratch;

namespace MediatrFromScratchSample
{
    public class PrintToConsoleRequest : IRequest<bool>
    {
        public string Text { get; init; }
    }
}