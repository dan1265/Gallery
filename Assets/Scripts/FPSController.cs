using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public float interactionDistance = 5f;

    public bool inVideo = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerCamera = Camera.main;  // Asumiendo que tienes una cámara principal
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (inVideo)
            return;

        HandleInteraction();
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

    void HandleInteraction()
    {
        // El rayo sale del centro de la cámara (Viewport)
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        // Dispara el rayo
        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            Button button = hit.collider.GetComponent<Button>();
            if (button != null)
            {
                // Si el jugador presiona el botón de "Usar" (ej: el clic izquierdo o la tecla E)
                if (Input.GetMouseButtonDown(0))
                {
                    // **¡Aquí es donde se ejecuta el botón!**
                    button.onClick.Invoke();
                }
            }
        }
        // Si no golpeamos nada interactivo, vuelve al estado normal del puntero.
    }
}
