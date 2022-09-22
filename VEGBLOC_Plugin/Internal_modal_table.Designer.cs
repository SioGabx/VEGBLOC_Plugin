namespace VEGBLOC_Plugin
{
    partial class Internal_modal_table
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Internal_modal_table));
            this.datatable = new System.Windows.Forms.DataGridView();
            this.datatable_nom_latin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datatable_veg_largeur = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datatable_veg_hauteur = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datatable_vegetation_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datatable_color = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.start_from_data = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.datatable)).BeginInit();
            this.SuspendLayout();
            // 
            // datatable
            // 
            this.datatable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datatable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.datatable_nom_latin,
            this.datatable_veg_largeur,
            this.datatable_veg_hauteur,
            this.datatable_vegetation_type,
            this.datatable_color});
            this.datatable.Location = new System.Drawing.Point(0, 41);
            this.datatable.Name = "datatable";
            this.datatable.RowHeadersWidth = 10;
            this.datatable.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.datatable.RowTemplate.Height = 24;
            this.datatable.Size = new System.Drawing.Size(916, 412);
            this.datatable.TabIndex = 0;
            this.datatable.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.datatable_CellValueChanged);
            this.datatable.CurrentCellChanged += new System.EventHandler(this.datatable_CurrentCellChanged);
            this.datatable.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.datatable_RowsAdded);
            this.datatable.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.datatable_RowsRemoved);
            this.datatable.Sorted += new System.EventHandler(this.datatable_Sorted);
            this.datatable.KeyDown += new System.Windows.Forms.KeyEventHandler(this.datatable_KeyDown);
            // 
            // datatable_nom_latin
            // 
            this.datatable_nom_latin.HeaderText = "Nom Latin";
            this.datatable_nom_latin.MinimumWidth = 6;
            this.datatable_nom_latin.Name = "datatable_nom_latin";
            this.datatable_nom_latin.Width = 300;
            // 
            // datatable_veg_largeur
            // 
            this.datatable_veg_largeur.HeaderText = "Largeur";
            this.datatable_veg_largeur.MinimumWidth = 6;
            this.datatable_veg_largeur.Name = "datatable_veg_largeur";
            this.datatable_veg_largeur.Width = 125;
            // 
            // datatable_veg_hauteur
            // 
            this.datatable_veg_hauteur.HeaderText = "Hauteur";
            this.datatable_veg_hauteur.MinimumWidth = 6;
            this.datatable_veg_hauteur.Name = "datatable_veg_hauteur";
            this.datatable_veg_hauteur.Width = 125;
            // 
            // datatable_vegetation_type
            // 
            this.datatable_vegetation_type.HeaderText = "Type de végétation";
            this.datatable_vegetation_type.MinimumWidth = 6;
            this.datatable_vegetation_type.Name = "datatable_vegetation_type";
            this.datatable_vegetation_type.ToolTipText = "ARBRE, ARBUSTE, VIVACE, GRAMINEE, GRIMPANTE";
            this.datatable_vegetation_type.Width = 200;
            // 
            // datatable_color
            // 
            this.datatable_color.HeaderText = "Couleur = R,G,B";
            this.datatable_color.MinimumWidth = 6;
            this.datatable_color.Name = "datatable_color";
            this.datatable_color.ToolTipText = "Par défaut";
            this.datatable_color.Width = 150;
            // 
            // start_from_data
            // 
            this.start_from_data.Location = new System.Drawing.Point(12, 12);
            this.start_from_data.Name = "start_from_data";
            this.start_from_data.Size = new System.Drawing.Size(140, 23);
            this.start_from_data.TabIndex = 1;
            this.start_from_data.Text = "Valider";
            this.start_from_data.UseVisualStyleBackColor = true;
            this.start_from_data.Click += new System.EventHandler(this.start_from_data_Click);
            // 
            // Internal_modal_table
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 454);
            this.Controls.Add(this.start_from_data);
            this.Controls.Add(this.datatable);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(934, 501);
            this.MinimumSize = new System.Drawing.Size(934, 501);
            this.Name = "Internal_modal_table";
            this.Text = "VEGBLOC_FROM_LIST - Créer des blocs";
            ((System.ComponentModel.ISupportInitialize)(this.datatable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView datatable;
        private System.Windows.Forms.Button start_from_data;
        private System.Windows.Forms.DataGridViewTextBoxColumn datatable_nom_latin;
        private System.Windows.Forms.DataGridViewTextBoxColumn datatable_veg_largeur;
        private System.Windows.Forms.DataGridViewTextBoxColumn datatable_veg_hauteur;
        private System.Windows.Forms.DataGridViewTextBoxColumn datatable_vegetation_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn datatable_color;
    }
}