using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class CameraMover : MonoBehaviour
{
	public float speed;
	public Boundary boundary;
	
	void Update ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
	
		float step = speed * Time.deltaTime;

		transform.position = new Vector3 
			(
				Mathf.Clamp (transform.position.x + (step * moveHorizontal), boundary.xMin, boundary.xMax), 
				transform.position.y,
				Mathf.Clamp (transform.position.z + (step * moveVertical), boundary.zMin, boundary.zMax)
				);
	}
}
