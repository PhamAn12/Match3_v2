using System.Collections;
using System.Collections.Generic;
using Entitas;
using UnityEngine;
using UnityEngine.UI;

public class ReplaceViewRocketSystem : ReactiveSystem<GameEntity>
{
    private GameContext gameContext;
    private Dropdown mDropdown;
    public ReplaceViewRocketSystem(GameContext context) : base(context)
    {
        gameContext = context;
    }


    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Rocket);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasRocket;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            if (e.rocket.typeRocket == "Horizontal")
            {
                e.ReplaceAsset("Prefabs/Rocket");
            }
            else if (e.rocket.typeRocket == "Vertical")
            {
                e.ReplaceAsset("Prefabs/VRocket");
            }
        }
    }


}