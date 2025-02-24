namespace PdArchEcsCore.CommandBuffer.Systems;

using System;

public abstract class ForEachCommandUpdateSystem<TCommand>(ICommandBuffer commandBuffer)
    : CommandSystem<TCommand>(commandBuffer)
    where TCommand : struct
{
    protected sealed override void Execute(Span<TCommand> commands)
    {
        foreach (ref var command in commands)
        {
            Execute(ref command);
        }
    }

    protected abstract void Execute(ref TCommand command);
}
