using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class BackgroundController : MonoBehaviour
{
    [SerializeField] private RawImage _image;
    [SerializeField] float _x, _y;

    private void Update()
    {
        _image.uvRect = new Rect(_image.uvRect.position + new Vector2(_x,_y) * Time.deltaTime, _image.uvRect.size);
    }
}