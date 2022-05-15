using UnityEngine;

public class RotateGameObject : MonoBehaviour
{
    public float rotationSpeed;
    public int whichDirection = 0;
    public bool canRotate = true;
    // // Update is called once per frame
    void FixedUpdate()
    {
        if (canRotate)
        {
            if (whichDirection == 0)
                transform.Rotate(90 * rotationSpeed * Time.deltaTime, 0, 0);//x
            else if (whichDirection == 1)
                transform.Rotate(0, 90 * rotationSpeed * Time.deltaTime, 0);//y
            else if (whichDirection == 2)
                transform.Rotate(0, 0, 90 * rotationSpeed * Time.deltaTime);//z

        }

    }
}
