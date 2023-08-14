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
	public partial class PageDetalleAlumnos : ContentPage
	{
		public PageDetalleAlumnos (Alumnos alumnos)
		{
			InitializeComponent ();
			
			labelId.Text = alumnos.id;
			labelNombres.Text = alumnos.nombres;
            labelApellidos.Text = alumnos.apellidos;
            labelSexo.Text = alumnos.sexo;
            labelDireccion.Text = alumnos.direccion;
        }
	}
}