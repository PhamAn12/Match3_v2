using System.Collections.Generic;
using Entitas;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class ProcessInputSystem : ReactiveSystem<InputEntity>
{
    private readonly GameContext gameContext;
    public ProcessInputSystem(InputContext Input, GameContext Game) : base(Input)
    {
        gameContext = Game;
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
//        Debug.Log("INPUT" + input + "INPUT ENTITY" + inputEntity);
//
//        Debug.Log("position of input entity :" + input.x + "  " + input.y);
        foreach (var e in gameContext.GetEntitiesWithPosition(new Vector2(input.x,input.y)))
        {
            //e.isMove = true;

            if (e.isMove == true)
                e.isMove = false;
//            if (e.isBoom)
//            {
//                e.isMove = true;
//                Debug.Log("e is boom : " + e);
//                
//                e.isTapOnBoom = true;    
//            }
//            else if (e.isDisco)
//            {
//                e.isMove = true;
//                Debug.Log("Tap on disco : " + e);
//                //e.isMove = true;
//                e.isTapOnDisco = true;
//                
//            }
//            else if (e.hasRocket)
//            {
//                e.isMove = true;
//                Debug.Log("Tap on rocket : " + e);
//                //e.isMove = true;
//                if (e.rocket.typeRocket == "Vertical")
//                {
//                    e.ReplaceTapOnRocket("TapOnVR");
//                }
//                else if (e.rocket.typeRocket == "Horizontal")
//                {
//                    e.ReplaceTapOnRocket("TapOnHR");
//                }   
//            }
//            else
            //{
                //e.isMove = true;
                e.isTabbed = true;
                //Debug.Log("Tap on akdkd : " + e);
                //e.isMove = true;
            //}


            //    e.isDestroyed = true;

        }
    }

    
}