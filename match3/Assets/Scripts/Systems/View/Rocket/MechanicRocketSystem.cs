using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class MechanicRocketSystem : ReactiveSystem<GameEntity>
{
    private GameContext gameContext;
    private IGroup<GameEntity> blockOnBoard;
    public MechanicRocketSystem(GameContext game) : base(game)
    {
        gameContext = game;
        blockOnBoard = gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.Position, GameMatcher.BoadGameElement)
        );
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.TapOnRocket).NoneOf(GameMatcher.BlockRocketMechanic));
        
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isBlockRocketMechanic == false;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        
        var flag = 0; // boom
        var flag1 = 0; // rocket
        var flag2 = 0; // xung quanh có > 2 quả bom 
        var allBlock = blockOnBoard.GetEntities();
        foreach (var e in entities) if(e.hasPosition)
        {
            var posX = e.position.value.x;
            var posY = e.position.value.y;
            int count = 0;
            foreach (var block in allBlock)
            {
                //Debug.Log("Cac block : " + block);
                // Check các trường hợp xung quanh e có bom, rocket hay 2 rocket 
                if (block.position.value.y == posY + 1.5f && block.position.value.x == posX
                    || block.position.value.y == posY - 1.5f && block.position.value.x == posX
                    || block.position.value.y == posY && block.position.value.x == posX + 1.5f
                    || block.position.value.y == posY && block.position.value.x == posX - 1.5f)
                {
                    if (block.isBoom)
                    {
                        //flag = 1;
                        count++;
                                
                    }
                    else if (block.hasRocket)
                    {
                        
                        flag1 = 1;
                    }
                    
                }

            }

            if (count == 1)
            {
                flag = 1;
            }
            else if (count > 1)
            {
                flag2 = 1;
            }
            //Debug.Log("count : " + count + "flag, flag1, flag2 : " + flag + " " + flag1 + " " + flag2);
            if (e.hasTapOnRocket)
            {
                if (e.tapOnRocket.typeRocket == "TapOnVR")
                {
                    foreach (var block in allBlock)
                    {
                        // trường hợp xung quanh có >2 quả bom
                        if (flag2 == 1)
                        {
                            for (var i = posX - 4.5f; i < posX + 4.5f; i += 1.5f)
                            {
                                for (var j = posY - 4.5f; j < posY + 4.5f; j += 1.5f)
                                {
                                    if (block.position.value.x == i && block.position.value.y == j)
                                    {
                                        block.ReplaceRemoveViewRocket("RemoveVR");
                                    }
                                }
                            }
                        }
                        // Truong hop 1 bom sat rocket
                        else if (flag == 1)
                        {
                            if (block.position.value.x == posX || block.position.value.x == posX + 1.5f 
                                                               || block.position.value.x == posX -1.5f
                            )
                            {
                                block.ReplaceRemoveViewRocket("RemoveVR");
                            }
                            if (block.position.value.y == posY || block.position.value.y == posY + 1.5f 
                                                               || block.position.value.y == posY -1.5f
                            )
                            {
                                block.ReplaceRemoveViewRocket("RemoveHR");
                            }
                        }
                        // truong hop rocket sat rocket
                        else if (flag1 == 1)
                        {
                            if (block.position.value.x == posX)
                            {
                                if (block.isBoom)
                                    block.isTapOnBoom = true;
                                else
                                    block.ReplaceRemoveViewRocket("RemoveVR");
                            }
                            if (block.position.value.y == posY)
                            {
                                if (block.isBoom)
                                    block.isTapOnBoom = true;
                                else
                                    block.ReplaceRemoveViewRocket("RemoveHR");
                            }
                        }
                        // trường hợp thường 
                        else if (block.position.value.y != posY && block.position.value.x == posX)
                        {
//                            // Truong hop rocket cat nhau
                            if (block.hasRocket && block.rocket.typeRocket == "Horizontal")
                            {
                                block.ReplaceTapOnRocket("TapOnHR");
                            }
                            // truong hop cat bom
                            else if (block.isBoom)
                            {
                                block.isTapOnBoom = true;
                            }
                            //truong hop cat disco
                            else if (block.isDisco)
                            {
                                block.isTapOnDisco = true;
                            }
                            // truong hop thuong
                            else 
                                block.ReplaceRemoveViewRocket("RemoveVR");
                        }
                    }
                    e.ReplaceRemoveViewRocket("RemoveVR");
                }
                else if (e.tapOnRocket.typeRocket == "TapOnHR")
                {
                    
                    foreach (var block in allBlock)
                    {
                        if (flag2 == 1)
                        {
                            for (var i = posX - 4.5f; i < posX + 4.5f; i += 1.5f)
                            {
                                for (var j = posY - 4.5f; j < posY + 4.5f; j += 1.5f)
                                {
                                    if (block.position.value.x == i && block.position.value.y == j)
                                    {
                                        block.ReplaceRemoveViewRocket("RemoveHR");
                                    }
                                }
                            }
                        }
                        else if (flag == 1)
                        {
                            if (block.position.value.y == posY || block.position.value.y == posY + 1.5f 
                                                               || block.position.value.y == posY -1.5f
                            )
                            {
                                block.ReplaceRemoveViewRocket("RemoveHR");
                            }
                            if (block.position.value.x == posX || block.position.value.x == posX + 1.5f 
                                                               || block.position.value.x == posX -1.5f
                            )
                            {
                                block.ReplaceRemoveViewRocket("RemoveVR");
                            }
                            
                        }
                        else if (flag1 == 1)
                        {
                            
                            if (block.position.value.x == posX)
                            {
                                if (block.isBoom)
                                    block.isTapOnBoom = true;
                                else
                                block.ReplaceRemoveViewRocket("RemoveVR");
                            }
                            if (block.position.value.y == posY)
                            {
                                if (block.isBoom)
                                    block.isTapOnBoom = true;
                                else
                                block.ReplaceRemoveViewRocket("RemoveHR");
                            }
                        }
                        else if (block.position.value.y == posY && block.position.value.x != posX)
                        {
                            // truong hop cat nhau
                            if (block.hasRocket && block.rocket.typeRocket == "Vertical")
                            {
                                block.ReplaceTapOnRocket("TapOnVR");
                            }
                            // truong hop cat bom
                            else if (block.isBoom)
                            {
                                block.isTapOnBoom = true;
                            }
                            // truong hop cat disco
                            else if (block.isDisco)
                            {
                                block.isTapOnDisco = true;
                            }
                            //truong hop thuong                            
                            else
                                block.ReplaceRemoveViewRocket("RemoveHR");
                        }
                    }
                    e.ReplaceRemoveViewRocket("RemoveHR");
                }
            }// if (e.hasTapOnRocket)
        }// for
    }// class Execute

}
