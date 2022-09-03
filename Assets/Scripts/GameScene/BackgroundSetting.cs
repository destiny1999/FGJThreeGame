using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSetting : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]public int backgroundCode;
    [SerializeField] float moveSpeed;
    bool canMove = true;
    private void Update()
    {
        if (canMove)
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
        
    }
    public void SetMoveStatus(bool status)
    {
        canMove = status;
    }
}
