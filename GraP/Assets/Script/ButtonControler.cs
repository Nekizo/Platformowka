using UnityEngine;
//using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ButtonControler : MonoBehaviour
{
    [SerializeField] GameObject[] buttons;
    [SerializeField] private Vector3 scale;
    private int buttonNumber;
    private UIControls playerInputAction;
    [SerializeField] private Scene[] scenes;

    void Awake()
    {



        playerInputAction = new UIControls();
        playerInputAction.Move.Enable();

        playerInputAction.Move.Approval.performed += On;
        playerInputAction.Move.Arrows.performed += MoveJoystic;
        


    }
    private void Start()
    {
        MenuMenager.instance.MenuOnGlobal += ResetValue;
        buttons[0].transform.localScale = scale;
    }
    private void ResetValue()
    {
        buttonNumber = 0;
        setSize();
    }
    void On(InputAction.CallbackContext ctx)
    {
        if (MenuMenager.instance.activeMenu)
        {
            Play();
        }

    }

    private bool joystic;
    void MoveJoystic(InputAction.CallbackContext ctx)
    {
        if (MenuMenager.instance.activeMenu)
        {
            float y = ctx.ReadValue<Vector2>().y;
            if (Mathf.Abs(y) == 1)
            {
                buttonNumber -= Mathf.RoundToInt(y);
            }
            else
            {
                if (Mathf.Abs(Mathf.RoundToInt(y)) == 1)
                {
                    if (!joystic)
                    {
                        buttonNumber -= Mathf.RoundToInt(y);
                    }
                    joystic = true;
                }
                else
                {
                    joystic = false;
                }
            }
            //Debug.Log("MoveJoystic-"+y);

            if (buttons.Length - 1 < buttonNumber)
            {
                buttonNumber = 0;
            }
            if (0 > buttonNumber)
            {
                buttonNumber = buttons.Length - 1;
            }
            setSize();
        }
    }
    private void setSize()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttonNumber == i)
            {
                buttons[i].transform.localScale = scale;
            }
            else
            {
                buttons[i].transform.localScale = Vector3.one;
            }

        }
    }
    private void Play()
    {
        if (buttonNumber==0)
        {
            MenuMenager.instance.MenuOff();
        }
        else if(buttonNumber == 1)
        {
            MenuMenager.instance.MenuOff();
            SaveMenager.instance.Load();

        }
    }
    /*
    
    {
        
        if (Input.GetButtonDown("VerticalItem"))
        {
            buttonNumber -= (int)Input.GetAxisRaw("VerticalItem");
        }
        else if(Input.GetButtonDown("Vertical"))
        {
            buttonNumber -= (int)Input.GetAxisRaw("Vertical");
        }
        if (buttons.Length-1 < buttonNumber)
        {
            buttonNumber = 0;
        }
        if (0 > buttonNumber)
        {
            buttonNumber = buttons.Length-1;
        }
        if (Input.GetButtonDown("Jump"))
        {
            buttons[buttonNumber].GetComponent<ButtonDane>().Play();
        }
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttonNumber == i)
            {
                buttons[i].transform.localScale = scale;
            }
            else
            {
                buttons[i].transform.localScale = Vector3.one;
            }
            
        }
        
            
    }*/
}
