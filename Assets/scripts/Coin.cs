using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;
    public AudioClip pickupSound;

    private bool collected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (collected) return;

        if (other.CompareTag("Player"))
        {
            collected = true;

            // Increase score
            GameManager.Instance.AddScore(coinValue);

            // Play sound at louder volume
            if (pickupSound != null)
                AudioSource.PlayClipAtPoint(pickupSound, transform.position, 1.5f);

            // Destroy coin
            Destroy(gameObject);
        }
    }
}