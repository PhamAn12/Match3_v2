using System.Collections;
using System.Linq.Expressions;
using DG.Tweening;
using Entitas;
using Entitas.Unity;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    Systems systems;
    Contexts contexts = Contexts.sharedInstance;
    private static GameController instance = null;
    IGroup<GameEntity> viewGroup;
    
    public static GameController Instance
    {
        get {
            if (instance == null)
                instance = FindObjectOfType<GameController>();
            return instance;
        }
    }
    private void Start()
    {
        var contexts = Contexts.sharedInstance;

        
        systems = new Feature("Systems")
            .Add(new InputSystem(contexts.input, contexts.game)
            .Add(new BoardSystems(contexts.game)
            ));
        systems.Initialize();
        
        
        

    }

    private void Update()
    {   
        systems.Execute(); 
        systems.Cleanup();
    }

    private void OnDestroy()
    {
        systems.ClearReactiveSystems();
        //systems.TearDown();
        systems.DeactivateReactiveSystems();
        contexts.Reset();

    }
    


}