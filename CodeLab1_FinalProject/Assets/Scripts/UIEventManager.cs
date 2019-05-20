using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIEventManager : MonoBehaviour
{
    public Animator anim;
    
    public float fadeMultiplier;
    
    private Text _title;
    private Text _instructions;

    private bool _gameStarted = true;
    private bool _readInstructions;
    
    // Start is called before the first frame update
    void Start()
    {
        _title = transform.GetChild(1).GetComponent<Text>();
        _instructions = transform.GetChild(2).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameStarted)
        {
            StartCoroutine(FadeToFullAlpha(fadeMultiplier, _title));
            anim.SetBool("PressEnterActive", true);
            _gameStarted = false;
        }

        if (Input.anyKey)
        {
            if (_readInstructions == false)
            {
                StartCoroutine(FadeTo0Alpha(fadeMultiplier, _title));
                StartCoroutine(FadeToFullAlpha(fadeMultiplier, _instructions));
            }

            if (_readInstructions)
            {
                SceneManager.LoadScene(1);
            }
        }
    }

    IEnumerator FadeToFullAlpha(float t, Text currentText)
    {
        currentText.color = new Color(currentText.color.r, currentText.color.g, currentText.color.b, 0);
        while (currentText.color.a < 1f)
        {
            currentText.color = new Color(currentText.color.r, currentText.color.g, currentText.color.b, currentText.color.a + Time.deltaTime/t);
            yield return null;
        }
    }
    
    IEnumerator FadeTo0Alpha(float t, Text currentText)
    {
        currentText.color = new Color(currentText.color.r, currentText.color.g, currentText.color.b, 1);
        while (currentText.color.a > 0f)
        {
            currentText.color = new Color(currentText.color.r, currentText.color.g, currentText.color.b, currentText.color.a - Time.deltaTime/t);
            
        }
        yield return new WaitForSeconds(2f);
        
        _readInstructions = true;
    }
}
