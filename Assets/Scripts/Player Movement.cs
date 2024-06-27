using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    private bool groundedPlayer;
     private float walkSpeed = 1.0f;  // Reduziere die Geschwindigkeit für langsames Gehen
    private float runSpeed = 4.0f;   // Geschwindigkeit für schnelles Laufen
    private float playerSpeed ;
    private float jumpHeight = 1.0f;
    private float gravityValue = 0f;
    private Vector3 playerVelocity;
    public float horizontalInput;
    public float verticalInput;
   // private Vector3 movedirection;
    public float verticalMovementInput; // Neue Variable für vertikale Bewegung
    public float turnspeed = 10;
    public float movespeed = 5;
    
    public Animator animator;
    // Start is called before the first frame update
    
     // Mausempfindlichkeit
    public float mouseSensitivity = 100f;
    private float xRotation = 0f;
        void Start()
    {
        //controller = gameObject.AddComponent<CharacterController>();
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked; // Sperrt den Cursor im Spiel
    }

    // Update is called once per frame
    void Update()
    {
        /*
        // read values from keyboard
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        verticalMovementInput = Input.GetAxis("VerticalMovement"); // Neue Eingabeachse lesen

        

        // move the object 
        //movedirection = new Vector3(horizontalInput , 0 ,verticalInput);
        //transform.Translate(movedirection * turnspeed * Time.deltaTime);

        // move the object forward/backward
        //transform.Translate(Vector3.forward * Time.deltaTime * verticalInput * movespeed, Space.World);
        transform.Translate(Vector3.forward * Time.deltaTime * verticalInput * movespeed);
        //Vector3 forwardMovement = transform.forward * verticalInput * movespeed * Time.deltaTime;
        // move the object sideways  
        Vector3 unitVector = new Vector3 (0.0f,0.0f,0.0f);
        Vector3 rotatedVector = Quaternion.AngleAxis(90, Vector3.right * Time.deltaTime * horizontalInput)* unitVector;    
        transform.Translate(rotatedVector, Space.World);
        
         // move the object up/down
        transform.Translate(Vector3.up * Time.deltaTime * verticalMovementInput, Space.World);
        // rotate the object
        if (verticalInput != 0 && horizontalInput != 0)
        {
            transform.Rotate(Vector3.up * horizontalInput  * Time.deltaTime * turnspeed, Space.World);
        }*/
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        verticalMovementInput = Input.GetAxis("VerticalMovement");

        //Vector3 move = new Vector3(Input.GetAxis("Horizontal"),  Input.GetAxis("VerticalMovement"), Input.GetAxis("Vertical"));

        playerSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
      // Vector3 turnVelocity = transform.up *Input.GetAxis("Horizontal"); 
       Vector3 move = transform.right * horizontalInput + transform.forward * verticalInput  + transform.up * verticalMovementInput;
        

       
        controller.Move(move * Time.deltaTime * playerSpeed);
       // transform.Rotate(turnVelocity * Time.deltaTime * turnspeed);

 // Rotation des Spielers um die Y-Achse (umsehen)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);
        if (Camera.main != null)
        {
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
        else
        {
            Debug.LogWarning("Main camera not found. Please ensure your main camera has the 'MainCamera' tag.");
        }


        animator.SetFloat("InputHorizontal", horizontalInput);
        animator.SetFloat("InputVertical", verticalInput);
        animator.SetFloat("InputMagnitude", new Vector2(horizontalInput, verticalInput).magnitude);
        animator.SetBool("IsGrounded", groundedPlayer);

        if (move != Vector3.zero)
        {
            //gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
        
        playerVelocity.y += gravityValue * Time.deltaTime;
        //controller.Move(playerVelocity * Time.deltaTime);

       
        

        
    }
}
