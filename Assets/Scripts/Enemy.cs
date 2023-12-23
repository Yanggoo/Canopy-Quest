using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    public int damage;
    public int health;
    public float flashTime;
    private SpriteRenderer spriteRender;
    private Color oringinalColor;
    public GameObject bloodEffect;
    private PlayerHealth playerHealth;
    public GameObject dropCoin;
    public GameObject floatPoint;

    // Start is called before the first frame update
    public void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        oringinalColor = spriteRender.color;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            Instantiate(dropCoin, transform.position, Quaternion.identity);
        }
    }

    public void TakeDamage(int damageGot)
    {
        GameObject gb = Instantiate(floatPoint, transform.position, Quaternion.identity);
        gb.transform.GetChild(0).GetComponent<TextMesh>().text = damageGot.ToString();
        health -= damageGot;
        FlashColor(flashTime);
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
        GameController.cameraShake.Shake();
    }
    void FlashColor(float time)
    {
        spriteRender.color = Color.red;
        Invoke("ResetColor", time);
    }
    void ResetColor()
    {
        spriteRender.color = oringinalColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            if (playerHealth != null)
            {
                playerHealth.DamagePlayer(damage);
            }
        }
    }
}
