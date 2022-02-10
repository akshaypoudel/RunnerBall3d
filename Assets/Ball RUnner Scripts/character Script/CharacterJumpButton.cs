using UnityEngine;

public class CharacterJumpButton : MonoBehaviour
{
    public Rigidbody rigidBody;
    bool isGrounded=true;
    public float jump=5f;
    Vector3 moveDirection;
    public float gravity=-19f;

    private void Start()
    {
        rigidBody=GetComponent<Rigidbody>();
    }
    public void Jump()
    {
        if(isGrounded)
        {
            // player.transform.position=new Vector3(0f,jump,0f);
            rigidBody.AddForce(new Vector3(0, jump, 0), ForceMode.Impulse);
            isGrounded=false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}






























