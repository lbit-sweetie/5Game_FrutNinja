using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public string type;
    public bool isContact;
    public PScore player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Brd")
        {
            if(!isContact)
                isContact = true;
            else
            {
                player.TakeDamage();
                Destroy(gameObject, 1f);
            }
        }
    }
}