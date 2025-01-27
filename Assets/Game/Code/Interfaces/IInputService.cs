namespace Game.Code.Interfaces
{
    public interface IInputService
    {
        bool IsLeftPressed { get; }
        bool IsRightPressed { get; }
        bool IsAccelerationPressed { get; }
        bool IsInflationPressed { get; }
        bool IsInfiniteFlightSelected { get; }
        bool IsJumpPressed { get; }
        
        
        bool ConsumeLeftPress();
        bool ConsumeRightPress();
        bool ConsumeAccelerationPress();
        bool ConsumeInflationPress();
        bool ConsumeInfiniteFlightPress();
        bool ConsumeJumpPress();
    }
}