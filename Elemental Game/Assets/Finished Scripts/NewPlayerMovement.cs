//using Microsoft.CSharp.RuntimeBinder;
using UnityEngine;

public class NewPlayerMovement : MonoBehaviour
{

	public float speed;

	public float gravity;
	public float maxVelocityChange;

	public Rigidbody rb;

	public float jumpHeight;
	private bool grounded = false;

    public bool canJump = false;

    void Awake()
	{
		rb.freezeRotation = true;
		rb.useGravity = false;

    }

	void FixedUpdate()
	{
		// Calculate how fast we should be moving
		Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		targetVelocity = transform.TransformDirection(targetVelocity);
		targetVelocity *= speed;

		// Apply a force that attempts to reach our target velocity
		Vector3 velocity = rb.velocity;

		Vector3 velocityChange = (targetVelocity - velocity);
		velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
		velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
		velocityChange.y = 0;

		rb.AddForce(velocityChange, ForceMode.VelocityChange);

		// We apply gravity manually for more tuning control
		rb.AddForce(new Vector3(0, -gravity * rb.mass, 0));

        if (grounded && canJump)
		{   
			if (Input.GetButton("Jump"))
			{
				rb.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
			}
		}
	}
	void OnCollisionEnter(Collision collision)
	{
		grounded = true;
	}

	private void OnCollisionExit(Collision collision)
	{
		grounded = false;
	}

	float CalculateJumpVerticalSpeed()
	{
		// From the jump height and gravity we deduce the upwards speed 
		// for the character to reach at the apex.
		return Mathf.Sqrt(2 * jumpHeight * gravity);
	}

}
