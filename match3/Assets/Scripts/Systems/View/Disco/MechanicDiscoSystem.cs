using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Entitas;
using TMPro.Examples;
using UnityEngine;
using Random = UnityEngine.Random;

public class MechanicDiscoSystem : ReactiveSystem<GameEntity>
{
    private GameContext gameContext;
    private IGroup<GameEntity> blockOnBoard;
    static readonly string[] Rocket = {
        "Vertical",
        "Horizontal"

    };
    public MechanicDiscoSystem(GameContext game) : base(game)
    {
        gameContext = game;
        blockOnBoard = gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.Position, GameMatcher.BoadGameElement)
        );
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AnyOf(GameMatcher.TapOnDisco));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isDisco;
    }

    protected override void Execute(List<GameEntity> entities)
    {

        foreach (var e in entities)
        {
            
//            Debug.Log("e in disco mechanic : " + e);
            var allBlock = blockOnBoard.GetEntities().ToList();
            var posX = e.position.value.x;
            var posY = e.position.value.y;
            var flag = 0; // boom
            var flag1 = 0; // rocket
            var flag2 = 0; // disco
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
                        flag1 = 1;
                    }
                    else if (block.isDisco)
                    {
                        flag2 = 1;
                    }
                }
            }

            Debug.Log("flag ; " + flag + "flag1 : " + flag1 + "flag2 : " + flag2 + "e : " + e);
            
            if (e.isDisco)
            {
                if (e.typeDisco.typeDisco.Equals("Red"))
                {
                    Debug.Log("RED");
                    Handle(flag, flag1, flag2, allBlock, e,"Prefabs/Piece0");
                }
                if (e.typeDisco.typeDisco.Equals("Blue"))
                {
                    Debug.Log("BLUE");
                    Handle(flag, flag1, flag2, allBlock, e,"Prefabs/Block_1");
                }
                if (e.typeDisco.typeDisco.Equals("Green"))
                {
                    Debug.Log("GREEN");
                    Handle(flag, flag1, flag2, allBlock, e,"Prefabs/Block_2");
                }
                if (e.typeDisco.typeDisco.Equals("Yellow"))
                {
                    Debug.Log("YELLOW");
                    Handle(flag, flag1, flag2, allBlock, e,"Prefabs/Piece3");
                }
            }
        }

    }

    void HandleTapOnDisco(string blockname, List<GameEntity> allBlock, GameEntity e)
    {

        foreach (var block in allBlock)
        {
            if (block.asset.name.Equals(blockname))
            {
                block.isRemoveViewDisco = true;
            }
        }

        e.isRemoveViewDisco = true;
    }
    

    void Handle(int flag, int flag1,int flag2, List<GameEntity> allBlock, GameEntity e, string nameBlock)
    {
        Queue<GameEntity> blockQueueBoom = new Queue<GameEntity>(); // chuyển hình lần lượt
        Queue<GameEntity> blockQueueBoomExplosive = new Queue<GameEntity>(); // nổ lần lượt 
        Queue<GameEntity> blockQueueRocket = new Queue<GameEntity>();// chuyển hình lần lượt thành rocket
        Queue<GameEntity> blockQueueRocketExplosive = new Queue<GameEntity>();// nổ lần lượt
        if (flag2 == 1)
        {
            foreach (var block in allBlock)
            {
                block.isRemoveViewDisco = true;
            }
        }
        else if (flag == 1)
        {
            //blockQueueBoom.Enqueue(e);
            e.isRemoveViewDisco = true;
            foreach (var block in allBlock)
            {
                if (block.asset.name.Equals(nameBlock) || block.isBoom)
                {
                    // Enqueue 
                    blockQueueBoom.Enqueue(block);
                    blockQueueBoomExplosive.Enqueue(block);
                    // cho nổ 
                    //GameController.Instance.StartCoroutine(DelayBeforeBoom(0.5f, block,e));   
                }
                        
            }
            
            //e.isRemoveViewDisco = true;
            
            
            float t = 0.0f;
            while (blockQueueBoom.Count != 0)
            {
                
                var blockPeek = blockQueueBoom.Peek();
                GameController.Instance.StartCoroutine(PeekTurnBoom(t, blockPeek));
                blockQueueBoom.Dequeue();
                t += 0.1f;
            }
            Debug.Log("TTTTTTTT: " + t);
            while (blockQueueBoomExplosive.Count != 0)
            {
                var blockPeek = blockQueueBoomExplosive.Peek();
                Debug.Log("Boom in queue : " + blockPeek + " time : " + t);
                GameController.Instance.StartCoroutine(DelayBeforeBoom(t, blockPeek));
                blockQueueBoomExplosive.Dequeue();
                t += 0.15f;
            }
        }
        else if (flag1 == 1)
        {
            e.isRemoveViewDisco = true;
            foreach (var block in allBlock)
            {
                
                if(block.asset.name.Equals(nameBlock) || block.hasRocket)
                {
//                    block.ReplaceRocket(Rocket[Random.Range(0, Rocket.Length)]);
//                    GameController.Instance.StartCoroutine(DelayBeforeRocket(0.5f, block,e));
                    blockQueueRocket.Enqueue(block);
                    blockQueueRocketExplosive.Enqueue(block);
                }
                    
            }
            //blockQueueRocket.Enqueue(e);
            
            float t = 0.0f;
            while (blockQueueRocket.Count != 0)
            {
                var blockPeek = blockQueueRocket.Peek();
                
                GameController.Instance.StartCoroutine(PeekTurnRocket(t, blockPeek));
                blockQueueRocket.Dequeue();
                t += 0.1f;
            }

            while (blockQueueRocketExplosive.Count != 0)
            {
                var blockPeek = blockQueueRocketExplosive.Peek();
                //Debug.Log("Block Peek : " + blockPeek);
                GameController.Instance.StartCoroutine(DelayBeforeRocket(t, blockPeek));
                blockQueueRocketExplosive.Dequeue();
                t += 0.3f;
            }
        }
        else
        {
            HandleTapOnDisco(nameBlock, allBlock, e);
        }
    }
    // chuyển lần lượt các block thành bom 
    IEnumerator PeekTurnBoom(float time, GameEntity e)
    {
        yield return new WaitForSeconds(time);
        if(e.hasPosition)
            e.isBoom = true;
    }
    // chuyển lần lượt các block thành rocket 
    IEnumerator PeekTurnRocket(float time, GameEntity e)
    {
        yield return new WaitForSeconds(time);
        if(e.hasPosition)
            e.ReplaceRocket(Rocket[Random.Range(0,Rocket.Length)]);
    }
    private IEnumerator DelayBeforeBoom(float time, GameEntity block)
    {
        yield return new WaitForSeconds(time);
        // lọc điều kiện block trong queue chưa bị nổ bởi bom khác và phải là bom
        if (!block.isTapOnBoom && block.hasView && block.hasPosition && block.isBoom)
        {
            
            //Debug.Log("block no : " + block);
            block.isTapOnBoom = true;
        }
            
        //e.isRemoveViewDisco = true;
    }
    IEnumerator DelayBeforeRocket(float time, GameEntity block)
    {
        
        yield return new WaitForSeconds(time);
        //e.isRemoveViewDisco = true;
        if (block.hasRocket && block.hasView && block.hasView && !block.hasTapOnRocket)
        {
            if (block.rocket.typeRocket == "Vertical")
            {
                block.ReplaceTapOnRocket("TapOnVR");
            }
            else if (block.rocket.typeRocket == "Horizontal")
            {
                block.ReplaceTapOnRocket("TapOnHR");
            }
        }

        
    }


}
