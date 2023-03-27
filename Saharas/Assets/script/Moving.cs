using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    private Rigidbody body;
    private Animator animator;
    private Vector3 moveDirection;

    public float speed = 5f;
    public float jumpForce = 10f;
    public float sensitivity = 10f;
    public Transform head;
    private float rotationY;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float forward = Input.GetAxis("Vertical");
        float right = Input.GetAxis("Horizontal");
        bool ground = Physics.Raycast(transform.position + Vector3.up * 0.2f, Vector3.down, 1.5f);
        bool jump = Input.GetButtonDown("Jump") && ground;

        Vector3 rotation = new Vector3(0, Input.GetAxis("Mouse X"));
        rotationY += Input.GetAxis("Mouse Y") * sensitivity;
        rotationY = Mathf.Clamp(rotationY, -45f, 90f);
        head.localEulerAngles = new Vector3(-rotationY, 0, 0);
        transform.Rotate(rotation * sensitivity);

        animator.SetFloat("Right", right);
        animator.SetFloat("Forward", forward);
        animator.SetBool("Jump", jump);

        moveDirection = transform.forward * forward + transform.right * right;
        moveDirection *= speed;
        body.velocity = moveDirection + Vector3.up * body.velocity.y;

        if (jump)
        {
            StartCoroutine("Jump");
        }
    }

    IEnumerator Jump()
    {
        float curJump = jumpForce;
        while (curJump > 0)
        {
            body.velocity += new Vector3(0, Mathf.Sqrt(curJump / 2), 0);
            curJump -= 1.1f;
            yield return new WaitForFixedUpdate();
        }
    }
}