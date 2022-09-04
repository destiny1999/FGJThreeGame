using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoExerciseShowActionController : MonoBehaviour
{
    [SerializeField] Sprite pressDownSprite;
    [SerializeField] Sprite pressUpSprite;
    // Start is called before the first frame update
    public void ChangeSprite(bool pressDown)
    {
        if (pressDown)
        {
            this.GetComponent<SpriteRenderer>().sprite = pressDownSprite;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = pressUpSprite;
        }
    }
}
