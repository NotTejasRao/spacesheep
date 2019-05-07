using UnityEngine;

public class PlayerBodyMovementHandler : MonoBehaviour {

    private Rigidbody rigidbody;
    public float speed = 10.0f;
    public float maxVelocityChange = 10.0f;
    public float jumpHeight = 2.0f;
    public bool canJump = true;
    private bool grounded = false;
    private bool superJumped = false;
    private float gravity;
    private PlayerInventoryHandler playerInventoryHandler;
    private Collider collider;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        gravity = GetComponent<GenericCustomGravityHandler>().gravity;
        playerInventoryHandler = GetComponent<PlayerInventoryHandler>();
    }

    void FixedUpdate()
    {
        if (grounded)
        {
            // Calculate how fast we should be moving
            Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            targetVelocity = transform.TransformDirection(targetVelocity);
            targetVelocity *= speed;

            // Apply a force that attempts to reach our target velocity
            Vector3 velocity = rigidbody.velocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;
            rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);

            // Jump
            if ((playerInventoryHandler.uraniumCount > 0) && canJump && Input.GetButton("Jump"))
            {
                rigidbody.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
                superJumped = true;
            }
        }

        // We apply gravity manually for more tuning control
        rigidbody.AddForce(new Vector3(0, -gravity * rigidbody.mass, 0));

        grounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.name == "Ground")
        {
            if (!superJumped) return;
            superJumped = false;
            Collider[] colliders = Physics.OverlapSphere(transform.position, 50);
            foreach (Collider col in colliders)
            {
                if (col.transform.name.StartsWith("Sheep"))
                {
                    col.gameObject.GetComponent<SheepHandler>().ReduceHealth();
                }
            }
            playerInventoryHandler.uraniumCount--;
        }
    }


    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            grounded = true;
        }        
    }

    float CalculateJumpVerticalSpeed()
    {
        // From the jump height and gravity we deduce the upwards speed 
        // for the character to reach at the apex.
        return Mathf.Sqrt(2 * jumpHeight * gravity);
    }

}
