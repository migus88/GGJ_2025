using UnityEngine;

public class BouncingObject : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(Random.Range(-50f,50f), Random.Range(-50f,50f));
    }
}
