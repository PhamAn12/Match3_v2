using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class RenderSpriteSystems : ReactiveSystem<GameEntity> 
{
    readonly GameContext context;

    public RenderSpriteSystems(GameContext Game) : base(Game) {
        context = Game;
    }
    public RenderSpriteSystems(IContext<GameEntity> context) : base(context)
    {
    }

    public RenderSpriteSystems(ICollector<GameEntity> collector) : base(collector)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Asset);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasAsset && entity.hasView;
    }

    protected override void Execute(List<GameEntity> entities)
    {       
       
        foreach (var e in entities)
        {
            

            Debug.Log("Asset name :" + e.asset.name);

            GameObject GO = Resources.Load(e.asset.name) as GameObject;
            //GO.transform.position = new Vector3(i,j, 0);
            
            try
            {
                UnityEngine.Object.Instantiate(GO);
                    
                    
            }
            catch (EntitasException)
            {
                 Debug.Log("can't not initialize");
            }

            

        }
    }
}