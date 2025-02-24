namespace PdArchEcsCore.Interfaces;

using System;

public interface IBootstrap : IDisposable
{
    public void Pause(bool isPaused);
    public void Reset();
}
