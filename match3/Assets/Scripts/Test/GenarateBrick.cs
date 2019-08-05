using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenarateBrick : MonoBehaviour
{
 //   private GameObject block;
    // Start is called before the first frame update
    void Start()
    {
        GameObject gameObject = Resources.Load("Prefabs/GenerateBrick") as GameObject;
        //gameObject.transform.position = new Vector3(1,1,0);
     //   Instantiate(gameObject);
        gameObject.transform.position = new Vector3(1,4,0);
        Instantiate(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
