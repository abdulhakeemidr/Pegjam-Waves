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
        yield return StartCoroutine(ShowMainText());
        yield return new WaitForSeconds(2);
        yield return StartCoroutine(FadeMainText(1));
    }

    public IEnumerator EndRound(int round)
    {
        txtRound.text = "WAVE " + round + "CLEARED";
        yield return StartCoroutine(ShowMainText());
        yield return new WaitForSeconds(2);
        yield return StartCoroutine(FadeMainText(1));
    }

    public IEnumerator ShowGameOver()
    {
        txtRound.text = "GAME OVER";
        yield return StartCoroutine(ShowMainText());
        yield return new WaitForSeconds(2);
        yield return StartCoroutine(FadeMainText(1));
    }

    private IEnumerator ShowMainText()
    {
        canvasGroup.alpha = 1f;
        yield return null;
    }
    
    private IEnumerator FadeMainText(float duration)
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
