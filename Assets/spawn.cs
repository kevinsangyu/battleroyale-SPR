using System;
using System.IO.Pipes;
using UnityEngine;

public class spawn : MonoBehaviour
{
    public GameObject NPC;
    public float minBoundsx;
    public float maxBoundsx;
    public float minBoundsy;
    public float maxBoundsy;
    public int amount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnNPCs();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void spawnNPCs()
    {
        string[] tags = new string[] { "Rock", "Scissors", "Paper" };
        foreach (string tag in tags)
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject clone = Instantiate(NPC, GetRandomPosition(), NPC.transform.rotation);
                pathfinder pathfinderScript = clone.GetComponent<pathfinder>();
                pathfinderScript.editTag(tag);
            }
        }
        //GameObject clone = Instantiate(NPC, GetRandomPosition(), NPC.transform.rotation);
        //pathfinder pathfinderScript = clone.GetComponent<pathfinder>();
        //pathfinderScript.editTag("Rock");
    }

    Vector2 GetRandomPosition()
    {
        float randomX = UnityEngine.Random.Range(minBoundsx, maxBoundsx);
        float randomY = UnityEngine.Random.Range(minBoundsy, maxBoundsy);
        return new Vector2(randomX, randomY);
    }
}
