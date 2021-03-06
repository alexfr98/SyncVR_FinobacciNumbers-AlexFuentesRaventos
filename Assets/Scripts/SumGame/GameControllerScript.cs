using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameControllerScript : MonoBehaviour
{
    //Button to show the Fibonacci numbers
    public Button clickButton;
    //Text with Fibonacci serie
    public TMP_InputField fibonacciNumbers;

    //Toggle to enable the studying mode
    public Toggle sutdyingMode;
    //Dropdown multiple choice - studying mode
    public TMP_Dropdown dropdownMultipleChoice;
    //Score text - studying mode
    public TextMeshProUGUI scoreText;
    //Audio Source controller
    public AudioSource audioSource;
    private int score = 0;

    private bool studyingModeActivated = false;

    private bool correctChoice = false;
    //List where it will be the Fibonacci numbers
    private List<long> numbers = new List<long> { 0 };

    // Start is called before the first frame update
    void Start()
    {
        //Starting text
        fibonacciNumbers.text = "If you want to know the Fibonacci numbers, CLICK ON THE BUTTON BELOW ";

        //Adding click listener to the button
        clickButton.onClick.AddListener(ShowNumbers);

        //Listener to knw when the toggle is clicked
        sutdyingMode.onValueChanged.AddListener(delegate {
            studyingModeChanged();
        });

        //Listener to know when the player make a choice
        dropdownMultipleChoice.onValueChanged.AddListener(delegate {
            choiceDone();
        });


        //Deactivating the studying mode features
        dropdownMultipleChoice.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("InitialPage");
        }
    }

    private void ShowNumbers()
    {


        if (numbers.Count == 1)
        {
            AudioClip audio = Resources.Load<AudioClip>("Audio/DefaultAudio");
            audioSource.clip = audio;
            fibonacciNumbers.text = "Fibonnaci Number: " + createFibonnaciText(numbers);
            numbers.Add(1);

        }
        else
        {
            if (studyingModeActivated)
            {
                clickButton.enabled = false;
                createOptions();
                scoreText.text = "Score " + score;
                if (correctChoice && numbers.Count > 2)
                {
                    AudioClip audio = Resources.Load<AudioClip>("Audio/AplauseAudio");
                    audioSource.clip = audio;
                }
                else if(!correctChoice && numbers.Count > 2)
                {

                    AudioClip audio = Resources.Load<AudioClip>("Audio/ErrorAudio");
                    audioSource.clip = audio;
                    
                }
                else
                {
                    AudioClip audio = Resources.Load<AudioClip>("Audio/DefaultAudio");
                    audioSource.clip = audio;
                }
                fibonacciNumbers.text = "Fibonnaci Number: " + createFibonnaciText(numbers);
                //Fibonacci: Sum of the two last digits
                long newNumber = numbers[numbers.Count - 1] + numbers[numbers.Count - 2];
                numbers.Add(newNumber);

            }
            else
            {
                fibonacciNumbers.text = "Fibonnaci Number: " + createFibonnaciText(numbers);
                //Fibonacci: Sum of the two last digits
                long newNumber = numbers[numbers.Count - 1] + numbers[numbers.Count - 2];
                numbers.Add(newNumber);
                AudioClip audio = Resources.Load<AudioClip>("Audio/DefaultAudio");
                audioSource.clip = audio;
            }

            

        }
        audioSource.Play();
    }

    private void createOptions()
    {
        dropdownMultipleChoice.gameObject.SetActive(true);
        dropdownMultipleChoice.options.Clear();

        string sum = numbers[numbers.Count - 1].ToString() + " + " + numbers[numbers.Count - 2].ToString() + " =";
        //Creating three options. The first one is the good one, the second one is the good one + random numbre between 0 and 5, and the third one is the same but substracting
        string goodOption = (numbers[numbers.Count - 1] + numbers[numbers.Count - 2]).ToString();
        string firstBadOption = (numbers[numbers.Count - 1] + numbers[numbers.Count - 2] + (int)UnityEngine.Random.Range(0.0f, 5.0f)).ToString();
        string secondBadOption = (numbers[numbers.Count - 1] + numbers[numbers.Count - 2] - (int)UnityEngine.Random.Range(0.0f, 5.0f)).ToString();

        //Adding the options to the dropdown
        List<string> newOptions = new List<string> { goodOption, firstBadOption , secondBadOption };
        var rnd = new System.Random();
        //Shuffling the options
        var randomized = newOptions.OrderBy(item => rnd.Next());

        foreach (string option in randomized)
        {
            dropdownMultipleChoice.options.Add(new TMP_Dropdown.OptionData(option));
        }
        dropdownMultipleChoice.options.Insert(0, new TMP_Dropdown.OptionData(sum));
        dropdownMultipleChoice.value = 0;

    }
    private void studyingModeChanged()
    {
        if (!studyingModeActivated)
        {
            studyingModeActivated = true;
            sutdyingMode.isOn = true;
            numbers = new List<long> { 0 };
            scoreText.gameObject.SetActive(true);
            correctChoice = true;
            clickButton.enabled = true;

        }
        else
        {
            sutdyingMode.isOn = false;
            numbers = new List<long> { 0 };
            dropdownMultipleChoice.gameObject.SetActive(false);
            scoreText.gameObject.SetActive(false);
            studyingModeActivated = false;
            clickButton.enabled = true;
        }
        fibonacciNumbers.text = "If you want to know the Fibonacci numbers, CLICK ON THE BUTTON BELOW ";
    }

    private void choiceDone()
    {

        if(dropdownMultipleChoice.value != 0)
        {
            if (dropdownMultipleChoice.options[dropdownMultipleChoice.value].text == (numbers[numbers.Count - 2] + numbers[numbers.Count - 3]).ToString())
            {
                score++;
                correctChoice = true;
            }
            else
            {
                correctChoice = false;
            }
            dropdownMultipleChoice.gameObject.SetActive(false);
            clickButton.enabled = true;
            
        }

    }
    private string createFibonnaciText(List<long> numbersList)
    {
        String numbersText = "";
        for (int i = 0; i < numbersList.Count; i++)
        {
            numbersText = numbersText + " " + numbersList[i].ToString() + " ";
        }
        return numbersText;
    }
}
