using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float range;
    [SerializeField] BoxCollider2D boxCollider; // Drag the BoxCollider2D here

    Vector2 waypoint;

    // Start is called before the first frame update
    void Start()
    {
        SetNewDestination();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, waypoint, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, waypoint) < range)
        {
            SetNewDestination();
        }
    }

    void SetNewDestination()
    {
        Bounds bounds = boxCollider.bounds;
        Vector2 min = bounds.min;
        Vector2 max = bounds.max;

        waypoint = new Vector2(
            Random.Range(min.x, max.x),
            Random.Range(min.y, max.y)
        );
    }

    // This method is called when the collider attached to the GameObject collides with another collider
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("TrawlerArm"))
        {
            Debug.Log("Hit Obstacle");

            // probs playing an anim here

            // Exit the game
            Application.Quit();

            // If running in the editor, also stop playing
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
