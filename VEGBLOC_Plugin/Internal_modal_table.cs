using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace VEGBLOC_Plugin
{
    public partial class Internal_modal_table : Form
    {
        public Internal_modal_table()
        {
            InitializeComponent();
            
        }

        private void excel_paste_Click(object sender, EventArgs e)
        {

        }

        private void datatable_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                PasteClipboardValue();
                /*
                 DataObject o = (DataObject)Clipboard.GetDataObject();
                 if (o.GetDataPresent(DataFormats.StringFormat))
                 {
                     string[] pastedRows = Regex.Split(o.GetData(DataFormats.StringFormat).ToString().TrimEnd("\r\n".ToCharArray()), "\r");
                     int j = 0;
                     try { j = datatable.CurrentRow.Index; } catch { }
                     foreach (string pastedRow in pastedRows)
                     {
                         DataGridViewRow r = new DataGridViewRow();
                         r.CreateCells(datatable, pastedRow.Split(new char[] { '\t' }));
                         datatable.Rows.Insert(j, r);
                         j++;
                     }
                 }*/
            }

            if (e.KeyCode == Keys.Delete)
            {
                foreach (DataGridViewRow row in datatable.SelectedRows)
                {
                    try
                    {
                        datatable.Rows.RemoveAt(row.Index);
                    }
                    catch { }
                }
            }
        }
        public DataGridView global_datatable;
        private void start_from_data_Click(object sender, EventArgs e)
        {
            global_datatable = datatable;

            foreach (DataGridViewRow row in datatable.Rows)
            {
                string nom_vegetal = (string)row.Cells["datatable_nom_latin"].Value;
                string type_vegetal = (string)row.Cells["datatable_vegetation_type"].Value;
                string color = (string)row.Cells["datatable_color"].Value;
                double hauteur_vegetal;
                try
                {
                    if (row.Cells["datatable_veg_hauteur"].Value == null || row.Cells["datatable_veg_hauteur"].Value.ToString().Trim() == "")
                    {
                        row.Cells["datatable_veg_hauteur"].Value = 0;
                    }
                    hauteur_vegetal = (double)Convert.ToDouble(row.Cells["datatable_veg_hauteur"].Value);
                }
                catch (Exception)
                {
                    MessageBox.Show("Le champ \"Hauteur du végétal\" n'est pas un nombre. Végétal ligne " + row.HeaderCell.Value);
                    return;
                }
                double largeur_vegetal;
                try
                {
                    largeur_vegetal = (double)Convert.ToDouble(row.Cells["datatable_veg_largeur"].Value);
                }
                catch (Exception)
                {
                    MessageBox.Show("Le champ \"Largeur du végétal\" n'est pas un nombre. Végétal ligne " + row.HeaderCell.Value);
                    return;

                }



                if (String.IsNullOrEmpty(nom_vegetal) || String.IsNullOrEmpty(type_vegetal) || String.IsNullOrEmpty(hauteur_vegetal.ToString()) || String.IsNullOrEmpty(largeur_vegetal.ToString()))
                {
                    if (!String.IsNullOrEmpty(nom_vegetal))
                    {
                        if (String.IsNullOrEmpty(largeur_vegetal.ToString()))
                        {
                            MessageBox.Show("Le champ \"Largeur du végétal\" n'est pas remplis. Végétal ligne " + row.HeaderCell.Value);
                            return;
                        }
                        if (largeur_vegetal <= 0)
                        {
                            MessageBox.Show("Le champ \"Largeur du végétal\" ne peut pas être égale à 0. Végétal ligne " + row.HeaderCell.Value);

                            return;
                        }
                        if (String.IsNullOrEmpty(hauteur_vegetal.ToString()))
                        {
                            MessageBox.Show("Le champ \"Hauteur du végétal\" n'est pas remplis. Végétal ligne " + row.HeaderCell.Value);

                            return;
                        }
                        if (String.IsNullOrEmpty(type_vegetal))
                        {
                            MessageBox.Show("Le champ \"Type de végétal\" n'est pas remplis. Végétal ligne " + row.HeaderCell.Value);
                            return;
                        }



                    }


                }
            }

            this.Close();
            DialogResult = DialogResult.OK;
          
        }


        void update_row_number()
        {
            foreach (DataGridViewRow dGVRow in datatable.Rows)
            {
                dGVRow.HeaderCell.Value = String.Format("{0}", dGVRow.Index + 1);
            }

            // This resizes the width of the row headers to fit the numbers
            this.datatable.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
        }

        private void datatable_Sorted(object sender, EventArgs e)
        {
            update_row_number();
        }

        private void datatable_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            update_row_number();
        }

        private void datatable_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            update_row_number();
        }














        private void PasteClipboardValue()
        {
            update_row_number();
            //Show Error if no cell is selected
            if (datatable.SelectedCells.Count == 0)
            {
                MessageBox.Show("Veuillez selectionner une cellule", "Paste",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
          
            //Get the starting Cell
            DataGridViewCell startCell = GetStartCell(datatable);
            //Get the clipboard value in a dictionary
            Dictionary<int, Dictionary<int, string>> cbValue =
                    ClipBoardValues(Clipboard.GetText());
            int iRowIndex = startCell.RowIndex;

            int iRowIndex2 = startCell.RowIndex;
            foreach (int rowKey in cbValue.Keys)
            {
              
                if (!(iRowIndex2 <= datatable.Rows.Count - 1))
                {
                    string[] row = new string[] { "", "", "", "", "" }; ;
                    datatable.Rows.Add(row);
                   
                }
                iRowIndex2++;

            }
            datatable.Refresh();
           


            foreach (int rowKey in cbValue.Keys)
            {
                int iColIndex = startCell.ColumnIndex;
                foreach (int cellKey in cbValue[rowKey].Keys)
                {
                    //Check if the index is within the limit
                    if (iColIndex <= datatable.Columns.Count - 1)
                    {
                       
                        DataGridViewCell cell = datatable[iColIndex, iRowIndex];

                        //Copy to selected cells if 'chkPasteToSelectedCells' is checked
                        //if (cell.IsInEditMode)
                        //    cell.Value = cbValue[rowKey][cellKey];
                        //if (cbValue[rowKey][cellKey].Trim() != "") { 
                        cell.Value = cbValue[rowKey][cellKey];
                        
                        //}
                    }
                    iColIndex++;
                }
                iRowIndex++;
            }
            datatable.Refresh();
            

            //if (datatable.Rows.Count == datatable.RowIndex)
            //{
            //    string[] row = new string[] { "", "", "", "", "" }; ;
            //    datatable.Rows.Insert(datatable.Rows.Count - 1,row);
            //}

        }

        private DataGridViewCell GetStartCell(DataGridView dgView)
        {
            //get the smallest row,column index
            if (dgView.SelectedCells.Count == 0)
                return null;

            int rowIndex = dgView.Rows.Count - 1;
            int colIndex = dgView.Columns.Count - 1;

            foreach (DataGridViewCell dgvCell in dgView.SelectedCells)
            {
                if (dgvCell.RowIndex < rowIndex)
                    rowIndex = dgvCell.RowIndex;
                if (dgvCell.ColumnIndex < colIndex)
                    colIndex = dgvCell.ColumnIndex;
            }

            return dgView[colIndex, rowIndex];
        }

        private Dictionary<int, Dictionary<int, string>> ClipBoardValues(string clipboardValue)
        {
            Dictionary<int, Dictionary<int, string>>
            copyValues = new Dictionary<int, Dictionary<int, string>>();

            String[] lines = clipboardValue.Split('\n');

            for (int i = 0; i <= lines.Length - 2; i++)
            {
                copyValues[i] = new Dictionary<int, string>();
                String[] lineContent = lines[i].Split('\t');

                //if an empty cell value copied, then set the dictionary with an empty string
                //else Set value to dictionary
                if (lineContent.Length == 0)
                    copyValues[i][0] = string.Empty;
                else
                {
                    for (int j = 0; j <= lineContent.Length - 1; j++)
                        copyValues[i][j] = lineContent[j];
                }
            }
            return copyValues;
        }

        private void datatable_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void datatable_CurrentCellChanged(object sender, EventArgs e)
        {
        }
    }
}
