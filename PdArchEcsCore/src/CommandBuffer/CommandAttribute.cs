namespace PdArchEcsCore.CommandBuffer;

using System;

/// <summary>
///     Marks a struct to be a command.
/// </summary>
[AttributeUsage(AttributeTargets.Struct)]
public class CommandAttribute : Attribute
{
}
