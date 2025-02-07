using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Referencia al GameObject del jugador.
    public GameObject player;

    // La distancia entre la cámara y el jugador.
    private Vector3 offset;

    // Start se llama antes del primer frame.
    void Start()
    {
        // Verificar si el jugador existe antes de calcular el offset.
        if (player != null)
        {
            offset = transform.position - player.transform.position;
        }
    }

    // LateUpdate se llama una vez por frame después de todos los métodos Update.
    void LateUpdate()
    {
        // Verificar si el jugador aún existe antes de intentar mover la cámara.
        if (player != null)
        {
            transform.position = player.transform.position + offset;
        }
    }
}
