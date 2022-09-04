using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class anykey : MonoBehaviour
{
    private float Time = 3f; 
    public GameObject Text; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey)
        {
            DontDestroyOnLoad(GameObject.Find("NextSceneInfo"));
            SceneManager.LoadScene("LoadingScence");
        }
        ShowText();
    }

    void ShowText()
    {
        Time--; 
        if(Time<=0)
        {
            Text.SetActive(true);
        }
    }
}
