using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ScenceLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void LoadScene()
    {
        // SceneManager.LoadScene("SampleScene");
        SceneManager.LoadScene("GameScene");
    }
}
