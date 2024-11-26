using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class DBConn : MonoBehaviour
{
    private string url = "http://localhost/Api/create_user.php";
    [SerializeField] private User user;
    [SerializeField] private ServerResponse response;

    public void Username(string username)
    {
        user.username = username;
    }

    public void Password(string password)
    {
        user.password = password;
    }

    public void CreatedBy(string createdBy)
    {
        user.created_by = createdBy; // Asignamos el valor de created_by
    }

    public void Login(string username, string password, string createdBy, System.Action<bool, int> callback)
    {
        StartCoroutine(LoginE(username, password, createdBy, callback));
    }

    IEnumerator LoginE(string username, string password, string createdBy, System.Action<bool, int> callback)
    {
        User user = new User
        {
            username = username,
            password = password,
            created_by = createdBy
        };

        string jsonString = JsonUtility.ToJson(user);
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonString);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Request Error: " + request.error);
            callback(false, -1); // Error en la solicitud, devolvemos -1
        }
        else
        {
            string responseText = request.downloadHandler.text;
            Debug.Log("Server Response: " + responseText);

            try
            {
                ServerResponse response = JsonUtility.FromJson<ServerResponse>(responseText);

                if (response.status == "success")
                {
                    int userId = response.userId; // Obtén el userId del JSON.
                    callback(true, userId); // Devuelve éxito y el userId.
                }
                else
                {
                    callback(false, -1); // Login fallido, devuelve un userId inválido.
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("JSON Parse Error: " + e.Message);
                callback(false, -1); // En caso de error de parseo.
            }
        }
    }
    private string insertScoreUrl = "http://localhost/Api/insert_game2.php";

    public void InsertScore(int userId, int score, string createdBy, System.Action<bool> callback)
    {
        StartCoroutine(InsertScoreCoroutine(userId, score, createdBy, callback));
    }

    private IEnumerator InsertScoreCoroutine(int userId, int score, string createdBy, System.Action<bool> callback)
    {
        var data = new
        {
            user_id = userId,
            score = score,
            created_by = createdBy // Incluimos el creador en los datos
        };

        string jsonString = JsonUtility.ToJson(data);
        UnityWebRequest request = new UnityWebRequest(insertScoreUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonString);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error en la conexión: " + request.error);
            callback(false);
        }
        else
        {
            string responseText = request.downloadHandler.text;
            Debug.Log("Respuesta del servidor: " + responseText);

            try
            {
                ServerResponse response = JsonUtility.FromJson<ServerResponse>(responseText);
                callback(response.status == "success");
            }
            catch
            {
                callback(false);
            }
        }
    }
    private string getBestScoreUrl = "http://localhost/Api/get_best_score.php";
    public void GetBestScore(int userId, System.Action<int> callback)
    {
        StartCoroutine(GetBestScoreCoroutine(userId, callback));
    }

    private IEnumerator GetBestScoreCoroutine(int userId, System.Action<int> callback)
    {
        string urlWithParams = getBestScoreUrl + "?user_id=" + userId;

        UnityWebRequest request = UnityWebRequest.Get(urlWithParams);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error en la conexión: " + request.error);
            callback(-1); // Error: devolvemos -1 como puntaje inválido
        }
        else
        {
            string responseText = request.downloadHandler.text;
            Debug.Log("Respuesta del servidor: " + responseText);

            try
            {
                BestScoreResponse response = JsonUtility.FromJson<BestScoreResponse>(responseText);
                callback(response.bestScore); // Devolvemos el mejor puntaje
            }
            catch
            {
                callback(-1); // Error de parseo: puntaje inválido
            }
        }
    }



}
[System.Serializable]
public class BestScoreResponse
{
    public int bestScore;
}
[System.Serializable]
public class User
{
    public string username;
    public string password;
    public string created_by;
}


[System.Serializable]
public class ServerResponse
{
    public string status; // Éxito o fallo
    public string message; // Mensaje del servidor
    public int userId; // Identificador del usuario, si aplica
}