using System.Windows;
using System.Windows.Controls;

namespace AccountsManager.Desktop.Components;

public partial class InputComponent : UserControl
{
    public static readonly DependencyProperty LabelContentProperty = DependencyProperty.Register(
        nameof(LabelContent), typeof(object), typeof(InputComponent));
    public static readonly DependencyProperty InputTextProperty = 
        DependencyProperty.Register(nameof(InputText), typeof(string), typeof(InputComponent));
    public static readonly DependencyProperty IsReadOnlyProperty = 
        DependencyProperty.Register(nameof(IsReadOnly), typeof(bool), typeof(InputComponent));

    public object LabelContent
    {
        get => GetValue(LabelContentProperty); 
        set => SetValue(LabelContentProperty, value);
    }

    public string InputText
    {
        get => (string)GetValue(InputTextProperty); 
        set => SetValue(InputTextProperty, value);
    }

    public bool IsReadOnly
    {
        get => (bool)GetValue(IsReadOnlyProperty);
        set => SetValue(IsReadOnlyProperty, value);
    }
    
    public InputComponent()
    {
        InitializeComponent();
    }
}