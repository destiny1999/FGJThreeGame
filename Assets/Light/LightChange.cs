using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightChange : MonoBehaviour
{
    [SerializeField] float duration;

    Light It; 
    // Start is called before the first frame update
    void Start()
    {
        It = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        float t = Mathf.PingPong(Time.time, duration) / duration;
        It.color = Color.Lerp(Color.black, Color.HSVToRGB(Random.Range(0, 360), Random.Range(50, 100), Random.Range(50,100)), t);
    }
}
