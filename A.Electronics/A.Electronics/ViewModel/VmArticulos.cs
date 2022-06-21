using A.Electronics.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;
using Xamarin.Forms;

namespace A.Electronics.ViewModel
{
    [Serializable]
    public class VmArticulos : INotifyPropertyChanged
    {


        public VmArticulos()
        {
            Guardar = new Command(
                    () =>
                    {


                        ModelArticulo x = new ModelArticulo()
                        {
                            codigo = Codigo,
                            nombre = Nombre,
                            modelo = Modelo,
                            marca = Marca,
                            precio = Precio
                          
                        };

                        x.ListSave.Add(new ModelArticulo { codigo = "", nombre = "", marca = "", modelo = "", precio = 0 } );

                       


                        BinaryFormatter formatter = new BinaryFormatter();
                        string ruta = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Model.aut");
                        Stream archivo = new FileStream(ruta, FileMode.Create, FileAccess.Write, FileShare.None);
                        formatter.Serialize(archivo, x);
                        archivo.Close();

                        bool existia = false;

                        for (int i = 0; i < LstGuardar.Count; i++)
                        {

                            if (LstGuardar[i].codigo == Codigo)
                            {
                                LstGuardar[i] = x;
                                existia = true;
                                // serializar
                                XmlSerializer xmlSerializer = new XmlSerializer(typeof(ModelArticulo));
                                Stream Mystream = new FileStream("myDoc.xml", FileMode.Create, FileAccess.Write);

                                xmlSerializer.Serialize(Mystream, LstGuardar);
                                Mystream.Close();

                                // deserializar

                                Stream stream = new FileStream("myDoc.xml", FileMode.Open, FileAccess.Read);

                                ModelArticulo modelArticulo = (ModelArticulo)xmlSerializer.Deserialize(stream);
                                stream.Close();


                            }

                        }

                        if (existia == false)
                        {
                            LstGuardar.Add(x);
                        
                        }

                    }
                    );


            Buscar = new Command(
                   () => {

                   

                         ModelArticulo temporal = new ModelArticulo();

                          for (int i = 0; i < LstGuardar.Count; i++)
                          {

                             temporal = LstGuardar[i];

                              if (temporal.codigo == Codigo)
                           { 
                                 Nombre = temporal.nombre;
                                  Modelo = temporal.modelo;
                                  Marca = temporal.marca;
                                  Precio = temporal.precio;




                               }


                          }
                       
                         }
                        );
                   }

    public ObservableCollection<ModelArticulo> LstGuardar { get; set; } = new ObservableCollection<ModelArticulo>();

    String codigo;

        public String Codigo
        {
            get => codigo;
            set
            {
                codigo = value;
                var args = new PropertyChangedEventArgs(nameof(Codigo));
                PropertyChanged?.Invoke(this, args);
            }
        }

        String nombre;

        public String Nombre
        {
            get => nombre;
            set
            {

                nombre = value;
                var args = new PropertyChangedEventArgs(nameof(Nombre));
                PropertyChanged?.Invoke(this, args);

            }
        }

        String modelo;

        public String Modelo
        {
            get => modelo;
            set
            {

                modelo = value;
                var args = new PropertyChangedEventArgs(nameof(Modelo));
                PropertyChanged?.Invoke(this, args);

            }
        }

        String marca;

        public String Marca
        {
            get => marca;
            set
            {

                marca = value;
                var args = new PropertyChangedEventArgs(nameof(Marca));
                PropertyChanged?.Invoke(this, args);

            }
        }


        int precio = 0;
        public int Precio
        {

            get => precio;
            set
            {

                precio = value;
                var args = new PropertyChangedEventArgs(nameof(Precio));
                PropertyChanged?.Invoke(this, args);

            }

        }

        public Command Guardar { get; }


        public Command Buscar { get; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
