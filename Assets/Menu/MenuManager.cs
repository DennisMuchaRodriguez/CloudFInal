using UnityEngine;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public TMP_Text usernameText;

    private void Start()
    {
        // Muestra el nombre del usuario en el texto
        usernameText.text = "Welcome, " + UserSession.Username;
    }
    public void CambioScena(string SceneGame)
    {
        SceneManager.LoadScene(SceneGame);
    }
}
