using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
    /// <summary>
    /// Moves and Rotate a game object to a specified position and rotation over time. The position can be defined by a transform in another object (using To Transform) or by setting an absolute position (using To Position, if To Transform is set to None).).
    /// </summary>
    [CommandInfo("LeanTween",
                 "Move and Rotate",
                 "Moves and Rotate a game object to a specified position and rotation over time. The position can be defined by a transform in another object (using To Transform) or by setting an absolute position (using To Position, if To Transform is set to None).")]
    [AddComponentMenu("")]
    [ExecuteInEditMode]
    public class MoveAndRotateLean : BaseLeanTweenCommand
    {
        [Tooltip("Target transform that the GameObject will move to")]
        [SerializeField]
        protected TransformData _toTransform;

        [Tooltip("Target world position that the GameObject will move to, if no From Transform is set")]
        [SerializeField]
        protected Vector3Data _toPosition;

        [Tooltip("Target rotation that the GameObject will rotate to, if no To Transform is set")]
        [SerializeField]
        protected Vector3Data _toRotation;

        [Tooltip("Whether to animate in world space or relative to the parent. False by default.")]
        [SerializeField]
        protected bool isLocal;

        public enum RotateMode { PureRotate, LookAt2D, LookAt3D }
        [Tooltip("Whether to use the provided Transform or Vector as a target to look at rather than a euler to match.")]
        [SerializeField]
        protected RotateMode rotateMode = RotateMode.PureRotate;


        public override LTDescr ExecuteTween()
        {
            //Move

            var loc = _toTransform.Value == null ? _toPosition.Value : _toTransform.Value.position;

            if (IsInAddativeMode)
            {
                loc += _targetObject.Value.transform.position;
            }

            if (IsInFromMode)
            {
                var cur = _targetObject.Value.transform.position;
                _targetObject.Value.transform.position = loc;
                loc = cur;
            }

            if (isLocal)
                LeanTween.moveLocal(_targetObject.Value, loc, _duration);
            else
                LeanTween.move(_targetObject.Value, loc, _duration);

            //Rotate

            var rot = _toTransform.Value == null ? _toRotation.Value : _toTransform.Value.rotation.eulerAngles;

            if (rotateMode == RotateMode.LookAt3D)
            {
                var pos = _toTransform.Value == null ? _toRotation.Value : _toTransform.Value.position;
                var dif = pos - _targetObject.Value.transform.position;
                rot = Quaternion.LookRotation(dif.normalized).eulerAngles;
            }
            else if (rotateMode == RotateMode.LookAt2D)
            {
                var pos = _toTransform.Value == null ? _toRotation.Value : _toTransform.Value.position;
                var dif = pos - _targetObject.Value.transform.position;
                dif.z = 0;

                rot = Quaternion.FromToRotation(_targetObject.Value.transform.up, dif.normalized).eulerAngles;
            }

            if (IsInAddativeMode)
            {
                rot += _targetObject.Value.transform.rotation.eulerAngles;
            }

            if (IsInFromMode)
            {
                var cur = _targetObject.Value.transform.rotation.eulerAngles;
                _targetObject.Value.transform.rotation = Quaternion.Euler(rot);
                rot = cur;
            }

            if (isLocal)
                return LeanTween.rotateLocal(_targetObject.Value, rot, _duration);
            else
                return LeanTween.rotate(_targetObject.Value, rot, _duration);
        }

        public override bool HasReference(Variable variable)
        {
            return _toTransform.transformRef == variable || _toPosition.vector3Ref == variable || base.HasReference(variable);
        }
    }
}