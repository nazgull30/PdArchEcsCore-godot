namespace PdArchEcsCore.Worlds;

using System.Collections.Generic;
using Arch.Core;

public interface IWorld
{
    public Entity Create();

    public void Destroy(Entity entity);

    public void GetEntities(in QueryDescription queryDescription, List<Entity> list);

    public Entity GetSingle(in QueryDescription queryDescription);

    public void Query(in QueryDescription queryDescription, ForEach forEntity);
    public void Query<T0>(in QueryDescription description, ForEach<T0> forEach);
    public void Query<T0, T1>(in QueryDescription description, ForEach<T0, T1> forEach);
    public void Query<T0, T1, T2>(in QueryDescription description, ForEach<T0, T1, T2> forEach);
    public void Query<T0, T1, T2, T3>(in QueryDescription description, ForEach<T0, T1, T2, T3> forEach);

    public void Query<T0>(in QueryDescription description, ForEachWithEntity<T0> forEach);
    public void Query<T0, T1>(in QueryDescription description, ForEachWithEntity<T0, T1> forEach);
    public void Query<T0, T1, T2>(in QueryDescription description, ForEachWithEntity<T0, T1, T2> forEach);
    public void Query<T0, T1, T2, T3>(in QueryDescription description, ForEachWithEntity<T0, T1, T2, T3> forEach);
}
