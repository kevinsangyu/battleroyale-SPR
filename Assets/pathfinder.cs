using Unity.VisualScripting;
using UnityEngine;

public class pathfinder : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //private GameObject preyObj, predObj;
    private string preyTag, predTag;
    //private float preyDist, predDist;
    private SpriteRenderer sr;
    public float speed = 5f;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //(preyObj, preyDist) = findPrey();
        //(predObj, predDist) = findPred();
        //Vector2 direction = preyObj.transform.position * preyDist - predObj.transform.position * predDist;

        //transform.position = Vector2.MoveTowards(this.transform.position, preyObj.transform.position, speed * Time.deltaTime);
    }

    public void editTag(string tag)
    {
        Debug.Log("Edit Tag called.");
        this.tag = tag;
        sr.sprite = Resources.Load<Sprite>("Sprites/" + tag);
        if (tag == "Rock") { this.preyTag = "Scissors"; this.predTag = "Paper"; }
        else if (tag == "Paper") { this.preyTag = "Rock"; this.predTag = "Scissors"; }
        else if (tag == "Scissors") { this.preyTag = "Paper"; this.predTag = "Rock"; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        editTag(predTag);
    }

    (GameObject, float) findPrey()
    {
        GameObject[] preys = GameObject.FindGameObjectsWithTag(preyTag);
        GameObject closest = null;
        float minDistance = float.MaxValue;
        Vector2 currentPosition = transform.position;

        foreach (GameObject prey in preys)
        {
            float distance = Vector3.Distance(currentPosition, prey.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = prey;
            }
        }

        return (closest, minDistance);
    }
    (GameObject, float) findPred()
    {
        GameObject[] preys = GameObject.FindGameObjectsWithTag(predTag);
        GameObject closest = null;
        float minDistance = float.MaxValue;
        Vector2 currentPosition = transform.position;

        foreach (GameObject prey in preys)
        {
            float distance = Vector3.Distance(currentPosition, prey.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = prey;
            }
        }

        return (closest, minDistance);
    }
}
