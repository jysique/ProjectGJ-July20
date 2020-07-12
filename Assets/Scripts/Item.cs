using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
public class Item : MonoBehaviour
{
    //private Button button;
    public LayerMask playerMask;
    // Start is called before the first frame update
    void Start()
    {
        //button = GetComponent<Button>();
        //button.onClick.AddListener(()=>OnClickItem());
    }

    // Update is called once per frame
    void Update()
    {
        //checkRadius();
    }

    public void OnClickItem(){
        
    }

    public void checkRadius() {
        //Collider[] Players = Physics.OverlapSphere(transform.position, 1.5f, playerMask);
        if (Physics.CheckSphere(transform.position, 1.5f, playerMask)) {
            Destroy(this.gameObject);
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 1.5f);
    }

}
