using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private InputActionReference look, movement, jump;
    private Vector2 movementInput;
    private Vector2 lookInput;

    //private Rigidbody rb;
    private Camera playerCamera;
    private CharacterController characterController;
    [SerializeField]
    private float lookSensitivityX;
    [SerializeField]
    private float lookSensitivityY;


    [SerializeField]
    private float movementSpeed;

    // Start is called before the first frame update
    void Awake()
    {
        //rb = GetComponent<Rigidbody>();
        playerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        movementInput = movement.action.ReadValue<Vector2>();
        lookInput = look.action.ReadValue<Vector2>();

        // look
        playerCamera.transform.Rotate(-lookInput.y * lookSensitivityY * Time.deltaTime, 0, 0);
        this.transform.Rotate(0, lookInput.x * lookSensitivityX * Time.deltaTime, 0);

        // move
        Vector3 speed = this.transform.forward.normalized * (movementInput.y * movementSpeed * Time.deltaTime);
        speed += this.transform.right.normalized * (movementInput.x * movementSpeed * Time.deltaTime);
        characterController.Move(speed);
    }

    private void FixedUpdate()
    {


    }


}
