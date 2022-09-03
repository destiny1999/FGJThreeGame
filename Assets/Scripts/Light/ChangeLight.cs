using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;

public class ChangeLight : MonoBehaviour
{
    [SerializeField] float duration;

    Color  newColor;
    Light2D It; 
    // Start is called before the first frame update
    void Start()
    {
        It = GetComponent<Light2D>();
        newColor = Color.HSVToRGB(Random.Range(0, 360), 80, 80);
    }

    // Update is called once per frame
    void Update()
    {
        
        float t = Mathf.PingPong(Time.time, duration) / duration;
        It.color = Color.Lerp(Color.black, Color.white, t);
        
    }
}
