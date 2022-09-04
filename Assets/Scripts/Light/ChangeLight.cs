using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;

public class ChangeLight : MonoBehaviour
{
    [SerializeField] float duration;
    [SerializeField] bool toColor2;
    [SerializeField] float changeTime;
    [SerializeField] Color color1;
    [SerializeField] Color color2;
    Color  newColor;
    Light2D light2D; 
    //float Standard =1;

    // Start is called before the first frame update
    void Start()
    {
        light2D = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (toColor2)
        {
            light2D.color = Color.Lerp(light2D.color, color2, changeTime * Time.deltaTime);
            if (light2D.color == color2) toColor2 = false;
        }
        else
        {
            light2D.color = Color.Lerp(light2D.color, color1, changeTime * Time.deltaTime);
            if (light2D.color == color1) toColor2 = true;
        }
        //float t = Mathf.PingPong(Time.time, duration) / duration;
        //light2D.color = Color.Lerp(Color.black, Getcolor(), t);

    }
    /*
    float GetStandad (float Standard)
    {
        return (Random.Range(0,Standard )); 
    }*/
    /*
    Color Getcolor()
    {
        newColor = new Vector4(GetStandad(Standard), GetStandad(Standard), GetStandad(Standard));
        return newColor; 
    }*/
}
