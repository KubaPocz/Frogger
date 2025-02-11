using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManager;
    private Vector3 playerLastPosition;
    float vertical;
    float horizontal;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerLastPosition = gameObject.transform.position;
    }

    void Update()
    {
        if (Time.timeScale == 1.0f)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                playerLastPosition = gameObject.transform.position;
                gameObject.transform.position += new Vector3(0, 1);
                gameManager.score -= 100;
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                playerLastPosition = gameObject.transform.position;
                gameObject.transform.position += new Vector3(0, -1);
                gameManager.score -= 100;
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                playerLastPosition = gameObject.transform.position;
                gameObject.transform.position += new Vector3(-1, 0);
                gameManager.score -= 100;
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                playerLastPosition = gameObject.transform.position;
                gameObject.transform.position += new Vector3(1, 0);
                gameManager.score -= 100;
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                gameManager.ToMenu();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Win")
        {
            gameManager.Win();
        }
        else if (collision.gameObject.tag == "Border")
        {
            gameObject.transform.position = playerLastPosition;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Car")
        {
            StartCoroutine(gameManager.Death());
        }
    }
}
