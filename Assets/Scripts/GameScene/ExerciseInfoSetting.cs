using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExerciseInfoSetting : MonoBehaviour
{
    [SerializeField] Image inputButtonTip;
    [SerializeField] List<int> exerciseTimes;
    [SerializeField] List<Sprite> allTipsSprite;
    [SerializeField] Text timesText;
    [SerializeField] GameObject UIInfo;
    int times = 0;
    int type = 0;
    KeyCode thisKeyCode = KeyCode.None;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(times >= 0)
        {
            timesText.text = times+"";
        }
        if (thisKeyCode!= KeyCode.None && Input.GetKeyDown(thisKeyCode))
        {
            print("press");
            times--;
            if(times> 0)
            {
                GameObject.Find("newExerciseObject").GetComponent<DoExerciseShowActionController>().ChangeSprite(true);
            }
            
            if(times == 0)
            {
                UIInfo.SetActive(false);
                StartCoroutine(GameManager.Instance.ExecriseOK(this.gameObject));
                //gameObject.SetActive(false);
            }
        }
        else if(times>0 && thisKeyCode != KeyCode.None && Input.GetKeyUp(thisKeyCode))
        {
            GameObject.Find("newExerciseObject").GetComponent<DoExerciseShowActionController>().ChangeSprite(false);
        }
        
    }
    public void SetInfo(int type)
    {
        print(type);
        print("set exercise info");
        times = exerciseTimes[type];
        this.type = type;
        switch (type)
        {
            case 0:
                thisKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), "T");
                break;
            case 1:
                thisKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Q");
                break;
            case 2:
                thisKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), "W");
                break;
            case 3:
                thisKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), "E");
                break;
            case 4:
                thisKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), "R");
                break;
        }
        print(thisKeyCode.ToString());
        inputButtonTip.sprite = allTipsSprite[type];
        UIInfo.SetActive(true);
        this.gameObject.SetActive(true);
    }
}
