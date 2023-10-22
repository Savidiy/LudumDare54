using TMPro;
using UnityEngine;

namespace LudumDare54
{
    public class FPS : MonoBehaviour
    {
        private bool _isActive;
        private float _updateTimer;

        public TMP_Text Text;
        public float UpdatePeriod = 0.15f;

        private void Awake()
        {
            Text.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F2))
            {
                _isActive = !_isActive;
                Text.gameObject.SetActive(_isActive);
            }

            if (!_isActive)
                return;

            _updateTimer -= Time.deltaTime;
            if (_updateTimer > 0)
                return;

            _updateTimer = UpdatePeriod;
            float fps = 1f / Time.deltaTime;
            Text.text = "FPS: "+fps.ToString("F0");
        }
    }
}