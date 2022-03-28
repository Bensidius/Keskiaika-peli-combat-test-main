using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour {

    
    public Animator animator;

    // public components
    public int lives = 3;
    public float gravity = 9.81f;
    public float speed = 3f;
    public float jumpForce = 10f;
    public bool isDead;

   public bool attack = false;

     

     // boolean components
    public bool levelFinished;
    public bool canControl;
    public bool hitGround;
    public bool onGround;
    public bool onSlope;

    // PhysicsMaterials
    public PhysicsMaterial2D slippery;
    public PhysicsMaterial2D sticky;



    // Private components
    Rigidbody2D rigidbody2d;
    private SpriteRenderer sRend;
    private Animator anim;

        // Other components
        public Transform groundCheck;
        public LayerMask groundMask;

        Vector2 lookDirection = new Vector2(1,0);



    // Start is called before the first frame update
    void Start()
    {

        





        rigidbody2d = GetComponent<Rigidbody2D>(); // Search for the rigidbody component
        sRend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        groundMask = LayerMask.GetMask("Ground");

        canControl = true;
        isDead = false;
        levelFinished = false;
        onSlope = false;
        
        rigidbody2d.sharedMaterial = sticky;

    }
    void Update()
    {

      


// Jump
        if (Input.GetKeyDown(KeyCode.Space) && hitGround)
         {
                rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpForce);
                anim.SetBool("Jumping", true);
                onGround = false;
                onSlope = false;
         }
        // Checking if falling
        if(rigidbody2d.velocity.y < 0f && !hitGround)
            {
                anim.SetBool("Jumping", false);
                anim.SetBool("Falling", true);
            }
            else
            {
                anim.SetBool("Falling", false);
            }

        
        
        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                Princess character = hit.collider.GetComponent<Princess>();
                if (character != null)
                {
                    character.DisplayDialog();
                }
            }
        }





        // cast a line to check if player is on the ground 
        hitGround = Physics2D.Linecast(transform.position, groundCheck.position, groundMask); // Check for all hits on ground collider
        Debug.DrawLine(transform.position, groundCheck.position, Color.red); // Visualize the line created above in the Scene view

        // cast a ray to check angle between the player and the ground
        RaycastHit2D slopeHit = Physics2D.Raycast(transform.position, Vector2.down, 1.4f, groundMask);


        // If we have a hit the ground 
        if (slopeHit)
    {
        float slopeAngle = Vector2.Angle(slopeHit.normal, Vector2.right);
        //print(slopeAngle);

        // if angle is smaller then 75 deg or greater then 115 deg, then... 
        if(slopeAngle < 75f || slopeAngle > 115f)
        {
            onSlope = true;
        }

        else if (slopeAngle == 0f || (slopeAngle > 75f && slopeAngle < 115f))
        {
            onSlope = false;
        }

    }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float ySpeed = rigidbody2d.velocity.y; 

        // If player is on the ground, change the physics to material to high friction 
        if(hitGround)
        {
            rigidbody2d.sharedMaterial = sticky;
        }

        // if not, change to no friction
        else
        {
            rigidbody2d.sharedMaterial = slippery;
            onGround = false;
        }

        // eliminate slow fall after a slope
        if (onGround && !onSlope && rigidbody2d.velocity.y <0f)
        {
            onGround = false;
        }



        if (canControl)
        { 

        // movement
        if(Input.GetKey(KeyCode.A))
        {
            rigidbody2d.velocity = new Vector2(-speed, ySpeed);
            sRend.flipX = true;
            anim.SetBool("Moving", true); 
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rigidbody2d.velocity = new Vector2(speed, ySpeed);
            sRend.flipX = false;
            anim.SetBool("Moving", true);
        }


       


        // If landed & not moving
        else if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && onGround)
        {
            rigidbody2d.velocity = Vector2.zero;
            anim.SetBool("Moving", false);
        }
        
      }

        if(onGround && levelFinished)
        {
            anim.SetBool("Falling", false);
        }




    }


 
    void OnTriggerEnter2D(Collider2D other)
    {
        //switch (other.gameObject.tag)
        //{
            // If player hits the enemy
            
                //if (!isDead //&& !powerUpActivated)
                //{
                    //canControl = false;
                    //anim.SetBool("Death", true);
                    //isDead = true;
                    //AddBonusScore();
                    //playerAudio.PlayOneShot(gameOversound, 0.7f);
                    //Destroy(other.gameObject);
                //}
               
       

    //}
        
        
        
        if (other.gameObject.tag == "Enemy")  // If player hits the spikes
        {
            canControl = false;
            anim.SetBool("Death", true);
            isDead = true;

           
            
        }
        if (other.gameObject.tag == "Finish") // If player hits the goal
        { 
            canControl = false;
            anim.SetBool("Moving", false);
            levelFinished = true;
            
        }
        if (other.gameObject.tag == "Princess") // If player hits the goal
        { 
            canControl = false;
            anim.SetBool("Moving", false);
            levelFinished = true;
            
        }
    }

private void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.tag == "Enemy")
    {
        Destroy(gameObject);

        // Let's load GameOver -Scene

        SceneManager.LoadScene("GameOver");
    }
}





    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground") && hitGround)
        {
            onGround = true;
        }
    }




}
