using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] List<GameObject> backgrounds;
    [SerializeField] List<float> moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < backgrounds.Count; i++)
        {
            backgrounds[i].transform.Translate(Vector3.right * moveSpeed[i] * Time.deltaTime);
        }
    }
}
