using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace ZB.Dialogue.Graffiti
{
    public class ReadableShower : MonoBehaviour
    {
        public bool m_Showing { get => m_showing; }

        [SerializeField] private Transform m_tf_show;
        private Transform m_holderTarget;

        private bool m_showing;

        public void ShowStart(Transform holderTarget)
        {
            m_holderTarget = holderTarget;

            ShowPosFix_C = ShowPosFix();
            StartCoroutine(ShowPosFix_C);
        }

        public void ShowStop()
        {
            m_holderTarget = null;

            StopCoroutine(ShowPosFix_C);
            m_showing = false;
            m_tf_show.DOKill();
            m_tf_show.DOScale(Vector3.zero, 0.3f).SetEase(Ease.OutQuart);
        }

        private void Awake()
        {
            m_tf_show.gameObject.SetActive(true);
            m_tf_show.localScale = Vector3.zero;
        }

        IEnumerator ShowPosFix_C;
        IEnumerator ShowPosFix()
        {
            m_showing = true;

            m_tf_show.position = Camera.main.WorldToScreenPoint(m_holderTarget.position);

            yield return null;
            m_tf_show.DOKill();
            m_tf_show.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutQuart);

            while (true)
            {
                m_tf_show.position = Camera.main.WorldToScreenPoint(m_holderTarget.position);

                yield return null;
            }
        }
    }
}