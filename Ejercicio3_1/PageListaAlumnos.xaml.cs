using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ejercicio3_1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageListaAlumnos : ContentPage
    {
        AlumnosRepository repository = new AlumnosRepository();
        public PageListaAlumnos()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            var alumnos = await repository.GetAll();
            ListaAlumnosView.ItemsSource = null;
            ListaAlumnosView.ItemsSource= alumnos;
        }

        private void AgregarToolBarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new PageAgregarAlumnos());
        }

        private void ListaAlumnosView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }
            var alumnos = e.Item as Alumnos;
            Navigation.PushModalAsync(new PageDetalleAlumnos(alumnos));
            ((ListView)sender).SelectedItem = null;
        }

        private async void EliminarTap_Tapped(object sender, EventArgs e)
        {
            var respuesta = await DisplayAlert("Eliminar", "¿Esta seguro de eliminar el dato?","Si", "No");
            
            if (respuesta)
            {
                string id = ((TappedEventArgs)e).Parameter.ToString();
                bool eliminar = await repository.Eliminar(id);

                if (eliminar)
                {
                    await DisplayAlert("Informacion", "Datos eliminados exitosamente!", "OK");
                    OnAppearing();
                }
                else
                {
                    await DisplayAlert("Error", "Error al eliminar!", "OK");
                }
            }
        }

        private async void EditarTap_Tapped(object sender, EventArgs e)
        {
            string id = ((TappedEventArgs)e).Parameter.ToString();
            var alumnos = await repository.GetById(id);

            if (alumnos == null)
            {
                await DisplayAlert("Advertencia", "Dato no encontrado.", "Ok");
            }
            else
            {
                alumnos.id = id;
                await Navigation.PushModalAsync(new PageEditarAlumnos(alumnos));
            }
        }
    }
}