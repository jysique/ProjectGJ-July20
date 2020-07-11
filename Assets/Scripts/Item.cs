using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Item : MonoBehaviour
{
    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(()=>OnClickItem());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickItem(){
        
    }

}
