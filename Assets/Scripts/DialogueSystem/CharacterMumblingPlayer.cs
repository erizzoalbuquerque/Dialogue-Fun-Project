using System.Threading;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Yarn.Markup;
using Yarn.Unity;


namespace DialogueSystem
{
    public class CharacterMumblingPlayer : ActionMarkupHandler
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private CharacterVoiceSO _defaultVoice;

        private float _lastMumbleTime;
        private CharacterVoiceSO _currentCharacterVoice;
        private Tween _currentBobbleTween;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public override YarnTask OnCharacterWillAppear(int currentCharacterIndex, MarkupParseResult line, CancellationToken cancellationToken)
        {
            string characterName = GetCharacterNameFromMarkup(line);

            char currentChar = line.Text[currentCharacterIndex];

            Mumble(characterName, currentChar);

            return YarnTask.CompletedTask;
        }

        public override void OnLineDisplayBegin(MarkupParseResult line, TMP_Text text)
        {
            return;
        }

        public override void OnLineDisplayComplete()
        {
            return;
        }

        public override void OnLineWillDismiss()
        {
            return;
        }

        public override void OnPrepareForLine(MarkupParseResult line, TMP_Text text)
        {
            return;
        }

        private string GetCharacterNameFromMarkup(MarkupParseResult line)
        {
            if (line.TryGetAttributeWithName("character", out MarkupAttribute characterAttribute))
            {
                if (characterAttribute.TryGetProperty("name", out MarkupValue value))
                {
                    return value.StringValue;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }        
        }

        void Mumble(string characterName, char currentChar)
        {
            DialogueCharacter character = DialogueManager.Instance.GetDialogueCharacter(characterName);

            _currentCharacterVoice = character != null ? character.Voice : _defaultVoice;

            if (Time.time - _lastMumbleTime < _currentCharacterVoice.CooldownDuration)
            {
                return; // Cooldown not yet passed, skip playing sound
            }

            if (_currentCharacterVoice.TypewriterSounds == null || _currentCharacterVoice.TypewriterSounds.Count == 0)
            {
                return; // No sounds assigned, skip playing sound
            }

            // if current char is whitespace, poctuation or new line, skip playing sound
            if (char.IsWhiteSpace(currentChar) || char.IsPunctuation(currentChar) || currentChar == '\n')
            {
                return;
            }

            _audioSource.volume = _currentCharacterVoice.BaseVolume;        
            _audioSource.pitch = _currentCharacterVoice.BasePitch + Random.Range(_currentCharacterVoice.PitchVariationRange.x, _currentCharacterVoice.PitchVariationRange.y); 
            
            // pick up sounds in the list deterministically based on the current character
            // get low case
            currentChar = char.ToLower(currentChar);
            int index = Mathf.Abs(char.ToLower(currentChar).GetHashCode()) % _currentCharacterVoice.TypewriterSounds.Count;                
            
            _audioSource.PlayOneShot(_currentCharacterVoice.TypewriterSounds[index]);
                    
            if (character != null)
            {
                _currentBobbleTween?.Complete();
                _currentBobbleTween = character.BobblingPivot.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 0.2f).SetEase(Ease.OutSine);
            }
            
            _lastMumbleTime = Time.time;
        }
    }
}
