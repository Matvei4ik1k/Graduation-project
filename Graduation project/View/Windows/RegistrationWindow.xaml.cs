using Graduation_project.AppData;
using Graduation_project.Models;
using System.Windows;

namespace Graduation_project.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }


        private void RegistrationBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LoginTb.Text) || string.IsNullOrWhiteSpace(EmailTb.Text) || string.IsNullOrWhiteSpace(ThinkPasswordPb.Password) || string.IsNullOrWhiteSpace(RepeatPasswordPb.Password))
            {
                MessageBox.Show("Заполните все поля", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (ThinkPasswordPb.Password != RepeatPasswordPb.Password)
                {
                    MessageBox.Show("Пароли не совпадают", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    try
                    {
                        using (var context = new GraduationProjectContext())
                        {
                            string passwordHash = PasswordHelper.HashPassword(ThinkPasswordPb.Password);
                            var newUser = new User()
                            {
                                Email = EmailTb.Text,
                                Name = LoginTb.Text,
                                Password = passwordHash
                            };
                            context.Add(newUser);
                            context.SaveChanges();
                        }
                        MessageBox.Show("Успешная регистрация", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при регистрации {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void AuthorizationBtn_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationWindow authorization = new AuthorizationWindow();
            authorization.Show();
            this.Close();
        }
    }
}
