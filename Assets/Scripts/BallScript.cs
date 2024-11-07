using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{

    public Vector2 ball_initial_force;
    Rigidbody2D rb;
    GameObject player_obj;
    float delta_x;
    AudioSource audio_src;
    public AudioClip hit_sound;
    public AudioClip lose_sound;
    public GameDataScript game_data;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player_obj = GameObject.FindGameObjectWithTag("Player");
        delta_x = transform.position.x;
        audio_src = Camera.main.GetComponent<AudioSource>();
    }


    void Update()
    {
        if (rb.isKinematic)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                rb.isKinematic = false;
                rb.AddForce(ball_initial_force);
            }
            else
            {
                var pos = transform.position;
                pos.x = player_obj.transform.position.x + delta_x;
                transform.position = pos;
            }
        }
        if (!rb.isKinematic && Input.GetKeyDown(KeyCode.J))
        {
            var v = rb.velocity;
            if (Random.Range(0, 2) == 0) v.Set(v.x - 0.1f, v.y + 0.1f);
            else v.Set(v.x + 0.1f, v.y - 0.1f);
            rb.velocity = v;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
 //       if(game_data.sound) audio_src.PlayOneShot(lose_sound, 5);
        Destroy(gameObject);
        player_obj.GetComponent<PlayerScript>().BallDestroyed();
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if(game_data.sound) audio_src.PlayOneShot(hit_sound, 5);
    //}
}
