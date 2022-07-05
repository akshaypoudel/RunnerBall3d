using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if ((collider.gameObject.CompareTag("Enemy") || collider.gameObject.CompareTag("Tree"))
            && this.gameObject.tag != "LogBox")
        {
            collider.gameObject.SetActive(false);
        }
        if (collider.gameObject.tag == "Ground" && this.gameObject.tag == "LogBox")
        {
            TreeBehaviourAfterHittingGround();
        }
    }

    private void TreeBehaviourAfterHittingGround()
    {
        this.gameObject.GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
        Invoke("FreezeRotationY", 0.5f);
    }

    private void FreezeRotationY()
    {
        this.gameObject.GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY;
    }
}
