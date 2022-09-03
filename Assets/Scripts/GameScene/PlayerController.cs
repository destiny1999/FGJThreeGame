using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    bool jumpping = false;
    bool jump = false;
    // Start is called before the first frame update
    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * Time.deltaTime * moveSpeed);
        if (jumpping)
        {
            transform.Translate(Vector2.up * Time.deltaTime * jumpForce);
            jump = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
            jumpping = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.transform.name);
        if (collision.transform.CompareTag("Plane"))
        {
            jumpping = false;
        }
    }
    
}
