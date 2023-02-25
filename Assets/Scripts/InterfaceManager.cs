using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    public Text txtHealth;
    public Text txtRound;
    public CanvasGroup canvasGroup;

    public void UpdateHealth(int health)
    {
        txtHealth.text = health.ToString();
    }

    public IEnumerator StartRound(int round)
    {
        txtRound.text = "WAVE " + round + " START!";
        yield return StartCoroutine(ShowRoundText());
        yield return StartCoroutine(FadeRoundText(3000));
    }

    public IEnumerator EndRound(int round)
    {
        txtRound.text = "WAVE " + round + "CLEARED";
        yield return StartCoroutine(ShowRoundText());
        yield return StartCoroutine(FadeRoundText(3000));
    }

    private IEnumerator ShowRoundText()
    {
        canvasGroup.alpha = 1f;
        yield return null;
    }
    
    private IEnumerator FadeRoundText(float duration)
    {
        float startAlpha = canvasGroup.alpha;
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            float normalizedTime = timeElapsed / duration;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, normalizedTime);
            yield return null;
        }

        canvasGroup.alpha = 0f;
    }
}
