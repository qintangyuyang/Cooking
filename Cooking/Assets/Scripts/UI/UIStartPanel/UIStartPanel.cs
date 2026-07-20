using Cooking.Manager;
using TMPro;
using UnityEngine.UI;

namespace Cooking.UI
{
    public class UIStartPanel : UIBase
    {
        private Button _StartBtn;
        private Button _ContinueBtn;
        private Button _ImageBtn;
        private Button _SettingBtn;
        private Button _QuitBtn;

        private TextMeshProUGUI _StartText;
        private TextMeshProUGUI _ContinueText;
        private TextMeshProUGUI _ImageText;
        private TextMeshProUGUI _SettingText;
        private TextMeshProUGUI _QuitText;

        private UIBinder _uiBinder;

        public override void Init()
        {
            _uiBinder = this.gameObject.GetComponent<UIBinder>();
            if (_uiBinder != null)
            {
                //按钮绑定
                _StartBtn = _uiBinder.GetButton("StartBtn");
                _ContinueBtn = _uiBinder.GetButton("ContinueBtn");
                _ImageBtn = _uiBinder.GetButton("ImageBtn");
                _SettingBtn = _uiBinder.GetButton("SettingBtn");
                _QuitBtn = _uiBinder.GetButton("QuitBtn");

                //文本绑定
                _StartText = _uiBinder.GetText("Start_Txt");
                _ContinueText = _uiBinder.GetText("Continue_Txt");
                _ImageText = _uiBinder.GetText("Image_Txt");
                _SettingText = _uiBinder.GetText("Setting_Txt");
                _QuitText = _uiBinder.GetText("Quit_Txt");

                //点击事件监听
                _StartBtn.onClick.AddListener(OnClickStartBtn);
                _ContinueBtn.onClick.AddListener(OnClickContinueBtn);
                _ImageBtn.onClick.AddListener(OnClickImageBtn);
                _SettingBtn.onClick.AddListener(OnClickSettingBtn);
                _QuitBtn.onClick.AddListener(OnClickQuitBtn);

                RefreshText();
            }
        }

        private void RefreshText()
        {
            _StartText.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_1");
            _ContinueText.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_2");
            _ImageText.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_3");
            _SettingText.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_4");
            _QuitText.text = LanguageManager.Instance.GetText("COMMON_TEXT_KEY_5");
        }

        protected override void OnLanguageChanged()
        {
            RefreshText();
        }

        private void OnClickStartBtn()
        {
            //开始界面逻辑
            UIManager.Instance.OpenUI<UIProcessPanel>();
            UIManager.Instance.CloseUI<UIStartPanel>();
        }

        private void OnClickContinueBtn()
        {
            //继续游戏逻辑
        }

        private void OnClickImageBtn()
        {
            //窗外集逻辑
        }

        private void OnClickSettingBtn()
        {
            //设置界面逻辑
            UIManager.Instance.OpenUI<UISettingPanel>();
        }

        private void OnClickQuitBtn()
        {
            //退出游戏逻辑
        }

        public override void OnClose()
        {
            base.OnClose();
            //点击取消事件监听
            _StartBtn.onClick.RemoveListener(OnClickStartBtn);
            _ContinueBtn.onClick.RemoveListener(OnClickContinueBtn);
            _ImageBtn.onClick.RemoveListener(OnClickImageBtn);
            _SettingBtn.onClick.RemoveListener(OnClickSettingBtn);
            _QuitBtn.onClick.RemoveListener(OnClickQuitBtn);
        }
    }
}
