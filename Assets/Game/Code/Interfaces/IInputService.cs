namespace Game.Code.Interfaces
{
    public interface IInputService
    {
        bool IsLeftPressed { get; }
        bool IsRightPressed { get; }
        bool IsAccelerationPressed { get; }
        bool IsInflationPressed { get; }
        bool IsDeflationPressed { get; }
        bool IsJumpPressed { get; }
        
        
        bool ConsumeLeftPress();
        bool ConsumeRightPress();
        bool ConsumeAccelerationPress();
        bool ConsumeInflationPress();
        bool ConsumeDeflationPress();
        bool ConsumeJumpPress();
    }
}