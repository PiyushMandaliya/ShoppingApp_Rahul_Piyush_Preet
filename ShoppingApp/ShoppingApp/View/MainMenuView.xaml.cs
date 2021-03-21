
using ShoppingApp.Data;
using ShoppingApp.Services;
using ShoppingApp.View.AdminView;
using ShoppingApp.View.UserView;
using ShoppingApp.ViewModel;
using System.Windows;

namespace ShoppingApp.View
{
    /// <summary>
    /// Interaction logic for MainMenuView.xaml
    /// </summary>
    public partial class MainMenuView : Window
    {

        UserRepository userRepository;
        UserService userService;

        public MainMenuView()
        {
            InitializeComponent();
            setInitialData();
            DataContext = CreateViewModel();
        }

        private MainMenuViewModel CreateViewModel()
        {
            MainMenuViewModel mainMenuViewModel = new MainMenuViewModel();
            mainMenuViewModel.AdminLoginAction += OpenAdminLoginWindow;
            mainMenuViewModel.UserLoginAction += OpenUserLoginWindow;
            return mainMenuViewModel;
        }

        private void setInitialData()
        {
            userRepository = new UserRepository();
            userService = new UserService(userRepository);
        }

        private void OpenAdminLoginWindow()
        {
            AdminLoginView adminLoginView = new AdminLoginView();
            adminLoginView.Show();
            this.Close();
        }

        private void OpenUserLoginWindow()
        {
            LoginView loginView = new LoginView(userService);
            loginView.Show();
            this.Close();
        }
    }
}
