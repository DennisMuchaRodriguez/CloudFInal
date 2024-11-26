using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class LoginManager : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public TMP_InputField createdByInput;
    public TMP_Text messageText;

    [SerializeField] private DBConn dbConn;

    private void Start()
    {
        dbConn = FindObjectOfType<DBConn>();
    }

    public void OnLoginButtonPressed()
    {
        dbConn.Login(usernameInput.text, passwordInput.text, createdByInput.text, OnLoginResponse);
    }

    private void OnLoginResponse(bool success, int userId)
    {
        if (success)
        {
            PlayerPrefs.SetInt("userId", userId); // Guarda el ID del usuario
            UserSession.UserId = userId;

            SceneManager.LoadScene("Menu");
        }
        else
        {
            Debug.LogError("Error de login: credenciales incorrectas.");
            messageText.text = "Login Failed";
        }
    }
}
public static class UserSession
{
    public static int UserId { get; set; }
    public static string Username { get; set; }
}