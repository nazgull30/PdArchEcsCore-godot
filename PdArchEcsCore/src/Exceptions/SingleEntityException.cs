namespace PdArchEcsCore.Exceptions;

public class SingleEntityException(int count) : EcsException($"Expected exactly one entity in collection but found {count}!");
