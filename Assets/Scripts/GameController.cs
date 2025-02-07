using UnityEngine;
using UnityEngine.SceneManagement;  // Para cargar la escena de nuevo

public class GameController : MonoBehaviour
{
    public void RestartGame()
{
    Debug.Log("Reiniciando el juego...");
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}
}
