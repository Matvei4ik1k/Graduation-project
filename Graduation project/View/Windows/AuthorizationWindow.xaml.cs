using Graduation_project.AppData;
using Graduation_project.Model;
using System.Windows;

namespace Graduation_project.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTb.Text;
            string password = PasswordPb.Password;

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Заполните все поля", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                using (var context = new GraduationProjectContext())
                {
                    var user = context.Users.FirstOrDefault(u => u.Name == login);
                    if (user != null)
                    {
                        if (PasswordHelper.VerifyPassword(password, user.Password))
                        {
                            UserSession.UserId = user.Id;
                            UserSession.UserName = user.Name;

                            MessageBox.Show("Успешеый вход", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                            MainWindow mainWindow = new MainWindow();
                            mainWindow.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Неправильный пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пользователь с таким логином не найден", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при входе {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void RegistrationBtn_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registration = new RegistrationWindow();
            registration.Show();
            this.Close();
        }

        private void LoginTb_GotFocus(object sender, RoutedEventArgs e)
        {
            if (LoginTb.Text=="Введите логин")
            {
            LoginTb.Text = "";
            }
        }

        private void LoginTb_LostFocus(object sender, RoutedEventArgs e)
        {
            if (LoginTb.Text == "")
            {
                LoginTb.Text = "Введите логин";
            }
        }

        private void PasswordPb_GotFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordPb.Password == "qwerqwer")
            {
                PasswordPb.Password = "";
            }
        }

        private void PasswordPb_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordPb.Password == "")
            {
                PasswordPb.Password = "qwerqwer";
            }
        }
    }
}
