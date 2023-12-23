using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    private Renderer myRender;
    public int numBlinks;
    public float blinkTime;
    private Animator animator;
    public float dieTime;
    private new Rigidbody2D rigidbody2D;
    private PolygonCollider2D polygonCollider2D;
    public float hitBoxCDTime;

    private ScreenFlash screenFlash;
    // Start is called before the first frame update
    void Start()
    {
        myRender = GetComponent<Renderer>();
        animator = GetComponent<Animator>();
        HealthBar.healthMax = health;
        HealthBar.healthCurrent = health;
        screenFlash = GetComponent<ScreenFlash>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        GameController.isGameActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamagePlayer(int damage)
    {
        screenFlash.FlashScreen();
        health -= damage;
        if (health < 0)
            health = 0;
        HealthBar.healthCurrent = health;
        if (health <= 0)
        {
            GameController.isGameActive = false;
            rigidbody2D.velocity = new Vector2(0, 0);
            animator.SetTrigger("Die");
            Invoke("BackToMenu", dieTime);
        }
        BlinkPlayer(numBlinks, blinkTime);
        polygonCollider2D.enabled = false;
        StartCoroutine(ShowPlayerHitBox());
    }

    IEnumerator ShowPlayerHitBox()
    {
        yield return new WaitForSeconds(hitBoxCDTime);
        polygonCollider2D.enabled = true;
    }


    private void BackToMenu()
    {
        SceneManager.LoadScene("Menu");

    }

    void BlinkPlayer(int numBlinks, float seconds)
    {
        StartCoroutine(DoBlinks(numBlinks, seconds));
    }

    IEnumerator DoBlinks(int numBlinks, float seconds)
    {
        for (int i=0; i < numBlinks; i++){
            myRender.enabled = !myRender.enabled;
            yield return new WaitForSeconds(seconds);
        }
        myRender.enabled = true;
    }
}
