// ============================================================
// LocalizedUITypes.cs
//
// This file belongs to the Reflectis.SDK.Core.I2Loc assembly,
// which is compiled only when both define symbols are present:
//   UITOOLKIT  — set automatically via versionDefines (com.unity.ugui)
//   I2LOC      — set automatically by I2LocDefineSymbolToggler
// ============================================================
using I2.Loc;
using static I2.Loc.LocalizationManager;
using System.Collections.Generic;
using UnityEngine.UIElements;

// ============================================================
// LocalizationHelper — shared attach/detach logic
// ============================================================
public static class LocalizationHelper
{
    public static void Setup(VisualElement el, OnLocalizeCallback updateAction)
    {
        el.RegisterCallback<AttachToPanelEvent>(_ =>
        {
            updateAction();
            OnLocalizeEvent += updateAction;
        });
        el.RegisterCallback<DetachFromPanelEvent>(_ =>
        {
            OnLocalizeEvent -= updateAction;
        });
    }

    public static string Translate(string key)
    {
        if (string.IsNullOrEmpty(key)) return null;
        return LocalizationManager.GetTranslation(key);
    }
}

// ============================================================
// TEXT ELEMENTS
// ============================================================

// --- Label ---

[UxmlElement]
public partial class LocalizedLabel : Label
{
    [UxmlAttribute] [TermsPopup] public string locKey { get => _k; set { _k = value; Apply(); } }
    string _k;
    public LocalizedLabel() => LocalizationHelper.Setup(this, Apply);
    void Apply() { var t = LocalizationHelper.Translate(_k); if (t != null) text = t; }
}

// --- Button ---

[UxmlElement]
public partial class LocalizedButton : Button
{
    [UxmlAttribute] public string locKey { get => _k; set { _k = value; Apply(); } }
    string _k;
    public LocalizedButton() => LocalizationHelper.Setup(this, Apply);
    void Apply() { var t = LocalizationHelper.Translate(_k); if (t != null) text = t; }
}

// --- Toggle ---

[UxmlElement]
public partial class LocalizedToggle : Toggle
{
    [UxmlAttribute] public string locKey { get => _k; set { _k = value; Apply(); } }
    string _k;
    public LocalizedToggle() => LocalizationHelper.Setup(this, Apply);
    void Apply() { var t = LocalizationHelper.Translate(_k); if (t != null) label = t; }
}

// --- RadioButton ---

[UxmlElement]
public partial class LocalizedRadioButton : RadioButton
{
    [UxmlAttribute] public string locKey { get => _k; set { _k = value; Apply(); } }
    string _k;
    public LocalizedRadioButton() => LocalizationHelper.Setup(this, Apply);
    void Apply() { var t = LocalizationHelper.Translate(_k); if (t != null) label = t; }
}

// --- Foldout ---

[UxmlElement]
public partial class LocalizedFoldout : Foldout
{
    [UxmlAttribute] public string locKey { get => _k; set { _k = value; Apply(); } }
    string _k;
    public LocalizedFoldout() => LocalizationHelper.Setup(this, Apply);
    void Apply() { var t = LocalizationHelper.Translate(_k); if (t != null) text = t; }
}

// --- GroupBox ---

[UxmlElement]
public partial class LocalizedGroupBox : GroupBox
{
    [UxmlAttribute] public string locKey { get => _k; set { _k = value; Apply(); } }
    string _k;
    public LocalizedGroupBox() => LocalizationHelper.Setup(this, Apply);
    void Apply() { var t = LocalizationHelper.Translate(_k); if (t != null) text = t; }
}

// --- ProgressBar ---

[UxmlElement]
public partial class LocalizedProgressBar : ProgressBar
{
    [UxmlAttribute] public string locKey { get => _k; set { _k = value; Apply(); } }
    string _k;
    public LocalizedProgressBar() => LocalizationHelper.Setup(this, Apply);
    void Apply() { var t = LocalizationHelper.Translate(_k); if (t != null) title = t; }
}

// ============================================================
// INPUT FIELDS — label + optional placeholder
// ============================================================

// --- TextField ---

[UxmlElement]
public partial class LocalizedTextField : TextField
{
    [UxmlAttribute] public string locKeyLabel { get => _kl; set { _kl = value; Apply(); } }
    [UxmlAttribute] public string locKeyPlaceholder { get => _kp; set { _kp = value; Apply(); } }
    string _kl, _kp;
    public LocalizedTextField() => LocalizationHelper.Setup(this, Apply);
    void Apply()
    {
        var tl = LocalizationHelper.Translate(_kl); if (tl != null) label = tl;
        var tp = LocalizationHelper.Translate(_kp); if (tp != null) textEdition.placeholder = tp;
    }
}

// --- IntegerField ---

[UxmlElement]
public partial class LocalizedIntegerField : IntegerField
{
    [UxmlAttribute] public string locKey { get => _k; set { _k = value; Apply(); } }
    string _k;
    public LocalizedIntegerField() => LocalizationHelper.Setup(this, Apply);
    void Apply() { var t = LocalizationHelper.Translate(_k); if (t != null) label = t; }
}

