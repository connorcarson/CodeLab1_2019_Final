using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    
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
        
    }
}
