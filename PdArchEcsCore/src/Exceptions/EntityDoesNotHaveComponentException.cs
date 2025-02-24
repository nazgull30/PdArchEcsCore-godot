namespace PdArchEcsCore.Exceptions;

public class EntityDoesNotHaveComponentException(string componentName)
    : EcsException("Entity does not have a component " + componentName);
