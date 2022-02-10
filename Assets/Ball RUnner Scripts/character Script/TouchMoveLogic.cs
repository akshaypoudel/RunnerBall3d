using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMoveLogic: MonoBehaviour
{
    private Touch touch;
    public float speedModifier;
    public GameObject pauseMenu;
    public GameObject GameOverUI;
    public float jump=5f;

    void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                transform.position = new Vector3(
                    transform.position.x + touch.deltaPosition.x * speedModifier/2,
                    transform.position.y,
                    transform.position.z //+ touch.deltaPosition.y * speedModifier
                    );
            }
        }
    }
}
