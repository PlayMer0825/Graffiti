using UnityEngine;

namespace Insomnia{
	public enum SFX_GraffitiUI {
		Stencil_Finished = 0,
		Blackbook_Open,
		Blackbook_Close,
		Blackbook_PageShift,
		Bag_Open,
		Bag_Close,
		Bag_Select,
	}

	[RequireComponent(typeof(AudioSource))]
	public class Speaker : MonoBehaviour {
		[SerializeField] private AudioSource m_source = null;
		[SerializeField] private AudioClip[] m_clips = null;

        private void Awake() {
            m_source = GetComponent<AudioSource>();
        }

        //public void Play(SFX_GraffitiUI sfxType) {
        //	int type = (int)sfxType;

        //	if(CheckVaild(type) == false)
        //		return;

        //	m_source.clip = m_clips[type];
        //	m_source.Play();
        //}

        public void PlayOneShot(SFX_GraffitiUI sfxType) {
            int type = (int)sfxType;
			if(m_clips == null)
				return;

			if(m_clips.Length <= 0)
				return;

            if(CheckVaild(type) == false)
                return;

			m_source.clip = m_clips[type];
			m_source.PlayOneShot(m_clips[type]);
        }

        private bool CheckVaild(int val) {
            if(val < 0)
                return false;

            if(m_clips.Length <= val)
                return false;

            if(m_clips[val] == null)
                return false;

			return true;
        }
	}
}
