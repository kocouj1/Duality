using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private readonly float step = 0.05f;
    private readonly float maxMove = 0.5f;
    private readonly float zDef = 0f;
    [SerializeField] private KeyCode keyUp;
    [SerializeField] private KeyCode keyDown;
    [SerializeField] private KeyCode keyLeft;
    [SerializeField] private KeyCode keyRight;
    [SerializeField] private string playerName;
    [SerializeField] private GameObject winScreen;
    private bool keyBlocked;
    private bool leftKeyWasPressed;
    private bool rightKeyWasPressed;
    private bool upKeyWasPressed;
    private bool downKeyWasPressed;
    private bool keyPressed;
    private bool moveStarted;
    private float movedSteps;
    private Transform playerTransform;
    private AudioSource playerStep;
    private float playerNameMod;
    private Vector3 originalPos;
    // Start is called before the first frame update
    void Start()
    {
        keyBlocked = false;
        playerTransform = GetComponent<Transform>();
        playerNameMod = (playerName == "jang" ? -1.0f:1.0f);
        playerStep = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!keyBlocked)
        {
            if (Input.GetKeyDown(keyUp))
            {
                upKeyWasPressed = true;
                keyBlocked = true;
            }
            if (Input.GetKeyDown(keyDown))
            {
                downKeyWasPressed = true;
                keyBlocked = true;
            }
            if (Input.GetKey(keyLeft))
            {
                leftKeyWasPressed = true;
                keyBlocked = true;
            }
            if (Input.GetKey(keyRight))
            {
                rightKeyWasPressed = true;
                keyBlocked = true;
            }
            if (upKeyWasPressed || downKeyWasPressed || leftKeyWasPressed || rightKeyWasPressed) { keyPressed = true; }
        }

        
    }
    private void FixedUpdate()
    {
        if (keyPressed)
        {
            if (!moveStarted) { 
                moveStarted = true;
                movedSteps = 0;
                originalPos = new Vector3(playerTransform.position.x, playerTransform.position.y, zDef);
            }
            else
            {
                playerTransform.position = playerTransform.position + new Vector3((rightKeyWasPressed ? playerNameMod * step : zDef) - (leftKeyWasPressed ? playerNameMod * step : zDef), (upKeyWasPressed ? playerNameMod * step : zDef) - (downKeyWasPressed ? playerNameMod * step : zDef));
                movedSteps += step;
                if (playerName == "jin")
                {
                    if (!playerStep.isPlaying)
                    {
                        playerStep.Play();
                    }
                }
                if (PlayerPrefs.GetInt("collision") > 0)
                {
                    playerTransform.position = new Vector3(originalPos.x, originalPos.y, zDef);
                    movedSteps = 9999;
                }
                if (movedSteps >= maxMove)
                {
                    keyPressed = false;
                    upKeyWasPressed = false;
                    downKeyWasPressed = false;
                    leftKeyWasPressed = false;
                    rightKeyWasPressed = false;
                    moveStarted = false;
                    keyBlocked = false;
                    PlayerPrefs.SetInt("collision", PlayerPrefs.GetInt("collision")-1);
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Player collision
            if (winScreen != null)
            {
                winScreen.SetActive(true);
            }
        }
        else
        {
            // Wall collision
            PlayerPrefs.SetInt("collision", 2);
        }
    }
}
