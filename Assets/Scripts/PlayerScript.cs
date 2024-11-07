using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    const int max_level = 30;
    [Range(1, max_level)]
    public int level = 1;
    public float ball_velocity_mult = 0.02f;
    public GameObject blue_pref;
    public GameObject red_pref;
    public GameObject green_pref;
    public GameObject yellow_pref;
    public GameObject ball_pref;
    public GameObject XO_pref;
    static Collider2D[] colliders = new Collider2D[50];
    static ContactFilter2D contact_filter = new ContactFilter2D();
    public GameDataScript game_data;
    static bool game_started = false;
    public AudioClip point_sound;

    AudioSource sfxaudio_src;
    AudioSource musicaudio_src;

    int requiredPointsYoBall { get { return 400 + (level - 1) * 20; } }


    void Start()
    {
        musicaudio_src = Camera.main.GetComponent<AudioSource>();
        sfxaudio_src = GetComponent<AudioSource>();
        Cursor.visible = false;
        if (!game_started)
        {
            game_started = true;
            if (game_data.rest_on_start) game_data.Load();
        }
        level = game_data.level;
        SetMusic();
        StartLevel();
    }


    void Update()
    {
        if (Time.timeScale > 0)
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var pos = transform.position;
            pos.x = mousePos.x;
            transform.position = pos;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            game_data.music = !game_data.music;
            SetMusic();
        }
        if (Input.GetKeyDown(KeyCode.S)) game_data.sound = !game_data.sound;
        if (Input.GetButtonDown("Pause"))
        {
            if (Time.timeScale > 0) Time.timeScale = 0;
            else Time.timeScale = 1;
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            game_data.Reset();
            SceneManager.LoadScene("MainScene");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }

    void CreateBlocks(GameObject pref, float x_max, float y_max, int count, int max_count)
    {
        if (count > max_count) count = max_count;
        for(int i = 0; i < count; i++)
        {
            for(int k = 0; k < 20; k++)
            {
                var obj = Instantiate(pref, new Vector3((Random.value * 2 - 1) * x_max, Random.value * y_max, 0), Quaternion.identity);
                if (obj.GetComponent<Collider2D>().OverlapCollider(contact_filter.NoFilter(), colliders) == 0) break;
                Destroy(obj);
            }
            
        }
    }

    void CreateBalls()
    {
        //count = 2 ??
        int count = 1;
        if (game_data.balls == 1) count = 1;
        for (int i = 0; i < count; i++)
        {
            var obj = Instantiate(ball_pref);
            var ball = obj.GetComponent<BallScript>();
            ball.ball_initial_force += new Vector2(10 * i, 0);
            ball.ball_initial_force *= 1 + level * ball_velocity_mult;
        }
    }

    void StartLevel()
    {
        SetBackground();
        var y_max = Camera.main.orthographicSize * 0.8f;
        var x_max = Camera.main.orthographicSize * Camera.main.aspect * 0.85f;
        CreateBlocks(blue_pref, x_max, y_max, level, 8);
        CreateBlocks(red_pref, x_max, y_max, 1 + level, 10);
        CreateBlocks(green_pref, x_max, y_max, 1 + level, 12);
        CreateBlocks(yellow_pref, x_max, y_max, 2 + level, 15);
        CreateBlocks(XO_pref, x_max, y_max, level, 4);
        CreateBalls();
    }

    void SetBackground()
    {
        var bg = GameObject.Find("Background").GetComponent<SpriteRenderer>();
        bg.sprite = Resources.Load(level.ToString("d2"),
        typeof(Sprite)) as Sprite;
    }

    public void BallDestroyed()
    {
        game_data.balls--;
        StartCoroutine(BallDestroyedCoroutine());
    }

    public void BlockDestroyed(int points)
    {
        game_data.points += points;
        if(game_data.sound) sfxaudio_src.PlayOneShot(point_sound, 5);
        game_data.points_to_ball += points;
        if(game_data.points_to_ball >= requiredPointsYoBall)
        {
            game_data.balls++;
            game_data.points_to_ball -= requiredPointsYoBall;
            if (game_data.sound) StartCoroutine(BlockDestroyedCoroutine2());
        }
        StartCoroutine(BlockDestroyedCoroutine());
    }

    IEnumerator BallDestroyedCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        if (GameObject.FindGameObjectsWithTag("Ball").Length == 0) 
        { 
            if(game_data.balls > 0) CreateBalls();
        }
        else
        {
            game_data.Reset();
            SceneManager.LoadScene("MainScene");
        }
    }

    IEnumerator BlockDestroyedCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        if (GameObject.FindGameObjectsWithTag("Block").Length == 0)
        {
            if (level < max_level) game_data.level++;
            SceneManager.LoadScene("MainScene");
        }
    }

    IEnumerator BlockDestroyedCoroutine2()
    {
        for(int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(0.2f);
            sfxaudio_src.PlayOneShot(point_sound, 5);
        }

    }

    string OnOff(bool bool_value)
    {
        return bool_value ? "on" : "off";
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(5, 4, Screen.width - 10, 100),
            string.Format("<color=yellow><size=30>Level <b>{0}</b> Balls <b>{1}</b>" + " Score <b>{2}</b></size></color>",
            game_data.level, game_data.balls, game_data.points));
        GUIStyle style = new GUIStyle();
        style.alignment = TextAnchor.UpperRight;
        GUI.Label(new Rect(5, 14, Screen.width - 10, 100), string.Format("<color=yellow><size=20><color=white>Space</color>-pause {0}" +
                " <color=white>N</color>-new" + " <color=white>J</color>-jump" + " <color=white>M</color>-music {1}" +
                " <color=white>S</color>-sound {2}" + " <color=white>Esc</color>-exit</size></color>", OnOff(Time.timeScale > 0), OnOff(!game_data.music),
                OnOff(!game_data.sound)), style);
    }

    void SetMusic()
    {
        if (game_data.music) musicaudio_src.Play();
        else musicaudio_src.Stop();
    }

    private void OnApplicationQuit()
    {
        game_data.Save();
    }
}
