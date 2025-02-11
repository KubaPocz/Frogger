using UnityEngine;

public class CarController : MonoBehaviour
{
    public float carSpeed = 5;
    private SpriteRenderer spriteRenderer;
    private bool leftSide = false;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (gameObject.transform.position.x < 0)
        {
            leftSide = true;
            spriteRenderer.flipX = leftSide;
        }

    }
    void Update()
    {
        if (leftSide)
        {
            gameObject.transform.position += new Vector3((carSpeed) * Time.deltaTime, 0f, 0);
        }
        else
        {
            gameObject.transform.position -= new Vector3((carSpeed) * Time.deltaTime, 0f, 0f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Kill"))
        {
            Destroy(gameObject);
        }
    }
}
