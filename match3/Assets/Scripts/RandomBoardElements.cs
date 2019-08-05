using System;
using System.Collections;
using System.Collections.Generic;
using DesperateDevs.Utils;
using Entitas;
using Entitas.Unity;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public static class RandomBoardElements 
{
    // private static  GameObject block;
    static readonly string[] items = {
        //"Prefabs/GenerateBrick",
        "Prefabs/Piece0",
        "Prefabs/Block_2",
        "Prefabs/Block_1",
        "Prefabs/Piece3"
        //"Prefabs/Piece4"
        //"Prefabs/Piece5",

    };

    static Button okButton;
    
    public static GameEntity CreateGameBoard(this GameContext context)
    {
        
        string rows = PlayerPrefs.GetString("SizeRows");
        string cols = PlayerPrefs.GetString("SizeCols");
//        Debug.Log("r and c " + rows + " " + cols);
        if (rows == null || rows == "" && cols == null || cols == "")
        {
            rows = "8";
            cols = "8";
        }
        var entity = context.CreateEntity();
        
        entity.AddBoadGame(Int32.Parse(cols), Int32.Parse(rows));
        GameObject.Find("Main Camera").transform.position = new Vector3((Int32.Parse(cols) * 1.5f) / 2, (Int32.Parse(rows) * 1.5f) /2 , 0);
        return entity;
    }
    
    public static GameEntity CreateRandomPiece(this GameContext context, float x, float y) 
    {
        var entity = context.CreateEntity();
        
        entity.AddPosition(new Vector2(x  * 1.5f, y * 1.5f));
        if (entity.position.value.x == 0 && entity.position.value.y == 0 ||
            entity.position.value.x == 0 && entity.position.value.y == 1.5f)
        {
            entity.AddAsset("Prefabs/Piece0");
        }
        else
            entity.AddAsset(items[Random.Range(0, items.Length)]);
        //entity.isMovable = true;
        entity.isBoadGameElement = true;
        entity.isDownable = true;
        return entity;
    }

    public static GameEntity CreateRandomBlock(this GameContext context, float x, float y)
    {
        var entity = context.CreateEntity();
        entity.AddPosition(new Vector2(x  * 1.5f, y * 1.5f));
        if (entity.position.value.x == 0 && entity.position.value.y == 0 ||
            entity.position.value.x == 0 && entity.position.value.y == 1.5f)
        {
            entity.AddAsset("Prefabs/Piece0");
        }
        else
        {
            entity.AddAsset("Prefabs/GenerateBrick");
        }
        entity.isBoadGameElement = true;
        //entity.isMovable = true;
        return entity;
    }

    public static GameEntity CreateHeart(this GameContext context, float x, float y)
    {
        var entity = context.CreateEntity();
        entity.AddPosition(new Vector2(x,y));
        entity.AddAsset("Prefabs/Heart");
        entity.isHeart = true;
        return entity;
    }
    
}
