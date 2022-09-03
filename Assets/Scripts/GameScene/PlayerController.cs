using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float rushSpeed;
    [SerializeField] float rushTime;
    float speed;
    [SerializeField] float jumpForce;
    bool jumpping = false;
    bool jump = false;
    bool moving = true;
    bool rushing = false;

    // Start is called before the first frame update
    private void FixedUpdate()
    {
        if (moving)
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        }
        
        if (jumpping)
        {
            transform.Translate(Vector2.up * Time.deltaTime * speed);
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
        else
        {
            speed = moveSpeed;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
            jumpping = true;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            rushing = true;
            StartCoroutine(ReduceRushingTime());
        }
    }
    IEnumerator ReduceRushingTime()
    {
        float time = rushTime;
        while(time > 0)
        {
            time -= Time.deltaTime * 1;
            yield return null;
        }
        rushing = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Plane"))
        {
            jumpping = false;
        }
        if (collision.transform.CompareTag("SolidObstacle"))
        {
            if (rushing)
            {
                Destroy(collision.transform.gameObject);
            }
            else
            {
                Debug.Log("solid obstacle");
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Daughter"))
        {
            Debug.Log("finished");
            moving = false;
        }
        
    }
}
