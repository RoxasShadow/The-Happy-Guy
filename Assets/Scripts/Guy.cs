using UnityEngine;
using System;

public class Guy : MonoBehaviour
{
    public static float speed = 10.0F;
    public static float jumpSpeed = 10.0F;
    public static float gravity = 20.0F;
    public static float pushPower = 2.0F;

    private bool running = false;
    private Vector3 moveDirection = Vector3.zero;

    void FixedUpdate()
    {
        CharacterController controller = GetComponent<CharacterController>();

        if (Input.GetKeyDown(KeyCode.LeftAlt))
            running = true;
        if (Input.GetKeyUp(KeyCode.LeftAlt))
            running = false;

        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= running ? speed * 3 : speed;
            if (Input.GetKeyDown(KeyCode.Space))
                moveDirection.y = running ? jumpSpeed * 2 : jumpSpeed;
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
    
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        /* If I jump on the House.Down the audio plays. */
        if ((body == null || body.isKinematic) && hit.gameObject.name != "Terrain" && hit.gameObject.name != "Down")
        {
            audio.Play();
            return;
        }
        if (body == null || body.isKinematic)
            return;
        if (hit.moveDirection.y < -0.3)
            return;


        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        body.velocity = pushDir * pushPower * (running ? 2 * (int)Math.Pow(pushPower, 2) : 1);
    }
}