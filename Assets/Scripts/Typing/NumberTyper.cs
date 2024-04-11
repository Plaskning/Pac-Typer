using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberTyper : MonoBehaviour
{
    //create word bank
    public NumberBank NumberBank;
    private EnemyMovement enemyMovement;
    [SerializeField] public float attackableRange;
    [SerializeField] bool startTimeToKillTimer;
    public TextMeshProUGUI wordOutput;
    [SerializeField] private GameObject effect;
    private string remainingWord = string.Empty;
    private string currentWord = "muffins";

    private void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        wordOutput = GetComponentInChildren<TextMeshProUGUI>();
        GameObject temp = GameObject.FindGameObjectWithTag("WordBank");
        if (temp.TryGetComponent<NumberBank>(out NumberBank instancedNumberBank))
        {
            NumberBank = instancedNumberBank;
        }
    }

    private void Start()
    {
        NumberBank.ReshuffleWords();
        SetCurrentWord();
    }

    private void SetCurrentWord()
    {
        // get bank word
        currentWord = NumberBank.GetWord();
        SetRemainingWord(currentWord);

    }

    private void SetRemainingWord(string newString)
    {
        remainingWord = newString;
        wordOutput.text = remainingWord;
        if (remainingWord.Length == 0)
        {
            NumberBank.ReshuffleWords();
            Debug.Log("Resuffled Words into wordbank");
        }
    }

    private void Update()
    {
        CheckInput();
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

            if (keysPressed.Length == 1)
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
                //move to open state

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
