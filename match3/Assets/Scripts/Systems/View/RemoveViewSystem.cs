using System.Collections.Generic;
using System.Threading;
using DG.Tweening;
using Entitas;
using Entitas.Unity;
using UnityEngine;

public class RemoveViewSystem : ReactiveSystem<GameEntity>
{
    public RemoveViewSystem(GameContext Game) : base(Game) {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
        return context.CreateCollector( 
            GameMatcher.Asset.Removed(),
            //GameMatcher.Destroyed.Added(),
            GameMatcher.TypeMechanicsDestroy.Added()
        );

    }

    protected override bool Filter(GameEntity entity) {
        return entity.hasView ;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            
//            Debug.Log(e);
            if (e.hasTypeMechanicsDestroy)
            {
                if (e.typeMechanicsDestroy.type.Equals("Boom"))
                {
                    destroyViewBoom(e.view);
                    e.RemoveView();
                }
                else if (e.typeMechanicsDestroy.type.Equals("Rocket"))
                {
                    destroyViewRocket(e.view);
                    e.RemoveView();
                }

                else if (e.typeMechanicsDestroy.type.Equals("Normal"))
                {
                    destroyView(e.view);
                    e.RemoveView();
                }
            }
            else if (!e.hasTypeMechanicsDestroy)
            {
                destroyView(e.view);
                e.RemoveView();
            }         
        }

    }

    void destroyView(ViewComponent viewComponent) {
        var gameObject = viewComponent.gameObject;
        var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        var color = spriteRenderer.color;
        color.a = 0f;
        spriteRenderer.material.DOColor(color, 0.3f);
        
//        gameObject.Unlink();
//        Object.Destroy(gameObject);
        gameObject.transform
            .DOScale(Vector3.one * 1f, 0.3f)
            .OnComplete(() => {
                gameObject.Unlink();
                Object.Destroy(gameObject);
            });
    }
    void destroyViewBoom(ViewComponent viewComponent) {
        var gameObject = viewComponent.gameObject;
        var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        var color = spriteRenderer.color;
        color.a = 0f;
        spriteRenderer.material.DOColor(color, 0.3f);
        
//        gameObject.Unlink();
//        Object.Destroy(gameObject);
        gameObject.transform
            .DOScale(Vector3.one * 2f, 0.5f)
            .OnComplete(() => {
                gameObject.Unlink();
                Object.Destroy(gameObject);
            });
    }
    void destroyViewRocket(ViewComponent viewComponent) {
        var gameObject = viewComponent.gameObject;
        var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        var color = spriteRenderer.color;
        color.a = 0f;
        spriteRenderer.material.DOColor(color, 0.3f);
        
//        gameObject.Unlink();
//        Object.Destroy(gameObject);
        gameObject.transform
            .DOScale(Vector3.one * 1.9f, 0.3f)
            .OnComplete(() => {
                gameObject.Unlink();
                Object.Destroy(gameObject);
            });
    }

}