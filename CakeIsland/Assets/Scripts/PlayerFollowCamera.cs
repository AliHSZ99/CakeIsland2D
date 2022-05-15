using UnityEngine;

public class PlayerFollowCamera : MonoBehaviour
{   
    // To know the position of the player
    public Transform player;
    // To make sure the Z position stays. This is very important
    public Vector3 offset;

    //smoothFollow has the range of 2-10
    [Range(2, 10)]
    public float smoothFollow;     
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Use Fixed Update to avoid glitches
    private void FixedUpdate()
    {
        FollowPlayer();
    }

    // To make the follow Camera Smoother
    void FollowPlayer()
    {
        Vector3 targetPos = player.position + offset;
        // Make sure that the movement remain smooth to any PC. 
        Vector3 smoothedPos = Vector3.Lerp(transform.position, targetPos,smoothFollow *Time.fixedDeltaTime);
        transform.position = smoothedPos;
    }
}
