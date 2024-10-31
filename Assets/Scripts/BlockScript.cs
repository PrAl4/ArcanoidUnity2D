using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class BlockScript : MonoBehaviour
{

    public GameObject text_obj;
    TMP_Text text_component;
    public int hit_to_destroy;
    public int points;
    PlayerScript player_script;


    void Start()
    {
        if(text_obj != null)
        {
            text_component = text_obj.GetComponent<TMP_Text>();
            text_component.text = hit_to_destroy.ToString();
        }
        player_script = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hit_to_destroy--;
        if (hit_to_destroy == 0)
        {
            Destroy(gameObject);
            player_script.BlockDestroyed(points);
        }
        else if (text_component != null) text_component.text = hit_to_destroy.ToString();
    }

}
