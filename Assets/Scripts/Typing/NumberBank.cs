using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class NumberBank : MonoBehaviour
{
    private List<string> originalWords = new List<string>()
    {
        "1234", "5267", "3345", "6139", "9327", "6743", "2251", "8566"
    };

    private List<string> workingWords = new List<string>();


    private void Awake()
    {
        workingWords.AddRange(originalWords);
        Shuffle(workingWords);
        ConverToLower(workingWords);
    }

    public void ReshuffleWords()
    {
        workingWords.AddRange(originalWords);
        Shuffle(workingWords);
        ConverToLower(workingWords);
    }

    private void Shuffle(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int random = Random.Range(i, list.Count);
            string temporary = list[i];

            list[i] = list[random];
            list[random] = temporary;
        }
    }

    private void ConverToLower(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i] = list[i].ToLower();
        }
    }

    public string GetWord()
    {
        string newWord = string.Empty;
        if (workingWords.Count != 0)
        {
            newWord = workingWords.Last();
            workingWords.Remove(newWord);
        }
        return newWord;
    }
}
