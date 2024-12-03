using UnityEngine;

public class animationSound : MonoBehaviour
{
    public Animator animator; 
    public AudioSource audioSource; 

    // Name of the animation state you are checking
    private string animationStateName = "startPage_anim";
    
    void Update()
    {
        // Check if the specific animation is being played
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(animationStateName))
        {
            // If the animation is playing and the audio is not, start the audio
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
                audioSource.Stop();
        }
    }
}
