using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PhoneManager : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    [SerializeField] private RectTransform _targetTransform;
    private bool _isExpanded = false;
    [SerializeField] private GameObject _clonedImagePrefab;
    private GameObject _clonedImage;
    [SerializeField] private Transform _canvasTransform;

    [SerializeField] private TextMeshProUGUI _clockText;
    private float _timeElapsed = 0f;

    [SerializeField] private int _startHour = 12;
    [SerializeField] private int _startMinute = 0;
    [SerializeField] private int _startSecond = 0;

    [SerializeField] private Sprite _image;
    [SerializeField] private Sprite _screenSprite;
    [SerializeField] private GameObject _screen;

    private int _currentHour;
    private int _currentMinute;
    private int _currentSecond;

    private void Start()
    {
        _currentHour = _startHour;
        _currentMinute = _startMinute;
        _currentSecond = _startSecond;

        if (_clonedImagePrefab != null && _canvasTransform != null)
        {
            _clonedImage = Instantiate(_clonedImagePrefab, _canvasTransform);
            _clonedImage.SetActive(false);  
        }
    }

    public void ShowPhone()
    {
        _animator.SetBool("Show", true);
    }

    public void HidePhone()
    {
        _animator.SetBool("Show", false);
    }

    public void ToggleImageSize(Image originalImage)
    {
        if (!_isExpanded)
        {
            CreateAndExpandImage(originalImage);
        }
        else
        {
            ShrinkAndDestroyImage();
        }
    }

    private void CreateAndExpandImage(Image originalImage)
    {
        if (_clonedImage != null)
        {
            _clonedImage.SetActive(true);
            RectTransform clonedRectTransform = _clonedImage.GetComponent<RectTransform>();
            Image clonedImageComponent = _clonedImage.GetComponent<Image>();

            clonedImageComponent.sprite = originalImage.sprite;
            clonedImageComponent.preserveAspect = true;
            clonedImageComponent.raycastTarget = false;

            clonedRectTransform.SetParent(_canvasTransform, false);
            clonedRectTransform.sizeDelta = _targetTransform.sizeDelta;
            clonedRectTransform.position = _targetTransform.position;
            clonedRectTransform.localScale = _targetTransform.localScale;

            _isExpanded = true;
        }
    }

    public void ShrinkAndDestroyImage()
    {
        if (_clonedImage != null)
        {
            _clonedImage.SetActive(false);
        }

        _isExpanded = false;
    }

    private void Update()
    {
        if (_clockText != null)
        {
            _timeElapsed += Time.deltaTime;

            if (_timeElapsed >= 1f)
            {
                _timeElapsed = 0f;
                UpdateClock();
            }
        }
    }

    private void UpdateClock()
    {
        _currentSecond++;
        if (_currentSecond >= 60)
        {
            _currentSecond = 0;
            _currentMinute++;
        }
        if (_currentMinute >= 60)
        {
            _currentMinute = 0;
            _currentHour++;
        }
        if (_currentHour >= 24)
        {
            _currentHour = 0;
        }
        string formattedTime = string.Format("{0:D2}:{1:D2}", _currentHour, _currentMinute);
        _clockText.text = formattedTime;
    }

    public void ChangeScreenUi()
    {
        _screen.GetComponent<Image>().sprite = _image;
    }

    public void RevertScreenUi()
    {
        _screen.GetComponent<Image>().sprite = _screenSprite;
    }
}
