using UnityEngine;
using System.Collections;

public class LookAtMouse : MonoBehaviour {
	public float sensitivityY = 15f;
	public float minimumY = -60f;
	public float maximumY = 60f;
	
	float rotationY = 0f;
	
	Quaternion originalRotation;
	
	void Start ()
	{
		if(rigidbody)
			rigidbody.freezeRotation = true;
		originalRotation = transform.localRotation;
	}
	
	void Update ()
	{
		rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
		rotationY = ClampAngle(rotationY, minimumY, maximumY);
		Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, Vector3.left);
		transform.localRotation = originalRotation * yQuaternion;
	}
	
	public static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360f)
			angle += 360f;
		if (angle > 360f)
			angle -= 360f;
		return Mathf.Clamp(angle,min,max);
	}
}
