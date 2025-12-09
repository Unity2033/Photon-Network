# Easy Popup System

A powerful and flexible popup and toast notification system for Unity, designed to make creating beautiful, animated UI dialogs effortless. You Don't need to ADD MANAGER to use from API Popup and toast

## Features

### 🎯 **Easy-to-Use API**
- Simple one-line calls to create popups and toasts
- Intuitive method signatures with optional parameters
- Built-in callback system for user interactions

### 🎨 **Beautiful Animations**
- Multiple animation types: Fade, Scale, Slide Up
- Customizable animation controllers
- Smooth show/hide transitions
- Toast-specific animations

### 🎛️ **Flexible Configuration**
- ScriptableObject-based configuration
- Customizable colors, icons, and text
- Multiple positioning options for toasts
- Auto-hide functionality for toasts

### 📱 **Multiple Popup Types**
- **Info Popups** - Informational dialogs
- **Error Popups** - Error notifications
- **Warning Popups** - Warning messages
- **Rate Popups** - App rating requests
- **Custom Popups** - Fully customizable

### 🔔 **Toast Notifications**
- 9 different positioning options
- Auto-hide with configurable delay
- Click-to-dismiss functionality
- Multiple toast types (Info, Error, Warning)

### ⏳ **Loading System**
- **EasyLoader** - Simple loading screen management
- Automatic instantiation from Resources
- Global hide functionality
- Multiple loader cleanup support

## Quick Start

### 1. Setup

1. Import the Easy Popup System package into your Unity project
2. Add the `EasyPopupManager` prefab to your scene
3. Configure your popup and toast settings in the manager

### 2. Create a Simple Popup

```csharp
using EasyPopupSystem;

// Basic popup
EasyPopup.Create("Title", "Message");

// Popup with callbacks
EasyPopup.Create(
    "Confirm Action", 
    "Are you sure you want to proceed?",
    () => Debug.Log("Confirmed!"),
    () => Debug.Log("Cancelled!")
);
```

### 3. Create a Toast Notification

```csharp
using EasyPopupSystem;

// Basic toast
EasyToast.Create("Operation completed successfully!");

// Toast with custom settings
EasyToast.Create(
    "Error occurred!", 
    null, 
    "ToastError", 
    5f, 
    true, 
    EasyToastPosition.TopRight
);
```

### 4. Show a Loading Screen

```csharp
using EasyPopupSystem;

// Show loading screen
EasyLoader.Create();

// Hide loading screen
EasyLoader.Hide();

// Hide all loading screens (if multiple exist)
EasyLoader.HideAllLoaders();
```

## API Reference

### EasyPopup

#### Static Methods

```csharp
// Create popup from ScriptableObject
EasyPopup.Create(EasyPopupScriptableObjectScript scriptableObject, Action onConfirm = null, Action onCancel = null)

// Create popup with custom parameters
EasyPopup.Create(string title, string message, Action onConfirm = null, Action onCancel = null, 
                bool disableBackground = false, string prefabName = "PopupError", 
                string confirmButtonText = "Confirm", string cancelButtonText = "Cancel", 
                Color color = default)
```

#### Instance Methods

```csharp
// Handle user actions
popup.Confirm()
popup.Cancel()

// Customize appearance
popup.SetTitle(string title)
popup.SetMessage(string message)
popup.SetColor(Color color)
popup.SetIcon(Sprite icon)
```

### EasyToast

#### Static Methods

```csharp
// Create toast from ScriptableObject
EasyToast.Create(EasyToastScriptableObjectScript scriptableObject, Action onToastHidden = null)

// Create toast with custom parameters
EasyToast.Create(string message, Action onToastHidden = null, string prefabName = "Toast", 
                float autoHideDelay = 3f, bool autoHide = true, 
                EasyToastPosition toastPosition = EasyToastPosition.TopRight, 
                Color color = default, Sprite icon = null)
```

#### Toast Positions

```csharp
public enum EasyToastPosition {
    TopRight, Top, TopLeft,
    Right, Center, Left,
    BottomRight, Bottom, BottomLeft
}
```

### EasyLoader

#### Static Methods

