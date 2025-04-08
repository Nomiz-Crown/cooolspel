using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public List<GameObject> spawnList = new();
    public List<Vector2> spawnLocationList = new();

    public List<GameObject> barrierList = new();

    bool EnemiesSpawned = false;
    private List<GameObject> EnemiesActive = new();

    // Start is called before the first frame update
    void Start()
    {
        ToggleBarriers("Off");
    }

    // Update is called once per frame
    void Update()
    {
        CheckEnemiesActive();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (EnemiesSpawned || !collision.gameObject.CompareTag("Player")) return;

        ToggleBarriers("On");
        for (int i = 0; i < spawnList.Count; i++)
        {
            GameObject clone = Instantiate(spawnList[i]);
            EnemiesActive.Add(clone);
            clone.transform.position = spawnLocationList[i];
        }
        EnemiesSpawned = true;
    }
    void CheckEnemiesActive()
    {
        if (!EnemiesSpawned) return;
        int count = 0;
        for (int i = 0; i < EnemiesActive.Count; i++)
        {
            if (EnemiesActive[i] == null)
            {
                count++;
            }
        }
        if (count == EnemiesActive.Count)
        {
            ToggleBarriers("Off");
            Destroy(gameObject);
        }
    }
    void ToggleBarriers(string cond)
    {
        if (cond == "Off")
        {
            for (int i = 0; i < barrierList.Count; i++)
            {
                barrierList[i].SetActive(false);
            }
        }
        else if (cond == "On")
        {
            for (int i = 0; i < barrierList.Count; i++)
            {
                barrierList[i].SetActive(true);
            }
        }
    }
}
