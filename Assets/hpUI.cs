using UnityEngine;
using UnityEngine.UI;

public class hpUI : MonoBehaviour
{
    [SerializeField] private Text countText;
    [SerializeField] private float minXPos;
    [SerializeField] private float maxXPos;
    [SerializeField] private RectTransform hpBarRect;

    public void SetHp(int hp, int maxHp) 
    {
        countText.text = hp.ToString();
        float procentHp = hp/(maxHp / 100f);
        hpBarRect.offsetMax = new Vector2(-(minXPos - (minXPos - maxXPos) / 100 * procentHp), hpBarRect.offsetMax.y);
    }
}
