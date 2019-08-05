using System.Collections;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class MechanicBoomSystem : ReactiveSystem<GameEntity>
{
    private GameContext gameContext;
    private IGroup<GameEntity> blockOnBoard;
    public MechanicBoomSystem(GameContext game) : base(game)
    {
        gameContext = game;
        blockOnBoard = gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.Position, GameMatcher.BoadGameElement)
        );
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.TapOnBoom).NoneOf(GameMatcher.BlockBoomMechanic));
        
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isBlockBoomMechanic == false;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var allBlock = blockOnBoard.GetEntities();
        int flag = 0;  // boom
        int flag1 = 0; // rocket 
        int flag2 = 0; // disco 
        foreach (var e in entities)
        {
            //Debug.Log("e in boom mechanic : " + e);
            var posX = e.position.value.x;
            var posY = e.position.value.y;
            float posBx = 0 ;
            float posBy = 0 ;
            GameEntity discoBlock = new GameEntity();
            foreach (var block in allBlock)
            {
                if (block.position.value.y == posY + 1.5f && block.position.value.x == posX
                    || block.position.value.y == posY - 1.5f && block.position.value.x == posX
                    || block.position.value.y == posY && block.position.value.x == posX + 1.5f
                    || block.position.value.y == posY && block.position.value.x == posX - 1.5f)
                {
                    if (block.isBoom)
                    {
                        flag = 1;
                    }
                    else if (block.hasRocket)
                    {
                        var b = block;
                        posBx = b.position.value.x;
                        posBy = b.position.value.y;
                        flag1 = 1;
                    }
                    else if (block.isDisco)
                    {
                        flag2 = 0;
                        discoBlock = block;
                    }
                }
                if (block.position.value.y == posBy + 1.5f && block.position.value.x == posBx
                    || block.position.value.y == posBy - 1.5f && block.position.value.x == posBx
                    || block.position.value.y == posBy && block.position.value.x == posBx + 1.5f
                    || block.position.value.y == posY && block.position.value.x == posBx - 1.5f)
                {
                    // trường hợp bom cạnh rocket, rocket lại cạnh bom 
                    if (block.isBoom && !block.position.value.Equals(e.position.value))
                    {
                        Debug.Log("block pos : " + block.position.value + " e pos : " + e.position.value);
                        flag = 1;
                    }
                }
            }
            Debug.Log("posBx : " + posBx + "posBy : " + posBy + " e : " + e);
            Debug.Log("flag : " + flag + " flag1 : " + flag1 + "flag2 : " + flag2);
            // ưu tiên ăn disco trước bom rồi đến rocket 
            foreach (var block in allBlock)
            {
                // bom ăn disco sát
                if (flag2 == 1)
                {
                    //Debug.Log("vi tri block : " + discoBlock.position.value);
                    discoBlock.isTapOnDisco = true;
                }
                // boom an boom
                else if (flag == 1)
                {
                    for (var i = posX - 4.5f; i < posX + 4.5f; i += 1.5f)
                    {
                        for (var j = posY - 4.5f; j < posY + 4.5f; j += 1.5f)
                        {
                            if (block.position.value.x == i && block.position.value.y == j)
                            {
                                block.isRemoveViewBoom = true;
                            }
                        }
                    }
                }
                // boom an rocket
                else if (flag1 == 1)
                {
                    if (block.position.value.x == posX || block.position.value.x == posX + 1.5f 
                                                       || block.position.value.x == posX -1.5f
                        || block.position.value.y == posY || block.position.value.y == posY + 1.5f 
                                                       || block.position.value.y == posY -1.5f
                        )
                    {
                        block.isRemoveViewBoom = true;
                    }
                    
                }
                //an thuong
                else if (block.position.value.y == posY + 1.5f && block.position.value.x == posX + 1.5f
                    || block.position.value.y == posY + 1.5f && block.position.value.x == posX - 1.5f
                    || block.position.value.y == posY - 1.5f && block.position.value.x == posX - 1.5f
                    || block.position.value.y == posY - 1.5f && block.position.value.x == posX + 1.5f
                    || block.position.value.y == posY + 1.5f && block.position.value.x == posX
                    || block.position.value.y == posY - 1.5f && block.position.value.x == posX
                    || block.position.value.y == posY && block.position.value.x == posX + 1.5f
                    || block.position.value.y == posY && block.position.value.x == posX - 1.5f)
                {
                    // bom an rocket
                    if (block.hasRocket && block.rocket.typeRocket == "Vertical")
                    {
                        block.ReplaceTapOnRocket("TapOnVR");
                    }
                    else if (block.hasRocket && block.rocket.typeRocket.Equals("Horizontal"))
                    {
                        block.ReplaceTapOnRocket("TapOnHR");
                    }
                    // bom an bom
                    else if (block.isBoom)
                    {
                        block.isTapOnBoom = true;
                    }
                    // bom an disco
                    else if (block.isDisco)
                        block.isTapOnDisco = true;
                    else
                        block.isRemoveViewBoom = true;
                }
            }

            e.isRemoveViewBoom = true;
        } // for tong
    } //class 
}
