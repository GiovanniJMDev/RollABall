using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Rigidbody del jugador.
    private Rigidbody rb;

    // Variable para llevar un conteo de los objetos "PickUp" recogidos.
    private int count;

    // Movimiento en los ejes X e Y.
    private float movementX;
    private float movementY;

    // Velocidad de movimiento del jugador.
    public float speed = 0;

    // Fuerza de salto.
    public float jumpForce = 10f;

    // Variable para comprobar si el jugador está en el suelo.
    private bool isGrounded;

    // UI: texto que muestra el conteo de objetos "PickUp" recogidos.
    public TextMeshProUGUI countText;

    // UI: objeto que muestra el texto de victoria.
    public GameObject winTextObject;

    // UI: objeto que muestra el texto de derrota.
    public GameObject loseTextObject;

    // Start se llama antes de la primera actualización de fotograma.
    void Start()
    {
        // Obtener y almacenar el componente Rigidbody.
        rb = GetComponent<Rigidbody>();

        // Inicializar el conteo de objetos recogidos a cero.
        count = 0;

        // Actualizar el texto del conteo.
        SetCountText();

        // Inicialmente desactivar el texto de victoria y derrota.
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
    }

    // Esta función se llama cuando se detecta una entrada de movimiento.
    void OnMove(InputValue movementValue)
    {
        // Convertir el valor de entrada en un Vector2 para el movimiento.
        Vector2 movementVector = movementValue.Get<Vector2>();

        // Almacenar los componentes X e Y del movimiento.
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    // Esta función se llama cuando se detecta una entrada de salto.
void OnJump(InputValue jumpValue)
{
    if (isGrounded)
    {
        Debug.Log("Salto detectado");  // Esto te ayudará a ver si está detectando la entrada correctamente.
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    else
    {
        Debug.Log("No está en el suelo, no salta");
    }
}

    // FixedUpdate se llama una vez por cada fotograma de actualización de la física.
    private void FixedUpdate()
    {
        // Crear un vector 3D para el movimiento utilizando las entradas X e Y.
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        // Aplicar fuerza al Rigidbody para mover al jugador.
        rb.AddForce(movement * speed);
    }

    // Detecta cuando el jugador entra en contacto con un objeto.
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            // Desactivar el objeto recogido (lo hace desaparecer).
            other.gameObject.SetActive(false);

            // Incrementar el conteo de objetos recogidos.
            count++;

            // Actualizar el texto del conteo.
            SetCountText();
        }
    }

    // Actualiza el texto que muestra el conteo de objetos recogidos.
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= 8)
        {
            // Mostrar el texto de victoria.
            winTextObject.SetActive(true);

            // Destruir el objeto enemigo si se gana.
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }

    // Detecta las colisiones con otros objetos (como el suelo).
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;  // El jugador está tocando el suelo.
        }
    }

    // Detecta cuando el jugador ya no está tocando el suelo.
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;  // El jugador ha dejado de tocar el suelo.
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Destruir el objeto actual (el jugador) al chocar con un enemigo.
            Destroy(gameObject);

            // Mostrar el texto de derrota.
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
        }
    }
}
