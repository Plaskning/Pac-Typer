using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Typer : MonoBehaviour
{

    //create word bank
    public WordBank wordBank;
    private EnemyMovement enemyMovement;
    [SerializeField] private float attackableRange;
    public TextMeshProUGUI wordOutput = null;
    [SerializeField] private GameObject effect;
    private string remainingWord = string.Empty;
    private string currentWord = "muffins";

    private void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        GameObject temp = GameObject.FindGameObjectWithTag("WordBank");
        if(temp.TryGetComponent<WordBank>(out WordBank instancedWordBank))
        {
            wordBank = instancedWordBank;
        }
        SetCurrentWord();
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
        Debug.Log(remainingWord);
        if(remainingWord.Length == 0)
        {
            Debug.Log("ran out of words");
        }
    }

    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (enemyMovement.distance > attackableRange)
            return;

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
            RemoveLetter();

            if (IsWordComplete())
            {
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
