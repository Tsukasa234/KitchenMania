using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    //Singleton
    public static Player Instance { get; private set; }

    //Events
    public event EventHandler OnPickedSomething;
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }


    //Private Serialized Variables
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;

    //Private Variables
    private bool isWalking;
    private Vector3 lastInteractDir;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;

    private void Awake()
    {
        //Check if is more have than one instance of the player
        if (Instance != null)
        {
            Debug.Log("There is more than one player instance");
        }
            Instance = this;
    }

    private void Start()
    {
        //Subscribe to Input Events
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternativeAction += GameInput_OnInteractAlternativeAction;
    }

    //Function event of the Alternative Interaction Action Input
    private void GameInput_OnInteractAlternativeAction(object sender, EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaying())
        {
            return;
        }
        //Send the Counter that the user is looking
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }

    //Function Event of the Interaction Action Input
    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaying())
        {
            return;
        }
        //Send the Counter that the user is looking
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    private void Update()
    {
        //Call Functions
        HandleMovement();
        HandleInteraction();
    }

    //Is the player walking function
    public bool IsWalking()
    {
        return isWalking;
    }

    //Funtion of the Interactions Handles
    private void HandleInteraction()
    {
        //Vectors for the Move Direction of the Player
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        //Save the Last Direction 
        lastInteractDir = (moveDir == Vector3.zero) ? lastInteractDir : moveDir;
        //Distance for interaction
        float interactDistance = 2f;
        //Check if the raycast hit a GameObject
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, layerMask))
        {
            //Check if the Raycast hit a Counter
            if (raycastHit.transform.TryGetComponent(out BaseCounter counter))
            {
                //Has ClearCounter
                if (counter != selectedCounter)
                {
                    SetSelectedCounter(counter);
                }
            }
            //If the raycast no hit a counter
            else
            {
                SetSelectedCounter(null);
            }
        }
        //If the raycast no hit a counter
        else
        {
            SetSelectedCounter(null);
        }
    }

    //Function for the movement handler
    private void HandleMovement()
    {
        //Vectors for the Movement of the player
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        //variables for the movement and collision for the player
        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .65f;
        float playerHeight = 2f;
        //Check if the player is not hit with other static gameobject
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        //If the player can or cannot move
        if (!canMove)
        {
            //Cannot move towards moveDir

            //Attempt only X movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
            canMove = (moveDir.x < -.5f || moveDir.x > +.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            //If the player can move
            if (canMove)
            {
                //Can move only  on the X
                moveDir = moveDirX;
            }
            else
            {
                //cannot move only on the X

                //Attempt only Z movement
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = (moveDir.z < -.5f || moveDir.z > +.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                //If the player can move
                if (canMove)
                {
                    //Can move only on the Z
                    moveDir = moveDirZ;
                }
                else
                {
                    // Cannot move in any direction
                }
            }
        }
        //If the player can move
        if (canMove)
        {
            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }

        //Send if the player is walking
        isWalking = moveDir != Vector3.zero;

        //Logic for the rotation of the player
        float rotationSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
    }
    
    //Set the selected counter and send it through the event invoke
    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }

    //Get the Kitchen Object Hold Point for the transform
    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }

    //Set the kitchenObject to the player
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null)
        {
            OnPickedSomething?.Invoke(this, EventArgs.Empty);
        }
    }

    //Return the KitchenObject Function
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    //Clear the kitchen Object from the player
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    //return if kitchen object is null or not
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
