using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class BlockScript : MonoBehaviour
{

    public GameObject text_obj;
    public GameObject bonus_prefab;
    TMP_Text text_component;
    public int hit_to_destroy;
    public int points;
    PlayerScript player_script;
    SpriteRenderer spriteRenderer;
    public Color backgroundColor;
    public Color textColor;
    public string text;


    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (text_obj != null)
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
            BlockDestroyToBonus();
            Destroy(gameObject);
            player_script.BlockDestroyed(points);
        }
        else if (text_component != null) text_component.text = hit_to_destroy.ToString();
    }

    private void BlockDestroyToBonus()
    {
        var bonus_obj = Instantiate(bonus_prefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        var bonus = Random.Range(0, 6);
        switch (bonus)
        {
            case 0:
                BonusBase new_bonus = bonus_obj.AddComponent<BonusBase>();
                new_bonus.backgroundColor = backgroundColor;
                new_bonus.textColor = textColor;
                new_bonus.text = text;
                break;
            case 1:
                BonusSlow slow_bonus = bonus_obj.AddComponent<BonusSlow>();
                slow_bonus.backgroundColor = Color.green;
                slow_bonus.textColor = Color.white;
                slow_bonus.text = "Slow";
                break;
            case 2:
                BonusFast fast_bonus = bonus_obj.AddComponent<BonusFast>();
                fast_bonus.backgroundColor = Color.red;
                fast_bonus.textColor = Color.white;
                fast_bonus.text = "Fast";
                break;
            case 3:
                BonusBall ball_bonus = bonus_obj.AddComponent<BonusBall>();
                ball_bonus.backgroundColor = Color.green;
                ball_bonus.textColor = Color.white;
                ball_bonus.text = "Ball";
                break;
            case 4:
                BonusPlus2 plus2_bonus = bonus_obj.AddComponent<BonusPlus2>();
                plus2_bonus.backgroundColor = Color.blue;
                plus2_bonus.textColor = Color.white;
                plus2_bonus.text = "+2 Ball";
                break;
            case 5:
                BonusPlus10 plus10_bonus = bonus_obj.AddComponent<BonusPlus10>();
                plus10_bonus.backgroundColor = Color.blue;
                plus10_bonus.textColor = Color.white;
                plus10_bonus.text = "+10 Ball";
                break;
        }
    }

}
