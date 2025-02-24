namespace PdArchEcsCore.CommandBuffer;

using System;

public interface ICommandBuffer
{
    public ref TCommand Create<TCommand>() where TCommand : struct;

    public void Create<TCommand>(TCommand command) where TCommand : struct;

    public Span<TCommand> GetCommands<TCommand>() where TCommand : struct;

    public void Clear<TCommand>() where TCommand : struct;
}
