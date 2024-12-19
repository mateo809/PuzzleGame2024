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

    public int StartHour = 9;
    public int StartMinute = 10;
    public int StartSecond = 0;

    [HideInInspector] public int CurrentHour;
    [HideInInspector] public int CurrentMinute;
    [HideInInspector] public int CurrentSecond;

    [SerializeField] private Sprite _image;
    [SerializeField] private Sprite _screenSprite;
    [SerializeField] private GameObject _screen;

    [SerializeField] private GameObject _defeatPanel;



    private void Start()
    {
        CurrentHour = StartHour;
        CurrentMinute = StartMinute;
        CurrentSecond = StartSecond;

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
        CurrentSecond++;
        if (CurrentSecond >= 60)
        {
            CurrentSecond = 0;
            CurrentMinute++;
        }
        if (CurrentMinute >= 60)
        {
            CurrentMinute = 0;
            CurrentHour++;
        }
        if (CurrentHour >= 24)
        {
            CurrentHour = 0;
        }
        string formattedTime = string.Format("{0:D2}:{1:D2}", CurrentHour, CurrentMinute);
        _clockText.text = formattedTime;

        if(CurrentHour == 9 && CurrentMinute == 30)
        {
            _defeatPanel.SetActive(true);
        }
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
