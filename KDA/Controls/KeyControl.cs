﻿using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace KDA.Controls;


public class KeyControl : ButtonBase
{

    public object KeyCode
    {
        get { return GetValue(KeyCodeProperty); }
        set { SetValue(KeyCodeProperty, value); }
    }

    // Using a DependencyProperty as the backing store for KeyCode.  This enables animation, styling, binding, etc...
    /// <summary>Identifies the <see cref="KeyCode"/> dependency property.</summary>
    public static readonly DependencyProperty KeyCodeProperty =
        DependencyProperty.Register(nameof(KeyCode), typeof(object), typeof(KeyControl));



    public bool IsKeyPressed
    {
        get { return (bool)GetValue(IsKeyPressedProperty); }
        set { SetValue(IsKeyPressedProperty, value); }
    }

    // Using a DependencyProperty as the backing store for IsPressed.  This enables animation, styling, binding, etc...
    /// <summary>Identifies the <see cref="IsKeyPressed"/> dependency property.</summary>
    public static readonly DependencyProperty IsKeyPressedProperty =
        DependencyProperty.Register(nameof(IsKeyPressed), typeof(bool), typeof(KeyControl));



    public bool IsPressedZoomIn
    {
        get { return (bool)GetValue(IsPressedZoomInProperty); }
        set { SetValue(IsPressedZoomInProperty, value); }
    }

    // Using a DependencyProperty as the backing store for IsPressedZoomIn.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty IsPressedZoomInProperty =
        DependencyProperty.Register(nameof(IsPressedZoomIn), typeof(bool), typeof(KeyControl));




    public Brush KeyPressedBrush
    {
        get { return (Brush)GetValue(KeyPressedBrushProperty); }
        set { SetValue(KeyPressedBrushProperty, value); }
    }

    // Using a DependencyProperty as the backing store for KeyPressedBrush.  This enables animation, styling, binding, etc...
    /// <summary>Identifies the <see cref="KeyPressedBrush"/> dependency property.</summary>
    public static readonly DependencyProperty KeyPressedBrushProperty =
        DependencyProperty.Register(nameof(KeyPressedBrush), typeof(Brush), typeof(KeyControl));



    public Brush MouseOverBrush
    {
        get { return (Brush)GetValue(MouseOverBrushProperty); }
        set { SetValue(MouseOverBrushProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MouseOverBrush.  This enables animation, styling, binding, etc...
    /// <summary>Identifies the <see cref="MouseOverBrush"/> dependency property.</summary>
    public static readonly DependencyProperty MouseOverBrushProperty =
        DependencyProperty.Register(nameof(MouseOverBrush), typeof(Brush), typeof(KeyControl));



    public Brush MousePressedBrush
    {
        get { return (Brush)GetValue(MousePressedBrushProperty); }
        set { SetValue(MousePressedBrushProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MousePressedBrush.  This enables animation, styling, binding, etc...
    /// <summary>Identifies the <see cref="MousePressedBrush"/> dependency property.</summary>
    public static readonly DependencyProperty MousePressedBrushProperty =
        DependencyProperty.Register(nameof(MousePressedBrush), typeof(Brush), typeof(KeyControl));





    public CornerRadius CornerRadius
    {
        get { return (CornerRadius)GetValue(CornerRadiusProperty); }
        set { SetValue(CornerRadiusProperty, value); }
    }

    // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
    /// <summary>Identifies the <see cref="CornerRadius"/> dependency property.</summary>
    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(KeyControl));




    static KeyControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(KeyControl), new FrameworkPropertyMetadata(typeof(KeyControl)));
    }
}
