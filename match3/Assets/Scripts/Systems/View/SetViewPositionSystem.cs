using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class SetViewPositionSystem : ReactiveSystem<GameEntity>
{
    public SetViewPositionSystem(IContext<GameEntity> context) : base(context)
    {
    }

    public SetViewPositionSystem(ICollector<GameEntity> collector) : base(collector)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.View);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasView && entity.hasPosition;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            var pos = e.position;
            e.view.gameObject.transform.position = new Vector3(pos.value.x *1f,pos.value.y * 1f);
//            Debug.Log(pos);
            
//            Debug.Log(pos.value.x + "  " + pos.value.y);
        }
    }
}
