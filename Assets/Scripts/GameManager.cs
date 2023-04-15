using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    public AudioSource Music;
    public GameObject videoPlayer;
    public GameObject endConfetti;

    bool start = false;

    public BeatScroller BS;

    public static GameManager instance;

    public int currentScore = 0;
    public int scorePerNote = 100;
    public int scorePerGoodNote = 125;
    public int scorePerPerfectNote = 150;

    public int currentMultiplier = 1;
    public int multiplierTracker;
    public int[] multiplierThresholds;

    public Text scoreText;
    public Text multiText;
    public Text comboText;

    public float totalNotes;
    public float normalHits;
    public float goodHits;
    public float perfectHits;
    public float missedHits;

    public float currentCombo;
    public float maxCombo;

    public CameraShake cameraShake;
    public float shakeLength;
    public float shakeMagnitude;

    public GameObject resultsScreen;
    public TextMeshProUGUI normalAmount, goodAmount, perfectAmount, missAmount, percentageHit, maxComboText, finalScore, challengeText;

    public float endGoal;
    private bool hasEnded = false;

    bool isPaused = false;
    public GameObject pauseScreen;

    void Start()
    {
        instance = this;

        totalNotes = FindObjectsOfType<NoteObject>().Length;

        resultsScreen.SetActive(false);

        videoPlayer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!start)
        {
            if (Input.anyKeyDown)
            {
                start = true;
                BS.hasStarted = true;

                Music.Play();
                videoPlayer.SetActive(true);
            }
        } 
        if(hasEnded)
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene("Menu");
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                pauseScreen.SetActive(false);            
                isPaused = false;
                Music.Play();
                BS.isPaused = false;
                videoPlayer.GetComponent<VideoPlayer>().Play();
                foreach (var buttonController in GetComponentsInChildren<ButtonController>())
                {
                    buttonController.isPaused = false;
                }
                foreach (var noteObject in GetComponentsInChildren<NoteObject>())
                {
                    noteObject.isPaused = false;
                }
            } else
            {
                pauseScreen.SetActive(true);
                isPaused = true;
                Music.Pause();
                BS.isPaused = true;
                videoPlayer.GetComponent<VideoPlayer>().Pause();
                foreach (var buttonController in GetComponentsInChildren<ButtonController>())
                {
                    buttonController.isPaused = true;
                }
                foreach (var noteObject in GetComponentsInChildren<NoteObject>())
                {
                    noteObject.isPaused = true;
                }
            }
        }
    }

    public void NoteHit()
    {
        Debug.Log("Hit!");

        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {
            multiplierTracker++;
            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }

        currentCombo++;
        if (currentCombo >= maxCombo + 1)
        {
            maxCombo++;
        }

        multiText.text = $"Multiplier: {currentMultiplier}x";
        //currentScore += scorePerNote * currentMultiplier;
        scoreText.text = "Score: " + currentScore;
        comboText.text = "Current Combo: " + currentCombo;
    }

    public void NormalHit(bool shakeNote)
    {
        currentScore += scorePerNote * currentMultiplier;
        NoteHit();
        if (shakeNote)
        {
            StartCoroutine(cameraShake.Shake(shakeLength, shakeMagnitude));
        }
        normalHits++;
    }
    public void GoodHit(bool shakeNote)
    {
        currentScore += scorePerGoodNote * currentMultiplier;
        NoteHit();
        if (shakeNote)
        {
            StartCoroutine(cameraShake.Shake(shakeLength, shakeMagnitude));
        }
        goodHits++;
    }
    public void PerfectHit(bool shakeNote)
    {
        currentScore += scorePerPerfectNote * currentMultiplier;
        NoteHit();
        if (shakeNote)
        {
            StartCoroutine(cameraShake.Shake(shakeLength, shakeMagnitude));
        }
        perfectHits++;
    }

    public void NoteMiss(bool shakeNote)
    {
        Debug.Log("Miss!");

        if (currentScore >= 50)
        {
            currentScore -= 50;
        }
        
        scoreText.text = "Score: " + currentScore;
        currentMultiplier = 1;
        multiplierTracker = 0;
        multiText.text = $"Multiplier: {currentMultiplier}x";
        missedHits++;
        currentCombo = 0;
        comboText.text = "Current Combo: " + currentCombo;
    }
    
    public void NoNoteHit()
    {
        scoreText.text = "Score: " + currentScore;
        currentMultiplier = 1;
        multiplierTracker = 0;
        multiText.text = $"Multiplier: {currentMultiplier}x";
        missedHits++;
        currentCombo = 0;
        comboText.text = "Current Combo: " + currentCombo;
    }

    public void EndScreen()
    {
        resultsScreen.SetActive(true);
        Instantiate(endConfetti, new Vector2(0, 0), endConfetti.transform.rotation);
        hasEnded = true;

        normalAmount.text = normalHits.ToString();
        goodAmount.text = goodHits.ToString();
        perfectAmount.text = perfectHits.ToString();
        missAmount.text = missedHits.ToString();

        float totalHits = normalHits + goodHits + perfectHits;
        float percentHit = (totalHits / totalNotes) * 100f;

        percentageHit.text = percentHit.ToString("F2") + "%";
        maxComboText.text = maxCombo.ToString();

        finalScore.text = currentScore.ToString();

        if (currentScore >= endGoal)
        {
            challengeText.text = $"You reached {endGoal}, great job!";
        } else
        {
            challengeText.text = $"Try to reach {endGoal}!";
        }
    }
}