```csharp
// Create and show loading screen
EasyLoader Create()

// Hide the currently active loading screen
void Hide()

// Hide all loading screens (useful for cleanup)
void HideAllLoaders()
```

## Configuration

### Popup ScriptableObject

Create popup configurations using the `EasyPopupScriptableObjectScript`:

```csharp
[Header("Popup Content")]
public string title;
public string message;
public Color color = Color.white;
public Sprite icon;
public AnimatorController animatorController;

[Header("Button Settings")]
public string confirmButtonText = "Confirm";
public string cancelButtonText = "Cancel";

[Header("Popup Configuration")]
public string prefabName = "Popup";
public bool disableBackground = false;
```

### Toast ScriptableObject

Create toast configurations using the `EasyToastScriptableObjectScript`:

```csharp
[Header("Toast Content")]
public string title;
public string message;
public Color color = Color.red;
public Sprite icon;
public AnimatorController animatorController;

[Header("Toast Configuration")]
public string prefabName = "Toast";
public bool autoHide = true;
public float autoHideDelay = 3f;
public EasyToastPosition toastPosition = EasyToastPosition.TopRight;
```

## EasyPopupManager

The `EasyPopupManager` provides a centralized way to manage popups and toasts:

```csharp
// Create popup by name
EasyPopupManager.Instance.CreatePopup("MyPopupName");

// Create popup by index
EasyPopupManager.Instance.CreatePopup(0);

// Create toast by name
EasyPopupManager.Instance.CreateToast("MyToastName");

// Create toast by index
EasyPopupManager.Instance.CreateToast(0);
```

## Animation System

The system includes several pre-built animation controllers:

- **AnimatorFade** - Smooth fade in/out animations
- **AnimatorScale** - Scale-based animations
- **AnimatorSlideUp** - Slide-up entrance animations
- **AnimatorToast** - Toast-specific animations

You can customize animations by:
1. Creating your own AnimatorController
2. Assigning it to the `animatorController` field in ScriptableObjects
3. Or setting it programmatically using `SetAnimatorController()`

## Examples

### Loading Screen Usage

```csharp
public void LoadGameData() {
    // Show loading screen
    EasyLoader.Create();
    
    // Simulate async operation
    StartCoroutine(LoadDataCoroutine());
}

private IEnumerator LoadDataCoroutine() {
    // Simulate loading time
    yield return new WaitForSeconds(2f);
    
    // Hide loading screen when done
    EasyLoader.Hide();
    
    // Show success message
    EasyToast.Create("Game data loaded successfully!");
}

public void HandleMultipleLoaders() {
    // If you have multiple loaders, clean them all up
    EasyLoader.HideAllLoaders();
}
```

### Error Handling

```csharp
public void HandleNetworkError() {
    EasyPopup.Create(
        "Connection Error",
        "Unable to connect to the server. Please check your internet connection and try again.",
        () => RetryConnection(),
        () => GoToOfflineMode(),
        true,
        "PopupError",
        "Retry",
        "Offline Mode"
    );
}
```

### Success Notification

```csharp
public void ShowSuccessToast() {
    EasyToast.Create(
        "Settings saved successfully!",
        () => Debug.Log("Success toast hidden"),
        "ToastInfo",
        2f,
        true,
        EasyToastPosition.TopRight
    );
}
```

### Rating Request

```csharp
public void ShowRatingPopup() {
    EasyPopup.Create(
        "Enjoying the app?",
        "If you like our app, please consider giving it a 5-star rating!",
        () => OpenAppStore(),
        () => Debug.Log("Rating declined"),
        false,
        "PopupRate",
        "Rate Now",
        "Maybe Later"
    );
}
```

## Requirements

- TextMesh Pro
- Universal Render Pipeline (URP) recommended

## Installation

1. Download the Easy Popup System package
2. Import it into your Unity project
3. Add the `EasyPopupManager` prefab to your scene if you need use scriptableObjects
4. Configure your popup and toast settings
5. Start creating beautiful popups!

## Support

For support, questions, or feature requests, please contact DevePolers.

## License

This asset is created by DevePolers. Please refer to the license terms included with the package.

---

**Made with ❤️ by DevePolers**
