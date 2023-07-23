using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Insomnia{
	public class Notifier : MonoBehaviour {
		[Header("Notifier: References")]
		[SerializeField] private GameObject m_notiUI = null;
		[SerializeField] private Image m_notiImage = null;

        private void Awake() {

        }

        public void Initialize(Stencil image) {
			if(m_notiUI == null)
				return;

			if(m_notiImage == null)
				return;

            m_notiImage.sprite = image.MaskVisualSprite;
			StartCoroutine(CoScaler(1f));
		}
		
		public void OnClick_OK() {
            StartCoroutine(CoScaler(0f));
        }

		private IEnumerator CoScaler(float size) {
            while(true) {
				m_notiUI.transform.localScale = Vector3.Lerp(m_notiUI.transform.localScale, new Vector3(size, size, size), 0.1f);
				if(Mathf.Abs(size - m_notiUI.transform.localScale.x) <= 0.01f)
					break;

				yield return null;
			}

			m_notiUI.transform.localScale = new Vector3(size, size, size);
			yield break;
		}
	}
}
