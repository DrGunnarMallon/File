using System.Collections;
using UnityEngine;

public class GravityControl : MonoBehaviour
{
    [SerializeField] private float rotationTime = 0.5f;

    private Player player;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
    }

    public void reversePolarity()
    {
        // if (player.CanFlipGravity())
        // {
        StopAllCoroutines();
        rb.gravityScale *= -1;
        StartCoroutine(flipGravity());
        // }
    }

    IEnumerator flipGravity()
    {
        float elapsedTime = 0f;

        Quaternion startRotation = rb.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, 0, rb.gravityScale > 0 ? 0 : 180);

        while (elapsedTime < rotationTime)
        {
            rb.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime / rotationTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rb.transform.rotation = targetRotation;
    }
}
