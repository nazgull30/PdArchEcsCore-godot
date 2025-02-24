namespace PdArchEcsCore.Systems;

public interface IUpdateSystem : ISystem
{
    public void Update(double delta);
}
