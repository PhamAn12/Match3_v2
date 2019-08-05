using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using Entitas;
using Entitas.Unity;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.SceneManagement;


public class CheckDieSystem : ReactiveSystem<GameEntity>

{
    Contexts context;
    private int Size = 9;
    private readonly GameContext gameContext;

    private IGroup<GameEntity> heartGroup;

    // All block in the board
    private IGroup<GameEntity> blockGroup;

    // All block without brick
    private IGroup<GameEntity> blockSuffle;
    private IGroup<GameEntity> viewGroup;
    private IGroup<GameEntity> mechanicsGroup;
    private readonly string ASSET_NAME_BRICK = "Prefabs/GenerateBrick";

    public CheckDieSystem(GameContext Game) : base(Game)
    {
        gameContext = Game;
        heartGroup = gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.Heart, GameMatcher.Position));
        blockGroup = gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.Position,
            GameMatcher.BoadGameElement).NoneOf(GameMatcher.Heart, GameMatcher.MoveNum));
        blockSuffle = gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.Position,
            GameMatcher.BoadGameElement, GameMatcher.Downable).NoneOf(GameMatcher.Heart, GameMatcher.MoveNum));
        viewGroup = gameContext.GetGroup(GameMatcher.View);
        mechanicsGroup =
            gameContext.GetGroup(GameMatcher.AnyOf(GameMatcher.Boom, GameMatcher.Rocket, GameMatcher.Disco));
    }


    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.MoveNum);
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var blocksHeart = heartGroup.GetEntities();
        var blocks = blockGroup.GetEntities();
        var blockSuffles = blockSuffle.GetEntities();
        var mechanicBlockGroup = mechanicsGroup.GetEntities();
        int[,] Free = new int[Size, Size];

        int rnd;
        List<GameEntity> _listBlockSuffle = null;
        for (int i = 0; i < Size * Size; i++) Free[i % Size, i / Size] = 2;

        // check if num of block available and has no mechanic item on board
        if (CheckColorBlock(blockSuffles) == false && mechanicBlockGroup.Length == 0)
        {
            // call while still can't use move
            while (CheckDied() == false)
            {
                List<int> randomList = new List<int>();

                //Debug.Log("DIE");
                // Suffle all blocks if can't move

                for (int t = 0; t < blockSuffles.Length; t++)
                {
                    var temp = blockSuffles[t].position.value;
                    do
                    {
                        rnd = Random.Range(0, blockSuffles.Length);
                    } while (randomList.Contains(rnd));

                    randomList.Add(rnd);

                    blockSuffles[t].ReplacePosition(new Vector2(blockSuffles[rnd].position.value.x,
                        blockSuffles[rnd].position.value.y));
                    blockSuffles[rnd].ReplacePosition(new Vector2(temp.x, temp.y));
                }
            }
        }

        if (CheckColorBlock(blockSuffles) || entities[0].moveNum.value == 0)
        {
            //Debug.Log("THUA CUOC");
            GameController.Instance.StartCoroutine(DelayBeforeSwitchScene(0.9f));
        }
    }

    private IEnumerator DelayBeforeSwitchScene(float time)
    {
        yield return new WaitForSeconds(time);
        foreach (var viewElement in viewGroup)
        {
            if (viewElement.view.gameObject != null)
            {
                viewElement.view.gameObject.Unlink();
            }
        }

        SceneManager.LoadScene("Lose");
    }

    // check can't move 
    bool CheckDied()
    {
        var blocksHeart = heartGroup.GetEntities();
        var blocks = blockGroup.GetEntities();
        foreach (var b in blocks)
        {
//            Debug.Log("all block : " + b  );
        }
//        Debug.Log("block 0 : " + blocks[0]);

        var name = blocks[0].asset.name;
        var x = blocks[0].position.value.x;
        var y = blocks[0].position.value.y;
        Queue<GameEntity> queue = new Queue<GameEntity>();
        List<GameEntity> blockInQueue = new List<GameEntity>();
        int[,] Free = new int[Size, Size];
        for (int i = 0; i < Size * Size; i++) Free[i % Size, i / Size] = 0;
        queue.Enqueue(blocks[0]);
        Free[(int) (x / 1.5f), (int) (y / 1.5f)] = 1;
        var flag = 0;
        while (queue.Count != 0)
        {
            //Debug.Log("Queue count :" + queue.Count);
            GameEntity blockPeek = queue.Peek();
            //Debug.Log("GameEntityPeek :" + blockPeek.position.value.x + "  " + blockPeek.position.value.y);
            queue.Dequeue();
            foreach (var b in blocks)
            {
                if (b.position.value.x == blockPeek.position.value.x + 1.5 &&
                    b.position.value.y == blockPeek.position.value.y
                    && Free[(int) (b.position.value.x / 1.5f), (int) (b.position.value.y / 1.5f)] == 0
                )
                {
                    if (b.asset.name != blockPeek.asset.name ||
                        (b.asset.name == blockPeek.asset.name && b.asset.name.Equals(ASSET_NAME_BRICK)))
                    {
                        Free[(int) (b.position.value.x / 1.5f), (int) (b.position.value.y / 1.5f)] = 1;
                        queue.Enqueue(b);
                        // Check trong queue co 2 block cung mau canh nhau hay ko
                        foreach (var i in queue.ToArray())
                        {
                            if (!b.asset.name.Equals(ASSET_NAME_BRICK) && i.asset.name.Equals(b.asset.name) &&
                                ((b.position.value.x == i.position.value.x && b.position.value.y
                                  == i.position.value.y + 1.5) ||
                                 (b.position.value.x == i.position.value.x && b.position.value.y
                                  == i.position.value.y - 1.5) ||
                                 (b.position.value.x == i.position.value.x + 1.5 && b.position.value.y
                                  == i.position.value.y) ||
                                 (b.position.value.x == i.position.value.x - 1.5 && b.position.value.y
                                  == i.position.value.y)))
                            {
                                //Debug.Log("i11" + i.position.value.x + "  " + i.position.value.y);
                                //Debug.Log("ChuaDie11");
                                //Debug.Log("b11" + b.position.value.x + "  " + b.position.value.y);
                                return true;
                            }
                        }

                        //Debug.Log("b1" + b.position.value.x + "  " + b.position.value.y);
                        flag = 0;
                    }
                    else if (b.asset.name == blockPeek.asset.name)
                    {
                        //Debug.Log("ChuaDie1");
                        flag = 1;
                        return true;
                    }
                }

                if (b.position.value.x == blockPeek.position.value.x - 1.5 &&
                    b.position.value.y == blockPeek.position.value.y
                    && Free[(int) (b.position.value.x / 1.5f), (int) (b.position.value.y / 1.5f)] == 0
                )
                {
                    if (b.asset.name != blockPeek.asset.name ||
                        (b.asset.name == blockPeek.asset.name && b.asset.name.Equals(ASSET_NAME_BRICK)))
                    {
                        Free[(int) (b.position.value.x / 1.5f), (int) (b.position.value.y / 1.5f)] = 1;
                        queue.Enqueue(b);
                        // Check trong queue co 2 block cung mau canh nhau hay ko
                        foreach (var i in queue.ToArray())
                        {
                            if (!b.asset.name.Equals(ASSET_NAME_BRICK) && i.asset.name.Equals(b.asset.name) &&
                                ((b.position.value.x == i.position.value.x && b.position.value.y
                                  == i.position.value.y + 1.5) ||
                                 (b.position.value.x == i.position.value.x && b.position.value.y
                                  == i.position.value.y - 1.5) ||
                                 (b.position.value.x == i.position.value.x + 1.5 && b.position.value.y
                                  == i.position.value.y) ||
                                 (b.position.value.x == i.position.value.x - 1.5 && b.position.value.y
                                  == i.position.value.y)))
                            {
                                //Debug.Log("i22" + i.position.value.x + "  " + i.position.value.y);
                                //Debug.Log("ChuaDie22");
                                //Debug.Log("b22" + b.position.value.x + "  " + b.position.value.y);
                                return true;
                            }
                        }

                        //Debug.Log("b2" + b.position.value.x + "  " + b.position.value.y);
                        flag = 0;
                    }
                    else if (b.asset.name == blockPeek.asset.name)
                    {
                        //Debug.Log("ChuaDie2");
                        flag = 1;
                        return true;
                    }
                }

                if (b.position.value.x == blockPeek.position.value.x &&
                    b.position.value.y == blockPeek.position.value.y + 1.5
                    && Free[(int) (b.position.value.x / 1.5f), (int) (b.position.value.y / 1.5f)] == 0
                )
                {
                    if (b.asset.name != blockPeek.asset.name ||
                        (b.asset.name == blockPeek.asset.name && b.asset.name.Equals(ASSET_NAME_BRICK)))
                    {
                        Free[(int) (b.position.value.x / 1.5f), (int) (b.position.value.y / 1.5f)] = 1;
                        queue.Enqueue(b);
                        // Check trong queue co 2 block cung mau canh nhau hay ko
                        foreach (var i in queue.ToArray())
                        {
                            if (!b.asset.name.Equals(ASSET_NAME_BRICK) && i.asset.name.Equals(b.asset.name) &&
                                ((b.position.value.x == i.position.value.x && b.position.value.y
                                  == i.position.value.y + 1.5) ||
                                 (b.position.value.x == i.position.value.x && b.position.value.y
                                  == i.position.value.y - 1.5) ||
                                 (b.position.value.x == i.position.value.x + 1.5 && b.position.value.y
                                  == i.position.value.y) ||
                                 (b.position.value.x == i.position.value.x - 1.5 && b.position.value.y
                                  == i.position.value.y)))
                            {
                                //Debug.Log("i33" + i.position.value.x + "  " + i.position.value.y);
                                //Debug.Log("ChuaDie33");
                                //Debug.Log("b33" + b.position.value.x + "  " + b.position.value.y);
                                return true;
                            }
                        }
                        //Debug.Log("b3" + b.position.value.x + "  " + b.position.value.y);

                        flag = 0;
                    }
                    else if (b.asset.name == blockPeek.asset.name)
                    {
                        //Debug.Log("ChuaDie3");
                        flag = 1;
                        return true;
                    }
                }

                if (b.position.value.x == blockPeek.position.value.x &&
                    b.position.value.y == blockPeek.position.value.y - 1.5
                    && Free[(int) (b.position.value.x / 1.5f), (int) (b.position.value.y / 1.5f)] == 0
                )
                {
                    if (b.asset.name != blockPeek.asset.name ||
                        (b.asset.name == blockPeek.asset.name && b.asset.name.Equals(ASSET_NAME_BRICK)))
                    {
                        Free[(int) (b.position.value.x / 1.5f), (int) (b.position.value.y / 1.5f)] = 1;
                        queue.Enqueue(b);
                        // Check trong queue co 2 block cung mau canh nhau hay ko
                        foreach (var i in queue.ToArray())
                        {
                            if (!b.asset.name.Equals(ASSET_NAME_BRICK) && i.asset.name.Equals(b.asset.name) &&
                                ((b.position.value.x == i.position.value.x && b.position.value.y
                                  == i.position.value.y + 1.5) ||
                                 (b.position.value.x == i.position.value.x && b.position.value.y
                                  == i.position.value.y - 1.5) ||
                                 (b.position.value.x == i.position.value.x + 1.5 && b.position.value.y
                                  == i.position.value.y) ||
                                 (b.position.value.x == i.position.value.x - 1.5 && b.position.value.y
                                  == i.position.value.y)))
                            {
                                //Debug.Log("i44" + i.position.value.x + "  " + i.position.value.y);
                                //Debug.Log("ChuaDie44");
                                //Debug.Log("b44" + b.position.value.x + "  " + b.position.value.y);
                                return true;
                            }
                        }

                        //Debug.Log("b4" + b.position.value.x + "  " + b.position.value.y);
                        flag = 0;
                    }
                    else if (b.asset.name == blockPeek.asset.name)
                    {
                        //Debug.Log("ChuaDie4");
                        flag = 1;
                        return true;
                    }
                }
            }
        } //while()


        return false;
    }
    // Check condition can be suffle

    bool CheckColorBlock(GameEntity[] blockSuffle)
    {
        List<GameEntity> s1 = new List<GameEntity>();
        List<GameEntity> s2 = new List<GameEntity>();
        List<GameEntity> s3 = new List<GameEntity>();
        List<GameEntity> s4 = new List<GameEntity>();
        List<GameEntity> s5 = new List<GameEntity>();
        foreach (var block in blockSuffle)
        {
            if (block.asset.name.Equals("Prefabs/Block_1")) s1.Add(block);
            if (block.asset.name.Equals("Prefabs/Block_2")) s2.Add(block);
            if (block.asset.name.Equals("Prefabs/Piece0")) s3.Add(block);
            if (block.asset.name.Equals("Prefabs/Piece3")) s4.Add(block);
            if (block.asset.name.Equals("Prefabs/Piece4")) s5.Add(block);
        }

        if (s1.Count == 0 && s2.Count == 0 && s3.Count == 0 && s4.Count == 0 && s5.Count == 0)
            return false;
        else if ((s1.Count == 1 || s1.Count == 0) && (s2.Count == 1 || s2.Count == 0) &&
                 (s3.Count == 1 || s3.Count == 0) &&
                 (s4.Count == 1 || s4.Count == 0) && (s5.Count == 1 || s5.Count == 0))
            return true;

        return false;
    }
}