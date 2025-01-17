﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// This script control the main menu buttons and animations of mainmenu
/// </summary>

public class MainMenuController : MonoBehaviour {

    //we get the ref to our setting button animator
    //we have serialized field because the variable is private and we want to access it from inspector
    [SerializeField]
    private Animator settingButtonAnim;

    //this bool tell wheater the buttons setting button holder are hidden or not 
    private bool hidden;
    //this prevent the user from touching the setting button when the setting button animation is playing
    private bool canTouchSettingButtons;

    //ref to music button and its sprite
    [SerializeField]
    private Button musicBtn;
    [SerializeField]
    private Sprite[] musicBtnSprite;

    private AudioSource clickSound;


    public GameObject Drag, Math, Playbutton;

	// Use this for initialization
	void Start ()
    {
        Drag.SetActive(false);
        Math.SetActive(false);
        //at start we check the music status and the assign the sprite to the music button and vol to game
        if (GameManager.singleton.isMusicOn)
        {
            AudioListener.volume = 1;
            musicBtn.image.sprite = musicBtnSprite[1];
        }
        else
        {
            AudioListener.volume = 0;
            musicBtn.image.sprite = musicBtnSprite[0];
        }

        clickSound = GetComponent<AudioSource>();

        //at start we want this bool to be true
        canTouchSettingButtons = true;
        hidden = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //this is used to control the animation of setting button
    IEnumerator DisableWhilePlayingSettingAnim()
    {
        //is check if button is click
        if (canTouchSettingButtons)
        {
            //we check if the buttons are hidden
            if (hidden)
            {
                //if yes then we play slideIn animation and wait for 1.2 sec and the we again make the button interactable
                canTouchSettingButtons = false;
                settingButtonAnim.Play("SlideIn");
                hidden = false;
                yield return new WaitForSeconds(1.2f);
                canTouchSettingButtons = true;
            }
            else
            {
                canTouchSettingButtons = false;
                settingButtonAnim.Play("SlideOut");
                hidden = true;
                yield return new WaitForSeconds(1.2f);
                canTouchSettingButtons = true;
            }


        }
    }

    //method which we will assign to setting button
    public void SettingBtn()
    {
        //when setting button is clicked we start out coroutine
        StartCoroutine(DisableWhilePlayingSettingAnim());
        clickSound.Play();
    }

    //method which we will assign to play button
    public void PlayButton()
    {
        Playbutton.SetActive(false);
        //Application.LoadLevel("ModeSelector"); // if you are using unity below 5.3 version
        Drag.SetActive(true);
        Math.SetActive(true);
        //SceneManager.LoadScene("ModeSelector");//use this for 5.3 version
        //SceneManager.LoadScene("DragAndDropAnimal");
        
        clickSound.Play();
    }

    public void Drag_()
    {
        SceneManager.LoadScene("DragMenu");
        clickSound.Play();

    }

    public void Math_()
    {
        SceneManager.LoadScene("ModeSelector");
        clickSound.Play();
    }

    //method which we will assign to quit button
    public void QuitButton()
    {
        //SceneManager.LoadScene("GamePlay");
        Application.Quit();
    }

    //method which we will assign to music button
    public void MusicButton()
    {
        //it check the music status wheather its on of not and when we click the button it make is on or off respectively
        clickSound.Play();

        if (GameManager.singleton.isMusicOn)
        {
            AudioListener.volume = 0;
            musicBtn.image.sprite = musicBtnSprite[0];
            GameManager.singleton.isMusicOn = false;
            GameManager.singleton.Save();
        }
        else
        {
            AudioListener.volume = 1;
            musicBtn.image.sprite = musicBtnSprite[1];
            GameManager.singleton.isMusicOn = true;
            GameManager.singleton.Save();
        }
    }

    //method which we will assign to more games button
    public void MoreGameButton()
    {
        //here you can provide link of your other games 
        //Application.OpenURL("other game url address");
        clickSound.Play();
    }

    //method which we will assign to  info button
    public void InfoButton()
    {//this is to provide the info on how to play game
        clickSound.Play();
    }

    //method which we will assign to rate button
    public void RateButton()
    {
        //here provide the link of your game so player can rate it
        //Application.OpenURL("game url address");
        clickSound.Play();
    }






}
