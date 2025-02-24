namespace PdArchEcsCore.Utils.Impl;

using System.Collections.Generic;
using Arch.Core;

public class LinkedEntityRepository : ILinkedEntityRepository
{
    private readonly Dictionary<ulong, Entity> _links = [];

    public void Add(ulong id, Entity entity)
    {
        _links.TryAdd(id, entity);
    }

    public Entity Get(ulong id) => _links[id];

    public bool HasItem(ulong id)
    {
        return _links.ContainsKey(id);
    }

    public void Delete(ulong id)
    {
        _links.Remove(id);
    }
}
