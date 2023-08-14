using Firebase.Storage;
using Plugin.Media;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ejercicio3_1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageEditarAlumnos : ContentPage
    {
       
        AlumnosRepository repository = new AlumnosRepository();
        Plugin.Media.Abstractions.MediaFile photo = null;

        public PageEditarAlumnos(Alumnos alumnos)
        {
            InitializeComponent();

            txtnombre.Text = alumnos.nombres;
            txtapellido.Text = alumnos.apellidos;
            txtsexo.SelectedItem = alumnos.sexo;
            txtdireccion.Text = alumnos.direccion;
            txtid.Text = alumnos.id;
            //fotoalumno = alumnos.fotografia;
        }

        

        private async void BtnEditar_Clicked(object sender, EventArgs e)
        {
            
            string Anombres = txtnombre.Text;
            string Aapellidos = txtapellido.Text;
            string Asexo = txtsexo.SelectedItem as String;
            string Adireccion = txtdireccion.Text;
            

            if (string.IsNullOrEmpty(Anombres) || string.IsNullOrEmpty(Aapellidos)
                || string.IsNullOrEmpty(Asexo) || string.IsNullOrEmpty(Adireccion))
            {
                await DisplayAlert("Advertencia", "No dejar campos vacios!!!", "OK");
            }
            else
            {
                Alumnos alumnos = new Alumnos();
                string imagen = await repository.subirFoto(photo.GetStream(), Path.GetFileName(photo.Path));
               
                alumnos.id = txtid.Text;
                alumnos.nombres = Anombres;
                alumnos.apellidos = Aapellidos;
                alumnos.sexo = Asexo;
                alumnos.direccion = Adireccion;
                alumnos.foto = imagen;

                bool editar = await repository.Editar(alumnos);
                if (editar)
                {
                    await DisplayAlert("Informacion", "Alumno editado exitosamente!!!", "OK");
                    await Navigation.PopModalAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Ocurrio un error al editar los datos!!!", "OK");
                }
            }
        }

        public void limpiar()
        {
            txtid.Text = string.Empty;
            txtnombre.Text = string.Empty;
            txtapellido.Text = string.Empty;
            txtsexo.SelectedItem = string.Empty;
            txtdireccion.Text = string.Empty;
        }

        private async void BtnCancelar_Clicked(object sender, EventArgs e)
        {
            limpiar();
            await Navigation.PopModalAsync();
        }


        private async void btnfoto_Clicked(object sender, EventArgs e)
        {
            photo = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "imagen",
                Name = "Foto.jpg",
                SaveToAlbum = true
            });

            if (photo != null)
            {
                fotoalumno.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
            }
        }
    }
}