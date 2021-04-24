using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{


    public Transform player;

    public int spawnAmount;
    public float innerSpawnRadius;
    public float outerSpawnRadius;

    public List<GameObject> spawnables;



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnObjects");
    }

    IEnumerator SpawnObjects()
    {
        while(true)
        {
            Collider[] colliders = Physics.OverlapSphere(player.position, outerSpawnRadius);
            if (colliders.Length > spawnAmount * 2)
            {
                yield return new WaitForSeconds(30);
            }
            else
            {
                for (int i = 0; i < spawnAmount; i++)
                {

                    float r = outerSpawnRadius * Mathf.Sqrt(Random.value);
                    float theta = Random.value * 360;


                    float x = player.position.x + r * Mathf.Cos(theta * Mathf.Deg2Rad);
                    float y = player.position.y + r * Mathf.Sin(theta * Mathf.Deg2Rad);

                    Vector3 spawnPos = new Vector3(x, y, 0);



                    Instantiate(spawnables[Random.Range(0, spawnables.Count - 1)], spawnPos, Quaternion.identity);
                }
            }

            yield return new WaitForSeconds(30);
        }

    }


}
