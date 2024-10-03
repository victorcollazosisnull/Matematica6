using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Mathematics.Week6
{
    public class PlaneController : MonoBehaviour
    {
        [Header("Vida")]
        public int vidaActual = 5;

        [Header("Controls Properties")]
        [SerializeField] private float pitchPlane;
        [SerializeField] private float pitchGain = 1f;
        [SerializeField] private MinMax pitchTreshHold;
        [SerializeField] private float rollPlane;
        [SerializeField] private float rollhGain = 1f;
        [SerializeField] private MinMax rollTreshHold;

        [Header("Rotation Data")]
        [SerializeField] private Quaternion qx = Quaternion.identity; //<0,,0,0,1>
        [SerializeField] private Quaternion qy = Quaternion.identity; //<0,,0,0,1>
        [SerializeField] private Quaternion qz = Quaternion.identity; //<0,,0,0,1>
        [Header("Sonidos")]
        public AudioSource sound;
        [SerializeField] private Quaternion r = Quaternion.identity; //<0,,0,0,1>
        private float anguloSen;
        private float anguloCos;

        protected float _pitchDirection = 0f;
        protected float _rollDirection = 0f;

        private void FixedUpdate()
        {
            pitchPlane += _pitchDirection * pitchGain;

            pitchPlane = Mathf.Clamp(pitchPlane, pitchTreshHold.MinValue, pitchTreshHold.MaxValue);

            rollPlane += _rollDirection * rollhGain;

            rollPlane = Mathf.Clamp(rollPlane, rollTreshHold.MinValue, rollTreshHold.MaxValue);

            //rotacion z -> x -> y
            anguloSen = Mathf.Sin(Mathf.Deg2Rad * rollPlane * 0.5f);
            anguloCos = Mathf.Cos(Mathf.Deg2Rad * rollPlane * 0.5f);
            qz.Set(0, 0, anguloSen, anguloCos);

            anguloSen = Mathf.Sin(Mathf.Deg2Rad * pitchPlane * 0.5f);
            anguloCos = Mathf.Cos(Mathf.Deg2Rad * pitchPlane * 0.5f);
            qx.Set(anguloSen, 0, 0, anguloCos);

            /*anguloSen = Mathf.Sin(Mathf.Deg2Rad * rollPlane * 0.5f);
            anguloCos = Mathf.Cos(Mathf.Deg2Rad * rollPlane * 0.5f);
            qy.Set(0, anguloSen, 0, anguloCos);*/

            //multiplicación y -> x -> z
            r = qy * qx * qz;
            if (_horizontalDirection == 0f && _verticalDirection == 0f)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.identity, Time.deltaTime * 12f);  
            }
            else
            {
                transform.rotation = r;
            }

            UpdatePosition();
        }

        

        //Pitch -> X Axis
        public void RotatePitch(InputAction.CallbackContext context)
        {
            _pitchDirection = context.ReadValue<float>();
        }

        //Roll -> Z Axis
        public void RotateRoll(InputAction.CallbackContext context)
        {
            _rollDirection = context.ReadValue<float>();
        }




        private float _verticalDirection = 0f;
        private float _horizontalDirection = 0f;
        [Header("Velocidad del Avión")]
        [SerializeField] private float velocitySpeed = 5f;

        private Rigidbody _myRB;

        private void Start()
        {
            _myRB = GetComponent<Rigidbody>();
        }

        public void TranslateVertical(InputAction.CallbackContext context)
        {
            _verticalDirection = context.ReadValue<float>();
        }

        public void TranslateHorizontal(InputAction.CallbackContext context)
        {
            _horizontalDirection = context.ReadValue<float>();
        }

        private void UpdatePosition()
        {
            _myRB.velocity = new Vector3(-_horizontalDirection * velocitySpeed, -_verticalDirection * velocitySpeed, 0f);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("enemigo"))
            {
                sound.Play();
                Damage();
            }
        }
        void Damage()
        {
            vidaActual = vidaActual - 1;
        }
        private void Update()
        {
            if (vidaActual <= 0)
            {
                Destroy(gameObject);
                SceneManager.LoadScene("gameover");
            }
        }
    }

    [System.Serializable]
    public struct MinMax
    {
        public float MinValue;
        public float MaxValue;
    }
}
