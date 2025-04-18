using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScr : MonoBehaviour
{
    public int _coins = 0;
    public int rareCoin;
    public int rareMagic;
    [Header("Texts")] public TMP_Text levelSpeedMove;
    public TMP_Text levelSpeedKatana;
    public TMP_Text btnSpeedMove;
    public TMP_Text btnSpeedKatana;
    public TMP_Text coinsText;
    public TMP_Text rareCoinTxt;
    public TMP_Text rareMagicTxt;

    [Header("Canvas")] public CanvasGroup storePanel;
    public CanvasGroup menuPanel;
    public CanvasGroup pageStore1;
    public CanvasGroup pageStore2;
    public CanvasGroup infoPanel;
    public CanvasGroup fade;

    [Space] public CanvasGroup currencyPanel;
    public CanvasGroup skinsPanel1;
    public CanvasGroup skinsPanel2;
    public CanvasGroup playBtn;
    public CanvasGroup infoBtn;


    private void Start()
    {
        Application.targetFrameRate = 120;
        Time.timeScale = 1;

        UpdateNumbers();

        Setter(pageStore1, true);
        Setter(menuPanel, true);
        Setter(storePanel);
        Setter(pageStore2);
        Setter(infoPanel);

        Animations.StartIdleAnimation(playBtn, 1f, 25f, 0.1f);
        Animations.StartIdleAnimation(infoBtn, 1f, 35f, 0.001f);

        fade.alpha = 1;
        fade.DOFade(0, 1);
    }

    private void Setter(CanvasGroup canvasGroup, bool state = false)
    {
        canvasGroup.alpha = state ? 1 : 0;
        canvasGroup.interactable = state;
        canvasGroup.blocksRaycasts = state;
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
        fade.DOFade(1, 0.6f).OnComplete(() =>
        {
            SceneManager.LoadScene("Gam");
        });
    }

    public void QuitQ()
    {
        fade.DOFade(1, 0.6f).OnComplete(() =>
        {
            Application.Quit();
        });
    }

    public void BuySpeedMoveKatana()
    {
        BuyUpgrade("speedOfMove");
        UpdateNumbers();
    }

    public void BuySpeedKatana()
    {
        BuyUpgrade("speedKatana");
        UpdateNumbers();
    }

    public void UpdateNumbers()
    {
        _coins = PlayerPrefs.GetInt("coins", 0);
        coinsText.text = _coins.ToString();

        if (PlayerPrefs.HasKey("speedOfMove"))
        {
            int levelMove = PlayerPrefs.GetInt("speedOfMove");
            levelSpeedMove.text = $"Level: {levelMove}";
            btnSpeedMove.text = $"Buy: {((levelMove + 1) * 100)}";
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

        rareCoin = PlayerPrefs.GetInt("rareCoin", 0);
        rareCoinTxt.text = rareCoin.ToString() + "/25";

        rareMagic = PlayerPrefs.GetInt("rareMagic", 0);
        rareMagicTxt.text = rareMagic.ToString() + "/25";
    }

    private void BuyUpgrade(string nameUpgrade)
    {
        if (_coins <= 0)
        {
            Animations.AnimateNotEnoughResources(currencyPanel, 1);
            return;
        }

        if (PlayerPrefs.HasKey(nameUpgrade))
        {
            int upgrade = PlayerPrefs.GetInt(nameUpgrade);
            if ((upgrade + 1) * 100 > _coins)
            {
                Animations.AnimateNotEnoughResources(currencyPanel, 1);
                return;
            }

            _coins -= (upgrade + 1) * 100;
            PlayerPrefs.SetInt(nameUpgrade, upgrade + 1);
            PlayerPrefs.SetInt("coins", _coins);
            PlayerPrefs.Save();
        }
        else
        {
            if (100 > _coins)
            {
                Animations.AnimateNotEnoughResources(currencyPanel, 1);
                return;
            }

            _coins -= 100;
            PlayerPrefs.SetInt(nameUpgrade, 1);
            PlayerPrefs.SetInt("coins", _coins);
            PlayerPrefs.Save();
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
            Animations.AnimateNotEnoughResources(skinsPanel1, 1);
            Animations.AnimateNotEnoughResources(skinsPanel2, 1);
        }
    }

    public void StoreSwitch(bool state)
    {
        UpdateNumbers();
        if (state)
        {
            Animations.AnimateCanvasGroup(menuPanel, false, 0.5f);
            Animations.AnimateCanvasGroup(storePanel, true, 1f);
        }
        else
        {
            Animations.AnimateCanvasGroup(storePanel, false, 0.5f);
            Animations.AnimateCanvasGroup(menuPanel, true, 1f);
        }
    }

    public void StorePagesSwitch(bool page)
    {
        if (page)
        {
            Animations.AnimateCanvasGroup(pageStore1, false, 0.3f);
            Animations.AnimateCanvasGroup(pageStore2, true, 0.8f);
        }
        else
        {
            Animations.AnimateCanvasGroup(pageStore2, false, 0.3f);
            Animations.AnimateCanvasGroup(pageStore1, true, 0.8f);
        }
    }

    public void InfoPanelSwitch(bool state)
    {
        Animations.AnimateCanvasGroup(infoPanel, state, 0.5f);
    }
}