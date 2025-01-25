namespace ScriptableObjects
{
    public interface IGameSettings
    {
        float DeflationSpeed { get; set; }
        float CollisionPenalty { get; set; }
        float Speed { get; set; }
    }
}