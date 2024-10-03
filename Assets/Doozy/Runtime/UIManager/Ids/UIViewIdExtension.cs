// Copyright (c) 2015 - 2023 Doozy Entertainment. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

//.........................
//.....Generated Class.....
//.........................
//.......Do not edit.......
//.........................

using System.Collections.Generic;
// ReSharper disable All
namespace Doozy.Runtime.UIManager.Containers
{
    public partial class UIView
    {
        public static IEnumerable<UIView> GetViews(UIViewId.CoreGameplay id) => GetViews(nameof(UIViewId.CoreGameplay), id.ToString());
        public static void Show(UIViewId.CoreGameplay id, bool instant = false) => Show(nameof(UIViewId.CoreGameplay), id.ToString(), instant);
        public static void Hide(UIViewId.CoreGameplay id, bool instant = false) => Hide(nameof(UIViewId.CoreGameplay), id.ToString(), instant);

        public static IEnumerable<UIView> GetViews(UIViewId.MainMenu id) => GetViews(nameof(UIViewId.MainMenu), id.ToString());
        public static void Show(UIViewId.MainMenu id, bool instant = false) => Show(nameof(UIViewId.MainMenu), id.ToString(), instant);
        public static void Hide(UIViewId.MainMenu id, bool instant = false) => Hide(nameof(UIViewId.MainMenu), id.ToString(), instant);

        public static IEnumerable<UIView> GetViews(UIViewId.Root id) => GetViews(nameof(UIViewId.Root), id.ToString());
        public static void Show(UIViewId.Root id, bool instant = false) => Show(nameof(UIViewId.Root), id.ToString(), instant);
        public static void Hide(UIViewId.Root id, bool instant = false) => Hide(nameof(UIViewId.Root), id.ToString(), instant);
    }
}

namespace Doozy.Runtime.UIManager
{
    public partial class UIViewId
    {
        public enum CoreGameplay
        {
            Common,
            Defeat,
            Victory
        }

        public enum MainMenu
        {
            Common
        }

        public enum Root
        {
            Common,
            LoadingScreen,
            Settings
        }    
    }
}