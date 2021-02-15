using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerDirection
{
    LEFT,
    RIGHT
}

public class PlayerMovement : MonoBehaviour
{


    [SerializeField, Range(0f, 100f)]
    float maxSpeed = 10f;

    [SerializeField, Range(0f, 100f)]
    float minSpeed = 5.0f;

    [SerializeField, Range(0f, 100f)]
    float jumpSpeed = 20.0f;

    [SerializeField, Range(0f, 100f)]
    float maxAcceleration = 14f;

    public PlayerDirection currentDirection;

    Rigidbody body;
    Vector3 velocity, desiredVelocity;

    public static PlayerMovement Instance;

    private float AIR_DAMPING = 0.5f;


    bool grounded;
    float distToGround;

    void Awake()
    {
        body = GetComponent<Rigidbody>();
        CapsuleCollider collider = GetComponent<CapsuleCollider>();
        grounded = true;
        distToGround = collider.bounds.extents.y + 0.01f;
        currentDirection = PlayerDirection.LEFT;

        if(Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        grounded = CheckGrounded();
        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        //playerInput = Vector2.ClampMagnitude(playerInput, 1f);

        desiredVelocity = new Vector3(playerInput.x, 0f, 0f) * maxSpeed;

        if (!grounded)
        {
            desiredVelocity.x *= AIR_DAMPING;
        }

        //Jump!
        if (playerInput.y > 0 && grounded)
        {
            velocity = body.velocity;
            velocity.y = jumpSpeed;
            body.velocity = velocity;
        }

    }

    void FixedUpdate()
    {
        velocity = body.velocity;
        velocity.x = desiredVelocity.x;
        velocity.y = body.velocity.y;
        body.velocity = velocity;

        if(body.velocity.x < 0)
        {
            currentDirection = PlayerDirection.LEFT;
        } else if (body.velocity.x > 0)
        {
            currentDirection = PlayerDirection.RIGHT;
        }
    }


    bool CheckGrounded() {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround);
    }
}
