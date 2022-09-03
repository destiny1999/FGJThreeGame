using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float orignalCameraZ;
    [SerializeField] float exerciseCameraZ;
    [SerializeField] float moveSpeed;
    [SerializeField] float rushSpeed;
    [SerializeField] float rushTime;
    [SerializeField] float rushSanityValue;
    [SerializeField] float autoSubSanityValue;
    [SerializeField] GameObject progress;
    float sanityValue;
    float speed;
    [SerializeField] float jumpForce;
    bool jumpping = false;
    bool jump = false;
    bool moving = true;
    bool rushing = false;
    bool exercise = false;
    bool cameraMoveOK = false;
    bool stopMove = false;
    // Start is called before the first frame update
    private void FixedUpdate()
    {
        if (moving)
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        }
        
        if (jumpping)
        {
            transform.Translate(Vector2.up * Time.deltaTime * jumpForce);
            jump = false;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if (rushing)
        {
            speed = rushSpeed;
        }
        else if (stopMove)
        {
            speed = 0;
        }
        else
        {
            speed = moveSpeed;
        }
        if (Input.GetKeyDown(KeyCode.Space) && !stopMove)
        {
            jump = true;
            jumpping = true;
        }
        if (Input.GetKeyDown(KeyCode.Z) && !rushing && !stopMove)
        {
            rushing = true;
            AddSanityValue(rushSanityValue);
            StartCoroutine(ReduceRushingTime());
        }
        if(sanityValue > 0f && !exercise)
        {
            SubSanityValue(autoSubSanityValue * Time.deltaTime);
        }
        if (exercise)
        {
            AddSanityValue(autoSubSanityValue * Time.deltaTime);
        }
    }
    void SubSanityValue(float value)
    {
        print(value);
        print(sanityValue - value);
        sanityValue = Mathf.Clamp(sanityValue - value, 0f, 100f);
        progress.GetComponent<ProgessController>().ChangeValue(sanityValue);
    }
    IEnumerator ReduceRushingTime()
    {
        transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        float time = rushTime;
        while(time > 0)
        {
            time -= Time.deltaTime * 1;
            yield return null;
        }
        rushing = false;
        GetComponent<Rigidbody2D>().gravityScale = 1;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Plane"))
        {
            jumpping = false;
        }
        
    }
    public void AddSanityValue(float value)
    {
        sanityValue = Mathf.Clamp(sanityValue + value, 0f, 100f);
        progress.GetComponent<ProgessController>().ChangeValue(sanityValue);
        if(sanityValue == 100)
        {
            Debug.Log("Game finished");
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Daughter"))
        {
            Debug.Log("finished");
            moving = false;
        }
        else if (collision.transform.CompareTag("obstacle"))
        {
            bool sliding = false;
            if (!jumpping && rushing) sliding = true;
            if (!exercise && !sliding)
            {
                Vector3 targetCameraPosition = 
                    new Vector3(Camera.main.transform.localPosition.x,
                                Camera.main.transform.localPosition.y,
                                exerciseCameraZ);
                StartCoroutine(MoveCamera(targetCameraPosition));

                stopMove = true;

                GameManager.Instance.CreateExerciseObject(collision.GetComponent<ExerciseSetting>().
                    GetExerciseCode());
                Destroy(collision.transform.gameObject);
            }
        }
    }
    IEnumerator MoveCamera(Vector3 targetCameraPosition)
    {
        while(Vector3.Distance(targetCameraPosition, Camera.main.transform.localPosition) > 0.1f)
        {
            Camera.main.transform.localPosition = Vector3.MoveTowards(
                    Camera.main.transform.localPosition, targetCameraPosition, 0.0002f);
            yield return null;
        }
        cameraMoveOK = true;
    }
    public bool GetCameraMoveStatus()
    {
        return cameraMoveOK;
    }
    public void SetExerciseStatus(bool status)
    {
        exercise = status;
        if (!exercise) stopMove = false;
    }
}
