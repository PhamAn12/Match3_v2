using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBlock : MonoBehaviour
{
    
    [SerializeField] private GameObject block;
    private void Awake()
    {
        
       Debug.Log("On GameObject Awake");
    }

    private void OnDisable()
    {
        
        Debug.Log("On GameObject Disable");
            
    }

    private void OnEnable()
    {
        Debug.Log("On GameObject Enable");
            
    }

    // Start is called before the first frame update
    void Start()
    {
        for (var i = 0; i < 4; i++)
        {
            for (var j = 0; j < 4; j++)
            {
                var block_instance = Instantiate(block) as GameObject;
                block_instance.transform.position = new Vector3(i ,j,0); 
            }
        }
        
        Debug.Log("On GameObject Start");
        
    }

    void Generate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
     //   Debug.Log("On Update");
        
    }
}
