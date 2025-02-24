namespace PdArchEcsCore.InstallerGenerator.Attributes;

using System;
using PdArchEcsCore.InstallerGenerator.Enums;

[AttributeUsage(AttributeTargets.Class)]
public class InstallAttribute : Attribute
{
    public readonly ExecutionPriority Priority;
    public readonly int Order;
    public readonly string[] Features;

    public InstallAttribute(
      ExecutionPriority priority,
      int order,
      params string[] features
    )
    {
        Priority = priority;
        Order = order;
        Features = features;
    }
}
