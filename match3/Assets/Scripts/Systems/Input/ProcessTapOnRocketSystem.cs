using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ProcessTapOnRocketSystem : ReactiveSystem<InputEntity>
{
    private GameContext gameContext;
    public ProcessTapOnRocketSystem(InputContext Input, GameContext game) : base(Input)
    {
        gameContext = game;
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
    {
        return context.CreateCollector(InputMatcher.Input);
    }

    protected override bool Filter(InputEntity entity)
    {
        return entity.hasInput;
    }

    protected override void Execute(List<InputEntity> entities)
    {
        var inputEntity = entities.SingleEntity();
        var input = inputEntity.input;
        foreach (var e in gameContext.GetEntitiesWithPosition(new Vector2(input.x,input.y)))
        {
            if (e.isMove)
                e.isMove = false;
            
            if (e.hasRocket)
            {
                e.isMove = true;
                Debug.Log("Tap on rocket : " + e);
                //e.isMove = true;
                if (e.rocket.typeRocket == "Vertical")
                {
                    e.ReplaceTapOnRocket("TapOnVR");
                }
                else if (e.rocket.typeRocket == "Horizontal")
                {
                    e.ReplaceTapOnRocket("TapOnHR");
                }

                
            }
        }
    }
}