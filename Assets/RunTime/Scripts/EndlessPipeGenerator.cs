using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessPipeGenerator : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private Camera mainCamera;

    [SerializeField] private PipeSpawner pipeSpawnerPrefab;

    [SerializeField] private float distForFirstPipe = 5;
    [SerializeField] private float distBetweenPipes = 3;
    [SerializeField] private int minPipesInFront = 2;

    [SerializeField] private Ground[] grounds;

    private List<PipeSpawner> pipes = new List<PipeSpawner>();

    private void Awake()
    {
        SpawnFirstPipe(Vector2.right * distForFirstPipe);
    }

    private void Update()
    {
        UpdatePipes();
        UpdateGround();

    }

    private void SpawnEndlessPipes(int pipeCount)
    {

        if(pipes.Count < 1)
        {
            Debug.Log("Expected at least one pipe to start from");
        }

        PipeSpawner lastPipe = pipes[pipes.Count - 1];
        for (int i = 0; i < pipeCount; i++)
        {
            Vector2 pipePos = lastPipe.transform.position + Vector3.right * distBetweenPipes;
            lastPipe = SpawnFirstPipe(pipePos);
        }
    }

    private void UpdateGround()
    {
        int lastIndex = grounds.Length - 1;
        for ( int i = lastIndex; i >= 0; i--)
        {
            Ground ground = grounds[i];

            if (player.transform.position.x > ground.Sprite.bounds.min.x &&
                !IsBoxVisibleX(ground.Sprite.bounds.center, ground.Sprite.bounds.size.x))
            {
                Ground lasGround = grounds[lastIndex];
                ground.transform.position = lasGround.transform.position + Vector3.right * ground.Sprite.bounds.size.x;
                grounds[i] = lasGround;
                grounds[lastIndex] = ground;
            }
        }
    }

    private void UpdatePipes()
    {
        if (pipes.Count > 0)
        {
            PipeSpawner lastPipe = pipes[pipes.Count - 1];
            if (lastPipe.transform.position.x < player.transform.position.x)
            {
                SpawnEndlessPipes(minPipesInFront);
            }

            int lastIndexToRemove = FindLastPipeIndexToRemove();
            if (lastIndexToRemove >= 0)
            {
                for (int i = 0; i <= lastIndexToRemove; i++)
                {
                    Destroy(pipes[i].gameObject);
                }
                pipes.RemoveRange(0, lastIndexToRemove + 1);
            }

        }
    }

    private PipeSpawner SpawnFirstPipe(Vector2 position)
    {
        PipeSpawner pipe = Instantiate(pipeSpawnerPrefab, position, Quaternion.identity, transform);
        pipe.SpawnPipes();
        pipes.Add(pipe);
        return pipe;
    }

    private int FindLastPipeIndexToRemove()
    {
        for (int i = pipes.Count - 1; i >= 0; i--)
        {
            PipeSpawner pipe = pipes[i];
            if (pipe.transform.position.x < player.transform.position.x && !isPipeVisible(pipe))
            {
                return i;
            }            
        }

        return -1;
    }

    private bool isPipeVisible(PipeSpawner pipe)
    {
        return IsBoxVisibleX(pipe.transform.position, 1f);
    }

    private bool IsBoxVisibleX(Vector3 center, float width)
    {
        Vector3 left = center - Vector3.right * width * 0.5f;
        Vector3 right = center + Vector3.right * width * 0.5f;

        Vector3 leftCamPos = mainCamera.WorldToViewportPoint(left);
        Vector3 rightCamPos = mainCamera.WorldToViewportPoint(right);

        return !(leftCamPos.x > 1 || rightCamPos.x < 0);
    }
}
