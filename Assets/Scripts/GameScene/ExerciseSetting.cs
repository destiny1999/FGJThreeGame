using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciseSetting : MonoBehaviour
{
    [SerializeField] int exerciseCode;
    // Start is called before the first frame update
    public int GetExerciseCode()
    {
        return exerciseCode;
    }
}
