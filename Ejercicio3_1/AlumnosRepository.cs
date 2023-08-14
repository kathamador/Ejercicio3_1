using Firebase.Database;
using Firebase.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio3_1
{
    public class AlumnosRepository
    {
        //CONEXION A FIREBASE
        FirebaseClient firebaseClient = new FirebaseClient("https://ejerciciocrud-6a775-default-rtdb.firebaseio.com/");
        FirebaseStorage firebaseStorage = new FirebaseStorage("ejerciciocrud-6a775.appspot.com");

        //METODO PARA GUARDAR
        public async Task<bool> Guardar(Alumnos alumnos)
        {
            var data = await firebaseClient.Child(nameof(Alumnos)).PostAsync(JsonConvert.SerializeObject(alumnos));

            if (!string.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            return false;
        }

        //METODO PARA OBTENER LISTADO DE ALUMNOS
        public async Task<List<Alumnos>> GetAll()
        {
            return (await firebaseClient.Child(nameof(Alumnos)).OnceAsync<Alumnos>()).Select(item => new Alumnos
            {
                nombres = item.Object.nombres,
                apellidos = item.Object.apellidos,
                sexo = item.Object.sexo,
                direccion = item.Object.direccion,
                foto = item.Object.foto,
                id = item.Key

            }).ToList();
        }

        //Obtener ID
        public async Task<Alumnos>GetById(string id)
        {
            return await firebaseClient.Child(nameof(Alumnos) + "/" + id).OnceSingleAsync<Alumnos>();
        }

        //METODO PARA EDITAR
        public async Task<bool>Editar(Alumnos alumnos)
        {
            await firebaseClient.Child(nameof(Alumnos) + "/" + alumnos.id).PutAsync(JsonConvert.SerializeObject(alumnos));
            return true;
        }

        //METODO PARA ELIMINAR
        public async Task<bool>Eliminar(string id)
        {
            await firebaseClient.Child(nameof(Alumnos) + "/" + id).DeleteAsync();
            return true;
        }

        //SUBIR FOTO
        public async Task<string>subirFoto(Stream foto, string nombrearchivo)
        {
            var imagen = await firebaseStorage.Child("Imagenes").Child(nombrearchivo).PutAsync(foto);
            return imagen;
        }

        //TRAER FOTO
        public async Task<string>GetFoto(string nombrearchivo)
        {
            var fotografia = await firebaseStorage.Child("Imagenes").Child(nombrearchivo).GetDownloadUrlAsync();
            return fotografia;
        }
    } 
}
