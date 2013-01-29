using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using MonoGame.Framework;


namespace HeroBash
{
    /// <summary>
    /// The root page used to display the game.
    /// </summary>
    public sealed partial class GamePage : SwapChainBackgroundPanel
    {
        readonly HeroBash _game;

        public static GamePage Instance;

        public GamePage(string launchArguments)
        {
            this.InitializeComponent();

            // Create the game.
            _game = XamlGame<HeroBash>.Create(launchArguments, Window.Current.CoreWindow, this);

            Instance = this;
        }

        public void ShowNameEntry()
        {
            if (GameManager.PlayerName != "Player")
                txtName.Text = GameManager.PlayerName;
            else
                txtName.Text = "";

            txtName.Visibility = Windows.UI.Xaml.Visibility.Visible;
            txtName.Focus(FocusState.Keyboard);
        }

        public void HideNameEntry()
        {
            btnDummy.Focus(FocusState.Keyboard); 
            txtName.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        public string GetNameEntry()
        {
            return txtName.Text;
        }

        private void txtName_LostFocus(object sender, RoutedEventArgs e)
        {
            btnDummy.Focus(FocusState.Keyboard);
        }
    }
}
