namespace PdArchEcsCore.Worlds;

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Arch.Core;
using PdArchEcsCore.Exceptions;

public class WorldWrapper(World world) : IWorld
{
    public Entity Create()
    {
        return world.Create();
    }

    public void Destroy(Entity entity)
    {
        world.Destroy(entity);
    }

    public void GetEntities(in QueryDescription queryDescription, List<Entity> list)
    {
        var query = world.Query(queryDescription);
        foreach (ref var chunk in query)
        {
            ref var entityFirstElement = ref chunk.Entity(0);
            foreach (var entityIndex in chunk)
            {
                var entity = Unsafe.Add(ref entityFirstElement, entityIndex);
                list.Add(entity);
            }
        }
    }

    public Entity GetSingle(in QueryDescription queryDescription)
    {
        Entity res = default;
        var count = 0;
        world.Query(in queryDescription, entity =>
        {
            res = entity;
            count++;
        });
        if (count != 1)
        {
            throw new SingleEntityException(count);
        }
        return res;
    }

    public void Query(in QueryDescription queryDescription, ForEach forEntity) => world.Query(in queryDescription, forEntity);
    public void Query<T0>(in QueryDescription description, ForEach<T0> forEach) => world.Query(in description, forEach);
    public void Query<T0, T1>(in QueryDescription description, ForEach<T0, T1> forEach) => world.Query(in description, forEach);
    public void Query<T0, T1, T2>(in QueryDescription description, ForEach<T0, T1, T2> forEach) => world.Query(in description, forEach);
    public void Query<T0, T1, T2, T3>(in QueryDescription description, ForEach<T0, T1, T2, T3> forEach) => world.Query(in description, forEach);

    public void Query<T0>(in QueryDescription description, ForEachWithEntity<T0> forEach) => world.Query(in description, forEach);
    public void Query<T0, T1>(in QueryDescription description, ForEachWithEntity<T0, T1> forEach) => world.Query(in description, forEach);
    public void Query<T0, T1, T2>(in QueryDescription description, ForEachWithEntity<T0, T1, T2> forEach) => world.Query(in description, forEach);
    public void Query<T0, T1, T2, T3>(in QueryDescription description, ForEachWithEntity<T0, T1, T2, T3> forEach) => world.Query(in description, forEach);
}
