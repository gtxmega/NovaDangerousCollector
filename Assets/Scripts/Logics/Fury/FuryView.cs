using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace Logics.Fury
{
    public class FuryView : MonoBehaviour
    {
        [SerializeField] private float _duration;
        [SerializeField] private float _speed;

        [SerializeField] private Image _insideImage;
        [SerializeField] private Image _outsideImage;

        [SerializeField] private RectTransform _outsideRect;

        private void Start()
        {
            _outsideRect.sizeDelta *= 2.0f;
            StartCoroutine(Anim());
        }

        private IEnumerator Anim()
        {
            float duration = _duration;
            float speed = (_outsideRect.sizeDelta.x / 2) / _duration;

            Vector2 velocity = new Vector2(speed, speed);

            while (duration > 0.0f)
            {
                _outsideRect.sizeDelta -= velocity * Time.deltaTime;
                duration -= Time.deltaTime;
                yield return null;
            }
        }
    }
}