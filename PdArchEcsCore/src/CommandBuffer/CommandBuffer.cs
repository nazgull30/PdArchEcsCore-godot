namespace PdArchEcsCore.CommandBuffer;

using System;
using System.Collections.Generic;

public class CommandBuffer : ICommandBuffer
{
    private static readonly List<Func<ICommandPool>> _poolFactories = [];

    private static int _commandsCounter;
    private static event Action<Func<ICommandPool>> PoolFactoryRegister;

    private readonly List<ICommandPool> _pools = [];

    public CommandBuffer()
    {
        lock (_poolFactories)
        {
            foreach (var poolFactory in _poolFactories)
            {
                _pools.Add(poolFactory());
            }
        }

        PoolFactoryRegister += poolFactory => _pools.Add(poolFactory());
    }

    public ref TCommand Create<TCommand>() where TCommand : struct
    {
        var pool = _pools[CommandPool<TCommand>.Index] as CommandPool<TCommand>;
        return ref pool.Create();
    }

    public void Create<TCommand>(TCommand command) where TCommand : struct
    {
        var pool = _pools[CommandPool<TCommand>.Index] as CommandPool<TCommand>;
        pool.Create(command);
    }

    public Span<TCommand> GetCommands<TCommand>() where TCommand : struct
    {
        var pool = _pools[CommandPool<TCommand>.Index] as CommandPool<TCommand>;
        return pool.Read();
    }

    public void Clear<TCommand>() where TCommand : struct
      => _pools[CommandPool<TCommand>.Index].Clear();

    private interface ICommandPool
    {
        public void Clear();
    }

    private sealed class CommandPool<TCommand> : ICommandPool
      where TCommand : struct
    {
        public static readonly int Index;

        private TCommand[] _pool = [];
        private int _position;

        static CommandPool()
        {
            static CommandPool<TCommand> factory() => new();

            lock (_poolFactories)
            {
                Index = _commandsCounter++;
                _poolFactories.Add(factory);
                PoolFactoryRegister(factory);
            }
        }

        public ref TCommand Create()
        {
            var position = _position++;
            if (_pool.Length <= _position)
            {
                Array.Resize(ref _pool, 1 << _pool.Length);
            }

            return ref _pool[position];
        }

        public void Create(TCommand command)
        {
            var position = _position++;
            if (_pool.Length <= _position)
            {
                Array.Resize(ref _pool, 1 << _pool.Length);
            }

            _pool[position] = command;
        }

        public Span<TCommand> Read() => new(_pool, 0, _position);

        public void Clear()
        {
            _position = 0;
            Array.Clear(_pool, 0, _pool.Length);
        }
    }
}
