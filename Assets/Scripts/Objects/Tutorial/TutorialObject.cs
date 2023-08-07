using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class TutorialObject : MonoBehaviour {
    [SerializeField] protected Canvas m_Canvas = null;
    [SerializeField] protected Image m_Image = null;
    [SerializeField] protected Sprite[] m_tutorialSprite = null;
    [SerializeField] protected bool m_isTemporal = true;
    [SerializeField] private bool m_opened = false;
    private bool m_activated = false;
    private bool m_disabled = false;
    private int m_spriteIndex = 0;

    public UnityEvent onTutorialFinished = new UnityEvent();

    protected int SpriteIndex { get => m_spriteIndex; }
    protected bool Activated { get => m_activated; }
    protected bool Disabled { get => m_disabled; }
    protected bool Opened { get => m_opened; }

    protected void Awake() {
        bool issue = true;

        issue &= ( m_Canvas == null || m_Image == null || m_tutorialSprite.Length <= 0 );

        if(issue) {
            gameObject.SetActive(false);
            return;
        }

        m_Image.sprite = m_tutorialSprite[0];

        if(m_isTemporal) {
            m_disabled = PlayerPrefs.GetInt($"{gameObject.name}_Tutorial") > 0;

            if(m_disabled) {
                gameObject.SetActive(false);
                m_disabled = true;
                return;
            }
        }

        OnAwake();
    }

    protected abstract void OnAwake();

    public virtual void Activate() {
        if(m_disabled)
            return;

        if(m_opened)
            return;

        m_activated = true;
        m_opened = true;
        OnClick();
        m_Canvas.gameObject.SetActive(true);
    }

    public virtual void OnClick() {
        if(m_disabled)
            return;

        if(m_opened == false)
            return;

        if(m_spriteIndex < m_tutorialSprite.Length) {
            m_Image.sprite = m_tutorialSprite[m_spriteIndex++];
        }
        else {
            Deactivate();
        }
    }

    public virtual void Deactivate() {
        if(m_disabled)
            return;

        if(m_opened == false)
            return;

        m_Canvas.gameObject.SetActive(false);
        m_opened = false;
        m_disabled = true;
        m_spriteIndex = 0;
        m_activated = false;

        onTutorialFinished?.Invoke();

        if(m_isTemporal) {
            PlayerPrefs.SetInt($"{gameObject.name}_Tutorial", 1);
            PlayerPrefs.Save();
        }
    }
}
