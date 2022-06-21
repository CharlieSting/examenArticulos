using System;
using System.Collections.Generic;
using System.Text;

namespace A.Electronics.Model
{
    [Serializable]
    public class ModelArticulo
    {
        //CodigoArticulo, Nombre, Modelo, Marca, Precio

        public string codigo { get; set; }    

        public string nombre { get; set; }

        public string modelo { get; set; }

        public string marca { get; set; }

        public int precio { get; set; }

        public List<ModelArticulo> ListSave = new List<ModelArticulo>();
    }
}
