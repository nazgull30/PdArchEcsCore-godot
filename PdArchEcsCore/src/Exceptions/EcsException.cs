namespace PdArchEcsCore.Exceptions;

using System;

public class EcsException(string message) : Exception(message);
