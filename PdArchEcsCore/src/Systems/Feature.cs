namespace PdArchEcsCore.Systems;

using System;
using System.Collections.Generic;

public interface IFeature
{
    public void AddBefore<TSystem>(ISystem system) where TSystem : ISystem;
    public void AddAfter<TSystem>(ISystem system) where TSystem : ISystem;
    public void Remove(ISystem system);
}

public class Feature : IFeature
{
    private readonly List<IInitializeSystem> _initializeSystems = [];
    private readonly List<IFixedUpdateSystem> _fixedUpdateSystems = [];
    private readonly List<IUpdateSystem> _updateSystems = [];

    public Feature(IEnumerable<ISystem> systems)
    {
        foreach (var system in systems)
        {
            Add(system);
        }
    }

    public Feature Add(ISystem system)
    {
        if (system is IInitializeSystem initializeSystem)
        {
            _initializeSystems.Add(initializeSystem);
        }
        if (system is IFixedUpdateSystem fixedUpdateSystem)
        {
            _fixedUpdateSystems.Add(fixedUpdateSystem);
        }
        if (system is IUpdateSystem updateSystem)
        {
            _updateSystems.Add(updateSystem);
        }
        return this;
    }

    public void AddBefore<TSystem>(ISystem system) where TSystem : ISystem
    {
        switch (system)
        {
            case IInitializeSystem initializeSystem:
                {
                    var index = GetInitializeSystemIndex<TSystem>();
                    _initializeSystems.Insert(index, initializeSystem);
                    break;
                }
            case IUpdateSystem updateSystem:
                {
                    var index = GeUpdateSystemIndex<TSystem>();
                    _updateSystems.Insert(index, updateSystem);
                    break;
                }
            case IFixedUpdateSystem fixedUpdate:
                {
                    var index = GeFixedUpdateSystemIndex<TSystem>();
                    _fixedUpdateSystems.Insert(index, fixedUpdate);
                    break;
                }
        }
    }

    public void AddAfter<TSystem>(ISystem system) where TSystem : ISystem
    {
        switch (system)
        {
            case IInitializeSystem initializeSystem:
                {
                    var index = GetInitializeSystemIndex<TSystem>();
                    _initializeSystems.Insert(index + 1, initializeSystem);
                    break;
                }
            case IUpdateSystem updateSystem:
                {
                    var index = GeUpdateSystemIndex<TSystem>();
                    _updateSystems.Insert(index + 1, updateSystem);
                    break;
                }
            case IFixedUpdateSystem fixedUpdate:
                {
                    var index = GeFixedUpdateSystemIndex<TSystem>();
                    _fixedUpdateSystems.Insert(index + 1, fixedUpdate);
                    break;
                }
        }
    }

    public void Remove(ISystem system)
    {
        switch (system)
        {
            case IInitializeSystem initializeSystem:
                _initializeSystems.Remove(initializeSystem);
                break;
            case IUpdateSystem updateSystem:
                _updateSystems.Remove(updateSystem);
                break;
            case IFixedUpdateSystem fixedUpdateSystem:
                _fixedUpdateSystems.Remove(fixedUpdateSystem);
                break;
        }
    }

    public void Initialize()
    {
        foreach (var system in _initializeSystems)
        {
            system.Initialize();
        }
    }

    public void Update(double delta)
    {
        foreach (var system in _updateSystems)
        {
            system.Update(delta);
        }
    }

    public void FixedUpdate(double delta)
    {
        foreach (var system in _fixedUpdateSystems)
        {
            system.FixedUpdate(delta);
        }
    }

    private int GetInitializeSystemIndex<TSystem>() where TSystem : ISystem
    {
        for (var i = 0; i < _initializeSystems.Count; i++)
        {
            var system = _initializeSystems[i];
            if (system.GetType() == typeof(TSystem))
                return i;
        }

        throw new ArgumentException($"{nameof(Feature)} no initialize system {typeof(TSystem).Name}");
    }

    private int GeUpdateSystemIndex<TSystem>() where TSystem : ISystem
    {
        for (var i = 0; i < _updateSystems.Count; i++)
        {
            var system = _updateSystems[i];
            if (system.GetType() == typeof(TSystem))
                return i;
        }

        throw new ArgumentException($"{nameof(Feature)} no update system {typeof(TSystem).Name}");
    }

    private int GeFixedUpdateSystemIndex<TSystem>() where TSystem : ISystem
    {
        for (var i = 0; i < _fixedUpdateSystems.Count; i++)
        {
            var system = _fixedUpdateSystems[i];
            if (system.GetType() == typeof(TSystem))
                return i;
        }

        throw new ArgumentException($"{nameof(Feature)} no fixed update system {typeof(TSystem).Name}");
    }
}
