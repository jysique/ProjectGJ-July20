using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuAnimation : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private Button btnBackOptions;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        btnBackOptions.onClick.AddListener(() => CloseOptionsMenu());
    }

    private void OpenOptionsMenu()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).gameObject.name == "btnMusic" && Globals.volume == 0)
                transform.GetChild(i).gameObject.SetActive(false);
            else if(transform.GetChild(i).gameObject.name == "btnNoMusic" && Globals.volume > 0)
                transform.GetChild(i).gameObject.SetActive(false);
            else
                transform.GetChild(i).gameObject.SetActive(true);
        }
        Globals.changeVolume = true;
    }
    private void CloseOptionsMenu()
    {
        Globals.changeVolume = false;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        anim.SetBool("close", true);
    }
    private void EndCloseOptionsMenu()
    {
        optionsMenu.SetActive(false);
        anim.SetBool("close", false);
        Globals.pauseActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
