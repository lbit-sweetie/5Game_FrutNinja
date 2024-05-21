using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScr : MonoBehaviour
{
    public Animator NEC;
    public int _coins = 0;
    public int rareCoin;
    public int rareMagic;
    public TMP_Text levelSpeedMove;
    public TMP_Text levelSpeedKatana;
    public TMP_Text btnSpeedMove;
    public TMP_Text btnSpeedKatana;
    public TMP_Text coinsText;
    public TMP_Text rareCoinTxt;
    public TMP_Text rareMagicTxt;

    private void Start()
    {
        UpdateNumbers();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("Delete");
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Gam");
    }
    public void QuitQ()
    {
        Application.Quit();
    }

    public void BuySpeedMoveKatana()
    {
        BuyUpgrade("speedOfMove");
        UpdateNumbers();
        Debug.Log(PlayerPrefs.GetInt("speedOfMove"));
    }

    public void BuySpeedKatana()
    {
        BuyUpgrade("speedKatana");
        UpdateNumbers();
        Debug.Log(PlayerPrefs.GetInt("speedKatana"));
    }

    public void UpdateNumbers()
    {
        if (PlayerPrefs.HasKey("coins"))
        {
            _coins = PlayerPrefs.GetInt("coins");
            Debug.Log(_coins.ToString() + " - Coins");
        }
        else
        {
            _coins = -1;
        }
        if (PlayerPrefs.HasKey("speedOfMove"))
        {
            int levelMove = PlayerPrefs.GetInt("speedOfMove");
            levelSpeedMove.text = "Level: " + levelMove;
            btnSpeedMove.text = "Buy: " + ((levelMove + 1) * 100);
        }
        else
        {
            levelSpeedMove.text = "Level: 0";
            btnSpeedMove.text = "Buy: 100";
        }
        if (PlayerPrefs.HasKey("speedKatana"))
        {
            int levelMove = PlayerPrefs.GetInt("speedKatana");
            levelSpeedKatana.text = "Level: " + levelMove;
            btnSpeedKatana.text = "Buy: " + ((levelMove + 1) * 100);
        }
        else
        {
            levelSpeedKatana.text = "Level: 0";
            btnSpeedKatana.text = "Buy: 100";
        }
        if (PlayerPrefs.HasKey("coins"))
        {
            coinsText.text = _coins.ToString();
        }
        else
        {
            coinsText.text = "0";
        }
        if (PlayerPrefs.HasKey("rareCoin"))
        {
            rareCoin = PlayerPrefs.GetInt("rareCoin");
            rareCoinTxt.text = rareCoin.ToString() + "/25";
            //Debug.Log(rareCoin.ToString() + " - rareCoin");
        }
        else
        {
            rareCoin = -1;
        }
        if (PlayerPrefs.HasKey("rareMagic"))
        {
            rareMagic = PlayerPrefs.GetInt("rareMagic");
            rareMagicTxt.text = rareMagic.ToString() + "/25";
            //Debug.Log(rareMagic.ToString() + " - rareMagic");
        }
        else
        {
            rareMagic = -1;
        }
    }

    private void BuyUpgrade(string nameUpgrade)
    {
        if (_coins != -1)
        {
            if (PlayerPrefs.HasKey(nameUpgrade))
            {
                int upgrade = PlayerPrefs.GetInt(nameUpgrade);
                if ((upgrade + 1) * 100 <= _coins)
                {
                    _coins -= (upgrade + 1) * 100;
                    PlayerPrefs.SetInt(nameUpgrade, upgrade + 1);
                    PlayerPrefs.SetInt("coins", _coins);
                    PlayerPrefs.Save();
                }
                else
                {
                    NEC.SetTrigger("NEC");
                }
            }
            else
            {
                if (100 <= _coins)
                {
                    _coins -= 100;
                    PlayerPrefs.SetInt(nameUpgrade, 1);
                    PlayerPrefs.SetInt("coins", _coins);
                    PlayerPrefs.Save();
                }
                else
                {
                    NEC.SetTrigger("NEC");
                }
            }
        }
        else
        {
            NEC.SetTrigger("NEC");
        }
    }

    public void BuyNewSkin()
    {
        if (rareCoin >= 25 && rareMagic >= 25)
        {
            PlayerPrefs.SetInt("2skin", 2);
            PlayerPrefs.Save();
        }
        else
        {
            NEC.SetTrigger("NECSkins");
        }
    }
}
