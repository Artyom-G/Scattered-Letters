using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class love_letter : MonoBehaviour
{
    public GameObject pop_effect;
    public GameObject canvas;
    public TMP_Text text;
    [TextAreaAttribute]
    public string message;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D _col){
        if(_col.gameObject.tag == "Player"){
            GameObject _pop = Instantiate(pop_effect);
            _pop.transform.position = gameObject.transform.position;
            canvas.SetActive(true);
            text.SetText(message);
            Destroy(gameObject);
        }
    }
}
