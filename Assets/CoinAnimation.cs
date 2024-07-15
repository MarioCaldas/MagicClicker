using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinAnimation : MonoBehaviour
{
    public float force = 100;
    public float forceUpRange = 100f; // Range of random force to apply
    public float forceMin = -100f; // Minimum upward force
    public float forceMax = 100f; // Maximum upward force
    public float torqueRange = 50f; // Range of random torque to apply

    [SerializeField] private Image wingsUpImage;

    [SerializeField] private Image wingsDownImage;
    private void Start()
    {
        ApplyRandomForce();

        StartCoroutine(CoinFadeout());
    }

    private void ApplyRandomForce()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Apply a random upward force within the specified range
            float randomYForce = Random.Range(25, forceMax);
            float randomXForce = Random.Range(forceMin, forceMax);
            rb.AddForce(new Vector2(randomXForce, randomYForce).normalized * force);

            // Apply a random torque for rotation
            float randomTorque = torqueRange;// Random.Range(-torqueRange, torqueRange);
            rb.AddTorque(randomTorque);
        }
    }

    IEnumerator CoinFadeout()
    {
        float elapsedTime = 0;
        float totalTime = 1.7f;
        Color originalColor = wingsUpImage.color;

        while (elapsedTime <= totalTime)
        {
            elapsedTime += Time.deltaTime;

            if((elapsedTime / totalTime) >= 0.85f)
            {
                float alpha = Mathf.Lerp(originalColor.a + elapsedTime, 0f, elapsedTime / totalTime);
                wingsUpImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                wingsDownImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            }

            yield return null;
        }
        wingsUpImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        wingsDownImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);

        Destroy(gameObject);

    }
}
