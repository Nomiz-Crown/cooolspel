using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement; // For loading scenes

[System.Serializable]
public class EnemyPrefab
{
    public GameObject prefab;
    public float pointValue;
}

public class WaveHandler : MonoBehaviour
{
    public List<GameObject> spawnPoints = new List<GameObject>();
    public List<EnemyPrefab> enemies = new List<EnemyPrefab>();
    public List<GameObject> activeEnemies = new List<GameObject>();

    public int wave = 1;
    public float wavePoints;
    public int EnemiesKilled = 0; // Track number of kills

    public TextMeshProUGUI waveText;
    public GameObject statsPanel; // Reference to the stats panel UI
    public TextMeshProUGUI waveDisplay; // TextMeshPro for displaying wave
    public TextMeshProUGUI killsDisplay; // TextMeshPro for displaying kills (this will now display the copied prefab text)

    public TextMeshProUGUI timePrefabText; // Reference to the TextMeshProUGUI prefab whose text will be copied

    private GameObject mainCharacterEnergyyy;
    private bool waveInProgress = false;
    private PerformanceTallyLogicV1 tally;
    public GameObject triggerObject;

    string filePath;
    string myLevel = "heatwave";
    void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, "highscore" + myLevel + ".json");
        tally = FindObjectOfType<PerformanceTallyLogicV1>();
        mainCharacterEnergyyy = GameObject.Find("MC V2 Cool");
        if (mainCharacterEnergyyy == null)
        {
            Debug.LogError("Player object not found! Make sure it's named correctly in the scene.");
        }
        StartCoroutine(HandleWave());
        statsPanel.SetActive(false);

        if (timePrefabText == null)
        {
            Debug.LogError("du har inte adddat tmp ts pmo icl.");
        }
    }

    void Update()
    {
        waveText.text = "Wave: " + wave;

        activeEnemies.RemoveAll(enemy => enemy == null);

        if (timePrefabText != null)
        {
            killsDisplay.text = timePrefabText.text;
        }

        if (triggerObject != null && triggerObject.activeInHierarchy)
        {
            ShowStatsPanel();
        }

        if (!waveInProgress && activeEnemies.Count == 0)
        {
            wave++;
            tally.UpdateTally("+ Wave Cleared", "Add");
            mchp playerScript = mainCharacterEnergyyy.GetComponent<mchp>();
            playerScript.TemperatureHealth = 0;
            StartCoroutine(HandleWave());
        }
    }

    IEnumerator HandleWave()
    {
        waveInProgress = true;
        wavePoints = 2 + wave;
        yield return new WaitForSeconds(5f);

        while (wavePoints > 0)
        {
            List<EnemyPrefab> affordableEnemies = enemies.FindAll(e => e.pointValue <= wavePoints);

            if (affordableEnemies.Count == 0)
            {
                break;
            }

            EnemyPrefab selectedEnemy = affordableEnemies[Random.Range(0, affordableEnemies.Count)];
            GameObject spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

            GameObject spawned = Instantiate(selectedEnemy.prefab, spawnPoint.transform.position, Quaternion.identity);
            activeEnemies.Add(spawned);

            wavePoints -= selectedEnemy.pointValue;

            yield return new WaitForSeconds(0.2f);
        }

        waveInProgress = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (activeEnemies.Contains(other.gameObject))
        {
            activeEnemies.Remove(other.gameObject);
            Destroy(other.gameObject);
            tally.UpdateTally("+ IS THERE FALL DAMAGAE?", "Add");
            EnemiesKilled++;
        }
        else if (other.gameObject == mainCharacterEnergyyy)
        {
            mchp playerScript = mainCharacterEnergyyy.GetComponent<mchp>();
            if (playerScript != null)
            {
                playerScript.TemperatureHealth = 100; // Set health to 100
                Debug.Log("Player health set to 100");
            }
        }
    }

    void ShowStatsPanel()
    {
        statsPanel.SetActive(true); // Show stats panel
        waveDisplay.text = "Wave: " + wave; // Display current wave
        // The killsDisplay now shows the copied text from the prefab
    }

    // Methods for buttons to load scenes
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu"); // Change to your actual Main Menu scene name
    }

    public void LoadHeatWave()
    {
        SceneManager.LoadScene("HeatWave"); // Change to your actual HeatWave scene name
    }
    public void SaveWaveScore()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, ""); // Initialize with an empty value
        }
        if (wave >= float.Parse(File.ReadAllText(filePath))) return;
        print(wave + " is what i save");
        File.WriteAllText(filePath, wave.ToString());
    }
}
