namespace Contracts.PresentationContracts.Signals.View
{
    public struct DestroyCubeSignal
    {
        public int ViewId { get; private set; }

        public DestroyCubeSignal(int viewId)
        {
            ViewId = viewId;
        }
    }
}