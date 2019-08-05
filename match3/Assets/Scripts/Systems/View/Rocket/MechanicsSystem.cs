using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Entitas;
using UnityEngine;

public class MechanicsSystem : ReactiveSystem<GameEntity>
{
    private GameContext gameContext;
    private IGroup<GameEntity> blockOnBoard;
    public MechanicsSystem(GameContext game) : base(game)
    {
        gameContext = game;
        
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.TapOnBoom));
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            e.isMove = true;
            if (e.hasTypeTapOn)
                if (e.typeTapOn.typeTap.Equals("TapOnBoom"))
                {
                    var posX = e.position.value.x;
                    var posY = e.position.value.y;

                    var allBlock = blockOnBoard.GetEntities();
                    foreach (var block in allBlock)
                    {
                        if (!block.hasTypeMechanicsDestroy &&
                            block.position.value.y == posY + 1.5f && block.position.value.x == posX + 1.5f
                            || block.position.value.y == posY + 1.5f && block.position.value.x == posX - 1.5f
                            || block.position.value.y == posY - 1.5f && block.position.value.x == posX - 1.5f
                            || block.position.value.y == posY - 1.5f && block.position.value.x == posX + 1.5f
                            || block.position.value.y == posY + 1.5f && block.position.value.x == posX
                            || block.position.value.y == posY - 1.5f && block.position.value.x == posX
                            || block.position.value.y == posY && block.position.value.x == posX + 1.5f
                            || block.position.value.y == posY && block.position.value.x == posX - 1.5f
                        )
                        {
                            if (block.asset.name.Equals("Prefabs/Rocket"))
                            {
                                block.ReplaceTypeTapOn("TapOnRocket");
                            }
                            else if (block.asset.name.Equals("Prefabs/Boom"))
                            {
                                block.ReplaceTypeTapOn("TapOnBoom");
                            }
                            else
                                block.ReplaceTypeMechanicsDestroy("Boom");
                        }
                    }

                    e.AddTypeMechanicsDestroy("Boom");
                }
                else if (e.typeTapOn.typeTap.Equals("TapOnRocket"))
                {
                    var posY = e.position.value.y;
                    var posX = e.position.value.x;
                    var allBlock = blockOnBoard.GetEntities();

                    foreach (var block in allBlock)
                    {
                        if (block.position.value.y == posY && block.position.value.x != posX &&
                            !block.hasTypeMechanicsDestroy)
                        {
                            if (block.asset.name.Equals("Prefabs/Boom"))
                            {
                                block.ReplaceTypeTapOn("TapOnBoom");
                            }
                            else
                                block.AddTypeMechanicsDestroy("Rocket");
                        }
                    }


                    e.AddTypeMechanicsDestroy("Rocket");
                }


        }
    }
}