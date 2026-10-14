using UnityEngine;

public class CharacterSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] footstepClips;

    private int _lastClipIndex = -1;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayFootstepSound()
    {
        if (footstepClips.Length > 0)
        {
            int randomIndex = Random.Range(0, footstepClips.Length);
            // Ensure we don't play the same clip twice in a row
            if (randomIndex == _lastClipIndex)
            {
                randomIndex = (randomIndex + 1) % footstepClips.Length;
            }
            _lastClipIndex = randomIndex;
            audioSource.PlayOneShot(footstepClips[randomIndex]);
        }
    }
}
