using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeSlashProj : MonoBehaviour
{
    public Animator anim;
    public int damage;

    public float prevX;
    public float prevY;

    // Start is called before the first frame update
    void Start()
    {
        prevX = transform.position.x;
        prevY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % 5 == 0)
        {
            anim.SetFloat("Horizontal", transform.position.x - prevX);
            anim.SetFloat("Vertical", transform.position.y - prevY);

            prevX = transform.position.x;
            prevY = transform.position.y;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") && collision.isTrigger != true)
        {
            Destroy(this.gameObject);
            if ((collision.isTrigger != true && collision.CompareTag("Enemy")) || collision.CompareTag("Ghost"))
            {
                collision.SendMessageUpwards("MagDamage", damage);
            }
        }
    }
}
