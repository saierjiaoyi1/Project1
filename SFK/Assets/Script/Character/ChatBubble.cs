using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ChatBubble : MonoBehaviour
{
    public CanvasGroup cg;
    public Text text;

    public void Say(string s)
    {
        if (cg.alpha > 0)
            return;

        var seq = DOTween.Sequence();
        seq.AppendCallback(() =>
        {
            cg.alpha = 0;
            text.text = s;
            cg.DOFade(1, 1);
        });

        seq.AppendInterval(4);
        seq.AppendCallback(() =>
        {
            cg.DOFade(0, 1);
        });
    }
}
