using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace VEGBLOC_Plugin
{
    public partial class Internal_modal : Form
    {
        public string nom_vegetal;
        public string type_vegetal;
        public double hauteur_vegetal;
        public double largeur_vegetal;
        public bool create_legende = true;
        MyUserSettings mus;

        public Internal_modal()
        {
            InitializeComponent();
            button_cancel.DialogResult = DialogResult.Cancel;

        }

        private void Internal_modal_Load(object sender, EventArgs e)
        {
            nom_vegetal = "Defaut";
            type_vegetal = "ARBUSTE";
            hauteur_vegetal = 1;
            largeur_vegetal = 1;
            input_vegetation_type.DataSource = input_vegetation_type.Items;

            //input_create_legende_vegetal.ContextMenu = ContextMenu;
            //input_hauteur_vegetal.ContextMenu = ContextMenu;
            //input_largeur_vegetal.ContextMenu = ContextMenu;
            //input_name_vegetal.ContextMenu = ContextMenu;
            //input_vegetation_type.ContextMenu = ContextMenu;
            //this.ContextMenu = ContextMenu;


            //this.DataBindings.Add(new Binding("SelectedIndex", mus, "SelectedIndex"));
            mus = new MyUserSettings();
            try
            {
                if (Convert.ToInt32(mus.index) > -1)
                {
                    // vegetation_type.SelectedIndex = Convert.ToInt32(mus.SelectedIndex);
                    input_vegetation_type.SelectedIndex = Convert.ToInt32(mus.index);
                }
            }
            catch (Exception)
            {
                input_vegetation_type.SelectedIndex = 0;
            }

            try
            {
                if (!String.IsNullOrEmpty(mus.legende))
            {
                    if (Convert.ToBoolean(mus.legende))
                    {
                        input_create_legende_vegetal.Checked =true;
                    }
                    else
                    {
                        input_create_legende_vegetal.Checked =false;    
                    }
                
            }
            }
            catch (Exception)
            {
                input_create_legende_vegetal.Checked = true;
            }

        }

        private void button_confirm_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(input_hauteur_vegetal.Text))
            {
                input_hauteur_vegetal.Text = "0";
                
            }
            if (String.IsNullOrEmpty(input_vegetation_type.Text) || String.IsNullOrEmpty(input_name_vegetal.Text) || String.IsNullOrEmpty(input_largeur_vegetal.Text) || String.IsNullOrEmpty(input_hauteur_vegetal.Text))
            {
                MessageBox.Show("Tout les champs ne sont pas remplis !");
                if (String.IsNullOrEmpty(input_vegetation_type.Text))
                {
                    input_vegetation_type.Focus();
                    return;
                }
                if (String.IsNullOrEmpty(input_name_vegetal.Text))
                {
                    input_name_vegetal.Focus();
                    return;
                }
                if (String.IsNullOrEmpty(input_largeur_vegetal.Text))
                {
                    input_largeur_vegetal.Focus();
                    return;
                }
                if (String.IsNullOrEmpty(input_hauteur_vegetal.Text))
                {
                    input_hauteur_vegetal.Focus();
                    return;
                }

                
            }
            if (!double.TryParse(input_largeur_vegetal.Text, out double largeur_vegetal_parse))
            {
                MessageBox.Show("La largeur du végétal n'est pas correct");
                input_largeur_vegetal.Focus();
                return;
            }

            if (largeur_vegetal_parse <= 0)
            {
                MessageBox.Show("La largeur du végétal ne peut pas être égale à 0");
                input_largeur_vegetal.Focus();
                return;
            }

            if (!double.TryParse(input_hauteur_vegetal.Text, out double hauteur_vegetal_parse))
            {
                MessageBox.Show("La hauteur du végétal n'est pas correct");
                input_hauteur_vegetal.Focus();
                return;
            }



            nom_vegetal = input_name_vegetal.Text;
            type_vegetal = input_vegetation_type.Text;
            hauteur_vegetal = hauteur_vegetal_parse;
            largeur_vegetal = largeur_vegetal_parse / 2;


            DialogResult = DialogResult.OK;
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void vegetation_type_KeyPress(object sender, KeyPressEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            cb.DroppedDown = true;
            string strFindStr;
            if (e.KeyChar == (char)8)
            {
                if (cb.SelectionStart <= 1)
                {
                    cb.Text = "";
                    return;
                }

                if (cb.SelectionLength == 0)
                    strFindStr = cb.Text.Substring(0, cb.Text.Length - 1);
                else
                    strFindStr = cb.Text.Substring(0, cb.SelectionStart - 1);
            }
            else
            {
                if (cb.SelectionLength == 0)
                    strFindStr = cb.Text + e.KeyChar;
                else
                    strFindStr = cb.Text.Substring(0, cb.SelectionStart) + e.KeyChar;
            }
            int intIdx = -1;
            // Search the string in the ComboBox list.
            intIdx = cb.FindString(strFindStr);
            if (intIdx != -1)
            {
                cb.SelectedText = "";
                cb.SelectedIndex = intIdx;
                cb.SelectionStart = strFindStr.Length;
                cb.SelectionLength = cb.Text.Length;
                e.Handled = true;
            }
            else
                e.Handled = true;

        }


        private void Internal_modal_FormClosing(object sender, FormClosingEventArgs e)
        {
            mus = new MyUserSettings();
            mus.index = input_vegetation_type.SelectedIndex.ToString();
            if (input_create_legende_vegetal.Checked)
            {
                create_legende = true;
            }
            else
            {
                create_legende = false;
            }
            mus.legende = create_legende.ToString();
            mus.Save();
        }

        private void Internal_modal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, true, true, true, true);
            }

            if (e.KeyData == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void input_create_legende_vegetal_CheckedChanged(object sender, EventArgs e)
        {
           if (input_create_legende_vegetal.Checked)
            {
                create_legende = true;
            }
            else
            {
                create_legende = false;
            }
        }

        private void chargerDepuisUneFeuilleExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Internal internal_class = new Internal();
            internal_class.Auto_Create_VEG_Block();
            
            
        }

        private void input_vegetation_type_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
