using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotControls : MonoBehaviour
{
    public float turnSpeed = 30f;
    public float moveSpeed = 30f;
    public float deadZone = .2f;

    private Joystick joystick;
    private Rigidbody rigidBody;
    private Animator animator;

    void Start()
    {
        // Grab all the needed components
        joystick = FindObjectOfType<Joystick>();
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        // Make sure the robot is open
        animator.SetBool("Open_Anim", true);
    }

    void Update()
    {
        // Handle rotation //

        // Get the desired look direction from the joystick
        Vector3 desiredDirection = new Vector3(joystick.Direction.x, 0, joystick.Direction.y);

        // Calculate the rotation delta towards the desired rotation based on the turning speed
        Vector3 rotationDelta = Vector3.RotateTowards(transform.forward, desiredDirection, turnSpeed * Time.deltaTime, 0f);

        // Set the robot's new rotation
        transform.rotation = Quaternion.LookRotation(rotationDelta);

        // Handle movement //

        // If the joystick is being pressed enough in any direction
        if(joystick.Direction.magnitude > deadZone)
        {
            // Add a force in the direction the robot is facing
            rigidBody.AddForce(transform.forward * moveSpeed);

            // Play the walking animation
            animator.SetBool("Walk_Anim", true);
        }
        // If the joystick isn't being pressed
        else
        {
            // Stop the walking animation
            animator.SetBool("Walk_Anim", false);
        }
    }
}
