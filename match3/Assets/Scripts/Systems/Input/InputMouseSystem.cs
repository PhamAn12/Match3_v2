using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Entitas;

public class InputMouseSystem : IExecuteSystem, ICleanupSystem
{
    private readonly InputContext inputContext;
    private readonly IGroup<InputEntity> inputs;
    public InputMouseSystem(InputContext Input)
    {
        inputContext = Input;
        inputs = inputContext.GetGroup(InputMatcher.Input);
    }


    public void Execute()
    {
        var elements = inputs.GetEntities();
        var mousePosition = Input.mousePosition;
        if (Input.GetMouseButtonDown(0))
        {
            var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector2.zero,100);
            //Debug.Log("HIT Collider" + hit.collider);
            if (hit.collider != null)
            {
                var pos = hit.collider.transform.position;
            
                inputContext.CreateEntity().AddInput( pos.x,  pos.y);
//                Debug.Log(elements);
//                Debug.Log("POS   " + pos);
            }
        }
        
    }
    
    public void Cleanup()
    {
        foreach (var e in inputs.GetEntities())
        {
            //Debug.Log("On cleanup system");
            e.Destroy();
        }
    }
}