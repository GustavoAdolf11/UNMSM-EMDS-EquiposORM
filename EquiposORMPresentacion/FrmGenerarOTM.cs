using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using EquiposORM.Persistencia;
using EquiposORM.Dominio;
using System.Collections.Generic;

namespace EquiposORMPresentacion
{
    public class FrmGenerarOTM : Form
    {
        private List<CheckBox> _checkBoxes = new();
        private List<Topico> _topicos = new();

        public FrmGenerarOTM()
        {
            Text = "Seleccionar Tópicos";
            //BackColor = System.Drawing.Color.FromArgb(50, 50, 50);
            //ForeColor = System.Drawing.Color.White;
            Width = 500;
            Height = 400;
            Font = new System.Drawing.Font("Segoe UI", 12);

            var lblTitulo = new Label
            {
                Text = "SELECCIONAR TOPICOS",
                Dock = DockStyle.Top,
                Height = 30,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Font = new System.Drawing.Font(Font, System.Drawing.FontStyle.Underline)
            };
            Controls.Add(lblTitulo);

            var groupBox = new GroupBox
            {
                Left = 45,
                Top = 30,
                Width = 410,
                Height = 200,
                //BackColor = System.Drawing.Color.FromArgb(40, 40, 40),
                //ForeColor = System.Drawing.Color.White
            };
            Controls.Add(groupBox);

            var panel = new Panel
            {
                /*
                Dock = DockStyle.Fill,
                //BackColor = System.Drawing.Color.FromArgb(40, 40, 40),
                AutoScroll = true
                */
                Left = 10,
                Top = 20,
                Width = groupBox.Width - 20,
                Height = groupBox.Height - 30,
                AutoScroll = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };
            groupBox.Controls.Add(panel);

            using var db = new AppDbContext();
            _topicos = db.Topicos.OrderBy(t => t.Id).ToList();

            int y = 10;
            foreach (var topico in _topicos)
            {
                var panelItem = new Panel
                {
                    /*
                    Left = 10,
                    Top = y,
                    Width = 400,
                    Height = 45,
                    //BackColor = System.Drawing.Color.FromArgb(90, 90, 90)
                    */
                    Left = 0,
                    Top = y,
                    Width = panel.Width - 20,
                    Height = 25
                };

                var lbl = new Label
                {
                    Text = topico.Nombre,
                    Left = 10,
                    Top = 5,
                    Width = 300,
                    Height = 20,
                    Font = new System.Drawing.Font(Font.FontFamily, 10, System.Drawing.FontStyle.Bold)
                };
                panelItem.Controls.Add(lbl);

                var chk = new CheckBox
                {
                    //Checked = topico.Seleccionado,
                    //Left = 350,
                    //Top = 10,
                    //Width = 30,
                    //Height = 25,
                    //Appearance = Appearance.Button,
                    //FlatStyle = FlatStyle.Flat,
                    //Text = "✔",
                    //TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                    //Font = new System.Drawing.Font(Font, System.Drawing.FontStyle.Bold)

                    Checked = topico.Seleccionado,
                    Left = 350,
                    Top = 5,
                    Width = 30,
                    Height = 20,
                    //BackColor = System.Drawing.Color.FromArgb(90, 90, 90),
                    ForeColor = System.Drawing.Color.White
                    // Sin Appearance, Text ni TextAlign
                };
                panelItem.Controls.Add(chk);
                _checkBoxes.Add(chk);

                panel.Controls.Add(panelItem);
                y += 30;
            }

            var btnGuardar = new Button
            {
                Text = "Generar OTM",
                Width = 150,
                Height = 40,
                Top = groupBox.Bottom + 10,
                Left = (Width - 150) / 2
            };
            btnGuardar.Click += BtnGuardar_Click;
            Controls.Add(btnGuardar);
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            var seleccionados = _topicos
                .Where((t, i) => _checkBoxes[i].Checked)
                .ToList();

            if (!seleccionados.Any())
            {
                MessageBox.Show("Seleccione al menos un tópico.");
                return;
            }

            using var db = new AppDbContext();
            var otm = new OTM
            {
                Fecha = DateTime.Now,
                Descripcion = "OTM generada desde formulario",
                OTMTopicos = seleccionados.Select(t => new OTMTopico { TopicoId = t.Id }).ToList()
            };
            db.OTMs.Add(otm);
            db.SaveChanges();

            MessageBox.Show("OTM generada correctamente.");
            Close();
        }
    }
}
