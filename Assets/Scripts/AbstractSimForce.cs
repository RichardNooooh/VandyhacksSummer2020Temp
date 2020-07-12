using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class AbstractSimForce : MonoBehaviour
{
    public GameObject o1;
    public GameObject o2;

    protected Rigidbody2D rb1;
    protected Rigidbody2D rb2;

    public virtual Vector2 getForce() { return Vector2.zero; }

    // Start is called before the first frame update
    void Start()
    {
        rb1 = o1.GetComponent<Rigidbody2D>();
        rb2 = o2.GetComponent<Rigidbody2D>();
    }
}
