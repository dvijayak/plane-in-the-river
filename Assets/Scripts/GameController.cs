using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{    
    public ZenvaVR.ObjectPool enemyPool;    
    public float enemySpawnIntervalSeconds = 3f;

    public float horizontalLimit = 3.6f;

    public Player player;

    public float score = 0;
    public Text scoreText;
    public float fuel = 100f;
    public Text fuelText;

    public float fuelDecreaseSpeed = 5f;
    public float fuelScoreAmount = 5f;

    public GameObject fuelCanPrefab;
    public float fuelCanSpawnIntervalSeconds = 6f;

    public float gameRestartInterval = 3f;

    private float enemySpawnTimer = 0;
    private float fuelCanSpawnTimer = 0;
    private float gameRestartTimer = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        RefreshCooldownTimer(ref gameRestartTimer, gameRestartInterval);
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsCoolingDown(ref enemySpawnTimer))
        {
            SpawnEnemy();

            RefreshCooldownTimer(ref enemySpawnTimer, enemySpawnIntervalSeconds);
        }
        Cooldown(ref enemySpawnTimer);

        if (!IsCoolingDown(ref fuelCanSpawnTimer))
        {
            SpawnFuelCan();

            RefreshCooldownTimer(ref fuelCanSpawnTimer, fuelCanSpawnIntervalSeconds);
        }
        Cooldown(ref fuelCanSpawnTimer);

        if (player != null)
        {
            fuel -= Time.deltaTime * fuelDecreaseSpeed;
            if (fuel <= 0)
            {
                fuel = 0;
                Destroy(player.gameObject);
            }
        }
        else
        {
            // Player has just died
            if (!IsCoolingDown(ref gameRestartTimer))
            {
                SceneManager.LoadScene("Game");
            }
            Cooldown(ref gameRestartTimer);
        }

        // Update score + fuel displays
        scoreText.text = $"Score: {score}";
        fuelText.text = $"Fuel: {(int)fuel}";
    }

    void SpawnEnemy()
    {
        GameObject go = enemyPool.GetObj();
        go.transform.SetParent(transform);
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(0, 1, 0));        
        go.transform.position = new Vector3(
            Random.Range(-horizontalLimit, horizontalLimit),
            point.y + 4f,
            transform.position.z) ;
        go.GetComponent<Enemy>().OnKill += EnemyKillHandler;
    }

    void EnemyKillHandler()
    {
        score += 25f;
    }

    void SpawnFuelCan()
    {        
        GameObject go = Instantiate(fuelCanPrefab);
        go.transform.SetParent(transform);
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(0, 1, 0));
        go.transform.position = new Vector3(
            Random.Range(-horizontalLimit, horizontalLimit),
            point.y + 4f,
            transform.position.z);
        go.GetComponent<FuelCan>().OnFuelCollect += FuelCanCollectHandler;
    }

    void FuelCanCollectHandler()
    {
        fuel += fuelScoreAmount;
    }

    void Cooldown(ref float cooldownTimer)
    {
        if (IsCoolingDown(ref cooldownTimer))
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    void RefreshCooldownTimer(ref float cooldownTimer, float interval)
    {
        cooldownTimer = interval;
    }

    bool IsCoolingDown(ref float cooldownTimer)
    {
        return cooldownTimer > 0;
    }
}
