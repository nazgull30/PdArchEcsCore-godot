namespace PdArchEcsCore.Systems;

public interface IFixedUpdateSystem : ISystem
{
    public void FixedUpdate(double delta);
}
