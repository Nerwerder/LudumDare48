using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkPointer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = transform.gameObject.GetComponent<SpriteRenderer>();
    }


    // Update is called once per frame
    void Update()
    {

        Vector3 targetPos = NearestClusterPos();


        Vector3 currentPos = Camera.main.transform.position;
        currentPos.z = 0.0f;
        Vector3 dir = (targetPos - currentPos).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (angle < 0)
            angle += 360;
        transform.localEulerAngles = new Vector3(0, 0, angle);

        float borderSize = 100.0f;
        Vector3 targetPosScreenPoint = Camera.main.WorldToScreenPoint(targetPos);
        bool isOffScreen = targetPosScreenPoint.x <= borderSize || targetPosScreenPoint.x >= Screen.width - borderSize || targetPosScreenPoint.y <= borderSize || targetPosScreenPoint.y >= Screen.height - borderSize;

        //Debug.Log(isOffScreen + " "+ targetPosScreenPoint);

        if (isOffScreen)
        {
            if (!spriteRenderer.enabled)
                spriteRenderer.enabled = true;

            Vector3 cappedTargetScreenPos = targetPosScreenPoint;
            if (cappedTargetScreenPos.x <= borderSize) cappedTargetScreenPos.x = borderSize;
            if (cappedTargetScreenPos.x >= Screen.width - borderSize) cappedTargetScreenPos.x = Screen.width - borderSize;
            if (cappedTargetScreenPos.y <= borderSize) cappedTargetScreenPos.y = borderSize;
            if (cappedTargetScreenPos.y >= Screen.height - borderSize) cappedTargetScreenPos.y = Screen.height - borderSize;

            Vector3 pointerWorldPos = Camera.main.ScreenToWorldPoint(cappedTargetScreenPos);
            transform.position = pointerWorldPos;
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0.0f);
        }
        else
        {
            if (spriteRenderer.enabled)
                spriteRenderer.enabled = false;
        }


    }


    Vector3 NearestClusterPos()
    {
        GameObject[] clusters = GameObject.FindGameObjectsWithTag("Cluster");

        float currentDistance = float.MaxValue;
        GameObject nearestCluster = null;

        foreach (GameObject cluster in clusters)
        {
            if (cluster.GetComponent<Spawner>().currentObjects.Count == 0)
                continue;
            float distance = Vector3.Distance(transform.position, cluster.transform.position);
            if (distance < currentDistance)
            {
                nearestCluster = cluster;
                currentDistance = distance;
            }
        }

        return nearestCluster.transform.position;
    }
}

