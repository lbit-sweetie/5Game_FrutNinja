using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PScore : MonoBehaviour
{
    [SerializeField] private float _score;
    public int health;
    public float usualScore;
    public float rareScore;
    public int _coins;

    public TMP_Text coinsText;
    public TMP_Text rareCoinsText;
    public TMP_Text rareMagicText;
    public TMP_Text winLoseText;
    public Animator dCanvasAnim;
    public Slider healthBar;

    [SerializeField] private int rareCoin;
    [SerializeField] private int rareMagic;

    bool isDead;

    private void Start()
    {
        healthBar.maxValue = health;
        healthBar.minValue = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("Data Clear");
            PlayerPrefs.DeleteAll();
        }
    }

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
                _score += usualScore;
                break;
            case "rareCoin":
                _score += rareScore;
                rareCoin++;
                break;
            case "rareMagic":
                _score += rareScore;
                rareMagic++;
                break;
            default:
                _score += 1;
                break;
        }
        _coins = (int)(_score / 50);
    }

    public void WinGame()
    {
        EndGameMath();
        if (PlayerPrefs.HasKey("levelGame"))
        {
            int level = PlayerPrefs.GetInt("levelGame");
            winLoseText.text = "Level " + level.ToString() + " passed";
            PlayerPrefs.SetInt("levelGame", level + 1);
            PlayerPrefs.Save();
        }
        else
        {
            winLoseText.text = "Level 1 passed";
            PlayerPrefs.SetInt("levelGame", 1);
            PlayerPrefs.Save();
        }
        dCanvasAnim.SetTrigger("Open");
        Time.timeScale = 0f;
    }
    public void LoseGame()
    {
        EndGameMath();
        if (PlayerPrefs.HasKey("levelGame"))
        {
            int level = PlayerPrefs.GetInt("levelGame");
            winLoseText.text = "Level " + level.ToString() + " defeated";
        }
        else
        {
            winLoseText.text = "Level 1 defeated";
        }
        dCanvasAnim.SetTrigger("Open");
        Time.timeScale = 0f;
    }

    public void EndGameMath()
    {
        _coins = (int)(_score / 50);
        rareCoinsText.text = "+" + rareCoin.ToString();
        rareMagicText.text = "+" + rareMagic.ToString();
        coinsText.text = "+" + _coins.ToString();
        if (PlayerPrefs.HasKey("bestscore"))
        {
            float a = PlayerPrefs.GetFloat("bestscore");
            if (_score >= a)
            {
                PlayerPrefs.SetFloat("bestscore", _score);
            }
        }
        else
        {
            PlayerPrefs.SetFloat("bestscore", _score);
        }

        if (PlayerPrefs.HasKey("coins"))
        {
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + _coins);
        }
        else
        {
            PlayerPrefs.SetInt("coins", _coins);
        }

        if (PlayerPrefs.HasKey("rareCoin"))
        {
            PlayerPrefs.SetInt("rareCoin", PlayerPrefs.GetInt("rareCoin") + rareCoin);
        }
        else
        {
            PlayerPrefs.SetInt("rareCoin", rareCoin);
        }

        if (PlayerPrefs.HasKey("rareMagic"))
        {
            PlayerPrefs.SetInt("rareMagic", PlayerPrefs.GetInt("rareMagic") + rareMagic);
        }
        else
        {
            PlayerPrefs.SetInt("rareMagic", rareMagic);
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Men");
        Time.timeScale = 1.0f;
    }
    public void Next()
    {
        SceneManager.LoadScene("Gam");
        Time.timeScale = 1.0f;
    }
}
