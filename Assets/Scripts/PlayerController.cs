using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PacInput inputActions;

    private Rigidbody rigidbody;

    [SerializeField] private GameObject explosionParticle;

    [SerializeField] private float movementForce;

    [SerializeField] private float maxSpeed;
    private bool isControlling;

    private bool isMoving;
    private float velocity;

    private Vector3 adjustedVector;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        inputActions = new PacInput();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateVelocity();
        HandleMovement();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 movementVector = context.ReadValue<Vector2>();

        Vector2 forceVector = movementVector * movementForce;

        adjustedVector = new Vector3(forceVector.x, 0, forceVector.y);

        if (context.started || context.performed)
        {
            isControlling = true;
        }
        else
        {
            isControlling = false;
        }

        // if no input stop force 

       //Debug.Log(movementVector);
    }

    private void HandleMovement()
    {
        if (isControlling)
        {
            if (rigidbody.velocity.magnitude <= maxSpeed)
            {
                rigidbody.AddForce(adjustedVector);
                Debug.Log("addingForce");
            }
        }
    }

    public void OnCharSelect(InputAction.CallbackContext context)
    {

    }

    public void OnCharEnter(InputAction.CallbackContext context)
    {
        Debug.Log("Shoot!");
    }

    public void OnNukeButton(InputAction.CallbackContext context)
    {
        Instantiate(explosionParticle,transform.position,Quaternion.identity);
    }

    private void CalculateVelocity()
    {
        velocity = rigidbody.velocity.magnitude;
        if(velocity == 0)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
    }
}
