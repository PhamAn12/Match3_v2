using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;
using UnityEngine.SceneManagement;
using Path = System.Collections.Generic.HashSet<System.Tuple<float, float>>;
public class ClassifyItemOverlay: ReactiveSystem<GameEntity>
{
    private GameContext gameContext;
    private static int Size = 99;
    private readonly string ASSET_NAME_BRICK = "Prefabs/GenerateBrick";
    Queue<GameEntity> queueRed = new Queue<GameEntity>();
    List<GameEntity> ListRed = new List<GameEntity>();
    bool[,] visited = new bool[Size,Size];
    public ClassifyItemOverlay(GameContext game) : base(game)
    {
        gameContext = game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Overlay);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isOverlay = true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var gameBoard = gameContext.CreateGameBoard().boadGame;
        //GameEntity[] movables = new GameEntity[100];
        
        //Debug.Log(gameBoard.columns + "va" + gameBoard.row);
        for (var c = 0 * 1.5f; c < gameBoard.columns * 1.5f; c += 1.5f)
        {
            for (var r = 0 * 1.5f; r < gameBoard.row * 1.5f; r += 1.5f)
            {
                var position = new Vector2(c, r);
                if (visited[(int) (c / 1.5f), (int) (r / 1.5f)])
                    continue;
                Path path = new Path();
                if(!gameContext.GetEntitiesWithPosition(position).Equals(null))
                {
                    
                    CheckAdjacent(c, r, visited, path);
                    Debug.Log("Path : " + path.Count() + "position : " + position);
                }

            }
        }
        //Debug.Log("mmdmd : " + gameContext.GetEntitiesWithPosition(new Vector2(0,1.5f)).SingleEntity());
    }

    void CheckAdjacent( float i, float j, bool[,] visited,
        HashSet<Tuple<float, float>> path)
    {
        GameEntity curentEntity = new GameEntity();
        //GameEntity currentValue = curentEntity;
        if(!gameContext.GetEntitiesWithPosition(new Vector2(i, j)).Equals(null))
        {
            //Debug.Log("kdkddkk : " + gameContext.GetEntitiesWithPosition(new Vector2(i, j)));
            curentEntity = gameContext.GetEntitiesWithPosition(new Vector2(i, j)).SingleEntity();
        }
        else
        {
            Debug.Log("khabanh");
        }
        path.Add(new Tuple<float, float>(i,j));
        visited[(int) (i / 1.5f),(int) (j/1.5f)] = true;
        //
        if (i >= 1.5f && !visited[(int) (i - 1.5f / 1.5f),(int) (j/1.5f)] &&
            gameContext.GetEntitiesWithPosition(new Vector2(i - 1.5f, j)).SingleEntity().asset.name == curentEntity.asset.name)
        {
            Debug.Log("xet : " + gameContext.GetEntitiesWithPosition(new Vector2(i - 1.5f, j)).SingleEntity().asset.name +  " current : " + curentEntity.asset.name);
            CheckAdjacent(i - 1.5f,j,visited,path);
        }
        
//        if (i < 8 && !visited[(int) (i + 1.5f / 1.5f),(int) (j/1.5f)] &&
//            gameContext.GetEntitiesWithPosition(new Vector2(i + 1.5f, j)).SingleEntity().asset.name == curentEntity.asset.name)
//        {
//            Debug.Log("xet : " + gameContext.GetEntitiesWithPosition(new Vector2(i + 1.5f, j)).SingleEntity().asset.name +  " current : " + curentEntity.asset.name);
//            CheckAdjacent(i + 1.5f,j,visited,path);
//        }
        
        if (j >= 1.5f && !visited[(int) (i/ 1.5f),(int) (j - 1.5f /1.5f)] &&
            gameContext.GetEntitiesWithPosition(new Vector2(i, j - 1.5f)).SingleEntity().asset.name == curentEntity.asset.name)
        {
            Debug.Log("xet : " + gameContext.GetEntitiesWithPosition(new Vector2(i, j - 1.5f)).SingleEntity().asset.name +  " current : " + curentEntity.asset.name);
            CheckAdjacent(i ,j - 1.5f,visited,path);
        }
//        
//        if (j <9 && !visited[(int) (i/ 1.5f),(int) (j + 1.5f /1.5f)] &&
//            gameContext.GetEntitiesWithPosition(new Vector2(i, j + 1.5f)).SingleEntity().asset.name == curentEntity.asset.name)
//        {
//            Debug.Log("xet : " + gameContext.GetEntitiesWithPosition(new Vector2(i, j + 1.5f)).SingleEntity().asset.name +  " current : " + curentEntity.asset.name);
//            CheckAdjacent(i ,j + 1.5f,visited,path);
//        }
    }
}