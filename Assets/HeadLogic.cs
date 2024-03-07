using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadLogic : MonoBehaviour
{
	private Camera cam;

	void Start () 
	{
		cam = Camera.main;
	}
	

	void Update () 
	{
		Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
		Vector3 target = mousePos;
        
		
		// rotate Y (green axis) towards mouse
		// transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);

		// rotate Y (green axis) away from mouse
		// transform.rotation = Quaternion.LookRotation(Vector3.forward, transform.position-mousePos);

		// rotate X (red axis) towards mouse
		// Vector3 perpendicular = Vector3.Cross(transform.position-mousePos,Vector3.forward);
		// transform.rotation = Quaternion.LookRotation(Vector3.forward, perpendicular);

		// rotate X (red axis) away from mouse
		//Vector3 perpendicular = Vector3.Cross(mousePos-transform.position,Vector3.forward);
		//transform.rotation = Quaternion.LookRotation(Vector3.forward, perpendicular);
		
		// using lookat
    	         transform.LookAt(target, new Vector3(0, 0, -1));
		
		// can also set transform.up (green axis)
		//mousePos.z = 0;
		//transform.up = mousePos - transform.position;
	}
}
