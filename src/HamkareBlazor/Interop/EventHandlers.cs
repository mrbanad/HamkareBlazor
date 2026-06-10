using System;
using Microsoft.AspNetCore.Components;

namespace HamkareBlazor;

// used in HamkareCollapse
[EventHandler("ontransitionend", typeof(EventArgs), enableStopPropagation: true, enablePreventDefault: false)]
public static class EventHandlers;
