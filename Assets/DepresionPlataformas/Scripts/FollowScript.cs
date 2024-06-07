using StarterAssets;
using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour
{
    public GameObject explosionAlcanzado;
    public GameObject pulsador;
    public GameObject targetObj;
    public ThirdPersonController _thirdPersonController;
    private bool isMoving;
    public float speed = 1.0f;
    public float rotationSpeed = 100.0f; // Velocidad de rotación alrededor del jugador
    private bool orbiting = false;

    Rigidbody rb;
    public float launchForce = 10f; // Fuerza con la que el Enemy será lanzado
    public float disappearanceDelay = 2f; // Retraso antes de que el Enemy desaparezca
    private bool isLaunched = false;

    public float detectionRange = 10.0f;
    private bool hasDetectedPlayer = false;

    public GameObject prefabFloatingText;
    public GameObject InstancePrefabFloatingText;
    private string[] badThoughts = new string[]{ "No puedes hacerlo", "No eres capaz", "No lo vas a conseguir", "Vas a fallar", "No eres tan bueno", "Siempre te equivocas", "Nadie cree en ti"};
    private bool spawnController;

    // Start is called before the first frame update
    void Start()
    {
        
        spawnController = true;
        rb = GetComponent<Rigidbody>();
        isMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            if (!orbiting)
            {
                MoveTowardsPlayer();
            }
            else
            {
                OrbitPlayer();
            }
        }

        if (isLaunched && Time.timeSinceLevelLoad >= disappearanceDelay)
        {
            gameObject.SetActive(false);
        }

        if (InstancePrefabFloatingText != null)
        {
            // Hacer que el texto mire al jugador
            InstancePrefabFloatingText.transform.LookAt(targetObj.transform);
            // Invertir la rotación en el eje Y para que el texto no se vea al revés
            if (!orbiting)
            {
                InstancePrefabFloatingText.transform.Rotate(0, 180, 0);
            }
        }
    }

    private void OrbitPlayer()
    {
        if (targetObj != null)
        {

            // Mover en círculos alrededor del jugador
            transform.RotateAround(targetObj.transform.position, Vector3.up, rotationSpeed * Time.deltaTime);

            //Lanzar mensajes
            if (spawnController)
            {
                StartCoroutine(SpawnFloatingText());
            }

        }
    }

    void MoveTowardsPlayer()
    {
        if (targetObj != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, targetObj.transform.position);
            if (distanceToPlayer <= detectionRange)
            {
                hasDetectedPlayer = true;
                //Lanzar mensajes
                if (spawnController)
                {
                    StartCoroutine(SpawnFloatingText());
                }
            }
            if (hasDetectedPlayer)
            {
                transform.position = Vector3.MoveTowards(this.transform.position, targetObj.transform.position, speed * Time.deltaTime);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.audioManagerInstance.PlaySFX("SFX_Depr_BTAttack");
            GetComponent<SphereCollider>().enabled = false;
            orbiting = true;
            targetObj.GetComponent<CharacterController>().enabled = false;
            _thirdPersonController.ChangeScaredState();
            explosionAlcanzado.SetActive(true);
            pulsador.SetActive(true);
            pulsador.transform.GetChild(0).gameObject.SetActive(true);
            pulsador.transform.GetChild(1).gameObject.SetActive(true);
            pulsador.GetComponent<PulsadorScript>().SetEnemigo(gameObject);
            pulsador.GetComponent<PulsadorScript>().complete = false;
        }
    }

    public void GetOut(Vector3 direction)
    {
        // Aplicar fuerza para lanzar al Enemy
        rb.isKinematic = false;
        rb.AddForce(direction * launchForce, ForceMode.Impulse);
        isLaunched = true;

        // Iniciar la cuenta regresiva para la desaparición
        Invoke("Disappear", disappearanceDelay);
    }
    void Disappear()
    {
        // Desactivar el objeto Enemy
        gameObject.SetActive(false);
    }

    private IEnumerator SpawnFloatingText()
    {
        spawnController = false;
        yield return new WaitForSecondsRealtime(3f);
        InstancePrefabFloatingText = Instantiate(prefabFloatingText, transform.position, Quaternion.identity, transform);
        int indiceRandom = (int)UnityEngine.Random.Range(0, badThoughts.Length);
        InstancePrefabFloatingText.GetComponent<TMP_Text>().text = badThoughts[indiceRandom];
        spawnController = true;

    }
}
