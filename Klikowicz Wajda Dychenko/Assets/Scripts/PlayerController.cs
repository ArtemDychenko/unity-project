using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement parameters")]
    [Range(0.01f, 20.0f)]
    [SerializeField]
    private float moveSpeed = 0.1f;

    [Range(0.01f, 20.0f)]
    [SerializeField]
    private float jumpForce = 6.0f;

    [SerializeField]
    private AudioClip coinSound;

    [SerializeField]
    private AudioClip itemSound;

    [SerializeField]
    private AudioClip damageSound;

    [SerializeField]
    private AudioClip killSound;

    [SerializeField]
    private AudioClip healSound;

    private AudioSource audioSource;

    private Rigidbody2D rigidBody;

    private Animator animator;

    private bool isFacingRight = true;


    private Vector2 startPosition;

    private bool isWalking = false;


    [SerializeField]
    private LayerMask groundLayer;

    [Range(0.01f, 20.0f)]
    [SerializeField]
    private float rayLength = 1.4f;

    // [Space( 10 )]

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();   
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        startPosition = transform.position;
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


    bool isGrounded()
    {
        return Physics2D.Raycast(this.transform.position, Vector2.down, rayLength, groundLayer.value);
    }

    void jump()
    {
        if (isGrounded())
        {
            rigidBody.AddForce(new Vector2(0.0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void Death()
    {
        GameManager.instance.RemoveLive();
        if (GameManager.instance.lives == 0)
        {
            Debug.Log("Koniec gry");
            transform.Translate(0.0f, -1232132131.0f, 0.0f, Space.World);
        }
        else
        {
            transform.position = startPosition;

        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("MovingPlatform"))
        {
            transform.SetParent(null);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bonus"))
        {
            audioSource.PlayOneShot(coinSound, AudioListener.volume);
            GameManager.instance.AddPoints(1);
            other.gameObject.SetActive(false);
        }

        if (other.CompareTag("Enemy"))
        {
            if (transform.position.y > other.gameObject.transform.position.y)
            {
                audioSource.PlayOneShot(killSound, AudioListener.volume);
                GameManager.instance.preyCounter++;
                Debug.Log("Killed an enemy");
            }else
            {
                audioSource.PlayOneShot(damageSound, AudioListener.volume);
                Death();
                
            }
        }


        if (other.CompareTag("MovingPlatform"))
        {
            transform.SetParent(other.transform);

        }


        if (other.CompareTag("Key"))
        {
            audioSource.PlayOneShot(itemSound, AudioListener.volume);
            GameManager.instance.AddKeys();
            other.gameObject.SetActive(false);
        }

        if (other.CompareTag("Heart"))
        {
            audioSource.PlayOneShot(healSound, AudioListener.volume);
            GameManager.instance.AddLive();
            Debug.Log("Acquired live");
            other.gameObject.SetActive(false);
        }

        if (other.CompareTag("Exit") && GameManager.instance.CollectedAllKeys())
        {
            GameManager.instance.AddPoints(100 * GameManager.instance.getLives());
            GameManager.instance.LevelCompleted();
            Debug.Log("You WIn");
        }

        if (other.CompareTag("FallLevel"))
        {
            Death();
        }

    }

    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isPause())
        {
            isWalking = false;


            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
                jump();


            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                transform.Translate(moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
                isWalking = true;
                if (isFacingRight == false)
                {
                    Flip();
                }
            }


            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                transform.Translate(-moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
                isWalking = true;
                if (isFacingRight == true)
                {
                    Flip();
                }
            }

            animator.SetBool("isGrounded", isGrounded());
            animator.SetBool("isWalking", isWalking);
        }


        // Debug.DrawRay(transform.position, rayLength * Vector3.down, Color.white, 1, false);
    }

}
