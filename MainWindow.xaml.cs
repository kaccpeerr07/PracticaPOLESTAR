using System;
using System.Net.Mail;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;

namespace PracticaPOLESTAR
{
    public partial class MainWindow : Window
    {
        // Credenciales de demostración
        private const string CorreoDemo = "cliente@polestar.com";
        private const string ContrasenaDemo = "Polestar123";

        // Colores de los campos y mensajes
        private readonly Brush colorNormal = new SolidColorBrush(
            Color.FromRgb(213, 213, 213));

        private readonly Brush colorError = new SolidColorBrush(
            Color.FromRgb(176, 0, 32));

        private readonly Brush colorInformacion = new SolidColorBrush(
            Color.FromRgb(119, 119, 119));

        private readonly Brush colorCorrecto = new SolidColorBrush(
            Color.FromRgb(35, 122, 59));

        // Numero de la campaña que se esta mostrando
        private int numeroCampana = 1;

        public MainWindow()
        {
            InitializeComponent();
        }

        // Muestra una pantalla y oculta las demás
        private void MostrarPagina(FrameworkElement pagina)
        {
            HomePage.Visibility = Visibility.Collapsed;
            VehiclesPage.Visibility = Visibility.Collapsed;
            AccountPage.Visibility = Visibility.Collapsed;
            DetailPage.Visibility = Visibility.Collapsed;

            pagina.Visibility = Visibility.Visible;
        }

        // Muestra la pantalla de inicio
        private void Inicio_Click(object sender, RoutedEventArgs e)
        {
            MostrarPagina(HomePage);
        }

        // Muestra la galería de vehículos
        private void Vehiculos_Click(object sender, RoutedEventArgs e)
        {
            MostrarPagina(VehiclesPage);
        }

        // Muestra la pantalla de cuenta
        private void Cuenta_Click(object sender, RoutedEventArgs e)
        {
            MostrarPagina(AccountPage);

            RestablecerCampos();

            // Mostramos las credenciales para probar el inicio de sesión
            LoginMessage.Text =
                "Credenciales de demostración\n" +
                "Correo: " + CorreoDemo + "\n" +
                "Contraseña: " + ContrasenaDemo;

            LoginMessage.Foreground = colorInformacion;
        }

        // Cambia a la campaña anterior
        private void AnteriorCampana_Click(object sender, RoutedEventArgs e)
        {
            numeroCampana--;

            if (numeroCampana < 1)
            {
                numeroCampana = 3;
            }

            MostrarCampana();
        }

        // Cambia a la campaña siguiente
        private void SiguienteCampana_Click(object sender, RoutedEventArgs e)
        {
            numeroCampana++;

            if (numeroCampana > 3)
            {
                numeroCampana = 1;
            }

            MostrarCampana();
        }

        // Actualiza el contenido de la campaña con una animación
        private void MostrarCampana()
        {
            // Animación para ocultar la campaña actual
            DoubleAnimation animacionSalida = new DoubleAnimation(
                1,
                0,
                TimeSpan.FromMilliseconds(125)
            );

            animacionSalida.Completed += (s, e) =>
            {
                // Actualizamos el contenido cuando desaparece
                if (numeroCampana == 1)
                {
                    CampaignCategory.Text = "01 / EXPERIENCIA";
                    CampaignTitle.Text = "Tu primera experiencia eléctrica";
                    CampaignDescription.Text =
                        "Descubre cómo se siente conducir un Polestar y conoce nuestra gama de vehículos.";

                    CampaignImage.Source = new BitmapImage(
                        new Uri("/Assets/Images/campana-experiencia.jpg",
                        UriKind.Relative));
                }
                else if (numeroCampana == 2)
                {
                    CampaignCategory.Text = "02 / MOVILIDAD";
                    CampaignTitle.Text = "Energía para cada día";
                    CampaignDescription.Text =
                        "Tecnología eléctrica pensada para acompañarte en tus desplazamientos cotidianos.";

                    CampaignImage.Source = new BitmapImage(
                        new Uri("/Assets/Images/campana-energia.jpg",
                        UriKind.Relative));
                }
                else if (numeroCampana == 3)
                {
                    CampaignCategory.Text = "03 / DISEÑO";
                    CampaignTitle.Text = "Diseñado para avanzar";
                    CampaignDescription.Text =
                        "Descubre la combinación de diseño, tecnología y comodidad en cada trayecto.";

                    CampaignImage.Source = new BitmapImage(
                        new Uri("/Assets/Images/campana-diseno.jpg",
                        UriKind.Relative));
                }

                CampaignNumber.Text = "0" + numeroCampana + " / 03";

                // Animación para mostrar la nueva campaña
                DoubleAnimation animacionEntrada = new DoubleAnimation(
                    0,
                    1,
                    TimeSpan.FromMilliseconds(125)
                );

                CampaignContent.BeginAnimation(
                    UIElement.OpacityProperty,
                    animacionEntrada
                );
            };

            // Iniciamos la animación de salida
            CampaignContent.BeginAnimation(
                UIElement.OpacityProperty,
                animacionSalida
            );
        }

