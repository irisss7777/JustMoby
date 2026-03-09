namespace Contracts.ApplicationContracts.Signal
{
    public struct ClickInputSignal
    {
        public bool ClickState { get; }

        public ClickInputSignal(bool clickState)
        {
            ClickState = clickState;
        }
    }
}