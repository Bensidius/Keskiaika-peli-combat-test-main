using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    // components
    public float speed = -0.5f;
    public bool isDead;
    private Rigidbody2D rb;
    private BoxCollider2D hitCollider;
    private Player player;

    
    // Start is called before the first frame update
    void Start()
    {
      rb = GetComponent<Rigidbody2D>();
      hitCollider = GetComponentInChildren<BoxCollider2D>();
      player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
// kuolon hetki
      isDead = GetComponent<Enemy>().Isdead;
        // if alive, keep moving
      if (!isDead)
      {
          rb.velocity = new Vector2(speed, 0f);
      } 
     if (isDead)
      {
          rb.velocity = new Vector2(0f, 0f);
      } 




    }



void OnTriggerEnter2D(Collider2D other)
{
    if ((other.gameObject.tag == "EnemyTrigger" || other.gameObject.tag == "Player") && !isDead) // If enemy hits trigger or player's corpse
    {
       speed = -speed; // Change the moving direction
       transform.localScale = new Vector3(transform.localScale.x * -1, 3.5f, 3.5f);  // flip the whole object

       //print("Törmäys collideriin" + "speed: " + speed);
    }
}


}
