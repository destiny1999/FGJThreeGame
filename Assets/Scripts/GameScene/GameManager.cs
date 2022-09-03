using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject planeObject;
    [SerializeField] Transform planeManager;
    [SerializeField] float eachPlaneDistance;
    [SerializeField][Tooltip("Show object after collision")] List<GameObject> exerciseObject;
    [SerializeField] GameObject player;
    [SerializeField][Tooltip("The exercise condition info")] GameObject exerciseInfo;
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
    public void CreateExerciseObject(int code)
    {
        Transform playerTransform= player.transform;
        GameObject newExerciseObject = Instantiate(exerciseObject[code],playerTransform);
        newExerciseObject.name = "newExerciseObject";
        StartCoroutine(ExecuteExercise(code));
    }
    IEnumerator ExecuteExercise(int code)
    {
        // wait for camera move ok and start to execrise
        while (!player.GetComponent<PlayerController>().GetCameraMoveStatus())
        {
            yield return null;
        }
        player.GetComponent<PlayerController>().SetCameraMoveStatus(false);
        player.GetComponent<PlayerController>().SetExerciseStatus(true);
        exerciseInfo.GetComponent<ExerciseInfoSetting>().SetInfo(code);
    }
    public IEnumerator ExecriseOK(GameObject exercisingInfoObject)
    {
        player.GetComponent<PlayerController>().SetExerciseStatus(false);
        Destroy(GameObject.Find("newExerciseObject"));
        player.GetComponent<PlayerController>().MoveCamera(true);
        while (!player.GetComponent<PlayerController>().GetCameraMoveStatus())
        {
            yield return null;
        }
        player.GetComponent<PlayerController>().SetCameraMoveStatus(false);
        player.GetComponent<PlayerController>().SetStopMoveStatus(false);
        exercisingInfoObject.SetActive(false);
    }
}
