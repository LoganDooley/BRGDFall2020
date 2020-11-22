using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using environ=System.Environment;

namespace FallingDream.System {
    public class SpawnObjects : MonoBehaviour
    {
        [Header("Spawn Location/Frequency")]
        // public float m_radius = 5f; // radius of area in x that objects can appear in
        public float m_spawnY = 0f;
        public float m_maxY = 10f;
        public int m_objsPerSec = 1;

        [Header("Object Behavior")]
        public float m_speed = 10f;
        public float m_torque = 10f;

        [Header("Related Objects")]
        public GameObject[] m_possibleObjects;
        public GameObject m_player;

        [Header("Spawn Distribution and Allowable Area")]
        [Range(0.1f, 10.0f)]
        public float m_spawnSigma;
        public float m_allowableOutOfBoundsRadius;

        private FallingDream.Player.SimplePlayerMovement _playerScript;
        private float m_radius;


        private float timeSinceSpawn = 0.0f;
        private int objCount = 0;

        // Start is called before the first frame update
        void Start()
        {
            Application.targetFrameRate = 60;
            Random.InitState(environ.TickCount);
            _playerScript = m_player.GetComponent<FallingDream.Player.SimplePlayerMovement>();
            m_radius = _playerScript.MaxDisplacement;
        }

        // Update is called once per frame
        void Update()
        {
            objCount++;
            timeSinceSpawn += Time.deltaTime;
            if (timeSinceSpawn >= 1.0 / m_objsPerSec) {
                timeSinceSpawn = timeSinceSpawn % (1.0f / m_objsPerSec);
                GameObject obj = m_possibleObjects[Random.Range(0, m_possibleObjects.Length)];
                GameObject objectInstance;
                if (objCount % 2 == 0) {
                    Vector2 objPos = Sample2DGaussian(new Vector2(_playerScript.transform.position.x, _playerScript.transform.position.z), m_spawnSigma, m_radius, m_allowableOutOfBoundsRadius);
                    objectInstance = Instantiate(
                        obj,
                        new Vector3(objPos.x, m_spawnY, objPos.y), // object position
                        Quaternion.identity);
                } else {
                    float rad = m_radius + m_allowableOutOfBoundsRadius;
                    objectInstance = Instantiate(
                        obj,
                        new Vector3(Random.Range(-rad, rad), m_spawnY, Random.Range(-rad, rad)), // object position
                        Quaternion.identity
                );
                }

                Rigidbody rb = objectInstance.GetComponent<Rigidbody>();
                float timeAlive = m_maxY / m_speed;
                float thrust = rb.mass * m_speed;
                rb.AddForce(objectInstance.transform.up * thrust, ForceMode.Impulse);
                rb.AddTorque(new Vector3(UnityEngine.Random.Range(0, m_torque), UnityEngine.Random.Range(0, m_torque), Random.Range(0, m_torque)));

                // Destroy(objectInstance, timeAlive);
            }
        }

        static Vector2 Sample2DGaussian(Vector2 mean, float sigma, float radiusBound, float outOfBoundsRad) {
            int i = 0;
            while (i < 100) {
                i++;
                float u1 = Random.Range(0.0f, 1.0f);
                float u2 = Random.Range(0.0f, 1.0f);

                float R = Mathf.Sqrt(-2 * Mathf.Log(u1));
                float th = 2 * Mathf.PI * u2;

                float x = R * Mathf.Cos(th);
                float y = R * Mathf.Sin(th);

                Vector2 toRet = new Vector2(x, y) * sigma + mean;

                if (Mathf.Abs(toRet.x) > radiusBound + outOfBoundsRad || Mathf.Abs(toRet.y) > radiusBound + outOfBoundsRad) {
                    Debug.Log("Generated invalid point, retrying");
                    continue;
                }
                return toRet;

            }
            return new Vector2();

        }




    }
}
