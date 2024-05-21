using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnSystem : MonoBehaviour
{
    public GameObject[] items;
    public GameObject[] rareItems;
    public Transform[] places;
    public float delaySpawn;
    WaitForSeconds wait;
    public GameObject player;
    public GameObject fatherObj;
    public Slider progresBar;

    int currLevel;
    int currObstacle;
    int maxObstacle;

    private void Start()
    {
        //maxObstacle = 10;
        GetLevel();
        wait = new WaitForSeconds(delaySpawn);
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            if (currObstacle < maxObstacle)
            {
                int verRare = Random.Range(0, 100);
                float power = Random.Range(10f, 20f);
                int ver = Random.Range(0, 100);
                int angular = Random.Range(100, 200);
                int indexPlace = Random.Range(0, places.Length);
                if (verRare >= 40 && verRare <= 50)
                {
                    SpawnRare(power, ver, angular, indexPlace);
                }
                else
                {
                    SpawnUsual(power, ver, angular, indexPlace);
                }
                progresBar.value = currObstacle - 3;
                yield return wait;
            }
            else
            {
                if (fatherObj.transform.childCount == 0)
                {
                    player.GetComponent<PScore>().WinGame();
                    StopAllCoroutines();
                }
                yield return null;
            }
        }
    }

    private void SpawnRare(float power, int ver, int angular, int indexPlace)
    {
        currObstacle++;
        int indexItem = Random.Range(0, rareItems.Length);
        GameObject rareObs = Instantiate(rareItems[indexItem], places[indexPlace].position, Quaternion.identity);
        rareObs.GetComponent<Rigidbody2D>().AddForce(places[indexPlace].up * power);
        rareObs.GetComponent<EnemyHealth>().player = this.player.GetComponent<PScore>();
        rareObs.transform.parent = fatherObj.transform;

        if (ver >= 50)
            rareObs.GetComponent<Rigidbody2D>().angularVelocity = -angular;
        else
            rareObs.GetComponent<Rigidbody2D>().angularVelocity = angular;
    }

    private void SpawnUsual(float power, int ver, int angular, int indexPlace)
    {
        currObstacle++;
        int indexItem = Random.Range(0, items.Length);
        GameObject usualObs = Instantiate(items[indexItem], places[indexPlace].position, Quaternion.identity);
        usualObs.GetComponent<Rigidbody2D>().AddForce(places[indexPlace].up * power);
        usualObs.GetComponent<EnemyHealth>().player = this.player.GetComponent<PScore>();
        usualObs.transform.parent = fatherObj.transform;

        if (ver >= 50)
            usualObs.GetComponent<Rigidbody2D>().angularVelocity = -angular;
        else
            usualObs.GetComponent<Rigidbody2D>().angularVelocity = angular;
    }

    private void GetLevel()
    {
        if (PlayerPrefs.HasKey("levelGame"))
        {
            currLevel = PlayerPrefs.GetInt("levelGame");
            maxObstacle = currLevel * 5;
        }
        else
        {
            currLevel = 1;
            maxObstacle = 10;
        }
        progresBar.maxValue = maxObstacle - 3;
        progresBar.minValue = 0;
        progresBar.value = 0;
    }
}
