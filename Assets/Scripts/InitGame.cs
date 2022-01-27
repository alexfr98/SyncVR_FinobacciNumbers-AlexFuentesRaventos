using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InitGame : MonoBehaviour
{
    public GameObject initPage;
    public GameObject instructionsPage;

    private Button sumButton;
    private Button substractButton;

    private Button backButton;
    private Button instructionsButton;

    // Start is called before the first frame update
    void Start()
    {
        sumButton = initPage.transform.GetChild(2).gameObject.GetComponent<Button>();
        //Adding click listener to the sum option button
        sumButton.onClick.AddListener(goSumGame);

        substractButton = initPage.transform.GetChild(1).gameObject.GetComponent<Button>();
        //Adding click listener to the substract option button
        substractButton.onClick.AddListener(goSubstractGame);

        instructionsButton = initPage.transform.GetChild(4).gameObject.GetComponent<Button>();
        //Adding click listener to the substract option button
        instructionsButton.onClick.AddListener(showInstructions);

        backButton = instructionsPage.transform.GetChild(1).gameObject.GetComponent<Button>();
        backButton.onClick.AddListener(hideInstructions);

        instructionsPage.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void goSumGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SumScene");
    }

    void goSubstractGame() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SubstractScene");
    }

    void hideInstructions()
    {
        initPage.SetActive(true);
        instructionsPage.SetActive(false);
    }

    void showInstructions()
    {
        instructionsPage.SetActive(true); ;
        initPage.SetActive(false);
    }
}
