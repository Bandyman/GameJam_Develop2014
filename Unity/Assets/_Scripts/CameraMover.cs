using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin = -5, xMax=2, zMin =2 , zMax=-12;
}

public class CameraMover : MonoBehaviour
{
	public float speed;
	public Boundary boundary;
	
	void Update ()
	{
				float moveHorizontal = Input.GetAxis ("Vertical");
				float moveVertical = -Input.GetAxis ("Horizontal");
	
		float step = speed * Time.deltaTime;

		transform.position = new Vector3 
			(
				Mathf.Clamp (transform.position.x + (step * moveHorizontal), boundary.xMin, boundary.xMax), 
				transform.position.y,
				Mathf.Clamp (transform.position.z + (step * moveVertical), boundary.zMin, boundary.zMax)
				);
	}
}
