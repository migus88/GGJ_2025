namespace Game.Code.Interfaces
{
    public interface IInputService
    {
        bool ConsumeLeftPress();
        bool ConsumeRightPress();
        bool ConsumeAccelerationPress();
        bool ConsumeInflationPress();
        bool ConsumeDeflationPress();
        bool ConsumeJumpPress();
    }
}