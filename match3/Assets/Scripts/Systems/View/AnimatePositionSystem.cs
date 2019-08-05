using System.Collections.Generic;
using DG.Tweening;
using Entitas;
using UnityEngine;

public sealed class AnimatePositionSystem : ReactiveSystem<GameEntity>
{
    private readonly float SPEED = 0.8f;
    readonly GameContext context;
    IGroup<GameEntity> _movebleBlock;
    private IGroup<GameEntity> mechanicGroup;
    private IGroup<GameEntity> tappedBlock;
    public AnimatePositionSystem(GameContext Game) : base(Game)
    {
        context = Game;
        // Downable : except brick block Movable : block spawn  
        _movebleBlock = context.GetGroup(GameMatcher.AllOf(GameMatcher.Movable, GameMatcher.Downable));
        mechanicGroup = context.GetGroup(GameMatcher.TypeMechanic);
        tappedBlock = context.GetGroup(GameMatcher.Tabbed);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Position);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasAsset && entity.hasPosition;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var tappedBlocks = tappedBlock.GetEntities();
        foreach (var block in tappedBlocks)
        {
            Debug.Log("tapped block : " + block);
        }
        var gameBoard = context.CreateGameBoard().boadGame;
        var topRow = gameBoard.row * 1.5f;
        var topCol = gameBoard.columns * 1.5f;
        float length;
        if (topRow > topCol)
            length = topRow;
        else
            length = topCol;
        var movebleBlock = _movebleBlock.GetEntities();
        List<GameEntity> movebleBlockList = new List<GameEntity>();
        foreach (var m in movebleBlock)
        {
            if(m.asset.name == "Prefabs/Rocket" || m.asset.name == "Prefabs/Boom")
                Debug.Log("ANC " + m);
            movebleBlockList.Add(m);
            m.isMovable = false;
        }
        foreach (var e in entities)
        {
//            Debug.Log("e in enities list : " + e.asset.name + "pos : " + e.position.value);
        }
        for (var r = 0 * 1.5f; r < length; r += 1.5f)
        {
            
            var count = 0; // count number of block be created in a column
            var tempr = r;
            foreach (var m in movebleBlockList)
            {
                if ((m.view.gameObject.transform.position.x == tempr) && (m.asset.name != "Prefabs/GenerateBrick") 
                                                                      && (m.asset.name != "Prefabs/Boom") 
                                                                      && (m.asset.name != "Prefabs/Rocket"))
                {
                    count++;
                }
            }

            //Debug.Log("COUNT " + count + "tempr " + tempr);
            foreach (var e in entities)
            {
                //Debug.Log("eeeeeee" + e.view.gameObject.transform.position);
                var pos = e.position;
                //Debug.Log("pospospospos" + pos.value.x + "  " + pos.value.y);
                
                var isTopRow = (pos.value.y.Equals(topRow - 1.5f));
                var isSecondRow = (pos.value.y.Equals(topRow - 3f));
                var isThirdRow = (pos.value.y.Equals(topRow - 4.5f));
                var is4thRow = (pos.value.y.Equals(topRow - 6f));
                var is5thRow = (pos.value.y.Equals(topRow - 7.5f));
                var is6thRow = (pos.value.y.Equals(topRow - 9f));
                var is7thRow = (pos.value.y.Equals(topRow - 10.5f));
                var is8thRow = (pos.value.y.Equals(topRow - 12f));
                var is9thRow = (pos.value.y.Equals(topRow - 13.5f));               
                
                bool [] numRow = new bool[10];
                
                numRow[1] = isTopRow;
                numRow[2] = isTopRow || isSecondRow;
                numRow[3] = isTopRow || isSecondRow || isThirdRow;
                numRow[4] = isTopRow || isSecondRow || isThirdRow || is4thRow;
                numRow[5] = isTopRow || isSecondRow || isThirdRow || is4thRow || is5thRow;
                numRow[6] = isTopRow || isSecondRow || isThirdRow || is4thRow || is5thRow || is6thRow;
                numRow[7] = isTopRow || isSecondRow || isThirdRow || is4thRow || is5thRow || is6thRow || is7thRow;
                numRow[8] = isTopRow || isSecondRow || isThirdRow || is4thRow || is5thRow || is6thRow || is7thRow
                    || is8thRow;
                numRow[9] = isTopRow || isSecondRow || isThirdRow || is4thRow || is5thRow || is6thRow || is7thRow
                            || is8thRow || is9thRow;
                

                if (count == 0)
                {
  
                    if (e.position.value.x == tempr)
                    {
                        e.view.gameObject.transform.DOMove(new Vector3(pos.value.x, pos.value.y, 0f), SPEED);
                    }             
                }
                for (int i = 1; i <= 9; i++)
                {
                    if (count == i)
                    {
                        if (e.position.value.x == tempr)
                        {
                            if (numRow[i])
                            {
                                e.view.gameObject.transform.position = new Vector3(pos.value.x, pos.value.y + 1.5f * i);
                            }
                            e.view.gameObject.transform.DOMove(new Vector3(pos.value.x, pos.value.y, 0f), SPEED);
                            
                        }

                        break;
                    }
                }
//
//                if (count == 1)
//                {
//                    if (e.position.value.x == tempr )
//                    {
//                        
//                        if (isTopRow)
//                        {
//                            e.view.gameObject.transform.position = new Vector3(pos.value.x, pos.value.y + 1.5f);
//                            
//
//                        }
//                        Debug.Log("trc : " + e.view.gameObject.transform.position + " " + e.position.value);
//                        e.view.gameObject.transform.DOMove(new Vector3(pos.value.x, pos.value.y, 0f), SPEED);
//                        //Debug.Log("sau : " + e.view.gameObject.transform.position + " " + e.position.value);
//                    }
//                }
//                if (count == 2)
//                {
//                    if (e.position.value.x == tempr)
//                    {
//                        if (isTopRow || isSecondRow)
//                        {
//                            e.view.gameObject.transform.position = new Vector3(pos.value.x, pos.value.y + 3f);
//                            
//                        }
//
//                        e.view.gameObject.transform.DOMove(new Vector3(pos.value.x, pos.value.y, 0f), SPEED);
//                    }
//                   
//                }
//                else if (count == 3)
//                {
//                    if (e.position.value.x == tempr)
//                    {
//                        if (isTopRow || isSecondRow || isThirdRow
//                            )
//                        {
//                            e.view.gameObject.transform.position = new Vector3(pos.value.x,pos.value.y + 4.5f);
//                            
//                        } 
//                        
//                        e.view.gameObject.transform.DOMove(new Vector3(pos.value.x, pos.value.y, 0f), SPEED);
//                    }
//                    
//                }
//                else if (count == 4)
//                {
//                    if (e.position.value.x == tempr)
//                    {
//                        if (isTopRow || isSecondRow || isThirdRow || is4thRow 
//                            )
//                        {
//                            e.view.gameObject.transform.position = new Vector3(pos.value.x,pos.value.y + 6f);
//                            
//                        } 
//                        
//                        e.view.gameObject.transform.DOMove(new Vector3(pos.value.x, pos.value.y, 0f), SPEED);
//                    }
//                   
//                }
//                else if (count == 5)
//                {
//                    if (e.position.value.x == tempr)
//                    {
//                        if (isTopRow || isSecondRow || isThirdRow|| is4thRow || is5thRow 
//                            )
//                        {
//                            e.view.gameObject.transform.position = new Vector3(pos.value.x,pos.value.y + 7.5f);
//                            
//                        } 
//                        
//                        e.view.gameObject.transform.DOMove(new Vector3(pos.value.x, pos.value.y, 0f), SPEED);
//                    }
//                   
//                }
//                else if (count == 6)
//                {
//                    if (e.position.value.x == tempr)
//                    {
//                        if (isTopRow || isSecondRow || isThirdRow|| is4thRow || is5thRow || is6thRow
//                           )
//                        {
//                            e.view.gameObject.transform.position = new Vector3(pos.value.x,pos.value.y + 9f);
//                            
//                        } 
//                        
//                        e.view.gameObject.transform.DOMove(new Vector3(pos.value.x, pos.value.y, 0f), SPEED);
//                    }
//                    
//                }
//                else if (count == 7)
//                {
//                    if (e.position.value.x == tempr)
//                    {
//                        if (isTopRow || isSecondRow || isThirdRow|| is4thRow || is5thRow|| is6thRow || is7thRow
//                           )
//                        {
//                            e.view.gameObject.transform.position = new Vector3(pos.value.x,pos.value.y + 10.5f);
//                            e.view.gameObject.transform.DOScaleY(1.2f, 0.3f)
//                                .OnComplete(
//                                    () => { e.view.gameObject.transform.DOScaleY(1f, 0.3f); }
//                                );
//                        } 
//                        
//                        e.view.gameObject.transform.DOMove(new Vector3(pos.value.x, pos.value.y, 0f), SPEED);
//                    }
//                    
//                }
//                else if (count == 8)
//                {
//                    if (e.position.value.x == tempr)
//                    {
//                        if (isTopRow || isSecondRow || isThirdRow|| is4thRow || is5thRow|| is6thRow || is7thRow || is8thRow
//                            )
//                        {
//                            e.view.gameObject.transform.position = new Vector3(pos.value.x,pos.value.y + 12f);
//                            
//                        } 
//                        
//                        e.view.gameObject.transform.DOMove(new Vector3(pos.value.x, pos.value.y, 0f), SPEED);
//                    }
//                    
//                }
//                else if (count == 9)
//                {
//                    if (e.position.value.x == tempr)
//                    {
//                        if (isTopRow || isSecondRow || isThirdRow|| is4thRow || is5thRow|| is6thRow || is7thRow 
//                            || is8thRow || is9thRow )
//                        {
//                            e.view.gameObject.transform.position = new Vector3(pos.value.x,pos.value.y + 13.5f);
//                            
//                        } 
//                        
//                        e.view.gameObject.transform.DOMove(new Vector3(pos.value.x, pos.value.y, 0f), SPEED);
//                    }
//                    
//                }
                
                
                
            }
        }
    }
}