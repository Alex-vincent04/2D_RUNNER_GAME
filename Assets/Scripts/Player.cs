using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public int speed;
    public int JumpSpeed;
    public float Score;
    public Rigidbody rb;
    public bool IsOnGround = true;
    private int JumpCount = 0;
    public Animator animator;
    public bool PowerUp = false;
    public bool Point2x = false;
    public bool Coin2x = false;
    public bool Speed2x = false;

    public AudioSource Audioso;
    public AudioClip coin;

    public TextMeshProUGUI Text;

    float StartTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartTimer -= Time.deltaTime;
        if (StartTimer <= 0)
        { 
            Point2x = false;
            PowerUp = false;
            Coin2x = false;
            Speed2x = false;
        }

        if (Input.GetKey(KeyCode.A) || (Input.GetKey(KeyCode.LeftArrow)))
        {
            if (transform.position.x < -4.75f)
            {
                transform.position = new Vector3(-4.75f, transform.position.y, transform.position.z);
            }
            else
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            }
        }
        if (Input.GetKey(KeyCode.D) || (Input.GetKey(KeyCode.RightArrow)))
        {
            if (transform.position.x > 4.75f)
            {
                transform.position = new Vector3(4.75f, transform.position.y, transform.position.z);
            }
            else
            {
                transform.Translate(Vector3.right * Time.deltaTime * speed);
            }

        }

        if (Input.GetKeyDown(KeyCode.Space) && JumpCount < 2)  /*IsOnGround == true &&*/
        {
            //animator.SetBool("Jump", true);
            if (transform.position.y >= 3f)
            {
                transform.position= new Vector3(transform.position.x,3f,transform.position.z);
            }
            //GetComponent<Rigidbody>().AddForce(Vector3.up * 7f, ForceMode.Impulse);

            else
            {
                GetComponent<Rigidbody>().AddForce(Vector3.up * 5f, ForceMode.Impulse);
                JumpCount++;
                //IsOnGround = false;
                
            /*transform.Translate(Vector3.up*Time.deltaTime * JumpSpeed);*/
            }

           //animator.SetBool("Jump", false);
            /*rb.AddForce(Vector3.up * 10f,ForceMode.Impulse);*/
            /*GetComponent<Rigidbody>().AddForce(Vector3.up * 10f, ForceMode.Impulse);*/

        }
        if (Input.GetKey(KeyCode.Space))
        {
            animator.SetBool("Jump", true);
        }
        else
        {
            animator.SetBool("Jump", false);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("obstacle"))
        {
            if (PowerUp == true)
            {
                Destroy(collision.gameObject);
            }
            else
            {
                Destroy(gameObject);
                SceneManager.LoadScene(2);
            }

        }
        if (collision.gameObject.CompareTag("planeCollision"))
        {
            IsOnGround=true;
            JumpCount = 0;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("coin"))
        {
            if (Point2x == true)
            {
                Score += 2;
            }
            else
            {
                Score += 1;
            }
            Destroy(other.gameObject);
            Text.text = "Score " + Score;
            Audioso.PlayOneShot(coin);
            
        }
        else if (other.gameObject.CompareTag("Powerup"))
        {
            PowerUp = true;
            Destroy(other.gameObject);
            StartTimer = 10f;
        }

        else if (other.gameObject.CompareTag("Point2x"))
        {
            Point2x = true;
            Destroy(other.gameObject);
            StartTimer = 10f;
        }

        else if (other.gameObject.CompareTag("Coin2x"))
        {
            Debug.Log("sucess");
            Coin2x = true;
            Destroy(other.gameObject);
            StartTimer = 10f;
        }

        else if (other.gameObject.CompareTag("Speed2x"))
        {
            Debug.Log("Increased");
            Speed2x = true;
            Destroy(other.gameObject);
            StartTimer = 10f;
        }
    }
}
