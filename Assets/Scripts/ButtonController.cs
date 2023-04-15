using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private SpriteRenderer SR;
    public Sprite btnImage;
    public Sprite clickImage;

    public KeyCode pressKey;
    public KeyCode pressKey2;

    public bool isPaused = true;

    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isPaused)
        {
            if (Input.GetKeyDown(pressKey))
            {
                SR.sprite = clickImage;
            }
            if (Input.GetKeyUp(pressKey))
            {
                SR.sprite = btnImage;
            }
            if (Input.GetKeyDown(pressKey2))
            {
                SR.sprite = clickImage;
            }
            if (Input.GetKeyUp(pressKey2))
            {
                SR.sprite = btnImage;
            }
        }
    }
}
