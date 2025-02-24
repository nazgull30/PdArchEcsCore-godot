namespace PdArchEcsCore.Utils;

using Arch.Core;

public interface ILinkable
{
    public void Link(Entity entity, ILinkedEntityRepository linkedEntityRepository);
    public void Link(Entity entity);
}
