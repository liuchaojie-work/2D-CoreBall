using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //针的发射位置
    private Transform startPoint;
    //针的生成位置
    private Transform spawnPoint;
    //针的预制体
    private GameObject pinPrefab;
    //摄像机
    private Camera mainCamera;
    //动画速度
    private float aniSpeed = 3.0f;
    private Pin currentPin;
    private ManagerVars vars;

    //表示游戏是否结束
    private bool isGameOver = false;
    //分数
    private int score = 0;
    //分数显示
    private Text txt_Score;

    //单例模式
    private static GameManager _Instance;
    public static GameManager Instance
    {
        get { return _Instance; }
    }

    private void Awake()
    {
        _Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        vars = ManagerVars.GetManagerVars();
        startPoint = GameObject.Find("StartPoint").transform;
        spawnPoint = GameObject.Find("SpawnPoint").transform;
        txt_Score = GameObject.Find("UI/Txt_Score").GetComponent<Text>();
        mainCamera = Camera.main;
        pinPrefab = vars.pinPrefab;
        SpawnPin();
    }

    // Update is called once per frame
    void Update()
    {
        if(isGameOver)
        {
            return;
        }
        if(Input.GetMouseButtonDown(0))
        {
            score++;
            txt_Score.text = score.ToString();
            currentPin.StartFly();
            SpawnPin();
        }
    }

    void SpawnPin()
    {
        currentPin = GameObject.Instantiate(pinPrefab, spawnPoint.position, pinPrefab.transform.rotation).GetComponent<Pin>();
    }

    public void GameOver()
    {
        if(isGameOver)
        {
            return;
        }
        GameObject.Find("Circle").GetComponent<RotateSelf>().enabled = false;
        StartCoroutine(GameOverAnimation());
        isGameOver = true;
    }

    IEnumerator GameOverAnimation()
    {
        while(true)
        {
            mainCamera.backgroundColor = Color.Lerp(mainCamera.backgroundColor, Color.red, aniSpeed * Time.deltaTime);
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, 4, aniSpeed * Time.deltaTime);
            if(Mathf.Abs(mainCamera.orthographicSize-4)<0.01f)
            {
                break;
            }
            yield return 0;
        }
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
