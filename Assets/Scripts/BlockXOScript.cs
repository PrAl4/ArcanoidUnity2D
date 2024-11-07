using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlockXOScript : MonoBehaviour
{
    public GameObject text_obj;
    TMP_Text text_component;
    public string hit_to_destroy = "O";
    public int points;
    PlayerScript player_script;
    public GameDataScript game_data;
    GameObject[] blocks;
    int blocks_lenght;

    private void Awake()
    {
        blocks = GameObject.FindGameObjectsWithTag("BlockXO");
        
    }

    void Start()
    {
        if (text_obj != null)
        {
            text_component = text_obj.GetComponent<TMP_Text>();
            text_component.text = hit_to_destroy;
        }
        player_script = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        blocks_lenght = FindObjectsOfType<BlockXOScript>().Length;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(hit_to_destroy == "O")
        {
            hit_to_destroy = "X";
            game_data.destroy_count++;
        }
        else
        {
            hit_to_destroy = "O";
            game_data.destroy_count--;

        }
        if(game_data.destroy_count == blocks.Length)
        {
            for(int i = 0; i < blocks.Length; i++)
            {
                Destroy(blocks[i]);
                player_script.BlockDestroyed(points);
            }
        }
        else if (text_component != null) text_component.text = hit_to_destroy;
    }
}
