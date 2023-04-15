using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;
    public KeyCode pressKey;
    public KeyCode pressKey2;
    private bool obtained = false;

    public GameObject hitEffect, okEffect, goodEffect, perfectEffect, missEffect;

    public CameraShake cameraShake;
    public bool shakeNoteBool = false;

    public bool isPaused = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(pressKey) || Input.GetKeyDown(pressKey2))
        {
            if(canBePressed && !isPaused)
            {
                Instantiate(hitEffect, new Vector2(transform.position.x, transform.position.y), hitEffect.transform.rotation);

                if (transform.position.x < -7.25 || transform.position.x > -6.75)
                {
                    GameManager.instance.NormalHit(shakeNoteBool);
                    Instantiate(okEffect, new Vector2(transform.position.x + 2, transform.position.y + 1), okEffect.transform.rotation);
                }
                else if (transform.position.x < -7.05 || transform.position.x > -6.95)
                {
                    GameManager.instance.GoodHit(shakeNoteBool);
                    Instantiate(goodEffect, new Vector2(transform.position.x + 2, transform.position.y + 1), goodEffect.transform.rotation);
                } else
                {
                    GameManager.instance.PerfectHit(shakeNoteBool);
                    Instantiate(perfectEffect, new Vector2(transform.position.x + 2, transform.position.y + 1), perfectEffect.transform.rotation);
                }

                obtained = true;
                gameObject.SetActive(false);
            } 
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Activator")
        {
            canBePressed = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canBePressed = false;

            if (!obtained)
            {
                GameManager.instance.NoteMiss(shakeNoteBool);
                Instantiate(missEffect, new Vector2(transform.position.x + 2.5f, transform.position.y + 1), missEffect.transform.rotation);
            }
        }
    }
}
