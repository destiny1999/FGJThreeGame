using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolce : MonoBehaviour
{
    // Start is called before the first frame update
    Material material; 

    bool isDissoliving = false; 
    float fade = 1f; 
    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        fade = Mathf.PingPong(Time.time, 1f);
       
            // fade-=Time.deltaTime;
        
        material.SetFloat("_Fade", fade);
       
    }

    
}
