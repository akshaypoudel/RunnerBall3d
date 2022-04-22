using UnityEngine;

public class MoveBarrel : MonoBehaviour
{
    [SerializeField]private float moveSpeed;
    [SerializeField]private float rotateSpeed;
    bool canDestroy = true;

    void FixedUpdate()
    {
        transform.Rotate(Vector3.left*rotateSpeed ,Space.World);
        transform.Translate(Vector3.back*moveSpeed *Time.deltaTime,Space.World);
        if(canDestroy)
        {
            canDestroy = false;
            Destroy(this.gameObject, 10);
        }
    }
   
}
