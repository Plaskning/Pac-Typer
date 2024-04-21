using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HardTyper : MonoBehaviour
{

    //create word bank
    public HardWordBank wordBank;
    private EnemyMovement enemyMovement;
    [SerializeField] public float attackableRange;
    [SerializeField] bool startTimeToKillTimer;
    [SerializeField] private float timeToKillTimer;
    public TextMeshProUGUI wordOutput;
    [SerializeField] private GameObject effect;
    private string remainingWord = string.Empty;
    private string currentWord = "muffins";

    private GameObject scoreManagerObject;
    private ScoreManager scoreManager;

    private void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        wordOutput = GetComponentInChildren<TextMeshProUGUI>();
        GameObject temp = GameObject.FindGameObjectWithTag("HardWordBank");
        if (temp.TryGetComponent<HardWordBank>(out HardWordBank instancedWordBank))
        {
            wordBank = instancedWordBank;
        }
    }

    private void Start()
    {
        timeToKillTimer = 10;
        wordBank.ReshuffleWords();
        SetCurrentWord();

        scoreManagerObject = GameObject.FindGameObjectWithTag("ScoreManager");
        scoreManager = scoreManagerObject.GetComponent<ScoreManager>();
    }

    private void SetCurrentWord()
    {
        // get bank word
        currentWord = wordBank.GetWord();
        SetRemainingWord(currentWord);
        
    }

    private void SetRemainingWord(string newString)
    {
        remainingWord = newString;
        wordOutput.text = remainingWord;
        if(remainingWord.Length == 0)
        {
            wordBank.ReshuffleWords();
            Debug.Log("Resuffled Words into wordbank");
        }
    }

    private void Update()
    {
        CheckInput();
        if(startTimeToKillTimer && timeToKillTimer > 0)
        {
            timeToKillTimer -= Time.deltaTime;  
        }
    }

    private void CheckInput()
    {
        if (enemyMovement.distance > attackableRange)
        {
            wordOutput.color = Color.white;
            wordOutput.fontSize = .5f;
            return;
        }

        wordOutput.color = Color.green;
        wordOutput.fontSize = .8f;

        if (Input.anyKeyDown)
        {
            string keysPressed = Input.inputString;

            if(keysPressed.Length == 1)
            {
                EnterLetter(keysPressed);
            }
        }
    }

    private void EnterLetter(string typedLetter)
    {
        if (IsCorrectLetter(typedLetter))
        {
            startTimeToKillTimer = true;
            RemoveLetter();

            if (IsWordComplete())
            {
                //Give player points based on timeToKill
                Debug.Log("Grant player 50 base points + " + 5 * ((int)timeToKillTimer) + " bonus points");

                scoreManager.currentScore += (50 + (5 * ((int)timeToKillTimer)));


                Instantiate(effect, wordOutput.transform.position, Quaternion.identity);
                Destroy(gameObject);
                //SetCurrentWord();
            }

        }
    }

    private bool IsCorrectLetter(string letter)
    {
        return remainingWord.IndexOf(letter) == 0;
    }

    private void RemoveLetter()
    {

        string newString = remainingWord.Remove(0, 1);
        SetRemainingWord(newString);
    }

    private bool IsWordComplete()
    {
        return remainingWord.Length == 0;
    }
}
