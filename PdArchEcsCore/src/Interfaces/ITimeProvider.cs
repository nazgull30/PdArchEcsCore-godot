namespace PdArchEcsCore.Interfaces;

public interface ITimeProvider
{
    public float Time { get; }
    public float DeltaTime { get; }
    public float UnscaledDeltaTime { get; }
    public float FixedDeltaTime { get; }
    public float TimeScale { get; set; }
}
