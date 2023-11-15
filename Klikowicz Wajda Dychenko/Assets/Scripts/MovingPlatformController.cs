using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{

    private float startPositionX;



    [Range(0.01f, 20.0f)]
    [SerializeField]
    private float moveRange = 1.0f;

    [Range(0.01f, 20.0f)]
    [SerializeField]
    private float moveSpeed = 0.1f;

    private bool isMovingRight = false;
   





    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        startPositionX = this.transform.position.x;
       
    }

    private void MoveRight()
    {
        transform.Translate(moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
    }

    private void MoveLeft()
    {
        transform.Translate(-moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
    }


   



    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isPause())
            return;

        float posX = transform.position.x;
        if (isMovingRight)
        {
            if (posX < moveRange + startPositionX)
            {
                MoveRight();
            }
            else
            {
               
                isMovingRight = false;
            }
        }
        else
        {
            if (posX > -moveRange + startPositionX)
            {
                MoveLeft();
            }
            else
            {
              
                isMovingRight = true;
            }
        }

    }


}
