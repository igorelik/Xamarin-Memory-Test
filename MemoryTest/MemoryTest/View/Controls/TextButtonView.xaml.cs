using System.Windows.Input;
using Xamarin.Forms;

namespace MemoryTest.View.Controls
{
    public partial class TextButtonView
    {
        public TextButtonView()
        {
            InitializeComponent();
            //PropertyChanged += TextButtonView_PropertyChanged;
        }

        void TextButtonView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == IsEnabledProperty.PropertyName)
            {
                DisabledOverlay.IsVisible = !IsEnabled;
            }
        }

        private void SetControlsColor()
        {
            frameOutline.BackgroundColor = AccentColor;
            frameOutline.OutlineColor = AccentColor;
        }


        #region ActionCommand property

        public static BindableProperty ActionCommandProperty =
            BindableProperty.Create<TextButtonView, ICommand>(ctrl => ctrl.ActionCommand,
                defaultValue: null,
                defaultBindingMode: BindingMode.TwoWay,
                propertyChanging: (bindable, oldValue, newValue) =>
                {
                    var ctrl = (TextButtonView)bindable;
                    ctrl.ActionCommand = newValue;
                });

        private ICommand _actionCommand;
        public ICommand ActionCommand
        {
            get
            {
                return _actionCommand;
            }
            set
            {
                SetValue(ActionCommandProperty, value);
                frameOutline.GestureRecognizers.Clear();
                if (value != null)
                {
                    frameOutline.GestureRecognizers.Add(new TapGestureRecognizer { Command = value });
                }
                _actionCommand = value;
            }
        }

        #endregion

        #region AccentColor property

        public static readonly BindableProperty AccentColorProperty =
            BindableProperty.Create<TextButtonView, Color>(ctrl => ctrl.AccentColor,
                defaultValue: Color.Default,
                defaultBindingMode: BindingMode.TwoWay,
                propertyChanging: (bindable, oldValue, newValue) =>
                {
                    var ctrl = (TextButtonView)bindable;
                    ctrl.AccentColor = newValue;
                });

        private Color _accentColor = Color.Default;
        public Color AccentColor
        {
            get
            {
                return _accentColor;
            }
            set
            {
                _accentColor = value;
                SetControlsColor();
                OnPropertyChanged();
            }
        }

        #endregion

        #region TextColor property

        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create<TextButtonView, Color>(ctrl => ctrl.TextColor,
                defaultValue: Color.White,
                defaultBindingMode: BindingMode.TwoWay,
                propertyChanging: (bindable, oldValue, newValue) =>
                {
                    var ctrl = (TextButtonView)bindable;
                    ctrl.TextColor = newValue;
                });

        private Color _textColor = Color.White;
        public Color TextColor
        {
            get
            {
                return _textColor;
            }
            set
            {
                _textColor = value;
                lblText.TextColor = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Text property

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create<TextButtonView, string>(ctrl => ctrl.Text,
                defaultValue: "Button",
                defaultBindingMode: BindingMode.TwoWay,
                propertyChanging: (bindable, oldValue, newValue) =>
                {
                    var ctrl = (TextButtonView)bindable;
                    ctrl.Text = newValue;
                });

        private string _text = "Button";
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                lblText.Text = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region FontSize property

        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create<TextButtonView, double>(ctrl => ctrl.FontSize,
                defaultValue: 18,
                defaultBindingMode: BindingMode.TwoWay,
                propertyChanging: (bindable, oldValue, newValue) =>
                {
                    var ctrl = (TextButtonView)bindable;
                    ctrl.FontSize = newValue;
                });

        private double _fontSize = 18;
        public double FontSize
        {
            get
            {
                return _fontSize;
            }
            set
            {
                _fontSize = value;
                lblText.FontSize = value;
                OnPropertyChanged();
            }
        }

        #endregion
    }
}
