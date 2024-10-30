using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    private VisualElement _PlayKey;
    private Button _openButtonPlayKey;
    private Button _closeButtonPlayKey;

    private VisualElement _CardTree;
    private Button _openButtonCardTree;
    private Button _closeButtonCardTree;

    private VisualElement _SettingScreen;
    private Button _openButtonSettingScreen;
    private Button _closeButtonSettingScreen;

    private VisualElement _CreditScreen;
    private Button _openButtonCreditScreen;
    private Button _closeButtonCreditScreen;
    

    void Start()
    {
        // UIDocument와 연결된 root를 불러오기
        var root = GetComponent<UIDocument>()?.rootVisualElement;

        if (root == null)
        {
            Debug.LogError("UIDocument가 할당되지 않았습니다.");
            return;
        }

        // UI 요소 초기화
        _PlayKey = root.Q<VisualElement>("PlayKey_Screen");
        _openButtonPlayKey = root.Q<Button>("Button_PlayKey");
        _closeButtonPlayKey = root.Q<Button>("Button_Close");

        _CardTree = root.Q<VisualElement>("CardTree_Screen");
        _openButtonCardTree = root.Q<Button>("Button_CardTree");
        _closeButtonCardTree = root.Q<Button>("Button_Close_1");

        _SettingScreen = root.Q<VisualElement>("Setting_Screen");
        _openButtonSettingScreen = root.Q<Button>("Button_Setting");
        _closeButtonSettingScreen = root.Q<Button>("Button_Close_2");

        _CreditScreen = root.Q<VisualElement>("Credit_Screen");
        _openButtonCreditScreen = root.Q<Button>("Button_Setting");
        _closeButtonCreditScreen = root.Q<Button>("Button_Close_3");

        // UI 초기 상태 설정
        _PlayKey.style.display = DisplayStyle.None;  // 기본적으로 숨김
        _openButtonPlayKey.clicked += OpenPlayKey;
        _closeButtonPlayKey.clicked += ClosePlayKey;

        _CardTree.style.display = DisplayStyle.None;  // 기본적으로 숨김
        _openButtonCardTree.clicked += OpenCardTree;
        _closeButtonCardTree.clicked += CloseCardTree;

        _SettingScreen.style.display = DisplayStyle.None;
        _openButtonCreditScreen.clicked += OpenSetting;
        _closeButtonCreditScreen.clicked += CloseSetting;

        _CreditScreen.style.display = DisplayStyle.None;
        _openButtonCreditScreen.clicked += OpenCredit;
        _closeButtonCreditScreen.clicked += CloseCredit;

    }

    // PlayKey 창을 여는 메서드
    private void OpenPlayKey()
    {
        _PlayKey.style.display = DisplayStyle.Flex;
    }

    // PlayKey 창을 닫는 메서드
    private void ClosePlayKey()
    {
        _PlayKey.style.display = DisplayStyle.None;
    }
    private void OpenCardTree()
    {
        _CardTree.style.display = DisplayStyle.Flex;
    }

    // PlayKey 창을 닫는 메서드
    private void CloseCardTree()
    {
        _CardTree.style.display = DisplayStyle.None;
    }
    private void OpenSetting()
    {
        _SettingScreen.style.display = DisplayStyle.Flex;
    }

    // PlayKey 창을 닫는 메서드
    private void CloseSetting()
    {
        _SettingScreen.style.display = DisplayStyle.None;
    }
    private void OpenCredit()
    {
        _CreditScreen.style.display = DisplayStyle.Flex;
    }

    // PlayKey 창을 닫는 메서드
    private void CloseCredit()
    {
        _CreditScreen.style.display = DisplayStyle.None;
    }
}
