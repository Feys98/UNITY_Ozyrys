using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class MetaEnemyController : MonoBehaviour
{
    /// <summary>
    /// Determines if enemy is immune to intensive light
    /// </summary>
    public bool LightImmunity;
    /// <summary>
    /// Determines base enemy health points
    /// </summary>
    public int BaseHealthPoints;
    /// <summary>
    /// Determines time span of next enemy spawn within dungeon
    /// </summary>
    public float RespawnTime;
    /// <summary>
    /// How many enemies of this type can spawn in dungeon
    /// </summary>
    public float MaxEnemiesCount;
    /// <summary>
    /// Determines move speed (
    /// </summary>
    public float MoveSpeed;

    public GameObject EnemyPrefab;

    private List<GameObject> spawnPoints;
    private int enemiesCount;
    private GameObject m_player;

    void Start()
    {
        StartCoroutine(nameof(SpawnEnemy));

        spawnPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("Spawner"));
        m_player = GameObject.FindGameObjectWithTag("Player");
    }

    public IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(RespawnTime);
        yield return new WaitUntil(() => MaxEnemiesCount > enemiesCount + 1);

        var lightDistance = m_player.GetComponentInChildren<Light2D>().pointLightOuterRadius;

        var spawnpoint = spawnPoints
            .AsEnumerable()
            .OrderBy(x => UnityEngine.Random.value)
            .Where(x => Vector3.Distance(x.transform.position, m_player.transform.position) >= lightDistance)
            .First();

        var enemy = Instantiate(EnemyPrefab, new Vector3(spawnpoint.transform.position.x, spawnpoint.transform.position.y, transform.position.z), Quaternion.identity);

        var controller = enemy.GetComponent<EnemyController>();
        controller.HealthPoints = BaseHealthPoints;
        controller.MoveSpeed = MoveSpeed;
        controller.OnKilled += () => enemiesCount--;

        enemiesCount++;

        yield return StartCoroutine(nameof(SpawnEnemy));
    }

}
