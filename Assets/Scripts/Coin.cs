using UnityEngine;

public class Coin : MonoBehaviour
{
    // Vieno golden coin verte
    [Range(0, 20)]
    public int CoinValue = 1;

    public AudioClip Sound;

    // Tikrinimas ar zaidejas paima coin ir updatina ui
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ScoreManager.Instance.ChangeScore(CoinValue, "+");
            AudioSource.PlayClipAtPoint(Sound, transform.position);
        }
    }
}
