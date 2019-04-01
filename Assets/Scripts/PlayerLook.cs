using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour 
{
	public string mouseXInputName, mouseYInputName;
    public float mouseSensitivity;
    [SerializeField] private Transform playerBody;

    private float xAxisClamp;

	private void Awake()
	{
		LockCursor();
		xAxisClamp = 0.0f;
	}

	private void LockCursor() 
	{
		Cursor.lockState = CursorLockMode.Locked; 
	}
	private void Update() {
		CameraRotation();
		if (Input.GetKey("escape"))
            Application.Quit();
	}

	private void CameraRotation() 
	{
		float mouseX = Input.GetAxis(mouseXInputName) * mouseSensitivity * Time.deltaTime;
	 	float mouseY = Input.GetAxis(mouseYInputName) * mouseSensitivity * Time.deltaTime;

	 	xAxisClamp += mouseY;

	 	if (xAxisClamp > 90.0f) {
	 		xAxisClamp = 90.0f;
	 		mouseY = 0.0f;
	 		ClampX(180.0f);
	 	}
	 	else if (xAxisClamp < -90.0f) {
	 		xAxisClamp = -90.0f;
	 		mouseY = 0.0f;
	 		ClampX(90.0f);
	 	}

	 	transform.Rotate(Vector3.left * mouseY);
	 	playerBody.Rotate(Vector3.up *mouseX);
	 }

	 private void ClampX (float value)
	 {
	 	Vector3 eulerRotation = transform.eulerAngles;
	 	eulerRotation.x = value;
	 	transform.eulerAngles = eulerRotation;
	 }
}