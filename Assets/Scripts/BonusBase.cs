using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BonusBase : MonoBehaviour
{
    public string text = "+100";
    public Color backgroundColor = Color.yellow;
    public Color textColor = Color.black;
    public TMP_Text textComponent;
    private SpriteRenderer spriteRenderer;
    protected BallScript[] ballScripts;
    public PlayerScript player_script;

    public void Start()
    {
        textComponent = GetComponentInChildren<TMP_Text>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        ballScripts = FindObjectsOfType<BallScript>();
        player_script = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        UpdateSpriteAndText();
    }

    public void UpdateSpriteAndText()
    {
        if (spriteRenderer is not null)
        {
            spriteRenderer.color = backgroundColor;
        }

        if (textComponent is not null)
        {
            textComponent.text = text;
            textComponent.color = textColor;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject otherObject = collision.gameObject;
        if (otherObject.CompareTag("Player"))
        {
            BonusActivate();
            Destroy(gameObject);
        }
        else if (otherObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

    public virtual void BonusActivate()
    {
        player_script.game_data.points += 100;
    }

}
