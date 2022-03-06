using System;
using UnityEngine;

/// <summary>
/// Add this script to a gameobject to rotate it using leantween
/// </summary>
public class LeanScale : CustomLeanTween
{

    /// <summary>
    /// Whether to start fading on start again after disabling
    /// </summary>
    [Tooltip("Whether to start fading on start again after disabling")]
    [SerializeField]
    private bool repeatOnDisable;
    /// <summary>
    /// Which scale the object should animate to
    /// </summary>
    [Tooltip("Scale the object should animate to")]
    [SerializeField]
    private Vector3 endScale;

    /// <summary>
    /// The initial scale of the object before animating
    /// </summary>

    [SerializeField] private bool useCustomScale;
    [SerializeField] private bool startAtCustomScale;
    [SerializeField] private Vector3 customInitialScale;

    private Vector3 initialScale;
    private bool atEndScale;

 
    new void Awake()
    {
        atEndScale = false;
        initialScale = useCustomScale ? customInitialScale : transform.localScale;

        if (startAtCustomScale)
        {
            transform.localScale = customInitialScale;
        }
        base.Awake();
    }

    new void OnDisable()
    {        
        base.OnDisable();
        if (repeatOnDisable)
        {
            gameObject.transform.localScale = initialScale;
            atEndScale = false;
        }
    }

    /// <summary>
    /// Animate the object
    /// </summary>
    public override void Animate()
    {
        if (loop)
            LeanTween.scale(gameObject, atEndScale ? initialScale : endScale, duration).setEase(easingStyle).setDelay(delay).setOnComplete(() => Animate());
        else
            LeanTween.scale(gameObject, atEndScale ? initialScale : endScale, duration).setEase(easingStyle).setDelay(delay);

        atEndScale = !atEndScale;
    }

    public void CloseScale()
    {
        LeanTween.scale(gameObject, initialScale, duration).setEase(easingStyle).setDelay(delay);
    }

    public void OpenScale()
    {
        LeanTween.scale(gameObject, endScale, duration).setEase(easingStyle).setDelay(delay);
    }

    /// <summary>
    /// Custom rotation with more options
    /// </summary>
    public void CustomScaleObject(Vector3 toScale, float customDelay = 0, Action onComplete = null)
    {
        if (loop)
            LeanTween.scale(gameObject, toScale, duration).setEase(easingStyle).setDelay(customDelay > 0 ? customDelay : delay).setLoopPingPong();
        else
            LeanTween.scale(gameObject, toScale, duration).setEase(easingStyle).setDelay(customDelay > 0 ? customDelay : delay).setOnComplete(onComplete);
    }
}