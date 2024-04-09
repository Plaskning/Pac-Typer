using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidbody;

    [SerializeField] private ParticleSystem explosionParticle;

    [SerializeField] private float movementForce;

    [SerializeField] private float maxSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("haha");
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 movementVector = context.ReadValue<Vector2>();

        Vector2 forceVector = movementVector * movementForce;

        Vector3 adjustedVector = new Vector3(forceVector.x, 0, forceVector.y);

        rigidbody.AddForce(adjustedVector);

        Debug.Log(movementVector);
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
        explosionParticle.Play();
    }
}
