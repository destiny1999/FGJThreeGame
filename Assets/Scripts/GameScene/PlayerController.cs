using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject dialogSystem;
    [SerializeField] Sprite talkSprite;
    [SerializeField] float orignalCameraZ;
    [SerializeField] float exerciseCameraZ;
    [SerializeField] float moveSpeed;
    [SerializeField] float rushSpeed;
    [SerializeField] float rushTime;
    [SerializeField] float rushSanityValue;
    [SerializeField] float autoSubSanityValue;
    [SerializeField] float exerciseAddSanityValue;
    [SerializeField] float cameraMoveSpeed;
    [SerializeField] float originalY = -2.23f;
    [SerializeField] GameObject progress;
    float sanityValue;
    float speed;
    [SerializeField] float jumpForce;
    bool jumpping = false;
    bool jump = false;
    bool moving = true;
    [SerializeField]bool rushing = false;
    bool exercise = false;
    [SerializeField]bool cameraMoveOK = false;
    [SerializeField] bool stopMove = false;
    Animator ani;
    private void Start()
    {
        ani = this.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    private void FixedUpdate()
    {
        if (moving)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        
        if (jumpping)
        {
            transform.Translate(Vector3.up * Time.deltaTime * jumpForce);
            jump = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Camera.main.transform.position = new Vector3(transform.position.x,
                                                     Camera.main.transform.position.y,
                                                     Camera.main.transform.position.z);
        if (rushing)
        {
            speed = rushSpeed;
        }
        else if (stopMove)
        {
            BackgroundController.Instance.ChangeAllBackgroundsStatu(false);
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
            ani.SetBool("rusing", rushing);
            AddSanityValue(rushSanityValue);
            StartCoroutine(ReduceRushingTime());
        }
        if(sanityValue > 0f && !stopMove)
        {
            SubSanityValue(autoSubSanityValue * Time.deltaTime);
        }
        if (exercise)
        {
            AddSanityValue(exerciseAddSanityValue * Time.deltaTime);
        }
    }
    void SubSanityValue(float value)
    {
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
        ani.SetBool("rusing", rushing);
        GetComponent<Rigidbody2D>().gravityScale = 2f;
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
            /*
            bool Invincible = false;
            if (!jumpping && rushing) sliding = true;*/
            if (!exercise && !rushing)
            {
                jumpping = false;
                //transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                transform.localPosition = new Vector3(transform.localPosition.x, originalY,
                                                      transform.localPosition.z);
                Vector3 targetCameraPosition = 
                    new Vector3(Camera.main.transform.localPosition.x,
                                Camera.main.transform.localPosition.y,
                                exerciseCameraZ);
                StartCoroutine(MoveCamera(targetCameraPosition));

                stopMove = true;

                GameManager.Instance.CreateExerciseObject(collision.GetComponent<ExerciseSetting>().
                    GetExerciseCode(), collision.transform.position);
                this.GetComponent<SpriteRenderer>().enabled = false;
                Destroy(collision.transform.gameObject);
            }
        }
        if (collision.transform.CompareTag("newBackgroundTrigger"))
        {
            BackgroundController.Instance.CreateNewBackground(collision.transform.parent.GetComponent<BackgroundSetting>().backgroundCode,
                                                              collision.transform.parent.position);
        }
        if (collision.transform.CompareTag("endLevelCharacter"))
        {
            stopMove = true;
            ani.enabled = false;
            GetComponent<SpriteRenderer>().sprite = talkSprite;
            Debug.Log("end level");
            dialogSystem.SetActive(true);
            progress.SetActive(false);
        }
    }
    public void MoveCamera(bool orignal)
    {
        cameraMoveOK = false;
        Vector3 targetCameraPosition;
        if (orignal)
        {
            targetCameraPosition =
                    new Vector3(Camera.main.transform.localPosition.x,
                                Camera.main.transform.localPosition.y,
                                orignalCameraZ);
        }
        else
        {
            targetCameraPosition =
                    new Vector3(Camera.main.transform.localPosition.x,
                                Camera.main.transform.localPosition.y,
                                exerciseCameraZ);
        }
        StartCoroutine(MoveCamera(targetCameraPosition));
    }
    IEnumerator MoveCamera(Vector3 targetCameraPosition)
    {
        
        while(Mathf.Abs(targetCameraPosition.z - Camera.main.transform.localPosition.z) > 0.11f)
        {
            int weight = targetCameraPosition.z == orignalCameraZ ? -1 : 1;
            Camera.main.transform.Translate(Vector3.forward * weight * cameraMoveSpeed);
            yield return null;
        }
        cameraMoveOK = true;
        print("move camera ok");
    }
    public void SetCameraMoveStatus(bool status)
    {
        cameraMoveOK = status;
    }
    public bool GetCameraMoveStatus()
    {
        return cameraMoveOK;
    }
    public void SetExerciseStatus(bool status)
    {
        exercise = status;
    }
    public void SetStopMoveStatus(bool status)
    {
        stopMove = status;
        if (!stopMove)
        {
            BackgroundController.Instance.ChangeAllBackgroundsStatu(true);
        }
    }
}
