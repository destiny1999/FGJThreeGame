using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject planeObject;
    [SerializeField] Transform planeManager;
    [SerializeField] float eachPlaneDistance;
    public static GameManager Instance;
    int createIndex = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
