using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;



public class MenuMenager : MonoBehaviour
{
    private OnMenuControler OnMenu;
    [SerializeField] private Scene[] scenes;
    [SerializeField] private float max;
    [SerializeField] private float min;
    [SerializeField] private float speed;
    public bool activeMenu;
    public static MenuMenager instance;
    public event System.Action MenuOnGlobal;
    void Awake()
    {

        instance = this;

        OnMenu = new OnMenuControler();
        OnMenu.Move.Enable();

        OnMenu.Move.Start.performed += On;

    }
    public void MenuOn()
    {
        StopAllCoroutines();
        GameDate.instance.StopGame.Invoke();
        StartCoroutine(MoveRight(speed));
        activeMenu = true;
        MenuOnGlobal.Invoke();
    }
    public void MenuOff()
    {
        StopAllCoroutines();
        GameDate.instance.StartGame.Invoke();
        StartCoroutine(MoveLeft(speed));
        activeMenu = false;
        PlayerInstance.instance.GetComponent<Rigidbody>().Sleep();
    }
    void On(InputAction.CallbackContext ctx)
    {
        if (!activeMenu)
        {
            MenuOn();
        }
        else
        {
            MenuOff();
        }
        
        //SaveMenager.instance.Load();
        //MenagerScene.instance.LoadScene(scenes);
    }
    IEnumerator MoveRight (float speed)
    {
        //Debug.Log("position" + transform.position.x);
        while (transform.position.x / Screen.width < max)
        {
            
            transform.position += Vector3.right * speed * Screen.width * Time.unscaledDeltaTime;
            yield return null;
        }
        transform.position = new Vector3(max* Screen.width, transform.position.y);
        
    }
    IEnumerator MoveLeft(float speed)
    {
        while (transform.position.x / Screen.width > min)
        {
            //Debug.Log("position" + transform.position.x);
            transform.position -= Vector3.right * speed * Screen.width * Time.unscaledDeltaTime;
            yield return null;
        }
        transform.position = new Vector3(min * Screen.width, transform.position.y);

    }
}
