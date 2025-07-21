using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XboxMock.Views.ContentViews;

public partial class AnimatedNavbar : ContentView
{
    public AnimatedNavbar()
    {
        InitializeComponent();
        CalculateNavbarColor();
        CalculateMarginHeight();
    }

    /// <summary>
    /// The starting color displayed before the background
    /// </summary>
    public Color StartColor
    {
        get => (Color)GetValue(StartColorProperty);
        set => SetValue(StartColorProperty, value);
    }

    public static readonly BindableProperty StartColorProperty =
        BindableProperty.Create(
            nameof(StartColor),
            typeof(Color),
            typeof(AnimatedNavbar),
            Colors.Transparent);
    
    /// <summary>
    /// The color that displays after we've reached the scroll threshold
    /// </summary>
    public static readonly BindableProperty ScrolledColorProperty =
        BindableProperty.Create(
            nameof(ScrolledColor),
            typeof(Color),
            typeof(AnimatedNavbar),
            Colors.Transparent);

    public Color ScrolledColor
    {
        get => (Color)GetValue(ScrolledColorProperty);
        set => SetValue(ScrolledColorProperty, value);
    }

    public static readonly BindableProperty ScrollThresholdProperty =
        BindableProperty.Create(
            nameof(ScrollThreshold),
            typeof(double),
            typeof(AnimatedNavbar),
            0d);

    public static readonly BindableProperty IsOffsetHeightProperty =
        BindableProperty.Create(
            nameof(IsOffsetHeight),
            typeof(bool),
            typeof(AnimatedNavbar),
            true,
            propertyChanged: IsOffsetHeight_PropertyChanged);

    private static void IsOffsetHeight_PropertyChanged(BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not AnimatedNavbar animatedNavbar)
        {
            return;
        }

        animatedNavbar.CalculateMarginHeight();
    }

    public bool IsOffsetHeight
    {
        get => (bool)GetValue(IsOffsetHeightProperty);
        set => SetValue(IsOffsetHeightProperty, value);
    }

    /// <summary>
    /// How far to scroll before the <see cref="ScrolledColor"/> is fully visible
    /// </summary>
    public double ScrollThreshold
    {
        get => (double)GetValue(ScrollThresholdProperty);
        set => SetValue(ScrollThresholdProperty, value);
    }

    public static readonly BindableProperty NavbarHeightProperty =
        BindableProperty.Create(
            nameof(NavbarHeight),
            typeof(int),
            typeof(AnimatedNavbar),
            100,
            propertyChanging: NavbarHeight_PropertyChanging,
            propertyChanged: NavbarHeight_PropertyChanged);

    private static void NavbarHeight_PropertyChanging(BindableObject bindable,
        object oldValue,
        object newValue)
    {
    }

    private static void NavbarHeight_PropertyChanged(BindableObject bindable,
        object oldValue,
        object newValue)
    {
        if (bindable is not AnimatedNavbar animatedNavbar)
        {
            return;
        }

        animatedNavbar.CalculateMarginHeight();
    }

    public void CalculateMarginHeight()
    {
        MarginHeight = IsOffsetHeight ? new Thickness(0, NavbarHeight, 0, 0) : new Thickness(0, 0, 0, 0);
    }
    
    public int NavbarHeight
    {
        get => (int)GetValue(NavbarHeightProperty);
        set => SetValue(NavbarHeightProperty, value);
    }

    public static readonly BindablePropertyKey MarginHeightProperty =
        BindableProperty.CreateReadOnly(
            nameof(MarginHeight),
            typeof(Thickness),
            typeof(AnimatedNavbar),
            default(Thickness));

    [EditorBrowsable(EditorBrowsableState.Never)]
    public Thickness MarginHeight
    {
        get => (Thickness)GetValue(MarginHeightProperty.BindableProperty);
        set => SetValue(MarginHeightProperty, value);
    }

    public static readonly BindableProperty ScrollContentProperty =
        BindableProperty.Create(
            nameof(ScrollContent),
            typeof(View),
            typeof(AnimatedNavbar),
            null);

    public View ScrollContent
    {
        get => (View)GetValue(ScrollContentProperty);
        set => SetValue(ScrollContentProperty, value);
    }

    public static readonly BindableProperty TitleViewProperty =
        BindableProperty.Create(
            nameof(TitleView),
            typeof(View),
            typeof(AnimatedNavbar),
            null);

    public View TitleView
    {
        get => (View)GetValue(TitleViewProperty);
        set => SetValue(TitleViewProperty, value);
    }

    private void ScrollView_OnScrolled(object? sender,
        ScrolledEventArgs e)
    {
        CalculateNavbarColor();
    }

    public void CalculateNavbarColor()
    {
        if (ScrollView == null)
        {
            return;
        }
        
        Intensity = (float)(ScrollView.ScrollY / ScrollThreshold);

        // var red = ScrolledColor.Red - StartColor.Red;
        // var green = ScrolledColor.Green - StartColor.Green;
        // var blue = ScrolledColor.Blue - StartColor.Blue;
        
        var red = StartColor.Red + (ScrolledColor.Red - StartColor.Red) * Intensity;
        var green = StartColor.Green + (ScrolledColor.Green - StartColor.Green) * Intensity;
        var blue = StartColor.Blue + (ScrolledColor.Blue - StartColor.Blue) * Intensity;
        
        NavbarColor = new Color(red, green, blue, Intensity);
    }

    public static readonly BindableProperty IntensityProperty =
        BindableProperty.Create(
            nameof(Intensity),
            typeof(float),
            typeof(AnimatedNavbar),
            0f);

    public float Intensity
    {
        get => (float)GetValue(IntensityProperty);
        set => SetValue(IntensityProperty, value);
    }

    public static readonly BindablePropertyKey NavbarColorProperty =
        BindableProperty.CreateReadOnly(
            nameof(NavbarColor),
            typeof(Color),
            typeof(AnimatedNavbar),
            Colors.Black);

    public Color NavbarColor
    {
        get => (Color)GetValue(NavbarColorProperty.BindableProperty);
        set => SetValue(NavbarColorProperty, value);
    }
}