using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCMovement : MonoBehaviour
{
    [Header("Movement Values")]
    [SerializeField] float movementSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float gravityForce;
    [SerializeField] float jumpForce;

    [Header("Components")]
    [SerializeField] CharacterController cc;
    [SerializeField] Animator anim;
    [SerializeField] Camera cam;

    [Header("Targetting")]
    public Transform target;
    public bool shouldLook;

    Vector3 movementDirection;
    Vector3 playerVelocity;
    public bool groundedPlayer;


    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //process gravity: check if grounded and reduce velocity if so
        groundedPlayer = cc.isGrounded;
        if(groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0;
        }

        //process player inputs
        float h = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //get camera direction
        Vector3 camX = cam.transform.right;
        Vector3 camZ = Vector3.Cross(camX, Vector3.up);

        //if moving, combine camera directions and inputs then move
        if( h != 0 || z != 0)
        {
            movementDirection = (camX * h) + (camZ * z);
            movementDirection.Normalize();

            cc.Move(movementDirection * movementSpeed * Time.deltaTime);
        }

        //if not locked onto target, determine rotation towards walking direction

        //if locked onto target, determine rotion towards target

        //find direction for animation based on movement relative to faceing

        //process gravity: apply downward motion
        playerVelocity.y += gravityForce * Time.deltaTime;
        cc.Move(playerVelocity * Time.deltaTime);
    }
}
