namespace PdArchEcsCore.CommandBuffer.Systems;

using PdArchEcsCore.Systems;

public abstract class ClearCommandSystem<TCommand>(ICommandBuffer commandBuffer) : IUpdateSystem
    where TCommand : struct
{
    public void Update(double delta)
    {
        commandBuffer.Clear<TCommand>();
    }
}
