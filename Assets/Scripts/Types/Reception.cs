using UnityEngine;
using UnityEngine.Events;



/// <summary>
/// Класс Ресепшен 
/// 1. Отображение нового Посетителя 
/// 2. Выставление игрока за Ресепшен
/// </summary>
[RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody))]
public class Reception : MonoBehaviour
{
    public event UnityAction newConsumerByeBegin;
    public event UnityAction leaveConsumer;

    /// <summary>
    /// Отображение нового Посетителя
    /// </summary>
    /// <param name="collision"></param>
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Consumer enemy))
        {
            newConsumerByeBegin?.Invoke();
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Consumer enemy))
        {
            leaveConsumer?.Invoke();
        }
    }


    /// <summary>
    /// Двойной клик по Респешену
    /// </summary>
    /*
    float doubleClickStart = 0;
    void OnMouseUp()
    {
        if ((Time.time - doubleClickStart) < 0.3f)
        {
            this.OnDoubleClick();
            doubleClickStart = -1;
        }
     else
        {
            doubleClickStart = Time.time;
        }
    }
    void OnDoubleClick()
    {
        Debug.Log("Double Clicked!");
    }
    */
}
