using UnityEngine;

public class BouncingObject : MonoBehaviour
{
    public Vector2 initialVelocity = new Vector2(5f, 5f);

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = initialVelocity;
    }
}
