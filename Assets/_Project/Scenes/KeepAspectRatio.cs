using UnityEngine;

public class KeepAspectRatio : MonoBehaviour
{
    //Por padrão vamos manter o aspect ratio em 16:9, caso algum projeto específico precise
    // de outro aspect ratio, lembrar também de alterar nos elementos do canvas.
    [SerializeField] private float _targetAspectRatio = 16f / 9.0f;
    private Camera _myCamera;
    private Rect _cameraRect;

    private void Awake()
    {
        _myCamera = GetComponent<Camera>();
        _cameraRect = _myCamera.rect;
    }

    private void Update()
    {
        // determine the game window's current aspect ratio
        float windowaspect = (float)Screen.width / (float)Screen.height;

        // current viewport height should be scaled by this amount
        float scaleheight = windowaspect / _targetAspectRatio;

        // if scaled height is less than current height, add letterbox
        if (scaleheight < 1.0f)
        {
            _cameraRect.width = 1.0f;
            _cameraRect.height = scaleheight;
            _cameraRect.x = 0;
            _cameraRect.y = (1.0f - scaleheight) / 2.0f;

            _myCamera.rect = _cameraRect;
        }
        else // add pillarbox
        {
            float scalewidth = 1.0f / scaleheight;
            
            _cameraRect.width = scalewidth;
            _cameraRect.height = 1.0f;
            _cameraRect.x = (1.0f - scalewidth) / 2.0f;
            _cameraRect.y = 0;

            _myCamera.rect = _cameraRect;
        }
    }
}