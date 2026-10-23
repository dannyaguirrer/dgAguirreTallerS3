namespace dgAguirreTallerS3.Views;

public partial class vContactos : ContentPage
{
	public vContactos()
	{
		InitializeComponent();
        datePickerFechaNacimiento.MaximumDate = DateTime.Today;
        datePickerFechaNacimiento.MinimumDate = DateTime.Today.AddYears(-100);
    }

    private async void btnGuardar_Clicked(object sender, EventArgs e)
    {

        if (pickerTipoIdentificacion.SelectedIndex == -1)
        {
            await DisplayAlertAsync("Error", "Seleccione el tipo de identificación", "Cerrar");
            return;
        }

        if (string.IsNullOrWhiteSpace(txtIdentificacion.Text))
        {
            await DisplayAlertAsync("Error", "Ingrese el número de identificación", "Cerrar");
            return;
        }
        if (string.IsNullOrWhiteSpace(txtNombres.Text))
        {
            await DisplayAlertAsync("Error", "Ingrese los nombres", "Cerrar");
            return;
        }
        if (string.IsNullOrWhiteSpace(txtApellidos.Text))
        {
            await DisplayAlertAsync("Error", "Ingrese los apellidos", "Cerrar");
            return;
        }
        if (string.IsNullOrWhiteSpace(txtCorreo.Text))
        {
            await DisplayAlertAsync("Error", "Ingrese el correo electrónico", "Cerrar");
            return;
        }
        if (string.IsNullOrWhiteSpace(txtSalario.Text))
        {
            await DisplayAlertAsync("Error", "Ingrese el salario", "Cerrar");
            return;
            
        }
        // ---- 3. Identificación según el tipo ----
        string tipo = pickerTipoIdentificacion.SelectedItem?.ToString() ?? "";
        string identificacion = txtIdentificacion.Text.Trim();

        if (tipo == "Cédula")
        {
            if (identificacion.Length != 10 || !identificacion.All(char.IsDigit))
            {
                await DisplayAlertAsync("Error", "La cédula debe tener 10 números", "Cerrar");
                return;
            }
        }
        else if (tipo == "RUC")
        {
            if (identificacion.Length != 13 || !identificacion.All(char.IsDigit))
            {
                await DisplayAlertAsync("Error", "El RUC debe tener 13 números", "Cerrar");
                return;
            }
        }
        

        
        if (!txtNombres.Text.All(c => char.IsLetter(c) || c == ' '))
        {
            await DisplayAlertAsync("Error", "Los nombres solo deben tener letras", "Cerrar");
            return;
        }
        if (!txtApellidos.Text.All(c => char.IsLetter(c) || c == ' '))
        {
            await DisplayAlertAsync("Error", "Los apellidos solo deben tener letras", "Cerrar");
            return;
        }

        if (datePickerFechaNacimiento.Date >= DateTime.Today)
        {
            await DisplayAlertAsync("Error", "Seleccione su fecha de nacimiento", "Cerrar");
            return;
        }

        // ---- 5. Correo: debe tener @ y punto ----
        if (!txtCorreo.Text.Contains("@") || !txtCorreo.Text.Contains("."))
        {
            await DisplayAlertAsync("Error", "Ingrese un correo válido (ejemplo: nombre@correo.com)", "Cerrar");
            return;
        }

        // ---- 6. Salario: número mayor que 0 ----
        double salario;
        try
        {
            salario = Convert.ToDouble(txtSalario.Text);
        }
        catch (Exception)
        {
            await DisplayAlertAsync("Error", "El salario debe ser un número", "Cerrar");
            return;
        }
        if (salario <= 0)
        {
            await DisplayAlertAsync("Error", "El salario debe ser mayor que 0", "Cerrar");
            return;
        }

        await Navigation.PushAsync(new vIess(
    tipo,
    identificacion,
    txtNombres.Text.Trim(),
    txtApellidos.Text.Trim(),
    datePickerFechaNacimiento.Date ?? DateTime.Today,
    txtCorreo.Text.Trim(),
    salario));
    }
}