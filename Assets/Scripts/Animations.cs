using DG.Tweening;
using UnityEngine;

public class Animations : MonoBehaviour
{
    public static void AnimateCanvasGroup(CanvasGroup canvasGroup, bool state, float animDuration)
    {
        canvasGroup.DOKill();

        if (state)
        {
            canvasGroup.gameObject.SetActive(true);
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

            canvasGroup.DOFade(1f, animDuration)
                .SetEase(Ease.OutQuad)
                .SetUpdate(true);
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
        else
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

            canvasGroup.DOFade(0f, animDuration)
                .SetEase(Ease.InQuad)
                .SetUpdate(true)
                .OnComplete(() => canvasGroup.gameObject.SetActive(false));
        }
    }

    public static void AnimateNotEnoughResources(CanvasGroup canvasGroup, float animDuration)
    {
        canvasGroup.DOKill();
        canvasGroup.transform.DOKill();

        canvasGroup.transform.DOShakePosition(
            duration: animDuration,
            strength: new Vector3(10f, 0, 0),
            vibrato: 9,
            randomness: 90,
            snapping: false,
            fadeOut: true
        ).SetUpdate(true);
    }

    public static void AnimateFadeInOut(CanvasGroup canvasGroup, float animDuration)
    {
        canvasGroup.DOKill();

        canvasGroup.gameObject.SetActive(true);
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        Sequence sequence = DOTween.Sequence();

        sequence.Append(canvasGroup.DOFade(1f, animDuration * 0.25f))
            .SetEase(Ease.OutQuad);

        sequence.AppendInterval(animDuration * 0.1f);

        sequence.Append(canvasGroup.DOFade(0f, animDuration * 0.65f))
            .SetEase(Ease.InQuad)
            .OnComplete(() => canvasGroup.gameObject.SetActive(false));

        sequence.SetUpdate(true);
    }

    public static void StartIdleAnimation(CanvasGroup element, float moveDuration, float moveStrength, float interval)
    {
        if (element == null) return;

        RectTransform rectTransform = element.GetComponent<RectTransform>();
        if (rectTransform == null) return;

        rectTransform.DOKill();

        Vector2 originalPos = rectTransform.anchoredPosition;

        Sequence idleSequence = DOTween.Sequence();
        idleSequence.Append(rectTransform.DOAnchorPos(originalPos + RandomOffset(moveStrength), moveDuration))
            .SetEase(Ease.InOutSine);
        idleSequence.AppendInterval(interval);
        idleSequence.Append(rectTransform.DOAnchorPos(originalPos, moveDuration))
            .SetEase(Ease.InOutSine);
        idleSequence.AppendInterval(interval);
        idleSequence.SetLoops(-1);
    }

    private static Vector2 RandomOffset(float strength)
    {
        return new Vector2(
            Random.Range(-strength, strength),
            Random.Range(-strength, strength));
    }
}