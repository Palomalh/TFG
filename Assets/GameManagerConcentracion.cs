using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class GameManagerConcentracion : MonoBehaviour
{
    public GameObject ball;
    public GameObject[] obstaculos;
    public Vector3 goal;  // Posicion de la meta
    public Camera mainCamera;

    private bool isDragging = false;
    private Vector3 startPosition;
    private Vector3 targetPosition;  // Posición objetivo hacia donde mover la bola

    private Vector2 mousePosition;
    private Vector2 mouseCanceledPos;
    private Vector2 dirMovimiento;

    private int currentObstaculo;
    private bool esObstaculo;

    [SerializeField] private float moveSpeed = 5f;  // Velocidad de movimiento de la bola
    [SerializeField] private float snapThreshold = 0.5f;  // Umbral para determinar si la bola debe "encajar" en una casilla

    [SerializeField] private CharacterController characterController;
    [SerializeField] private CharacterController[] obstaculoControllers;

    public InputAction inputPosition;
    public InputAction ClickAction;

    private Vector3 WorldPos
    {
        get
        {
            float z = mainCamera.WorldToScreenPoint(transform.position).z;
            return mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, z));
        }
    }
    private bool isClickedOn
    {
        get
        {
            Ray ray = mainCamera.ScreenPointToRay(mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                return hit.collider.transform == ball.transform;
            }
            return false;
        }
    }

    private void Awake()
    {
        inputPosition.Enable();
        ClickAction.Enable();
       // DragAction.performed += GetMousePosition;
        //DragAction.started += ClickAndDrag;
        ClickAction.performed += GetMousePosition;
        ClickAction.canceled += InputReleased;
       // DragAction.canceled += StopDragging;

    }

    private IEnumerator Drag()
    {
        isDragging = true;
        Vector3 offset = ball.transform.position - WorldPos;
        // grab
        while (isDragging)
        {
            // dragging
            ball.transform.position = WorldPos + offset;
            yield return null;
        }
        //drop
    }

    private void GetMousePosition(InputAction.CallbackContext context)
    {
        Debug.Log("GET MOUSE POSITION");
        mousePosition = inputPosition.ReadValue<Vector2>();
    }

    private void ClickAndDrag(InputAction.CallbackContext context)
    {
        Debug.Log("CLICK AND DRAG");
       mousePosition = context.ReadValue<Vector2>();

        /*  if (isClickedOn)
          {
              StartCoroutine(Drag());
          }
        */
    }

    private void InputReleased(InputAction.CallbackContext context)
    {
        mouseCanceledPos = inputPosition.ReadValue<Vector2>();
        dirMovimiento =  mouseCanceledPos - mousePosition;
        Vector2 vectorNormalizado = dirMovimiento.normalized;
        float x = Mathf.Abs(vectorNormalizado.x);
        float y = Mathf.Abs(vectorNormalizado.y);

        Debug.Log(vectorNormalizado + " " + x + " " + y);
        if (x > y)
        {
            Debug.Log("Movimiento horizontal");

            if (dirMovimiento.x >= 0)
            {
                Debug.Log("Derecha");
                characterController.Move(new Vector3(1, 0, 0));
            }else
            {
                characterController.Move(new Vector3(-1, 0, 0));
                Debug.Log("izquierda");
            }
        }
        else
        {
            Debug.Log("movimiento vertical");
            if (dirMovimiento.y >= 0)
            {
                characterController.Move(new Vector3(0, 0, 1));
                Debug.Log("Arriba");
            }
            else
            {
                characterController.Move(new Vector3(0, 0, -1));
                Debug.Log("abajo");
            }
        }

    }
}

