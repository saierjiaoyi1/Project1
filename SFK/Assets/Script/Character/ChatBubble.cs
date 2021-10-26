using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ChatBubble : MonoBehaviour
{
    public CanvasGroup cg;
    public Text text;
    public float fadeInTime = 0.35f;
    public float duration = 3.5f;
    public float fadeOutTime = 0.5f;

    private void Start()
    {
        cg.alpha = 0;
    }

    public void Say(string s)
    {
        if (cg.alpha > 0)
            return;

        var seq = DOTween.Sequence();
        seq.AppendCallback(() =>
        {
            cg.alpha = 0;
            text.text = s;
            cg.DOFade(1, fadeInTime);
        });

        seq.AppendInterval(duration);
        seq.AppendCallback(() =>
        {
            cg.DOFade(0, fadeOutTime);
        });
    }
}