// --- FloatField ---

[UxmlElement]
public partial class LocalizedFloatField : FloatField
{
    [UxmlAttribute] public string locKey { get => _k; set { _k = value; Apply(); } }
    string _k;
    public LocalizedFloatField() => LocalizationHelper.Setup(this, Apply);
    void Apply() { var t = LocalizationHelper.Translate(_k); if (t != null) label = t; }
}

// --- LongField ---

[UxmlElement]
public partial class LocalizedLongField : LongField
{
    [UxmlAttribute] public string locKey { get => _k; set { _k = value; Apply(); } }
    string _k;
    public LocalizedLongField() => LocalizationHelper.Setup(this, Apply);
    void Apply() { var t = LocalizationHelper.Translate(_k); if (t != null) label = t; }
}

// --- DoubleField ---

[UxmlElement]
public partial class LocalizedDoubleField : DoubleField
{
    [UxmlAttribute] public string locKey { get => _k; set { _k = value; Apply(); } }
    string _k;
    public LocalizedDoubleField() => LocalizationHelper.Setup(this, Apply);
    void Apply() { var t = LocalizationHelper.Translate(_k); if (t != null) label = t; }
}

// ============================================================
// SLIDERS
// ============================================================

// --- Slider ---

[UxmlElement]
public partial class LocalizedSlider : Slider
{
    [UxmlAttribute] public string locKey { get => _k; set { _k = value; Apply(); } }
    string _k;
    public LocalizedSlider() => LocalizationHelper.Setup(this, Apply);
    void Apply() { var t = LocalizationHelper.Translate(_k); if (t != null) label = t; }
}

// --- SliderInt ---

[UxmlElement]
public partial class LocalizedSliderInt : SliderInt
{
    [UxmlAttribute] public string locKey { get => _k; set { _k = value; Apply(); } }
    string _k;
    public LocalizedSliderInt() => LocalizationHelper.Setup(this, Apply);
    void Apply() { var t = LocalizationHelper.Translate(_k); if (t != null) label = t; }
}

// --- MinMaxSlider ---

[UxmlElement]
public partial class LocalizedMinMaxSlider : MinMaxSlider
{
    [UxmlAttribute] public string locKey { get => _k; set { _k = value; Apply(); } }
    string _k;
    public LocalizedMinMaxSlider() => LocalizationHelper.Setup(this, Apply);
    void Apply() { var t = LocalizationHelper.Translate(_k); if (t != null) label = t; }
}

// ============================================================
// VECTOR FIELDS
// ============================================================

// --- Vector2Field ---

[UxmlElement]
public partial class LocalizedVector2Field : Vector2Field
{
    [UxmlAttribute] public string locKey { get => _k; set { _k = value; Apply(); } }
    string _k;
    public LocalizedVector2Field() => LocalizationHelper.Setup(this, Apply);
    void Apply() { var t = LocalizationHelper.Translate(_k); if (t != null) label = t; }
}

// --- Vector3Field ---

[UxmlElement]
public partial class LocalizedVector3Field : Vector3Field
{
    [UxmlAttribute] public string locKey { get => _k; set { _k = value; Apply(); } }
    string _k;
    public LocalizedVector3Field() => LocalizationHelper.Setup(this, Apply);
    void Apply() { var t = LocalizationHelper.Translate(_k); if (t != null) label = t; }
}

// --- Vector4Field ---

[UxmlElement]
public partial class LocalizedVector4Field : Vector4Field
{
    [UxmlAttribute] public string locKey { get => _k; set { _k = value; Apply(); } }
    string _k;
    public LocalizedVector4Field() => LocalizationHelper.Setup(this, Apply);
    void Apply() { var t = LocalizationHelper.Translate(_k); if (t != null) label = t; }
}

// ============================================================
// DROPDOWN / ENUM
// ============================================================

[UxmlElement]
public partial class LocalizedDropdownField : DropdownField
{
    [UxmlAttribute] public string locKeyLabel { get => _kl; set { _kl = value; Apply(); } }

    // Comma-separated keys: "Items/Sword,Items/Shield,Items/Potion"
    [UxmlAttribute] public string locKeyChoices { get => _kc; set { _kc = value; Apply(); } }
    string _kl, _kc;

    public LocalizedDropdownField() => LocalizationHelper.Setup(this, Apply);

    void Apply()
    {
        var tl = LocalizationHelper.Translate(_kl);
        if (tl != null) label = tl;

        if (!string.IsNullOrEmpty(_kc))
        {
            var keys = _kc.Split(',');
            var translated = new List<string>();
            foreach (var k in keys)
            {
                var t = LocalizationHelper.Translate(k.Trim());
                translated.Add(t ?? k.Trim());
            }
            choices = translated;
        }
    }
}

[UxmlElement]
public partial class LocalizedEnumField : EnumField
{
    [UxmlAttribute] public string locKey { get => _k; set { _k = value; Apply(); } }
    string _k;
    public LocalizedEnumField() => LocalizationHelper.Setup(this, Apply);
    void Apply() { var t = LocalizationHelper.Translate(_k); if (t != null) label = t; }
}
