using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

/// <summary>
///  다이얼로그 상호작용 대상 오브젝트에 들어가야 함 
/// </summary>
namespace OperaHouse
{
    public class DialogObject : Interactable
    {
        [SerializeField]
        private InteractionArea _interactArea = null;

        [SerializeField]
        private GameObject _interactUiPos;

        [SerializeField] private bool is_P_InteractRangeCheck;
        [SerializeField] public bool isDialogMode;

        #region Unity Event Functions

        private void Awake()
        {
            
            _interactArea = GetComponentInChildren<InteractionArea>();

            if(_interactCanvas)
            {
                /// 다이얼로그 상호작용 오브젝트의 추가
                _interactCanvas = Instantiate(_interactCanvas, _interactUiPos.transform.position, Quaternion.identity, transform);
                _interactCanvas.SetActive(false);
            }
 
            isDialogMode = false;

            if (_interactArea == null) return;
        }
        #endregion

        public override void ReadyInteract(Collider other)
        {
            base.ReadyInteract(other);

            is_P_InteractRangeCheck = true;

            _interactCanvas.SetActive(true);

            StartInteract();

        }
        public override void UnReadyInteract(Collider other)
        {
            base.UnReadyInteract(other);

            is_P_InteractRangeCheck = false;

           _interactCanvas.SetActive(false);

            // 다이얼로그 UI 해제 
            Debug.Log("나가기1");
            FinishInteract();
        }
        public override void StartInteract()
        {

            _interactArea.SetColliderActivation(false);
            InteractionManager.Instance.StartedInteract(this);

        }

        public override void FinishInteract()
        {
            //TODO: 다이얼로그 기능 OFF
            _interactArea.SetColliderActivation(true);
            InteractionManager.Instance.FinishedInteract();

        }


    }


 
}
