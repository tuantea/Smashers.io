using UnityEngine;
using HighlightPlus;
public class HighLight : MonoBehaviour
{
    HighlightEffect hb;
    [SerializeField] PlayerAttack playerAttack;
    public HighlightEffect highlightEffect { get { return hb; } }

    public void Start()
    {
        Init();
        playerAttack.OnHit += HighLight_OnHit;
    }

    private void HighLight_OnHit(object sender, System.EventArgs e)
    {
        Highlight(true);
    }

    public void Init()
    {
        if (hb == null)
        {
            hb = GetComponent<HighlightEffect>();
        }
    }
    public void Highlight(bool state)
    {
        hb.HitFX(transform.position);
    }
}
