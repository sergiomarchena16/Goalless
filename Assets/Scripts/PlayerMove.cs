using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
	public string horizontalInput, verticalInput;
	public float speed;

	private CharacterController charController;

	[SerializeField] private AnimationCurve jumpFallOff;
	[SerializeField] private float jumpMulti;
	[SerializeField] private KeyCode jumpKey;

	private bool isJumping;

	private void Awake()
	{
		charController = GetComponent<CharacterController>();
	}

	private void Update()
	{
		PlayerMovement();
	}

	private void PlayerMovement() 
	{
		float verInput = Input.GetAxis(verticalInput) * speed;
		float horInput = Input.GetAxis(horizontalInput) * speed;

		Vector3 forwardMovement = transform.forward * verInput;
		Vector3 rightMovement = transform.right * horInput;

		charController.SimpleMove(forwardMovement + rightMovement);

		jumpInput();
	}

	private void jumpInput()
	{
		if (Input.GetKeyDown(jumpKey) && !isJumping)
		{
			isJumping = true;
			StartCoroutine(JumpEvent());
		}

	}
    
    private IEnumerator JumpEvent()
    {
    	float timeInAir = 0.0f;
    	do 
    	{
    		float jumpForce = jumpFallOff.Evaluate(timeInAir);
    		charController.Move(Vector3.up * jumpForce * jumpMulti * Time.deltaTime);
    		timeInAir+= Time.deltaTime;
    		yield return null;
    	} while(!charController.isGrounded && charController.collisionFlags != CollisionFlags.Above);

    	isJumping = false;
    }

}
