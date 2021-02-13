using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    Rigidbody body;
    Vector3 velocity, desiredVelocity, lastPosition;


    bool grounded;
    float distToGround;

    void Awake()
    {
        body = GetComponent<Rigidbody>();
        CapsuleCollider collider = GetComponent<CapsuleCollider>();
        grounded = true;
        distToGround = collider.bounds.extents.y + 0.01f;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        //playerInput = Vector2.ClampMagnitude(playerInput, 1f);
        desiredVelocity = new Vector3(playerInput.x, 0f, 0f) * maxSpeed;

        //Jump!
        if (playerInput.y > 0 && CheckGrounded())
        {
            velocity = body.velocity;
            velocity.y = jumpSpeed;
            body.velocity = velocity;
        }

    }

    void FixedUpdate()
    {
        velocity = body.velocity;
        float maxSpeedChange = maxAcceleration * Time.deltaTime;
        velocity.x = desiredVelocity.x;
        body.velocity = velocity;
    }


    bool CheckGrounded() {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround);
    }
}
