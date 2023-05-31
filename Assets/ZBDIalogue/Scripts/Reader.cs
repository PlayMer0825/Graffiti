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

        private Transform m_currentHolder;
        // 플레이어 객체 추가..
        private Transform m_playerTransform;

        private void OnTriggerEnter(Collider other)
        {
            //Holder 클래스 들고있는 오브젝트와 충돌
            if (other.gameObject.layer == m_targetLayer)
            {
                //Holder 클래스 가져오는데에 성공
                if(other.TryGetComponent(out m_holder) && !m_machine.m_Interacting)
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
                        m_currentHolder = other.transform;
                        m_readableShower.ShowStart(m_currentHolder);
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
            if (m_inputWaiting && !m_machine.m_Interacting)
            {
                NewExport();
                m_readableShower.ShowStop();
            }

            //다이얼로그 계속 진행
            else if (m_machine.m_Interacting)
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


            // 잠깐 추가한 부분... Holder 의 위치로 플레이어를 이동 , 그리고 Holder 에서 지정한 target 오브젝트를 향해서 바라봄(설정해주면 됨)
            m_playerTransform.position = m_currentHolder.position;
            Vector3 targetPosition = m_currentHolder.GetComponent<Holder>().targetObject.position;
            targetPosition.y = m_playerTransform.position.y;
            m_playerTransform.LookAt(targetPosition);

            m_playerTransform.gameObject.GetComponent<Animator>().SetFloat("moveWeight_Side", 0f);
            

        }

        private void Awake()
        {
            m_machine = FindObjectOfType<DialogueMachine>();
            m_readableShower = FindObjectOfType<ReadableShower>();
            m_playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                OnBtnInput();
            }

            if(!m_machine.m_Interacting && m_inputWaiting && !m_readableShower.m_Showing)
            {
                m_readableShower.ShowStart(m_currentHolder);
            }
        }
    }
}