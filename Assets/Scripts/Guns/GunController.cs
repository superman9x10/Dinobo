using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Animator animator;
    public Transform firePoint;

    public GameObject bullet;
    public float fireRate;
    public float pushForce;
    
    private float canShootAfter;
    private Vector2 gunPos;

    private bool canPushPlayer;
    private void Update()
    {
        rotateGun(); 
        shootingHandle();
    }

    void shootingHandle()
    {
        if(Input.GetMouseButton(0) && Time.time > canShootAfter)
        {
            animator.SetBool("isShooting", true);

            shooting();

            canShootAfter = Time.time + 1 / fireRate;
        } else
        {
            animator.SetBool("isShooting", false);
        }
    }

    void shooting()
    {
        GameObject bul = Instantiate(bullet, firePoint.position, firePoint.rotation);

        if(canPushPlayer)
        {
            this.GetComponentInParent<Rigidbody2D>().velocity = Vector2.up * pushForce;
        }

        bul.GetComponent<Bullet>().fireDir = firePoint.right;
        Destroy(bul, 2f);
    }
    void rotateGun()
    {
        gunPos = transform.position;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 lookDir = (mousePos - gunPos).normalized;

        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        
        if(angle >= -130f && angle <= -50f)
        {
            canPushPlayer = true;
        } else
        {
            canPushPlayer = false;
        }

        transform.rotation = Quaternion.Euler(0f, 0f, angle);


        if(Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(new Vector3(180f, 0f, -angle));
        }

    }
}
