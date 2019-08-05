using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ProcessTapOnDiscoSystem : ReactiveSystem<InputEntity>
{
    private GameContext gameContext;
    public ProcessTapOnDiscoSystem(InputContext Input, GameContext game) : base(Input)
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
            if (e.isDisco)
            {
                e.isMove = true;
                Debug.Log("Tap on disco : " + e);
                //e.isMove = true;
                e.isTapOnDisco = true;
                
            }

        }
    }
}