namespace dgAguirreTallerS3.Views;

public partial class vIess : ContentPage
{
    public vIess(string tipo, string identificacion, string nombres, string apellidos,
             DateTime fechaNacimiento, string correo, double salario)
    {
        InitializeComponent();

        int edad = DateTime.Today.Year - fechaNacimiento.Year;
        if (fechaNacimiento > DateTime.Today.AddYears(-edad))
        {
            edad--;
        }

        // Aporte al IESS (9,45 %)
        double aporte = salario * 0.0945;

        // Mostrar en los Labels
        lblIdentificacion.Text = tipo + ": " + identificacion;
        lblNombres.Text = "Nombres: " + nombres;
        lblApellidos.Text = "Apellidos: " + apellidos;
        lblFecha.Text = "Fecha de nacimiento: " + fechaNacimiento.ToString("dd/MM/yyyy");
        lblEdad.Text = "Edad: " + edad + " años";
        lblCorreo.Text = "Correo: " + correo;
        lblSalario.Text = "Salario: $" + salario.ToString("F2");
        lblAporte.Text = "Aporte IESS (9,45%): $" + aporte.ToString("F2");
    }

    private async void btnExportar_Clicked(object sender, EventArgs e)
    {
        string datos =
        "RESUMEN DEL CONTACTO\n" +
        lblIdentificacion.Text + "\n" +
        lblNombres.Text + "\n" +
        lblApellidos.Text + "\n" +
        lblFecha.Text + "\n" +
        lblEdad.Text + "\n" +
        lblCorreo.Text + "\n" +
        lblSalario.Text + "\n" +
        lblAporte.Text;

        // Grabar en la carpeta Documentos
        string ruta = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "contacto.txt");
        File.WriteAllText(ruta, datos);

        await DisplayAlertAsync("Exportado", "Archivo guardado en:\n" + ruta, "OK");    

    }

    private async void btnRegresar_Clicked(object sender, EventArgs e)
    {
        await  Navigation.PopAsync();
    }
}