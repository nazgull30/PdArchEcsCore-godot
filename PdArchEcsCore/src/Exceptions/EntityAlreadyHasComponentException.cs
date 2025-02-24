namespace PdArchEcsCore.Exceptions;

public class EntityAlreadyHasComponentException(string componentName)
    : EcsException("Entity already has a component " + componentName);
