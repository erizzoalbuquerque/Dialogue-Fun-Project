using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    [SerializeField] private RawImage _fadeImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeIn(float duration)
    {
        _fadeImage.DOFade(1f, duration);
    }

    public void FadeOut(float duration)
    {
        _fadeImage.DOFade(0f, duration);
    }
}
