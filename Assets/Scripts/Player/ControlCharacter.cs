using UnityEngine;
using System.Collections;

public class ControlCharacter : MonoBehaviour
{

    public CharacterController controller;
	public float speed = 5f;
    float defaultSpeed;
    public float slowSpeed;
    public float rotateSpeed;
    float defaultRotate;
    public float slowRotate;
    public float speedJump;
    public float speedSlowJump;
    float timer = 0f;
    float jumpTimer = 0f;
    public float iFrameLength;
    public float rollLength;
    [SerializeField] float quickTurnSpeed;
    [SerializeField] float gravity;

    [Space(20)]
    public float jumpHeight = 300f;

    [Space(20)]
    public bool isGrounded = false;
    public bool isSlowJumping = false;
    public bool isRolling = false;


    Vector3 velocity;


    public bool isSlow = false;
    public bool canDamage = true;
    public bool canRotate = true;
    public bool canJump = true;
    public bool canMove = true;


	void Start ()
	{
        defaultSpeed = speed;
        defaultRotate = rotateSpeed;
	}

    public void HandleUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (isSlow)
        {
            speed = slowSpeed;
            rotateSpeed = slowRotate;
        }
        else
        {
            speed = defaultSpeed;
            rotateSpeed = defaultRotate;
        }

        if (!isRolling && direction.z != 0f && canMove)
        {
            controller.Move(direction.z * transform.forward * Time.deltaTime * speed);
        }
        if (!isRolling && direction.x != 0f && canMove)
        {
            controller.Move(direction.x * transform.right * Time.deltaTime * speed);
        }

        Debug.DrawRay(transform.position, transform.forward * 5f, Color.red);
        
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 5f))
        {
            if (hit.collider.TryGetComponent<IInteractable>(out var interactable))
            {
                if (Input.GetButtonDown("Slow") && isGrounded)
                {
                   // Debug.Log(hit.collider.name);
                    interactable.Interact();
                }
            }
        }



        // Rotate Character + Camera

        if (Input.GetButton("RotateRight") && canRotate && canMove)
        {
            gameObject.transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        }

        if (Input.GetButton("RotateLeft") && canRotate && canMove)
        {
            gameObject.transform.Rotate(Vector3.up, -rotateSpeed * Time.deltaTime);
        }
        
        // Quick Turn

        if (vertical < 0 && isGrounded && canRotate && canMove)
        {
            if (Input.GetButtonDown("Slow"))
            {
                //TODO: Add smooth rotation
                gameObject.transform.Rotate(Vector3.up, Mathf.LerpAngle(0, 180, quickTurnSpeed));
            }
        }


        // Dodge and iFrames

        if (Input.GetButton("RotateRight") && Input.GetButtonDown("Jump") && vertical == 0 && horizontal == 0 && isGrounded && canMove)
        {
            canJump = false;
            canDamage = false;
            RollRight();
        }

        if (Input.GetButton("RotateLeft") && Input.GetButtonDown("Jump") && vertical == 0 && horizontal == 0 && isGrounded && canMove)
        {
            canJump = false;
            canDamage = false;
            RollLeft();
        }

        //Handle iFrame + Roll timing

        if (isRolling)
        {
            timer += Time.deltaTime;
            canRotate = false;
        }

        if (timer > iFrameLength)
        {
            canDamage = true;
        }

        if (timer > rollLength)
        {
            isRolling = false;
            canRotate = true;
            canJump = true;
            timer = 0;
        }

        GroundCheck();

        //Handle jumping
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (isGrounded && velocity.y < 0)
        {
            new WaitForSeconds(2);
            velocity.y = -1f;
        }


        if (Input.GetButtonDown("Jump") && direction.magnitude != 0 && isGrounded && canJump && canMove)
        {
            Jump();
        }
        if (Input.GetButtonDown("Jump") && direction.magnitude == 0 && isGrounded && canJump && canMove)
        {
            isSlowJumping = true;
            Jump();
        }


        if (Input.GetButton("Slow") && isGrounded)
        {
            isSlow = true;
        }
        else
        {
            isSlow = false;
        }

        if (!isGrounded)
        {
            speed = speedJump;
        }
        else
        {
            speed = defaultSpeed;
        }
        if (isSlowJumping)
        {
            speed = speedSlowJump;
        }
        else
        {
            speed = defaultSpeed;
        }

    }



    void GroundCheck()
    {
        

        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, 4f))
        {
            isGrounded = true;

            jumpTimer += Time.deltaTime;

            if (jumpTimer >= .25f)
            {
                canJump = true;
            }
            else
            {
                canJump = false;
            }
        }
        else
        {
            jumpTimer = 0f;
            isGrounded = false;
        }
    }

    void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    void RollRight()
    {
        isRolling = true;
    }

    void RollLeft()
    {
        isRolling = true;
    }
}
