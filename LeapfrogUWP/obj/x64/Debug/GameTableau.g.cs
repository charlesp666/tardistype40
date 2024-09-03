﻿#pragma checksum "C:\Users\charl\source\repos\charlesp666\tardistype40\LeapFrogUWP\GameTableau.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "7BD5B3AE44E49F2DC6D85970EAB947AB2509F864C13D55A6215BEDB04EA98AA6"
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

            // Static fields for each binding's enabled/disabled state
            private static bool isobj4ItemsSourceDisabled = false;

            public GameTableau_obj1_Bindings()
            {
            }

            public void Disable(int lineNumber, int columnNumber)
            {
                if (lineNumber == 82 && columnNumber == 19)
                {
                    isobj4ItemsSourceDisabled = true;
                }
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 4: // GameTableau.xaml line 80
                        this.obj4 = (global::Windows.UI.Xaml.Controls.GridView)target;
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
            }

            public void DisconnectUnloadedObject(int connectionId)
            {
                throw new global::System.ArgumentException("No unloadable elements to disconnect.");
            }

            public bool SetDataRoot(global::System.Object newDataRoot)
            {
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
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_gameDeck(obj.gameDeck, phase);
                    }
                }
            }
            private void Update_gameDeck(global::LeapfrogUWP.Cards obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_gameDeck_deckCards(obj.deckCards, phase);
                    }
                }
            }
            private void Update_gameDeck_deckCards(global::System.ComponentModel.BindingList<global::LeapfrogUWP.Cards.Card> obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // GameTableau.xaml line 80
                    if (!isobj4ItemsSourceDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Controls_ItemsControl_ItemsSource(this.obj4, obj, null);
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
            case 2: // GameTableau.xaml line 14
                {
                    this.gameTableau = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 3: // GameTableau.xaml line 30
                {
                    this.bdrHeader = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 4: // GameTableau.xaml line 80
                {
                    this.dataGridGameBoard = (global::Windows.UI.Xaml.Controls.GridView)(target);
                    ((global::Windows.UI.Xaml.Controls.GridView)this.dataGridGameBoard).ItemClick += this.dataGridGameBoard_CellClick;
                }
                break;
            case 5: // GameTableau.xaml line 127
                {
                    this.grdActions = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 6: // GameTableau.xaml line 153
                {
                    this.GameFooter = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 7: // GameTableau.xaml line 160
                {
                    this.btnNewGame = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnNewGame).Click += this.btnNewGame_Click;
                }
                break;
            case 8: // GameTableau.xaml line 171
                {
                    this.btnHelp = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnHelp).Click += this.btnHelp_Click;
                }
                break;
            case 12: // GameTableau.xaml line 50
                {
                    this.someImage = (global::Windows.UI.Xaml.Controls.Border)(target);
                }
                break;
            case 13: // GameTableau.xaml line 68
                {
                    this.tblHeader = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 14: // GameTableau.xaml line 56
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

