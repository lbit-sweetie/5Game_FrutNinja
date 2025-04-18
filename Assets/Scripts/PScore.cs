using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PScore : MonoBehaviour
{
    [SerializeField] private float _currentScore;
    public int health;
    public float usualScore;
    public float rareScore;
    public int _coins;

    public TMP_Text coinsText;
    public TMP_Text rareCoinsText;
    public TMP_Text rareMagicText;

    public TMP_Text winLoseText;

    public Slider healthBar;

    [SerializeField] private int rareCoin;
    [SerializeField] private int rareMagic;

    [Space] [Header("Canvas")] public CanvasGroup endGameCanvas;
    public TMP_Text levelTxt;
    public CanvasGroup fade;


    bool isDead;
    private int _level;

    private void Start()
    {
        fade.alpha = 1;
        fade.DOFade(0, 0.4f);
        _level = PlayerPrefs.GetInt("levelGame", 1);

        levelTxt.text = $"Level: {_level}";
        Animations.AnimateFadeInOut(levelTxt.GetComponent<CanvasGroup>(), 3);

        healthBar.maxValue = health;
        healthBar.minValue = 0;
    }

    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("Data Clear");
            PlayerPrefs.DeleteAll();
        }
    }*/

    public void TakeDamage(int amout = 1)
    {
        health -= amout;
        healthBar.value = health;
        if (health <= 0)
        {
            Debug.Log("Death");
            LoseGame();
        }
    }

    public void AddScore(string type)
    {
        switch (type)
        {
            case "usual":
                _currentScore += usualScore;
                break;
            case "rareCoin":
                _currentScore += rareScore;
                rareCoin++;
                break;
            case "rareMagic":
                _currentScore += rareScore;
                rareMagic++;
                break;
            default:
                _currentScore += 1;
                break;
        }

        _coins = (int) (_currentScore / 50);
    }

    public void WinGame()
    {
        EndGameMath();

        winLoseText.text = "Level " + _level.ToString() + " passed";
        PlayerPrefs.SetInt("levelGame", _level + 1);
        PlayerPrefs.Save();

        Animations.AnimateCanvasGroup(endGameCanvas, true, 1);

        Time.timeScale = 0f;
    }

    public void LoseGame()
    {
        EndGameMath();

        winLoseText.text = "Level " + _level.ToString() + " defeated";

        Animations.AnimateCanvasGroup(endGameCanvas, true, 1);
        Time.timeScale = 0f;
    }

    public void EndGameMath()
    {
        _coins = (int) (_currentScore / 50);
        rareCoinsText.text = "+" + rareCoin.ToString();
        rareMagicText.text = "+" + rareMagic.ToString();
        coinsText.text = "+" + _coins.ToString();

        if (PlayerPrefs.GetFloat("bestscore", 0) <= _currentScore)

            PlayerPrefs.SetFloat("bestscore", _currentScore);

        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins", 0) + _coins);
        PlayerPrefs.SetInt("rareCoin", PlayerPrefs.GetInt("rareCoin", 0) + rareCoin);
        PlayerPrefs.SetInt("rareMagic", PlayerPrefs.GetInt("rareMagic", 0) + rareMagic);
        PlayerPrefs.Save();
    }

    public void BackToMenu()
    {
        fade.DOFade(1, 0.6f).OnComplete(() =>
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene("Men");
        }).SetUpdate(true);
    }

    public void Next()
    {
        fade.DOFade(1, 0.6f).OnComplete(() =>
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene("Gam");
        }).SetUpdate(true);
    }
}