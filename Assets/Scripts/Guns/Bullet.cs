using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public Vector2 fireDir;

    public GameObject ExplosionFx;

    private void Start()
    {
        rb.AddForce(fireDir * speed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ground")
        {
            GameObject explo = Instantiate(ExplosionFx, this.transform.position, this.transform.rotation);
            Destroy(explo, 0.3f);
            Destroy(this.gameObject);
        }
    }
}
