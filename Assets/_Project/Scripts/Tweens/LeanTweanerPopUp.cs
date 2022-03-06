using UnityEngine;
using UnityEngine.Events;

public class LeanTweanerPopUp : MonoBehaviour
{
    [SerializeField]
    private LeanTweenType typePopUpAnim = LeanTweenType.easeOutBack, typeCloseAnim = LeanTweenType.easeInOutBack;

    [SerializeField]
    private float delayOfAnim = 0.05f, durationOfAnim = 0.3f, delayToClosePopUp = 0f;

    [SerializeField]
    private bool popUpOnEnable = true;

    [SerializeField]
    private bool disableObjectOnDisable;

    [SerializeField]
    private bool _playSoundOnEnable = true;

    [SerializeField]
    private UnityEvent OnPopUpStartToOpen;

    [SerializeField]
    private UnityEvent OnPopUpOpened;

    [SerializeField]
    private UnityEvent OnPopUpClosed;


    private Vector3 originalLocalScale;

    private void Awake()
    {
        SaveOriginalLocalScale();
    }

    private void OnEnable()
    {
        if (popUpOnEnable)
            AnimatePopUp();
        else
        {
            SetSizeToZero();
        }
    }

    void SaveOriginalLocalScale()
    {
        originalLocalScale = transform.localScale;
    }

    public void AnimatePopUp()
    {
        SetSizeToZero();

        LeanTween.scale(gameObject, originalLocalScale, durationOfAnim).setDelay(delayOfAnim).setEase(typePopUpAnim).setOnComplete(HandlePopUpOpened).setOnStart(HandlePopUpStart);

        if (delayToClosePopUp > 0)
        {
            Invoke("CloseAnim", delayToClosePopUp);
        }
    }

    void SetSizeToZero()
    {
        transform.localScale = Vector3.zero;
    }

    public void CloseAnim()
    {
        LeanTween.scale(gameObject, Vector3.zero, durationOfAnim).setDelay(delayOfAnim).setEase(typeCloseAnim).setOnComplete(HandlePopUpClosed);
    }

    void HandlePopUpStart()
    {
        OnPopUpStartToOpen?.Invoke();
    }

    void HandlePopUpOpened()
    {
        /*
        if (_playSoundOnEnable)
            AudioManager.Instance.Play("PopUp");
            */

        OnPopUpOpened?.Invoke();
    }

    void HandlePopUpClosed()
    {
        OnPopUpClosed?.Invoke();
        gameObject.SetActive(!disableObjectOnDisable);
    }
}