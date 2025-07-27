using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;
    public float mouseSensitivity = 2f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    private Transform cam;
    private Transform gun;

    public GameObject bullet;
    public Transform muzzle;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = transform.Find("Main Camera");
        gun = transform.Find("Shoulder");
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Checagem de solo
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Mant�m o personagem preso ao ch�o
        }

        // Entrada de movimento
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Pulo
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Gravidade
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Rota��o com o mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(Vector3.up * mouseX);

        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        cam.Rotate(Vector3.right * mouseY);
        gun.Rotate(Vector3.right * mouseY);

        // cria o tiro:
        if (Input.GetMouseButtonDown(0)) 
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bullet, muzzle.position, muzzle.rotation, null); // cria o tiro na posi��o do muzzle
    }
}
