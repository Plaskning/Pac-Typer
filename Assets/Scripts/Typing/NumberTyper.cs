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
    [SerializeField] Vector3 OpenPosition = new Vector3(0, -1.5f, 0);
    [SerializeField] Vector3 ClosedPosition = new Vector3(0, 0, 0);
    private bool isOpen = false;

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
                MoveDoor();
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

    private void MoveDoor()
    {
        if (isOpen)
            return;
        isOpen = true;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + ClosedPosition, 1f);
        StartCoroutine(CloseDoorAfterTime());
    }

    private IEnumerator CloseDoorAfterTime()
    {
        yield return new WaitForSeconds(4f);
        transform.position = Vector3.MoveTowards(transform.position, transform.position + OpenPosition, 1f);
        yield return new WaitForSeconds(1f);
        NumberBank.ReshuffleWords();
        SetCurrentWord();
        isOpen = false;
    }
}
