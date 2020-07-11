using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsMenuAnimation : MonoBehaviour
{
    [SerializeField] private GameObject creditsMenu;
    [SerializeField] private Button btnBackCredits;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        btnBackCredits.onClick.AddListener(() => CloseCreditsMenu());
    }

    private void OpenCreditsMenu()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }
    private void CloseCreditsMenu()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        anim.SetBool("close", true);
    }
    private void EndCloseCreditsMenu()
    {
        creditsMenu.SetActive(false);
        anim.SetBool("close", false);
        Globals.pauseActive = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
