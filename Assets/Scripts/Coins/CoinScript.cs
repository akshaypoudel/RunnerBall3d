using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private float rotateSpeed=3.5f;
    void FixedUpdate()
    {
        
        transform.Rotate(0, 90 * rotateSpeed*Time.deltaTime,0f, Space.World);
    }
    

}
