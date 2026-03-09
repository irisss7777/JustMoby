namespace Contracts.ApplicationContracts.Signal
{
    public struct PointerInRightPanelSignal
    {
        public bool InPanel { get; private set; }

        public PointerInRightPanelSignal(bool inPanel)
        {
            InPanel = inPanel;
        }
    }
}