using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TowerDefence
{
    [CreateAssetMenu(menuName = "Tower Defence / Sound")]
    public class SoundProperties : ScriptableObject
    {
        [SerializeField]
        private AudioClip[] _sounds;

        public AudioClip this[Sound index] => _sounds[(int)index];

#if UNITY_EDITOR
        [CustomEditor(typeof(SoundProperties))]
        public class SoundInspector : Editor
        {
            private static readonly int _soundCount = Enum.GetValues(typeof(Sound)).Length;

            private new SoundProperties target => base.target as SoundProperties;

            public override void OnInspectorGUI()
            {
                if (target._sounds.Length < _soundCount)
                {
                    Array.Resize(ref target._sounds, _soundCount);
                }

                for (int i = 0; i < target._sounds.Length; i++)
                {
                    Undo.RecordObject(target, "Sound Scriptable object");
                    target._sounds[i] = EditorGUILayout.ObjectField($"{(Sound)i}:", target._sounds[i], typeof(AudioClip), false) as AudioClip;
                }
            }
        }
#endif
    }
}
