namespace PdArchEcsCore;
using System;
using Arch.Core;
using PdArchEcsCore.Exceptions;
using PdArchEcsCore.Interfaces;
using PdArchEcsCore.Systems;

public class Bootstrap(Feature feature) : IBootstrap
{
    private bool _isInitialized;
    private bool _isPaused;

    public void Initialize()
    {
        if (_isInitialized)
        {
            throw new EcsException("[MainBootstrap] Bootstrap already is initialized");
        }

        feature.Initialize();
        _isInitialized = true;
    }

    public void Update(double delta)
    {
        if (_isPaused)
        {
            return;
        }

        feature.Update(delta);
    }

    public void FixedUpdate(double delta)
    {
        if (_isPaused)
        {
            return;
        }

        feature.FixedUpdate(delta);
    }

    public void Pause(bool isPaused)
    {
        _isPaused = isPaused;
    }

    public void Reset()
    {
        Pause(true);

        foreach (var world in World.Worlds)
        {
            world.Clear();
        }

        _isInitialized = false;
        Initialize();

        Pause(false);
    }

    public void Dispose()
    {
        foreach (var world in World.Worlds)
        {
            world?.Dispose();
        }
    }
}
