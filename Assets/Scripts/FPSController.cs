using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    public float moveSpeed = 5f;  // Velocidad de movimiento
    public float lookSpeed = 2f;  // Velocidad de rotación del ratón
    public float gravity = -9.81f;  // Fuerza de gravedad
    public float jumpHeight = 2f;  // Altura de salto (puedes ajustar esto si decides agregar salto más tarde)

    private CharacterController characterController;
    private Camera playerCamera;

    private float rotationX = 0f;
    private Vector3 velocity;  // Velocidad del jugador
    private bool isGrounded;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerCamera = Camera.main;  // Asumiendo que tienes una cámara principal
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Comprobar si estamos tocando el suelo
        isGrounded = characterController.isGrounded;

        // Movimiento
        float moveDirectionX = Input.GetAxis("Horizontal");  // A, D
        float moveDirectionZ = Input.GetAxis("Vertical");    // W, S

        Vector3 move = transform.right * moveDirectionX + transform.forward * moveDirectionZ;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;  // Evitar que el jugador "flote" al estar en el suelo
        }

        characterController.Move(move * moveSpeed * Time.deltaTime);

        // Gravedad
        velocity.y += gravity * Time.deltaTime;

        // Aplicar movimiento vertical (gravedad)
        characterController.Move(velocity * Time.deltaTime);

        // Rotación con el ratón
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
    }
}
