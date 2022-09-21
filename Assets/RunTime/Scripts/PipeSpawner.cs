using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] private Pipe pipeTop;
    [SerializeField] private Pipe pipeBot;

    Vector3 pipeTopPos;
    Vector3 pipeBotPos;

    private float distGap = 1;

    [Range(1.38f, 1.5f)]
    [SerializeField] private float maxDistGap, minDistGap;

    Vector3 maxGapPos;
    
    [SerializeField] private float randomGapPos = 4;

    

    public void SpawnPipes()
    {
        distGap = Random.Range(minDistGap, maxDistGap);

        maxGapPos = transform.position;
        maxGapPos.y += Random.Range(-randomGapPos, randomGapPos);
        transform.position = maxGapPos;

        pipeTopPos = transform.position;
        pipeTopPos.y += distGap - pipeTop.HeadPos.y - pipeTop.transform.position.y;
        Instantiate(pipeTop, pipeTopPos, Quaternion.identity, transform);

        pipeBotPos = transform.position;
        pipeBotPos.y -= distGap + pipeBot.HeadPos.y + pipeBot.transform.position.y;
        Instantiate(pipeBot, pipeBotPos, Quaternion.identity, transform);
    }

    private void OnDrawGizmos()
    {

        Gizmos.DrawCube(transform.position, Vector3.one * 0.3f);
        Gizmos.DrawLine(transform.position + (Vector3.up*distGap), transform.position - (Vector3.up*distGap));
    }
}
