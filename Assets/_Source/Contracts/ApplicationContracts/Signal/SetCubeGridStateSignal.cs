namespace Contracts.ApplicationContracts.Signal
{
    public struct SetCubeGridStateSignal
    {
        public bool IsActive { get; }

        public SetCubeGridStateSignal(bool isActive)
        {
            IsActive = isActive;
        }
    }
}