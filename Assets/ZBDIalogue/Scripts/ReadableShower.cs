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

        [SerializeField] Transform m_tf_show;

        private bool m_showing;

        public void ShowStart()
        {
            m_showing = true;
            m_tf_show.DOKill();
            m_tf_show.DOScale(Vector2.one, 0.3f).SetEase(Ease.OutQuart);
        }

        public void ShowStop()
        {
            m_showing = false;
            m_tf_show.DOKill();
            m_tf_show.DOScale(Vector2.zero, 0.3f).SetEase(Ease.OutQuart);
        }

        private void Awake()
        {
            m_tf_show.gameObject.SetActive(true);
            m_tf_show.localScale = Vector2.zero;
        }
    }
}