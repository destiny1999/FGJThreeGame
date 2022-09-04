using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;

public class ChangeLight : MonoBehaviour
{
    [SerializeField] float duration;

    Color  newColor;
    Light2D It; 
    float Standard =1;

    // Start is called before the first frame update
    void Start()
    {
        It = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        float t = Mathf.PingPong(Time.time, duration) / duration;
        It.color = Color.Lerp(Color.black, Getcolor(), t);
        
    }

    float GetStandad (float Standard)
    {
        return (Random.Range(0,Standard )); 
    }

    Color Getcolor()
    {
        newColor = new Vector4(GetStandad(Standard), GetStandad(Standard), GetStandad(Standard));
        return newColor; 
    }
}
