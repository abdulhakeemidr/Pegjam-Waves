using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : BeatResponder
{
    public GameObject objHeart;
    public Text txtHealth;
    public Text txtRound;
    public CanvasGroup canvasGroup;

    public override void OnBeat()
    {
        StartCoroutine(PulseHeart());
    }

    private IEnumerator PulseHeart()
    {
        Vector3 initialScale = objHeart.transform.localScale;
        float currentTime = 0f;
        float duration = 0.25f;
        Vector3 targetScale = new Vector3(1.25f, 1.25f, 1.25f);
    
        while (currentTime < duration)
        {
            objHeart.transform.localScale = Vector3.Lerp(initialScale, targetScale, currentTime / duration);
            currentTime += Time.deltaTime;
            yield return null;
        }

        objHeart.transform.localScale = initialScale;
        yield return null;
    }
    
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
