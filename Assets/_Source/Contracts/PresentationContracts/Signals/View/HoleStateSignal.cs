namespace Contracts.PresentationContracts.Signals.View
{
    public struct HoleStateSignal
    {
        public bool InHole { get; }

        public HoleStateSignal(bool inHole)
        {
            InHole = inHole;
        }
    }
}