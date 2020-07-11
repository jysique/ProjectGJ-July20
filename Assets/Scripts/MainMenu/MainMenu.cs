using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject creditsMenu;
    [SerializeField] private Slider slider;
    [Header("Main Menu")]
    public Button btnNewAdventure;
    public Button btnOptions;
    public Button btnCredits;
    public Button btnExit;
    [Header("Menus")]
    [SerializeField] private Button btnMusic;
    [SerializeField] private Button btnNoMusic;

    private float tempVolume;
    // Start is called before the first frame update
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        slider.value = Globals.volume;
    }
    void Start()
    {
        optionsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        btnNewAdventure.onClick.AddListener(()=> NewAdventure());
        btnOptions.onClick.AddListener(()=> Options());
        btnCredits.onClick.AddListener(()=> Credits());
        btnExit.onClick.AddListener(() => Exit());
        btnMusic.onClick.AddListener(() => DesactivateMusic());
        btnNoMusic.onClick.AddListener(() => ActivateMusic());
        ////
        Globals.volume = slider.value;
        audioSource.volume = Globals.volume;
    }
    public void DesactivateMusic()
    {
        tempVolume = slider.value;
        slider.value = 0;
    }

    public void ActivateMusic()
    {
        slider.value = tempVolume;
    }
    
    public void NewAdventure()
    {
        if (!Globals.pauseActive)
            SceneManager.LoadScene("koala");
    }
    public void Options()
    {
        if (!Globals.pauseActive)
        {
            Globals.pauseActive = true;
            optionsMenu.SetActive(true);
        }
    }
    public void Credits()
    {
        if (!Globals.pauseActive)
        {
            Globals.pauseActive = true;
            creditsMenu.SetActive(true);
        }
    }
    public void Exit()
    {
        //SceneManager.LoadScene("Splash Screen");

        if (!Globals.pauseActive)
            Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        if (Globals.changeVolume)
        {
            Globals.volume = slider.value;
            audioSource.volume = Globals.volume;
            if (audioSource.volume > 0f && audioSource.volume <= 1f)
            {
                btnMusic.gameObject.SetActive(true);
                btnNoMusic.gameObject.SetActive(false);
            }
            else if (audioSource.volume == 0f)
            {
                btnMusic.gameObject.SetActive(false);
                btnNoMusic.gameObject.SetActive(true);
            }
        }
    }
}
