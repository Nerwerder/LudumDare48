using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public int spawnAmount;
    public float spawnRadius;

    public List<GameObject> spawnables;

    public List<GameObject> currentObjects;

    bool needsRespawn = true;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnObjects");
    }

    IEnumerator SpawnObjects()
    {
        while(true)
        {

           for(int j = currentObjects.Count-1; j > -1 ; j--)
            {
                if (currentObjects[j] == null)
                {
                    currentObjects.RemoveAt(j);
                }
                    
            }


            if(needsRespawn)
            {
                for (int i = 0; i < spawnAmount; i++)
                {

                    float r = spawnRadius * Mathf.Sqrt(Random.value);
                    float theta = Random.value * 360;


                    float x = transform.position.x + r * Mathf.Cos(theta * Mathf.Deg2Rad);
                    float y = transform.position.y + r * Mathf.Sin(theta * Mathf.Deg2Rad);

                    Vector3 spawnPos = new Vector3(x, y, 0);
                    Collider2D collisons = Physics2D.OverlapCircle(spawnPos, 8);
                    if( collisons == false)
                    {
                        currentObjects.Add(Instantiate(spawnables[(int)(Random.value * spawnables.Count)], spawnPos, Quaternion.identity, transform));
                    }

                }
                needsRespawn = false;
                yield return new WaitForSeconds(30);
            }
            else if (currentObjects.Count == 0)
            {
                needsRespawn = true;
                yield return new WaitForSeconds(60);
            }

            yield return new WaitForSeconds(30);

        }

    }


}
