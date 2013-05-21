using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour
{
    public static float rotateSpeed = 5;
	public GameObject target;
	private Vector3 offset;
	
	void Start() {
		offset = target.transform.position - transform.position;
	}

    void FixedUpdate()
    {
		float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        target.transform.Rotate(0, horizontal, 0);
        /*target.transform.Rotate(horizontal, 0, 0); => KICK!*/

        float desiredAngle = target.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
		transform.position = target.transform.position - (rotation * offset);
		
		transform.LookAt(target.transform);

        if (Input.GetAxis("Mouse ScrollWheel") < 0 && this.camera.fieldOfView < 179)
            ++this.camera.fieldOfView;
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && this.camera.fieldOfView > 1)
            --this.camera.fieldOfView;
    }
}