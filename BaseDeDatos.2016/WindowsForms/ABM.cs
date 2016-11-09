using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using AccesoDatos;

namespace WindowsForms
{
    public partial class ABM : Form
    {
        private Persona _persona;

        public Persona PersonaDelFormulario
        {
            get { return _persona; }
        }
        
        public ABM() //altas
        {
            InitializeComponent();
        }

        public ABM(Persona p):this() //modificar y borrar
        {
            this._persona = p;
            this.txtApellido.Text = this._persona.Apellido;
            this.txtNombre.Text = this._persona.Nombre;
            this.txtEdad.Text = this._persona.Edad.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this._persona == null)
            {
                this._persona = new Persona(this.txtApellido.Text, this.txtNombre.Text, int.Parse(this.txtEdad.Text));
            }
            else
            {
                this._persona.Apellido = this.txtApellido.Text;
                this._persona.Nombre = this.txtNombre.Text;
                this._persona.Edad = int.Parse(this.txtEdad.Text);
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;


        }
    }
}
