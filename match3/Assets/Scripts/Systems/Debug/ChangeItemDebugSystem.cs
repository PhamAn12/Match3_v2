using System.Collections.Generic;
using Entitas;
using UnityEngine;
using UnityEngine.UI;

public class ChangeItemDebugSystem : ReactiveSystem<GameEntity>
{
    private GameContext gameContext;
    private IGroup<GameEntity> debugBlocks;
    private Dropdown dropdown;
    public ChangeItemDebugSystem(GameContext game) : base(game)
    {
        gameContext = game;
        debugBlocks = gameContext.GetGroup(GameMatcher.Debug);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.Tabbed,GameMatcher.Debug));
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        dropdown = GameObject.Find("Canvas/Dropdown").GetComponent<Dropdown>();
        foreach (var e in entities)
        {
            if (dropdown.value == 0)
            {
                e.isBoom = true;
            }
            else if (dropdown.value == 1)
            {
                e.ReplaceRocket("Vertical");
            }
            else if (dropdown.value == 2)
            {
                e.ReplaceRocket("Horizontal");
            }
            else if (dropdown.value == 3)
            {
                e.isDisco = true;
            }
        }
    }

}