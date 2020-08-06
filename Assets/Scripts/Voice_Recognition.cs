using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.UI;

public class Voice_Recognition : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    public Image redScore;
    public Image blueScore;

    public Sprite[] numbers;
    private int redIndex = 0;
    private int blueIndex = 0;
    public Button button;

    void Start()
    {
        actions.Add("punto rosso", AddRedPoint);
        actions.Add("punto blu", AddBluePoint);
        actions.Add("leva rosso", SubtractRedPoint);
        actions.Add("leva blu", SubtractBluePoint);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();

        redScore.sprite = numbers[redIndex];
        redScore.color = Color.red;
        blueScore.sprite = numbers[blueIndex];
        blueScore.color = Color.blue;
    }
    
    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    private void AddRedPoint()
    {
        redIndex++;
        redIndex = Mathf.Clamp(redIndex, 0, 9);
        redScore.sprite = numbers[redIndex];
    }

    private void AddBluePoint()
    {
        blueIndex++;
        blueIndex = Mathf.Clamp(blueIndex, 0, 9);
        blueScore.sprite = numbers[blueIndex];
    }

    private void SubtractRedPoint()
    {
        redIndex--;
        redIndex = Mathf.Clamp(redIndex, 0, 9);
        redScore.sprite = numbers[redIndex];
    }

    private void SubtractBluePoint()
    {
        blueIndex--;
        blueIndex = Mathf.Clamp(blueIndex, 0, 9);
        blueScore.sprite = numbers[blueIndex];
    }

    public void ResetScores()
    {
        redIndex = 0;
        blueIndex = 0;

        redScore.sprite = numbers[redIndex];
        blueScore.sprite = numbers[blueIndex];
    }
}