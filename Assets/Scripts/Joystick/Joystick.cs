using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [Header("UI")]
    [SerializeField] private RectTransform _backgroundUI;
    [SerializeField] private RectTransform _handleUI;

    [Header("Options")]
    [SerializeField] private float _deathArea;
    [SerializeField] private bool _pcSimulation;

    private Vector2 _inputVector;

    public bool IsActive => _inputVector != Vector2.zero;

    public float Horizontal => _inputVector.x;
    public float Vertical => _inputVector.y;
    public Vector2 InputVector => _inputVector;
    public Vector2 Direction => _inputVector.normalized;

    private bool _isDraggable;

    private void Start()
    {
        _backgroundUI.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_pcSimulation && _isDraggable == false)
        {
            Vector2 simInputVector = new Vector2(Input.GetAxis("Horizontal"),
                                                 Input.GetAxis("Vertical"));
            _inputVector = simInputVector;
            SetHandlePosition(_inputVector * AreaSize());
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_backgroundUI, eventData.position, eventData.pressEventCamera, out position))
        {
            float size = AreaSize();
            //position.x = position.x;
            //position.y = position.y;

            _inputVector = position / size;
            _inputVector = (_inputVector.magnitude > 1.0f) ? _inputVector.normalized : _inputVector;
            if (_inputVector.magnitude < _deathArea)
                _inputVector = Vector2.zero;

            SetHandlePosition(Vector2.ClampMagnitude(position, size));
        }
    }

    private float AreaSize()
    {
        return Mathf.Min(Mathf.Abs(_backgroundUI.rect.size.x), Mathf.Abs(_backgroundUI.rect.size.y)) * 0.5f;
    }

    private void SetHandlePosition(Vector2 position)
    {
        _handleUI.anchoredPosition = position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isDraggable = true;
        _backgroundUI.anchoredPosition = eventData.position - _backgroundUI.rect.size * 0.5f;
        _backgroundUI.gameObject.SetActive(true);
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isDraggable = false;
        _inputVector = Vector2.zero;
        _handleUI.anchoredPosition = Vector2.zero;
        _backgroundUI.gameObject.SetActive(false);
    }
}
