using Unity.VisualScripting;
using UnityEditorInternal.VR;
using UnityEngine;

public class pathfinder : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private GameObject preyObj, predObj;
    private string preyTag, predTag;
    private float preyDist, predDist;
    private SpriteRenderer sr;
    public float speed = 5f;
    public float fear_factor;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        (preyObj, preyDist) = findPrey();
        (predObj, predDist) = findPred();
        //if (preyObj != null) { Debug.DrawLine(transform.position, preyObj.transform.position, Color.green); }
        //if (predObj != null) { Debug.DrawLine(transform.position, predObj.transform.position, Color.red); }
        // There is no empty Vector value. Vector2.zero points to the center for some reason.
        // I would like to use an empty Vector value so I can just do prey_dir + pred_dir * fear_factor, but I can't.
        // Instead I have to account for each possibility.

        if (preyObj != null && predObj != null)
        {
            Vector2 prey_dir = preyObj.transform.position - transform.position;
            Vector2 pred_dir = (predObj.transform.position - transform.position) * -1;
            Vector2 direction = (prey_dir + pred_dir);
            transform.position = Vector2.MoveTowards(transform.position, direction, speed * Time.deltaTime);
        }
        else if (preyObj == null && predObj != null)
        {
            Vector2 pred_dir = predObj.transform.position;
            Vector2 direction = (pred_dir) * -1;
            transform.position = Vector2.MoveTowards(transform.position, direction, speed / 2 * Time.deltaTime); // penalty to running away
        }
        else if (predObj == null && preyObj != null)
        {
            Vector2 prey_dir = preyObj.transform.position;
            Vector2 direction = (prey_dir);
            transform.position = Vector2.MoveTowards(transform.position, direction, speed * Time.deltaTime);
        }
    }

    public void editTag(string tag)
    {
        this.tag = tag;
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = Resources.Load<Sprite>("Sprites/" + tag);
        if (tag == "Rock") { this.preyTag = "Scissors"; this.predTag = "Paper"; }
        else if (tag == "Paper") { this.preyTag = "Rock"; this.predTag = "Scissors"; }
        else if (tag == "Scissors") { this.preyTag = "Paper"; this.predTag = "Rock"; }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == this.predTag) { editTag(predTag); }
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