        // Abre la ficha del Polestar 2
        private void Polestar2_Click(object sender, RoutedEventArgs e)
        {
            MostrarDetalle(
                "Polestar 2",
                "Standard range Single motor",
                "48.200 €",
                "272 CV",
                "554 km",
                "6,4 s",
                "Fastback 100% eléctrico con tracción trasera. Combina un diseño escandinavo moderno con una conducción dinámica y tecnología avanzada.",
                "/Assets/Images/polestar-2.png"
            );
        }

        // Abre la ficha del Polestar 3
        private void Polestar3_Click(object sender, RoutedEventArgs e)
        {
            MostrarDetalle(
                "Polestar 3",
                "Rear motor",
                "81.900 €",
                "333 CV",
                "603 km",
                "6,5 s",
                "SUV eléctrico de lujo con tracción trasera, amplio espacio interior y un diseño que combina elegancia, tecnología y rendimiento.",
                "/Assets/Images/polestar-3.png"
            );
        }

        // Abre la ficha del Polestar 4
        private void Polestar4_Click(object sender, RoutedEventArgs e)
        {
            MostrarDetalle(
                "Polestar 4",
                "SUV Rear motor",
                "55.600 €",
                "272 CV",
                "630 km",
                "7,3 s",
                "SUV coupé eléctrico con tracción trasera. Destaca por su diseño atrevido, su silueta deportiva y una experiencia de conducción eléctrica moderna.",
                "/Assets/Images/polestar-4.png"
            );
        }

        // Actualiza la información y muestra la ficha seleccionada
        private void MostrarDetalle(
            string nombre,
            string version,
            string precio,
            string potencia,
            string autonomia,
            string aceleracion,
            string descripcion,
            string rutaImagen)
        {
            DetailTitle.Text = nombre;
            DetailSubtitle.Text = version;
            DetailPrice.Text = "Desde " + precio;
            DetailPower.Text = potencia;
            DetailRange.Text = autonomia;
            DetailAcceleration.Text = aceleracion;
            DetailDescription.Text = descripcion;

            DetailImage.Source = new BitmapImage(
                new Uri(rutaImagen, UriKind.Relative)
            );

            MostrarPagina(DetailPage);
        }

        // Restablece los bordes y el mensaje del formulario
        private void RestablecerCampos()
        {
            EmailTextBox.BorderBrush = colorNormal;
            PasswordInput.BorderBrush = colorNormal;

            LoginMessage.Text = "";
            LoginMessage.Foreground = colorError;
        }

        // Comprueba si el correo tiene un formato válido
        private bool EsCorreoValido(string email)
        {
            try
            {
                MailAddress direccion = new MailAddress(email);

                return direccion.Address.Equals(
                    email,
                    StringComparison.OrdinalIgnoreCase
                );
            }
            catch
            {
                return false;
            }
        }

        // Validación del formulario de inicio de sesión
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text.Trim();
            string password = PasswordInput.Password;

            RestablecerCampos();

            // Comprobamos si los campos están vacíos
            if (email == "" || password == "")
            {
                if (email == "")
                {
                    EmailTextBox.BorderBrush = colorError;
                    EmailTextBox.Focus();
                }

                if (password == "")
                {
                    PasswordInput.BorderBrush = colorError;

                    if (email != "")
                    {
                        PasswordInput.Focus();
                    }
                }

                LoginMessage.Text =
                    "Completa el correo electrónico y la contraseña.";

                LoginMessage.Foreground = colorError;

                return;
            }

            // Comprobamos el formato del correo electrónico
            if (!EsCorreoValido(email))
            {
                EmailTextBox.BorderBrush = colorError;
                EmailTextBox.Focus();

                LoginMessage.Text =
                    "Introduce una dirección de correo electrónico válida.";

                LoginMessage.Foreground = colorError;

                return;
            }

            // Comprobamos las credenciales de demostración
            if (email.Equals(
                    CorreoDemo,
                    StringComparison.OrdinalIgnoreCase)
                && password == ContrasenaDemo)
            {
                LoginMessage.Text =
                    "Inicio de sesión correcto. ¡Bienvenido!";

                LoginMessage.Foreground = colorCorrecto;
            }
            else
            {
                PasswordInput.BorderBrush = colorError;
                PasswordInput.Focus();

                LoginMessage.Text =
                    "El correo o la contraseña no son correctos.";

                LoginMessage.Foreground = colorError;
            }
        }
    }
}