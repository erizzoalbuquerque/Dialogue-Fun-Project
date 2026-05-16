using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    [CreateAssetMenu(fileName = "CharacterVoice", menuName = "Scriptable Objects/CharacterVoice")]
    public class CharacterVoiceSO : ScriptableObject
    {
        public List<AudioClip> TypewriterSounds;
        public float BasePitch = 1.0f;
        public Vector2 PitchVariationRange = new Vector2(-0.1f, 0.1f);
        public float CooldownDuration = 0.1f;
        public float BaseVolume = 1.0f;
    }
}
