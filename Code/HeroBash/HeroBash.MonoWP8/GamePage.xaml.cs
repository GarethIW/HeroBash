using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Xna.Framework;
using MonoGame.Framework.WindowsPhone;
using HeroBash.Resources;

namespace HeroBash
{
    public partial class GamePage : PhoneApplicationPage
    {
        public static GamePage Instance;

        private HeroBash _game;

        // Constructor
        public GamePage()
        {
            InitializeComponent();

            _game = XamlGame<HeroBash>.Create("", XnaSurface);

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();

            Instance = this;
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}

        public void ShowNameEntry()
        {
            if (GameManager.PlayerName != "Player")
                txtName.Text = GameManager.PlayerName;
            else
                txtName.Text = "";

            txtName.Visibility = Visibility.Visible;
            txtName.Focus();//FocusState.Keyboard);
        }

        public void HideNameEntry()
        {
            btnDummy.Focus();//FocusState.Keyboard);
            txtName.Visibility = Visibility.Collapsed;
        }

        public string GetNameEntry()
        {
            return txtName.Text;
        }

        private void txtName_LostFocus(object sender, RoutedEventArgs e)
        {
            btnDummy.Focus();//FocusState.Keyboard);
        }
    }
}