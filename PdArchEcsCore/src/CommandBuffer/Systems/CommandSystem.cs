namespace PdArchEcsCore.CommandBuffer.Systems;

using System;
using PdArchEcsCore.Systems;

public abstract class CommandSystem<TCommand>(ICommandBuffer commandBuffer) : IUpdateSystem
  where TCommand : struct
{
    protected virtual bool CleanUp => true;

    protected void ExecuteCommands()
    {
        var commands = commandBuffer.GetCommands<TCommand>();
        if (commands.Length == 0)
        {
            return;
        }

        Execute(commands);
        if (CleanUp)
        {
            commandBuffer.Clear<TCommand>();
        }
    }

    protected abstract void Execute(Span<TCommand> commands);

    public void Update(double delta)
    {
        ExecuteCommands();
    }
}
