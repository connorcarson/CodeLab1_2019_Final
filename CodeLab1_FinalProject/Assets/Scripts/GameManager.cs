using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreText;

    public GameObject winPanel;
    public GameObject winText;
    public GameObject yesButton;
    public GameObject noButton;
    
    private int cats = 0;

    public int Cats
    {
        get
        {
            return cats; 
            
        }
        set
        {
            cats = value;
            scoreText.text = cats + " of 12";
        }
    }

    public static GameManager instance;
    
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Cats >= 12)
        {
            winPanel.SetActive(true);
            winText.SetActive(true);
            yesButton.SetActive(true);
            noButton.SetActive(true);
        }

        if (Input.GetKey(KeyCode.C))
        {
            Cats = 12;
        }
    }

    public void PlayAgain()
    {
        Destroy(gameObject);
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
        
        print("In an actual build, this will call Application.Quit and quit the game.");
    }
}
