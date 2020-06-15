using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    [Header("血條")]
    public Image imgHp;
    [Header("金幣數量")]
    public Text textCoin;
    [Header("結束畫面")]
    public GameObject final;
    [Header("本次金幣數量")]
    public Text textCurrent;
    [Header("金幣音效")]
    public AudioClip soundCoin;
    [Header("障礙物音效")]
    public AudioClip soundObstacle;
    [Header("障礙物傷害值")]
    public float damage = 10;

    private AudioSource aud;
    private int countCoin;
    private string stringCoin;
    private float hp = 100;
    private float hpMax = 100;
    private bool gameOver;
    private Player player;
    private CameraControl cam;

    private void Start()
    {
        aud = GetComponent<AudioSource>();
        stringCoin = textCoin.text;
        player = FindObjectOfType<Player>();
        cam = FindObjectOfType<CameraControl>();
    }

    private void Update()
    {
        HpEffect();
    }

    /// <summary>
    /// 取得金幣
    /// </summary>
    public void GetCoin(Collider2D coin)
    {
        aud.PlayOneShot(soundCoin);
        countCoin++;
        textCoin.text = stringCoin + countCoin;
        Destroy(coin.gameObject);
    }

    /// <summary>
    /// 碰到障礙物
    /// </summary>
    public void GetHit(Collider2D obstacle)
    {
        aud.PlayOneShot(soundObstacle);
        hp -= damage;
        imgHp.fillAmount = hp / hpMax;
        Destroy(obstacle.gameObject);
        if (hp <= 0) Dead();
    }

    private void HpEffect()
    {
        if (gameOver) return;
        hp -= Time.deltaTime;
        imgHp.fillAmount = hp / hpMax;
    }

    public void Dead()
    {
        if (gameOver) return;
        gameOver = true;
        cam.speed = 0;
        player.Dead();
        StartCoroutine(FinalEffect());
    }

    private IEnumerator FinalEffect()
    {
        yield return new WaitForSeconds(1);

        final.SetActive(true);
        string stringCurrent = textCurrent.text;
        textCurrent.text = stringCurrent + countCoin;

        for (int i = 0; i < final.transform.childCount; i++)
            final.transform.GetChild(i).gameObject.SetActive(false);

        for (int i = 0; i < final.transform.childCount; i++)
        {
            final.transform.GetChild(i).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.3f);
        }
    }
}
