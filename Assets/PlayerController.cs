using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float Speed = .1f;
    public float jumpForce = 4f;

    public bool isGrounded = true;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckMove();
        CheckJump();
        CheckGround();
    }

    void CheckMove()
    {
        var movement = Vector3.zero;
        var factor = Speed * Time.deltaTime;
        var rotFactor = 1f;

        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (Cursor.lockState == CursorLockMode.Locked)
        {
            RotateWithMouse();

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * rotFactor);

        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up * -rotFactor);
        }

        if (Input.GetKey(KeyCode.W))
        {
            movement += transform.forward * factor;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement -= transform.forward * factor;
        }

        animator.SetFloat("movement", movement.magnitude);


        transform.position += movement;

    }

    void RotateWithMouse()
    {

        var rotFactor = 1f;
        var mouseX = Input.GetAxis("Mouse X");
        // var mouseY = Input.GetAxis("Mouse Y");
        transform.Rotate(Vector3.up * mouseX * rotFactor);
        // transform.Rotate(Vector3.left * mouseY * rotFactor);



    }


    void CheckJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isGrounded = false;
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("onJump");
        }
    }

    void CheckGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.1f))
        {
            isGrounded = true;
            animator.SetBool("isGrounded", true);
        }
        else
        {
            isGrounded = false;
        }
    }


}
