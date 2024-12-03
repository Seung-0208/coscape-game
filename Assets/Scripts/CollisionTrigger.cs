using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTrigger : MonoBehaviour
{
    public Animator animator; 

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("YourTag"))
        {
            animator.SetTrigger("YourAnimationTrigger"); 
        }
    }
}
