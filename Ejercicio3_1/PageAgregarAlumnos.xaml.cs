using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ejercicio3_1
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageAgregarAlumnos : ContentPage
	{
		AlumnosRepository repository = new AlumnosRepository();
        
        public PageAgregarAlumnos ()
		{
			InitializeComponent ();
		}

        private async void BtnGuardar_Clicked(object sender, EventArgs e)
        {
			string Anombres = txtnombre.Text;
			string Aapellidos = txtapellido.Text;
			string Asexo = txtsexo.SelectedItem as String;
			string Adireccion = txtdireccion.Text;

			if(string.IsNullOrEmpty(Anombres) || string.IsNullOrEmpty(Aapellidos) 
				|| string.IsNullOrEmpty(Asexo) || string.IsNullOrEmpty(Adireccion))
			{
				await DisplayAlert("Advertencia", "No dejar campos vacios!!!", "Cancelar");
			}
			else
			{
                Alumnos alumnos = new Alumnos();
               

                alumnos.nombres = Anombres;
                alumnos.apellidos = Aapellidos;
                alumnos.sexo = Asexo;
                alumnos.direccion = Adireccion;
            
                
                var guardar = await repository.Guardar(alumnos);
                if (guardar)
                {
                    await DisplayAlert("Informacion", "Alumno guardado exitosamente!!!", "OK");
                    limpiar();
                    await Navigation.PopModalAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Ocurrio un error al guardar los datos!!!", "OK");
                }
            }

        }

		public void limpiar()
		{
			txtnombre.Text = string.Empty;
            txtapellido.Text = string.Empty;
			txtsexo.SelectedItem = null;
            txtdireccion.Text = string.Empty;
        }

        private async void BtnCancelar_Clicked(object sender, EventArgs e)
        {
            limpiar();
            await Navigation.PopModalAsync();
        }

       
    }
}