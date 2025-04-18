using Unity.VisualScripting;
using UnityEngine;

public class KnifeMove : MonoBehaviour
{
    public float maxSpeed;
    public float minSpeed;
    public GameObject player;
    public GameObject deathPartsObs;

    public void Shoot(float speed)
    {
        //Debug.Log(speed);
        if (speed >= maxSpeed)
            speed = maxSpeed;
        if (speed <= minSpeed)
            speed = minSpeed;
        gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * speed * 20, ForceMode2D.Force);
        Destroy(gameObject, 5f);
    }
    private void Awake()
    {
        GetComponent<Rigidbody2D>().gravityScale = 0f;
    }

    public void SetPl(GameObject p)
    {
        player = p;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obs")
        {
            player.GetComponent<PScore>().AddScore(collision.gameObject.GetComponent<EnemyHealth>().type);
            Destroy(collision.gameObject);
            Instantiate(deathPartsObs, collision.transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obs")
        {
            player.GetComponent<PScore>().AddScore(collision.gameObject.GetComponent<EnemyHealth>().type);
            Destroy(collision.gameObject);
            Instantiate(deathPartsObs, collision.transform.position, Quaternion.identity);
        }
    }
}