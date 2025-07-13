using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI enemyScoreText;

    public GameObject playerForklift;
    public GameObject enemyForklift;

    private Vector3 playerStartPos;
    private Quaternion playerStartRot;
    private Vector3 enemyStartPos;
    private Quaternion enemyStartRot;
    public ItemSpawner itemSpawner;
    private int playerScore = 0;
    private int enemyScore = 0;

    public bool roundActive = false;  // Flag to track round state

    public int maxScore = 3;
    public TextMeshProUGUI winText; // Assign this in the Inspector

    public bool gameEnded = false;

    public GameObject playerForks;
    public GameObject enemyForks;

    private Vector3 playerForksStartPos;
    private Quaternion playerForksStartRot;
    private Vector3 enemyForksStartPos;
    private Quaternion enemyForksStartRot;



    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        // Store starting positions and rotations
        playerStartPos = playerForklift.transform.position;
        playerStartRot = playerForklift.transform.rotation;
        enemyStartPos = enemyForklift.transform.position;
        enemyStartRot = enemyForklift.transform.rotation;

            // Fork positions
        playerForksStartPos = playerForks.transform.position;
        playerForksStartRot = playerForks.transform.rotation;
        enemyForksStartPos = enemyForks.transform.position;
        enemyForksStartRot = enemyForks.transform.rotation;
    }

    public void AddPlayerScore(int amount)
    {
        if (roundActive) return;  // Prevent adding points while round is active

        playerScore += amount;
        playerScoreText.text = "Player: " + playerScore;
        game_over();

        roundActive = true;  // Start round

        // After a short delay, reset round and forks
        Invoke(nameof(ResetForklifts), 2f);
    }

    public void AddEnemyScore(int amount)
    {
        if (roundActive) return;  // Prevent adding points while round is active

        enemyScore += amount;
        enemyScoreText.text = "Enemy: " + enemyScore;
        game_over();

        roundActive = true;  // Start round

        // After a short delay, reset round and forks
        Invoke(nameof(ResetForklifts), 2f);
    }

    private void ResetForklifts()
    {
        // Reset player
        ResetForklift(playerForklift, playerStartPos, playerStartRot);

        // Reset enemy
        ResetForklift(enemyForklift, enemyStartPos, enemyStartRot);
        // Reset player forks
        ResetFork(playerForks, playerForksStartPos, playerForksStartRot);

        // Reset enemy forks
        ResetFork(enemyForks, enemyForksStartPos, enemyForksStartRot);


        itemSpawner.SpawnObstacles();

        // Mark the round as inactive, allowing points again
        roundActive = false;
    }

    private void ResetForklift(GameObject forklift, Vector3 position, Quaternion rotation)
    {
        Rigidbody rb = forklift.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        forklift.transform.position = position;
        forklift.transform.rotation = rotation;
    }
    private void ResetFork(GameObject fork, Vector3 position, Quaternion rotation)
    {
        Rigidbody rb = fork.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        fork.transform.position = position;
        fork.transform.rotation = rotation;
    }

    private void game_over()
    {
        if (enemyScore >= 3 || playerScore >= 3)
            {
            gameEnded = true;

            // Disable forklift controls
            playerForklift.GetComponent<CarController>().enabled = false;
            enemyForklift.GetComponent<ForkliftAI>().enabled = false;

            // Show win message
            if (playerScore >= 3)
            {   
                winText.gameObject.SetActive(true);
                winText.text = "PLAYER WINS! Press R to restart";
            }
            else if (enemyScore >= 3)
            {
                winText.gameObject.SetActive(true);
                winText.text = "ENEMY WINS! Press R to restart";
            }

            winText.gameObject.SetActive(true);
            }
    }
    void Update()
    {
        if (gameEnded && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //gameEnded = false;
        }
    }

}
