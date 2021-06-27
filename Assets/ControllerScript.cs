using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerScript : MonoBehaviour
{
    public GameObject menu;
    public UnityEngine.UI.Button startButton;
    public UnityEngine.UI.Text scoreLabel;
    public UnityEngine.UI.Text HPBar;
    public GameObject GameIsOver;
    public UnityEngine.UI.Text Record;
    public GameObject playerShip;
    public float stageTime;

    public static bool isStarted = false; // запущена ли игра?

    static int score;
    int maxScore = 0;
    private int HP;

    static AudioSource scoreSound;
    public Vector3 SpawnPosition;

    private bool isDead = false;

    float startTime;
    
    // Start is called before the first frame update
    void Start()
    {        
        scoreSound = GetComponent<AudioSource>();
    }

    public static void IncreaseScore(int increment)
    {
        score += increment;
        if (increment >= 100)
        {
            scoreSound.Play();
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        startButton.onClick.AddListener(delegate {
            menu.SetActive(false);
            isStarted = true;
            HP = 4;
            score = 0;
            startTime = Time.time;
            EmitterScript.minDelay = 2;
            
        });

        scoreLabel.text = "Счет: " + score;
        UpdateHP();
        FindPlayer();
        

        if (isDead && isStarted)
        {
            HP -= 1;
            if (HP <= 0)
            {
                isStarted = false;
                menu.SetActive(true);
                GameIsOver.SetActive(true);
                Record.text = "Максимальный счет: " + maxScore;                
            }
            else
            {                
                StartCoroutine(Respawn());
            }
            
            
        }
        if (score > maxScore)
        {
            maxScore = score;
        }
        
        if (Time.time - startTime > stageTime && EmitterScript.minDelay >= 0.25f)
        {
            startTime = Time.time;
            EmitterScript.minDelay /= 1.2f;
        }
    }
    IEnumerator Respawn()
    {       
        Instantiate(playerShip, SpawnPosition, Quaternion.identity); // Инициализация нового корабля
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // Определение нового корабля

        var box = player.GetComponent<BoxCollider>(); // Поиск коллайдеров
        var capsule = player.GetComponent<CapsuleCollider>();
        box.enabled = !box.enabled; // Отключение коллайдеров
        capsule.enabled = !capsule.enabled;

        var mesh = player.transform.Find("StarSparrow1").GetComponent<MeshRenderer>(); // Поиск сетки отрисовки
        
        for (int i = 0; i < 15; i++)
        {
            mesh.enabled = !mesh.enabled;
            yield return new WaitForSeconds(0.2f);            
        }
        mesh.enabled = true; // Включение отрисовки
        box.enabled = !box.enabled; // Включение коллайдеров
        capsule.enabled = !capsule.enabled;

    }
    void UpdateHP()
    {
        HPBar.text = "";
        for (int i = 0; i < HP; i++)
        {
            HPBar.text += "+";
        }

    }
    void FindPlayer()
    {
        isDead = GameObject.FindGameObjectWithTag("Player") == null ? true : false;
    }

}
