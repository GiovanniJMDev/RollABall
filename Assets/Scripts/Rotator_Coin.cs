using UnityEngine;

public class Rotator_Coin : MonoBehaviour
{
    // Variables públicas para la velocidad de rotación en cada eje
    public float rotationSpeedX = 15f;
    public float rotationSpeedY = 30f;
    public float rotationSpeedZ = 45f;

    // Update is called once per frame
    void Update()
    {
        // Usar las variables públicas para la rotación, ajustadas por Time.deltaTime
        transform.Rotate(new Vector3(rotationSpeedX, rotationSpeedY, rotationSpeedZ) * Time.deltaTime);
    }
}
