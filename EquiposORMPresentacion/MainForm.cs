using System;
using System.Windows.Forms;
using EquiposORMServicios;

namespace EquiposORMPresentacion
{
    public class MainForm : Form
    {

        private readonly ServicioBase< TecnicoDTO> _servicioTecnico;
        private readonly ServicioBase< EquipoDTO> _servicioEquipo;

        public MainForm(ServicioBase< TecnicoDTO> servicioTecnico,ServicioBase< EquipoDTO> servicioEquipo)
        {
            _servicioTecnico = servicioTecnico;
            _servicioEquipo = servicioEquipo;

            Text = "Gestión de Técnicos y Equipos";
            Width = 450;
            Height = 400;

            // Crear el menú principal
            var menuStrip = new MenuStrip();

            // Crear panel contenedor para formularios hijos
            var panelContenedor = new Panel { Dock = DockStyle.Fill, Top = menuStrip.Height };
            Controls.Add(panelContenedor);
            panelContenedor.BringToFront();

            // Función para mostrar un formulario hijo incrustado
            void MostrarFormularioHijo(Form frmHijo)
            {
                panelContenedor.Controls.Clear();
                frmHijo.TopLevel = false;
                frmHijo.FormBorderStyle = FormBorderStyle.None;
                panelContenedor.Controls.Add(frmHijo);

                frmHijo.StartPosition = FormStartPosition.Manual;
                frmHijo.Location = new System.Drawing.Point(
                    (panelContenedor.Width - frmHijo.Width) / 2
                );

                frmHijo.Show();
            }

            // Menú Equipos
            var menuEquipos = new ToolStripMenuItem("Equipos Médicos");
            var itemAdministrarEquipos = new ToolStripMenuItem("Administrar Equipos", null, (s, e) => {
                var frm = new FrmEquipos(_servicioEquipo, _servicioTecnico);
                MostrarFormularioHijo(frm);
            });
            menuEquipos.DropDownItems.Add(itemAdministrarEquipos);

            // Menú Técnicos
            var menuTecnicos = new ToolStripMenuItem("Técnicos");
            var itemAdministrarTecnicos = new ToolStripMenuItem("Administrar Técnicos", null, (s, e) => {
                var frm = new FrmTecnicos(_servicioTecnico);
                MostrarFormularioHijo(frm);
            });
            menuTecnicos.DropDownItems.Add(itemAdministrarTecnicos);

            // Menú Mantenimientos (placeholder para futuras funcionalidades)
            var menuMantenimientos = new ToolStripMenuItem("Mantenimiento");
            var itemOTM = new ToolStripMenuItem("Generar OTM", null, (s, e) => {
                var frm = new FrmGenerarOTM();
                MostrarFormularioHijo(frm);
            });
            menuMantenimientos.DropDownItems.Add(itemOTM);

            // Agregar los menús al menú principal
            menuStrip.Items.Add(menuEquipos);
            menuStrip.Items.Add(menuTecnicos);
            menuStrip.Items.Add(menuMantenimientos);

            // Agregar el menú al formulario
            MainMenuStrip = menuStrip;
            Controls.Add(menuStrip);
        }
    }
}