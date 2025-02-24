namespace PdArchEcsCore.Utils;

using Arch.Core;

public interface ILinkedEntityRepository
{
    public void Add(ulong id, Entity entity);
    public Entity Get(ulong id);
    public bool HasItem(ulong id);
    public void Delete(ulong id);
}