/*    private void Start()
    {
        esObstaculo = false;
        characterController = ball.GetComponent<CharacterController>();
        startPosition = Vector3.zero;  // Inicializa la posición inicial de la bola en el origen
        characterController.transform.position = startPosition;  // Coloca la bola en la posición inicial
        goal = new Vector3(0, 0, -5);

        obstaculoControllers = new CharacterController[obstaculos.Length];
        for (int i = 0; i < obstaculos.Length; i++)
        {
            obstaculoControllers[i] = obstaculos[i].GetComponent<CharacterController>();
        }

        DragAction.action.performed += OnMouseDrag;
        ClickAction.action.performed += OnClick;
    }

    private void Update()
    {
        if (characterController.transform.position == goal)
        {
            Debug.Log("HAS GANADO");
        }

       
        }

    public void OnMouseUp(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isDragging = false;
        }
    }


    public void OnMouseDrag(InputAction.CallbackContext obj)
    {


        /*Vector2 mousePosition = obj.ReadValue<Vector2>();
        Vector3 movement = new Vector3(Mathf.Round(mousePosition.x), 0f, Mathf.Round(mousePosition.y));
        CharacterController controller = esObstaculo ? obstaculoControllers[currentObstaculo] : characterController;
        float deltaX = movement.x - startPosition.x;
        float deltaZ = movement.z - startPosition.z;

        Debug.Log("newPosition X en dragging =" + movement.x);
        Debug.Log("newposition Z en dragging =" + movement.z);

        if (Mathf.Abs(deltaX) > Mathf.Abs(deltaZ))
        {

            targetPosition = new Vector3(Mathf.Round(movement.x), 0f, Mathf.Round(startPosition.z));
        }
        else
        {
            targetPosition = new Vector3(Mathf.Round(startPosition.x), 0f, Mathf.Round(movement.z));
        }
        if (Vector3.Distance(controller.transform.position, targetPosition) > snapThreshold)
        {

            Debug.Log("Entra al move");
            controller.Move(targetPosition);
            //controller.Move((targetPosition - controller.transform.position).normalized * moveSpeed * Time.deltaTime);
        }
        
        controller.Move(controller.transform.position + movement);
        

        if (!isDragging)
        {
            isDragging = true;
            initialPosition = Mouse.current.position.ReadValue();
        }

        Vector2 currentMousePosition = Mouse.current.position.ReadValue();
        Vector2 mousePosition = obj.ReadValue<Vector2>();


        Vector2 mouseDelta = currentMousePosition - initialPosition;

        Vector3 movement = new Vector3(Mathf.Round(mouseDelta.x), 0f, Mathf.Round(mouseDelta.y));
        CharacterController controller = esObstaculo ? obstaculoControllers[currentObstaculo] : characterController;
        controller.Move(controller.transform.position + movement);

    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (Physics.Raycast(mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()), out RaycastHit hit))
        {
            if (hit.collider.transform == ball.transform)
            {
                startPosition = characterController.transform.position;  // Guarda la posición inicial de la bola
                esObstaculo = false;  // Inicializa esObstaculo como falso
            }
            for (int i = 0; i < obstaculos.Length; i++)
            {
                if (hit.collider.transform == obstaculos[i].transform)
                {
                    startPosition = obstaculos[i].transform.position;  // Guarda la posición inicial de la bola
                    currentObstaculo = i;
                    esObstaculo = true;  // Establece esObstaculo como verdadero si se selecciona un obstáculo
                }
            }
        }

    }

    private void OnEnable()
    {
        DragAction.action.Enable();
        ClickAction.action.Enable();
        
    }

    private void OnDisable()
    {
        DragAction.action.Disable();
        ClickAction.action.Disable();
    }



}*/

    /*private void Update()
    {
        if (characterController.transform.position == goal)
        {
            Debug.Log("HAS GANADO");
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                if (hit.collider.transform == ball.transform)
                {
                    isDragging = true;  // Comienza a arrastrar la bola
                    startPosition = characterController.transform.position;  // Guarda la posición inicial de la bola
                    esObstaculo = false;  // Inicializa esObstaculo como falso
                }
                for (int i = 0; i < obstaculos.Length; i++)
                {
                    if (hit.collider.transform == obstaculos[i].transform)
                    {
                        isDragging = true;  // Comienza a arrastrar la bola
                        startPosition = obstaculos[i].transform.position;  // Guarda la posición inicial de la bola
                        currentObstaculo = i;
                        esObstaculo = true;  // Establece esObstaculo como verdadero si se selecciona un obstáculo
                    }
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;  // Se deja de arrastrar la bola
            esObstaculo = false;
            SnapBallToNearestCell();  // Hace que la bola se ajuste a la casilla más cercana
            //MoveObject();  // Mueve la bola u obstáculo hacia la posición objetivo
        }

        if (isDragging)
        {
            //Debug.Log (mainCamera.WorldToScreenPoint(GetObjectPosition()).z);
            Vector3 newPosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.WorldToScreenPoint(GetObjectPosition()).x));

            float deltaX = newPosition.x - startPosition.x;
            float deltaZ = newPosition.z - startPosition.z;

            Debug.Log("newPosition X en dragging =" + newPosition.x);
            Debug.Log("newposition Z en dragging =" + newPosition.z);

            if (Mathf.Abs(deltaX) > Mathf.Abs(deltaZ))
            { 

                targetPosition = new Vector3(Mathf.Round(newPosition.x), 0f, Mathf.Round(startPosition.z));
            }
            else
            {
                targetPosition = new Vector3(Mathf.Round(startPosition.x), 0f, Mathf.Round(newPosition.z));
            }
            MoveObject();  // Mueve la bola u obstáculo hacia la posición objetivo
        }
    }

    private void MoveObject()
    {
    CharacterController controller = esObstaculo ? obstaculoControllers[currentObstaculo] : characterController;
        if (Vector3.Distance(controller.transform.position, targetPosition) > snapThreshold)
        {
            Debug.Log("Entra al move");
            controller.Move(new Vector3(0,0,-0.1f));

            //controller.Move((targetPosition - controller.transform.position).normalized * moveSpeed * Time.deltaTime);
        }
    }

    private void SnapBallToNearestCell()
    {
        int x = Mathf.RoundToInt(characterController.transform.position.x);
        int z = Mathf.RoundToInt(characterController.transform.position.z);
        targetPosition = new Vector3(x, 0f, z);  // Ajusta la posición objetivo a la casilla más cercana
        Debug.Log("Ajuste a la casilla más cercana en SnapBall" + x + " "+ z);
    }


    private Vector3 GetObjectPosition()
    {
        if (esObstaculo)
        {
            return obstaculos[currentObstaculo].transform.position;
        }
        else
        {
            return characterController.transform.position;
        }
    }
}
    */