using UnityEngine;
using Yarn.Unity;

public class YarnSpinnerCustomCommands : MonoBehaviour
{
    [SerializeField] private ScreenFader _screenFader;
    [SerializeField] private GameObject _AttentionFX;
    [SerializeField] private GameObject _CheeseFX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [YarnCommand("fade_in")]
    public void FadeIn(float duration)
    {
        _screenFader.FadeIn(duration);
    }

    [YarnCommand("fade_out")]
    public void FadeOut(float duration)
    {
        _screenFader.FadeOut(duration);
    }

    [YarnCommand("show_attention_fx")]
    public void ShowAttentionFX()
    {
        _AttentionFX.SetActive(true);
    }

    [YarnCommand("show_cheese")]
    public void ShowCheese()
    {
        _CheeseFX.SetActive(true);
    }
}
