using UnityEngine;

public class playerScript : MonoBehaviour
{
    private Rigidbody2D myRigidBody;
    public int moveSpeed = 5;
    private Vector2 movement;
    private SpriteRenderer sr;
    private Sprite rock_sprite, paper_sprite, scissors_sprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        rock_sprite = Resources.Load<Sprite>("Sprites/rock");
        paper_sprite = Resources.Load<Sprite>("Sprites/paper");
        scissors_sprite = Resources.Load<Sprite>("Sprites/scissors");
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right
        movement.y = Input.GetAxisRaw("Vertical");   // W/S or Up/Down
        movement = movement.normalized; // Normalize to maintain consistent speed
        if (Input.GetKeyDown(KeyCode.R))
        {
            sr.sprite = rock_sprite;
            this.tag = "Rock";
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            sr.sprite = paper_sprite;
            this.tag = "Paper";
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            sr.sprite = scissors_sprite;
            this.tag = "Scissors";
        }
    }

    void FixedUpdate()
    {
        // Calculate new position
        Vector2 newPosition = myRigidBody.position + movement * moveSpeed * Time.fixedDeltaTime;

        // Move the Rigidbody2D to the new position
        myRigidBody.MovePosition(newPosition);
    }
}
