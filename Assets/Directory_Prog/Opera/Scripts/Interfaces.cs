using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OperaHouse {
    public interface Interactable {
        public void StartInteract();
        public void FinishInteract();
    }

    public interface Mountable {
        /// <summary>
        /// 설치를 시작할 때 호출하는 함수
        /// </summary>
        public void StartMount();

        //TODO: 아마 설치 대상 오브젝트의 커스텀 컴포넌트랑 위치 전달해줘야될듯?
        /// <summary>
        /// 설치할 위치를 성공적으로 설정했을 때 호출하는 함수
        /// </summary>
        public void MountOn();

        /// <summary>
        /// 설치에 실패했거나 취소했을 때 호출하는 함수
        /// </summary>
        public void FinishMount();
    }

    public interface QuestReceiver{
        
    }
}