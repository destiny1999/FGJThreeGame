using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] List<GameObject> backgrounds;
    [SerializeField] List<GameObject> backgroundParents;
    [SerializeField] List<float> moveSpeed;
    [SerializeField] float nextPosition;
    public static BackgroundController Instance;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        for(int i = 0; i < backgrounds.Count; i++)
        {
            backgrounds[i].transform.Translate(Vector3.left * moveSpeed[i] * Time.deltaTime);
        }*/
    }
    public void CreateNewBackground(int index, Vector3 selfPosition)
    {
        print("create new");
        GameObject newBackground = Instantiate(backgrounds[index], backgroundParents[index].transform);
        newBackground.transform.position = new Vector3(selfPosition.x + nextPosition,
                                                       selfPosition.y, selfPosition.z);
        if (backgroundParents[index].transform.childCount > 2)
        {
            Destroy(backgroundParents[index].transform.GetChild(0).gameObject);
        }
    }
    public void ChangeAllBackgroundsStatu(bool status)
    {
        for(int i = 0; i<backgroundParents.Count; i++)
        {
            foreach(Transform child in backgroundParents[i].transform)
            {
                print(child.name);
                child.GetComponent<BackgroundSetting>().SetMoveStatus(status);
            }
        }
    }
}
