using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ZB.Dialogue.Graffiti
{
    public class Reader : MonoBehaviour
    {
        [SerializeField] private int m_targetLayer;
        [SerializeField] private bool m_inputWaiting;

        private DialogueMachine m_machine;
        private ReadableShower m_readableShower;
        private Holder m_holder;

        private void OnTriggerEnter(Collider other)
        {
            //Holder 클래스 들고있는 오브젝트와 충돌
            if (other.gameObject.layer == m_targetLayer)
            {
                //Holder 클래스 가져오는데에 성공
                if(other.TryGetComponent(out m_holder))
                {
                    //강제 다이얼로그 입장
                    if (m_holder.m_ReadType == Holder.ReadType.enforce)
                    {
                        NewExport();
                    }

                    //버튼클릭 다이얼로그 입장 대기
                    else if (m_holder.m_ReadType == Holder.ReadType.btnInput)
                    {
                        m_inputWaiting = true;
                        m_readableShower.ShowStart();
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == m_targetLayer)
            {
                m_inputWaiting = false;
                m_readableShower.ShowStop();
            }
        }

        public void OnBtnInput()
        {
            //새 다이얼로그 입장
            if (m_inputWaiting && !m_machine.Interacting)
            {
                NewExport();
            }

            //다이얼로그 계속 진행
            else if (m_machine.Interacting)
            {
                m_machine.TryNext();
            }
        }

        private void NewExport()
        {
            //다이얼로그 시작
            UnityEvent uEvent_OnEscape;
            m_machine.NewExport(m_holder.GetEventID(out uEvent_OnEscape));
            m_machine.AddEscapeEvent(uEvent_OnEscape.Invoke);
        }

        private void Awake()
        {
            m_machine = FindObjectOfType<DialogueMachine>();
            m_readableShower = FindObjectOfType<ReadableShower>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                OnBtnInput();
                m_readableShower.ShowStop();
            }

            if(!m_machine.Interacting && m_inputWaiting && !m_readableShower.m_Showing)
            {
                m_readableShower.ShowStart();
            }
        }
    }
}