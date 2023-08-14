using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Ejercicio3_1
{
    public class Alumnos
    {
        //FirebaseStorage firebaseStorage = new FirebaseStorage("ejerciciocrud-6a775.appspot.com");
        public string id { set; get; }
        public string nombres { set; get; }
        public string apellidos { set; get; }
        public string sexo { set; get; }
        public string direccion { set; get; }
        public string foto { set; get;}

        /*
        public ImageSource fotografia
        {
            get
            {
                byte[] bytesImage = foto;
                var stream = new MemoryStream(bytesImage);
                return ImageSource.FromStream(() => stream);
            }      
        }

        public async Task<string> GetFoto()
        {
            var fotografia = await firebaseStorage.Child("Imagenes").GetDownloadUrlAsync();
            return fotografia;
        }
        */
    }
}
