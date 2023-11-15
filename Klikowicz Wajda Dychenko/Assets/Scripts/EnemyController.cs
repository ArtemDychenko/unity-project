using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private Animator animator;

    private Rigidbody2D rigidBody;

    private float startPositionX;

  

    [Range(0.01f, 20.0f)]
    [SerializeField]
    private float moveRange = 1.0f;

    [Range(0.01f, 20.0f)]
    [SerializeField]
    private float moveSpeed = 0.1f;

    private bool isMovingRight = false;
    private bool isFacingRight = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        startPositionX = this.transform.position.x;
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void MoveRight()
    {
        transform.Translate(moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
    }

    private void MoveLeft()
    {
        transform.Translate(-moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            if (transform.position.y < other.gameObject.transform.position.y)
            {
                
                animator.SetBool("isDead", true);
                moveSpeed = 0.0f;
                StartCoroutine(KillOnAnimationEnd());
                Debug.Log("Killed by player");
            }
           

        }

    }


    IEnumerator KillOnAnimationEnd()
    { 
       
        
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        
       
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
            } else
            {
                Flip();
                isMovingRight = false;
            }
        }else
        {
            if (posX > -moveRange + startPositionX)
            {
                MoveLeft();
            }
            else
            {
                Flip();
                isMovingRight = true;
            }
        }
        

    }
}
