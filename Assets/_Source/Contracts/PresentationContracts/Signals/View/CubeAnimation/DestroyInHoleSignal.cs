namespace Contracts.PresentationContracts.Signals.View.CubeAnimation
{
    public struct DestroyInHoleSignal
    {
        public int ViewId { get; }

        public DestroyInHoleSignal(int viewId)
        {
            ViewId = viewId;
        }
    }
}