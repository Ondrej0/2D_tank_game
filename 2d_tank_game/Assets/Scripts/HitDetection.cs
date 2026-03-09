using UnityEngine;

public class HitDetection : MonoBehaviour
{
    public string[] deadlyTags;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (string tag in deadlyTags)
        {
            if (collision.gameObject.CompareTag(tag))
            {
                Destroy(collision.gameObject); // destroy bullet
                Destroy(gameObject);           // destroy tank
                break;
            }
        }
    }
}