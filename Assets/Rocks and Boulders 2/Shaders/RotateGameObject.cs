using UnityEngine;
using System.Collections;

public class RotateGameObject : MonoBehaviour 
{
	public float rotationSpeed;
	public int whichDirection=0;

	// // Update is called once per frame
	void FixedUpdate () {
		if (whichDirection == 0) 
			transform.Rotate(90 * rotationSpeed * Time.deltaTime, 0, 0);
		else if(whichDirection == 1)
			transform.Rotate(0,90 * rotationSpeed * Time.deltaTime, 0);
		else if(whichDirection==2)
			transform.Rotate(0,0, 90 * rotationSpeed * Time.deltaTime);

	}
}
