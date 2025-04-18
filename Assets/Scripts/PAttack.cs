using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class PAttack : MonoBehaviour
{
    public GameObject _knifePref;
    public GameObject _knife2Pref;
    public float speedOfMove;
    public float tempXRight;
    public float tempXLeft;
    private float tempZnak = -1;
    private int upSpeed;

    private Vector2 startPos;
    private Camera _mainCamera;
    private bool _isActive = false;
    private GameObject currKnife;
    private bool isMoovin;

    private void Start()
    {
        Time.timeScale = 1;
        _mainCamera = Camera.main;
        TakeData();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_isActive)
        {
            startPos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            currKnife = Instantiate(_knifePref, transform.position, Quaternion.identity);
            currKnife.GetComponent<KnifeMove>().SetPl(gameObject);
            currKnife.transform.SetParent(transform);
            _isActive = true;
            isMoovin = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (!_isActive)
                return;
            float dif = GetDifferenceToCam(startPos).y;
            currKnife.GetComponent<KnifeMove>().Shoot(dif + upSpeed);
            _isActive = false;
            isMoovin = false;
        }
        if (_isActive)
        {
            Vector2 curMousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 diff = curMousePos - startPos;
            if (diff.y <= 0)
            {
                diff.y = 0;
            }

            currKnife.transform.up = new Vector2(
                currKnife.transform.up.x + diff.x * 100f,
                currKnife.transform.up.y + diff.y * 100f);
        }
    }

    private void FixedUpdate()
    {
        if (isMoovin)
        {
            MovePoint();
        }
    }

    private void MovePoint()
    {
        transform.position = new Vector3(
            transform.position.x + speedOfMove * 0.001f * tempZnak,
            transform.position.y,
            transform.position.z);

        if (transform.position.x >= tempXRight && transform.position.x > 0)
        {
            var a = new Vector3(tempXRight, transform.position.y, 0);
            transform.position = a;
            tempZnak = -1f;
            return;
        }
        else
        {
            if (transform.position.x <= tempXLeft && transform.position.x < 0)
            {
                var b = new Vector3(tempXLeft, transform.position.y, 0);
                transform.position = b;
                tempZnak = 1f;
                return;
            }
        }
    }

    private Vector2 GetDifferenceToCam(Vector2 dif)
    {
        Vector2 c = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        return c - dif;
    }

    private void TakeData()
    {
        if (PlayerPrefs.HasKey("2skin"))
        {
            if (PlayerPrefs.GetInt("2skin") == 2)
            {
                _knifePref = _knife2Pref;
            }
        }

        if (PlayerPrefs.HasKey("speedOfMove"))
        {
            speedOfMove = speedOfMove - (PlayerPrefs.GetInt("speedOfMove") * 1.5f);
        }
        if (PlayerPrefs.HasKey("speedKatana"))
        {
            upSpeed = PlayerPrefs.GetInt("speedKatana");
        }
    }
}