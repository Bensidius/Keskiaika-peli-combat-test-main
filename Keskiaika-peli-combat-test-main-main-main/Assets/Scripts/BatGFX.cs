using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BatGFX : MonoBehaviour
{

  public AIPath aiPath;

  [SerializeField]
    Transform player;

    [SerializeField]
    float agroRange;

  

  
public Component aiScript;







    // Update is called once per frame
    void Update()
    {

        if (GameObject.FindWithTag("Player") == null)
        {
            return;
        }
        //Disable enemy tracking if player is too far away
        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if(distToPlayer < agroRange)
        {
            aiPath.enabled = true;
        }

        else if (distToPlayer > agroRange)
        {
           aiPath.enabled = false;
        }

        //Sprite change direction
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-2.5f, 2.5f, 2.5f);
        }
        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
        }
    }

       
   
}


