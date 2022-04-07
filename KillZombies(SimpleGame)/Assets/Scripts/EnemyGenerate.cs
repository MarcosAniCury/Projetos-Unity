using System.Collections;
using UnityEngine;

public class EnemyGenerate : MonoBehaviour
{
    //Public vars
    public GameObject Zombie;
    public float GenerateEnemyTime = 1;
    public LayerMask LayerEnemys;

    //Private vars
    private float timeCount;
    private int enemysMaxAlive = 5;
    private int enemysAlive = 0;

    //Components
    GameObject player;

    //CONSTs
    const int RADIO_TO_GENERATE_RANDOM_POSITION = 3;
    const int DISTANCE_BETWEEN_PLAYER_AND_ENEMY_TO_SPAWN = 20;

    void Start()
    {
        player = GameObject.FindWithTag(Constants.TAG_PLAYER);
        for (int i = 0; i < enemysMaxAlive; i++) {
            StartCoroutine(GenerateZombie());
        }
    }

    void Update()
    {
        bool canGenerateEnemy = (Vector3.Distance(
            transform.position,
            player.transform.position
        ) > DISTANCE_BETWEEN_PLAYER_AND_ENEMY_TO_SPAWN);
        
        if (canGenerateEnemy && enemysAlive < enemysMaxAlive) {
            timeCount += Time.deltaTime;

            if (timeCount > GenerateEnemyTime) {
                StartCoroutine(GenerateZombie());
                timeCount = 0;
            }
        }
    }

    IEnumerator GenerateZombie()
    {
        Vector3 randomPosition;
        Collider[] colliders;
        do {
            randomPosition = GenerateRandomPosition();
            colliders = Physics.OverlapSphere(randomPosition, 1, LayerEnemys);
            yield return null;
        } while (colliders.Length > 0);
        Instantiate(Zombie, randomPosition, transform.rotation);
        enemysAlive++;
    }

    Vector3 GenerateRandomPosition()
    {
        Vector3 position = Random.insideUnitSphere * RADIO_TO_GENERATE_RANDOM_POSITION;
        position += transform.position;
        position.y = transform.position.y;

        return position;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, RADIO_TO_GENERATE_RANDOM_POSITION);
    }
}
