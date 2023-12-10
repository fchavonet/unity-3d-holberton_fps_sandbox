using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DamageBehavior : MonoBehaviour
{
    // Reference to the canvas group that displays the damage effect
    public CanvasGroup damageOverlayCanvasGroup;

    [Space(10)]
    // Fade-in an fade-out speed for the damage effect
    public float fadeInSpeed = 3.0f;
    public float fadeOutSpeed = 1.0f;

    // Static variable indicating if the player is in a danger zone
    public static bool playerInDangerZone = false;

    private void Start()
    {
        // Initialize the opacity of the canvas group to 0 at startup
        damageOverlayCanvasGroup.alpha = 0f;
    }

    private void Update()
    {
        // Check if the player is in a danger zone
        if (playerInDangerZone)
        {
            // Call the fade-in function
            FadeIn();
        }
        else
        {
            // Call the fade-out function
            FadeOut();
        }
    }

    private void FadeIn()
    {
        // Gradually increase the opacity of the canvas group up to 1.0
        if (damageOverlayCanvasGroup.alpha < 1.0f)
        {
            damageOverlayCanvasGroup.alpha += Time.deltaTime * fadeInSpeed;
        }
    }

    private void FadeOut()
    {
        // Gradually decrease the opacity of the canvas group down to 0
        if (damageOverlayCanvasGroup.alpha > 0f)
        {
            damageOverlayCanvasGroup.alpha -= Time.deltaTime * fadeOutSpeed;
        }
    }
}
