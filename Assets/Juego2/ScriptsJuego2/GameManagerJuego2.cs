using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerJuego2 : MonoBehaviour
{
    public static GameManagerJuego2 instance;

    public Text scoreText;
    public Text bestScoreText;
    public int score = 0;
    public float timer = 0f;

    private int bestScore = 0; // Mejor puntaje local o de la base de datos
    private bool useDatabase = false; // Indica si se usa base de datos

    [SerializeField] private DBConn dbConn; // Referencia al sistema de conexión con la base de datos
    private int userId;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        dbConn = FindObjectOfType<DBConn>(); // Conectar con la base de datos
        userId = PlayerPrefs.GetInt("userId", 0); // Obtener el ID del usuario logeado

        useDatabase = userId > 0; // Usar base de datos si hay usuario registrado

        if (useDatabase)
        {
            LoadBestScoreFromDatabase();
        }
        else
        {
            LoadGame(); // Cargar datos localmente
        }

        UpdateScore();
        UpdateBestScoreText();
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    public void IncreaseScore()
    {
        score++;
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    private void UpdateBestScoreText()
    {
        bestScoreText.text = "Best Score: " + bestScore;
    }

    public void GameOver()
    {
        if (score > bestScore)
        {
            bestScore = score;

            if (useDatabase)
            {
                SaveScoreToDatabase();
            }
            else
            {
                SaveGame();
            }
        }

        PlayerPrefs.SetInt("FinalScore", score);
        SceneManager.LoadScene("DerrotaJuego2");
    }

    private void SaveScoreToDatabase()
    {
        string createdBy = PlayerPrefs.GetString("username", "Unknown");
        dbConn.InsertScore(userId, score, createdBy, (success) =>
        {
            if (success)
            {
                Debug.Log("Score registrado exitosamente en la base de datos.");
            }
            else
            {
                Debug.LogError("Error al registrar el score en la base de datos.");
            }
        });
    }

    private void LoadBestScoreFromDatabase()
    {
        dbConn.GetBestScore(userId, (bestScoreFromDb) =>
        {
            if (bestScoreFromDb >= 0)
            {
                bestScore = bestScoreFromDb;
                UpdateBestScoreText();
            }
            else
            {
                Debug.LogError("Error al cargar el mejor puntaje desde la base de datos.");
            }
        });
    }

    public void SaveGame()
    {
        SaveData data = new SaveData();
        data.score = score;
        data.time = timer;
        data.bestScore = bestScore;

        SaveSystem.SaveGame(data);
    }

    public void LoadGame()
    {
        SaveData data = SaveSystem.LoadGame();

        if (data != null)
        {
            bestScore = data.bestScore;
            score = 0;

            UpdateScore();
        }
    }
}