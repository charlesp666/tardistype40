﻿#pragma checksum "C:\Users\charl\source\repos\charlesp666\tardistype40\LeapFrogUWP\GameTableau.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "121C072A0D617DA9570F2F194D94B7DD707E67BB82B8AB65E18A9FDFF4801928"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LeapfrogUWP
{
    partial class GameTableau : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 0.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private static class XamlBindingSetters
        {
            public static void Set_Windows_UI_Xaml_Controls_ItemsControl_ItemsSource(global::Windows.UI.Xaml.Controls.ItemsControl obj, global::System.Object value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = (global::System.Object) global::Windows.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::System.Object), targetNullValue);
                }
                obj.ItemsSource = value;
            }
            public static void Set_Windows_UI_Xaml_Controls_Image_Source(global::Windows.UI.Xaml.Controls.Image obj, global::Windows.UI.Xaml.Media.ImageSource value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = (global::Windows.UI.Xaml.Media.ImageSource) global::Windows.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::Windows.UI.Xaml.Media.ImageSource), targetNullValue);
                }
                obj.Source = value;
            }
            public static void Set_Windows_UI_Xaml_Controls_TextBlock_Text(global::Windows.UI.Xaml.Controls.TextBlock obj, global::System.String value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = targetNullValue;
                }
                obj.Text = value ?? global::System.String.Empty;
            }
        };

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 0.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private class GameTableau_obj1_Bindings :
            global::Windows.UI.Xaml.Markup.IDataTemplateComponent,
            global::Windows.UI.Xaml.Markup.IXamlBindScopeDiagnostics,
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            IGameTableau_Bindings
        {
            private global::LeapfrogUWP.GameTableau dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);

            // Fields for each control that has bindings.
            private global::Windows.UI.Xaml.Controls.GridView obj4;
            private global::Windows.UI.Xaml.Controls.Image obj14;
            private global::Windows.UI.Xaml.Controls.Image obj15;
            private global::Windows.UI.Xaml.Controls.TextBlock obj17;

            // Static fields for each binding's enabled/disabled state
            private static bool isobj4ItemsSourceDisabled = false;
            private static bool isobj14SourceDisabled = false;
            private static bool isobj15SourceDisabled = false;
            private static bool isobj17TextDisabled = false;

            private GameTableau_obj1_BindingsTracking bindingsTracking;

            public GameTableau_obj1_Bindings()
            {
                this.bindingsTracking = new GameTableau_obj1_BindingsTracking(this);
            }

            public void Disable(int lineNumber, int columnNumber)
            {
                if (lineNumber == 119 && columnNumber == 19)
                {
                    isobj4ItemsSourceDisabled = true;
                }
                else if (lineNumber == 77 && columnNumber == 24)
                {
                    isobj14SourceDisabled = true;
                }
                else if (lineNumber == 88 && columnNumber == 24)
                {
                    isobj15SourceDisabled = true;
                }
                else if (lineNumber == 102 && columnNumber == 28)
                {
                    isobj17TextDisabled = true;
                }
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 4: // GameTableau.xaml line 117
                        this.obj4 = (global::Windows.UI.Xaml.Controls.GridView)target;
                        break;
                    case 14: // GameTableau.xaml line 70
                        this.obj14 = (global::Windows.UI.Xaml.Controls.Image)target;
                        break;
                    case 15: // GameTableau.xaml line 81
                        this.obj15 = (global::Windows.UI.Xaml.Controls.Image)target;
                        break;
                    case 17: // GameTableau.xaml line 101
                        this.obj17 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    default:
                        break;
                }
            }

            // IDataTemplateComponent

            public void ProcessBindings(global::System.Object item, int itemIndex, int phase, out int nextPhase)
            {
                nextPhase = -1;
            }

            public void Recycle()
            {
                return;
            }

            // IGameTableau_Bindings

            public void Initialize()
            {
                if (!this.initialized)
                {
                    this.Update();
                }
            }
            
            public void Update()
            {
                this.Update_(this.dataRoot, NOT_PHASED);
                this.initialized = true;
            }

            public void StopTracking()
            {
                this.bindingsTracking.ReleaseAllListeners();
                this.initialized = false;
            }

            public void DisconnectUnloadedObject(int connectionId)
            {
                throw new global::System.ArgumentException("No unloadable elements to disconnect.");
            }

            public bool SetDataRoot(global::System.Object newDataRoot)
            {
                this.bindingsTracking.ReleaseAllListeners();
                if (newDataRoot != null)
                {
                    this.dataRoot = (global::LeapfrogUWP.GameTableau)newDataRoot;
                    return true;
                }
                return false;
            }

            public void Loading(global::Windows.UI.Xaml.FrameworkElement src, object data)
            {
                this.Initialize();
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::LeapfrogUWP.GameTableau obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | DATA_CHANGED | (1 << 0))) != 0)
                    {
                        this.Update_gameDeck(obj.gameDeck, phase);
                    }
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_cardPlayable(obj.cardPlayable, phase);
                        this.Update_cardNotPlayable(obj.cardNotPlayable, phase);
                    }
                    if ((phase & (NOT_PHASED | DATA_CHANGED | (1 << 0))) != 0)
                    {
                        this.Update_currentActivity(obj.currentActivity, phase);
                    }
                }
            }
            private void Update_gameDeck(global::LeapfrogUWP.Cards obj, int phase)
            {
                this.bindingsTracking.UpdateChildListeners_gameDeck(obj);
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | DATA_CHANGED | (1 << 0))) != 0)
                    {
                        this.Update_gameDeck_deckCards(obj.deckCards, phase);
                    }
                }
            }
            private void Update_gameDeck_deckCards(global::System.ComponentModel.BindingList<global::LeapfrogUWP.Cards.Card> obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // GameTableau.xaml line 117
                    if (!isobj4ItemsSourceDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Controls_ItemsControl_ItemsSource(this.obj4, obj, null);
                    }
                }
            }
            private void Update_cardPlayable(global::LeapfrogUWP.Cards.Card obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_cardPlayable_cardFace(obj.cardFace, phase);
                    }
                }
            }
            private void Update_cardPlayable_cardFace(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // GameTableau.xaml line 70
                    if (!isobj14SourceDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Controls_Image_Source(this.obj14, (global::Windows.UI.Xaml.Media.ImageSource) global::Windows.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::Windows.UI.Xaml.Media.ImageSource), obj), null);
                    }
                }
            }
            private void Update_cardNotPlayable(global::LeapfrogUWP.Cards.Card obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_cardNotPlayable_cardFace(obj.cardFace, phase);
                    }
                }
            }
            private void Update_cardNotPlayable_cardFace(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // GameTableau.xaml line 81
                    if (!isobj15SourceDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Controls_Image_Source(this.obj15, (global::Windows.UI.Xaml.Media.ImageSource) global::Windows.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::Windows.UI.Xaml.Media.ImageSource), obj), null);
                    }
                }
            }
            private void Update_currentActivity(global::CurrentActivity obj, int phase)
            {
                this.bindingsTracking.UpdateChildListeners_currentActivity(obj);
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | DATA_CHANGED | (1 << 0))) != 0)
                    {
                        this.Update_currentActivity_currentActivity(obj.currentActivity, phase);
                    }
                }
            }
            private void Update_currentActivity_currentActivity(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // GameTableau.xaml line 101
                    if (!isobj17TextDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj17, obj, null);
                    }
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 0.0.0.0")]
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            private class GameTableau_obj1_BindingsTracking
            {
                private global::System.WeakReference<GameTableau_obj1_Bindings> weakRefToBindingObj; 

                public GameTableau_obj1_BindingsTracking(GameTableau_obj1_Bindings obj)
                {
                    weakRefToBindingObj = new global::System.WeakReference<GameTableau_obj1_Bindings>(obj);
                }

                public GameTableau_obj1_Bindings TryGetBindingObject()
                {
                    GameTableau_obj1_Bindings bindingObject = null;
                    if (weakRefToBindingObj != null)
                    {
                        weakRefToBindingObj.TryGetTarget(out bindingObject);
                        if (bindingObject == null)
                        {
                            weakRefToBindingObj = null;
                            ReleaseAllListeners();
                        }
                    }
                    return bindingObject;
                }

                public void ReleaseAllListeners()
                {
                    UpdateChildListeners_gameDeck(null);
                    UpdateChildListeners_currentActivity(null);
                }

                public void PropertyChanged_gameDeck(object sender, global::System.ComponentModel.PropertyChangedEventArgs e)
                {
                    GameTableau_obj1_Bindings bindings = TryGetBindingObject();
                    if (bindings != null)
                    {
                        string propName = e.PropertyName;
                        global::LeapfrogUWP.Cards obj = sender as global::LeapfrogUWP.Cards;
                        if (global::System.String.IsNullOrEmpty(propName))
                        {
                            if (obj != null)
                            {
                                bindings.Update_gameDeck_deckCards(obj.deckCards, DATA_CHANGED);
                            }
                        }
                        else
                        {
                            switch (propName)
                            {
                                case "deckCards":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_gameDeck_deckCards(obj.deckCards, DATA_CHANGED);
                                    }
                                    break;
                                }
                                default:
                                    break;
                            }
                        }
                    }
                }
                private global::LeapfrogUWP.Cards cache_gameDeck = null;
                public void UpdateChildListeners_gameDeck(global::LeapfrogUWP.Cards obj)
                {
                    if (obj != cache_gameDeck)
                    {
                        if (cache_gameDeck != null)
                        {
                            ((global::System.ComponentModel.INotifyPropertyChanged)cache_gameDeck).PropertyChanged -= PropertyChanged_gameDeck;
                            cache_gameDeck = null;
                        }
                        if (obj != null)
                        {
                            cache_gameDeck = obj;
                            ((global::System.ComponentModel.INotifyPropertyChanged)obj).PropertyChanged += PropertyChanged_gameDeck;
                        }
                    }
                }
                public void PropertyChanged_currentActivity(object sender, global::System.ComponentModel.PropertyChangedEventArgs e)
                {
                    GameTableau_obj1_Bindings bindings = TryGetBindingObject();
                    if (bindings != null)
                    {
                        string propName = e.PropertyName;
                        global::CurrentActivity obj = sender as global::CurrentActivity;
                        if (global::System.String.IsNullOrEmpty(propName))
                        {
                            if (obj != null)
                            {
                                bindings.Update_currentActivity_currentActivity(obj.currentActivity, DATA_CHANGED);
                            }
                        }
                        else
                        {
                            switch (propName)
                            {
                                case "currentActivity":
                                {
                                    if (obj != null)
                                    {
                                        bindings.Update_currentActivity_currentActivity(obj.currentActivity, DATA_CHANGED);
                                    }
                                    break;
                                }
                                default:
                                    break;
                            }
                        }
                    }
                }
                private global::CurrentActivity cache_currentActivity = null;
                public void UpdateChildListeners_currentActivity(global::CurrentActivity obj)
                {
                    if (obj != cache_currentActivity)
                    {
                        if (cache_currentActivity != null)
                        {
                            ((global::System.ComponentModel.INotifyPropertyChanged)cache_currentActivity).PropertyChanged -= PropertyChanged_currentActivity;
                            cache_currentActivity = null;
                        }
                        if (obj != null)
                        {
                            cache_currentActivity = obj;
                            ((global::System.ComponentModel.INotifyPropertyChanged)obj).PropertyChanged += PropertyChanged_currentActivity;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 0.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1: // GameTableau.xaml line 1
                {
                    this.pgGameTableau = (global::Windows.UI.Xaml.Controls.Page)(target);
                }
                break;
            case 2: // GameTableau.xaml line 15
                {
                    this.gameTableau = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 3: // GameTableau.xaml line 32
                {
                    this.bdrHeader = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 4: // GameTableau.xaml line 117
                {
                    this.dataGridGameBoard = (global::Windows.UI.Xaml.Controls.GridView)(target);
                    ((global::Windows.UI.Xaml.Controls.GridView)this.dataGridGameBoard).ItemClick += this.dataGridGameBoard_CellClick;
                }
                break;
            case 5: // GameTableau.xaml line 164
                {
                    this.grdActions = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 6: // GameTableau.xaml line 190
                {
                    this.btnNewGame = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnNewGame).Click += this.btnNewGame_Click;
                }
                break;
            case 7: // GameTableau.xaml line 201
                {
                    this.btnStats = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnStats).Click += this.btnStats_Click;
                }
                break;
            case 8: // GameTableau.xaml line 212
                {
                    this.btnHelp = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnHelp).Click += this.btnHelp_Click;
                }
                break;
            case 9: // GameTableau.xaml line 223
                {
                    this.btnExit = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnExit).Click += this.btnExit_Click;
                }
                break;
            case 13: // GameTableau.xaml line 52
                {
                    this.someImage = (global::Windows.UI.Xaml.Controls.Border)(target);
                }
                break;
            case 14: // GameTableau.xaml line 70
                {
                    this.PlayableImage = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 15: // GameTableau.xaml line 81
                {
                    this.NotPlayableImage = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 16: // GameTableau.xaml line 92
                {
                    this.brdCurrentActivity = (global::Windows.UI.Xaml.Controls.Border)(target);
                }
                break;
            case 17: // GameTableau.xaml line 101
                {
                    this.tbCurrentActivity = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 18: // GameTableau.xaml line 58
                {
                    this.someJunk = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 0.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            switch(connectionId)
            {
            case 1: // GameTableau.xaml line 1
                {                    
                    global::Windows.UI.Xaml.Controls.Page element1 = (global::Windows.UI.Xaml.Controls.Page)target;
                    GameTableau_obj1_Bindings bindings = new GameTableau_obj1_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot(this);
                    this.Bindings = bindings;
                    element1.Loading += bindings.Loading;
                    global::Windows.UI.Xaml.Markup.XamlBindingHelper.SetDataTemplateComponent(element1, bindings);
                }
                break;
            }
            return returnValue;
        }
    }
}

