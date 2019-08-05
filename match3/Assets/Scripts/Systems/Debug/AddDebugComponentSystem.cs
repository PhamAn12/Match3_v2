using System.Collections;
using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AddDebugComponentSystem : ReactiveSystem<GameEntity>
{
    private GameContext gameContext;
    private Button btn;
    private Button btn2;
    private IGroup<GameEntity> viewGroup;
    public AddDebugComponentSystem(GameContext context) : base(context)
    {
        
        gameContext = context;
        viewGroup = gameContext.GetGroup(GameMatcher.View);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.BoadGameElement, GameMatcher.Position));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasView && entity.hasPosition;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        btn = GameObject.Find("Canvas/OKButton").GetComponent<Button>();
        btn2 = GameObject.Find("Canvas/Back").GetComponent<Button>();
        btn2.onClick.AddListener(SceneChange1);
        foreach (var e in entities)
        {
            btn.onClick.AddListener((() => AddDebugComponenent(e)));
        }
    }

    private void SceneChange1()
    {
        Debug.Log("jdjdjddj);");
        //SceneManager.LoadScene("Start");
        GameController.Instance.StartCoroutine(DelayBeforeSwitchScene(0.0f));
    }

    void AddDebugComponenent(GameEntity e)
    {
        if(e.hasView)
            e.isDebug = true;

    }
    
    private IEnumerator DelayBeforeSwitchScene(float time)
    {
        yield return  new WaitForSeconds(time);
        foreach (var viewElement in viewGroup)
        {
            if (viewElement.view.gameObject != null)
            {
                if (viewElement.view.gameObject.GetEntityLink() && viewElement.view.gameObject.GetEntityLink() == true)
                {
                    viewElement.view.gameObject.Unlink();
                }
            }
        }
        SceneManager.LoadScene("Start");

    }
}
