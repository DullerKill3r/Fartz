using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
    
{
    public float score;
    public float health;
    [SerializeField] AudioClip JumpSound;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float jumpPower = 200;
    [SerializeField] float speed = 10;
    [SerializeField] Text scoreText;
    [SerializeField] AudioClip ItemSound;
    [SerializeField] AudioClip DeathSound;
    

    SpriteRenderer sr;
    Rigidbody2D rb;
    BoxCollider2D bc;
    AudioSource audioPlayer;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        audioPlayer = GetComponent<AudioSource>();
        
    }
    bool GroundCheck()
    {
        return Physics2D.CapsuleCast(bc.bounds.center, bc.bounds.size,
        0f, 0f, Vector2.down, 0.5f, groundLayer);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene() .name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fruit"))
        { 
            score += 20; Destroy(collision.gameObject) ;
            audioPlayer.PlayOneShot(ItemSound, 1);
        }

        if (collision.gameObject.CompareTag("Trap"))
        { 

            

            audioPlayer.PlayOneShot(DeathSound, 1);


            Invoke("RestartLevel",1);
            
        }
    }
   
    // Update is called once per frame
    void Update()
    {


        scoreText.text = "Score: " + score;
        float dir = Input.GetAxisRaw("Horizontal");
        bool jump = Input.GetButtonDown("Jump");
        transform.Translate(dir * speed * Time.deltaTime, 0, 0);

        if (dir < 0)
            sr.flipX = true;
        if (dir > 0)
            sr.flipX = false;
        if (jump == true && GroundCheck())
        {
            rb.AddForce(Vector3.up * jumpPower);
            audioPlayer.PlayOneShot(JumpSound, 1);
        }

    }

}
