namespace VEGBLOC_Plugin
{
    partial class Internal_modal
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Internal_modal));
            this.button_confirm = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.input_name_vegetal = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.input_largeur_vegetal = new System.Windows.Forms.TextBox();
            this.input_hauteur_vegetal = new System.Windows.Forms.TextBox();
            this.input_vegetation_type = new System.Windows.Forms.ComboBox();
            this.input_create_legende_vegetal = new System.Windows.Forms.CheckBox();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.chargerDepuisUneFeuilleExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_confirm
            // 
            this.button_confirm.Location = new System.Drawing.Point(263, 198);
            this.button_confirm.Name = "button_confirm";
            this.button_confirm.Size = new System.Drawing.Size(136, 30);
            this.button_confirm.TabIndex = 4;
            this.button_confirm.Text = "Valider";
            this.button_confirm.UseVisualStyleBackColor = true;
            this.button_confirm.Click += new System.EventHandler(this.button_confirm_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.Location = new System.Drawing.Point(405, 198);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(136, 30);
            this.button_cancel.TabIndex = 5;
            this.button_cancel.Text = "Annuler";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // input_name_vegetal
            // 
            this.input_name_vegetal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.input_name_vegetal.ContextMenuStrip = this.contextMenu;
            this.input_name_vegetal.Location = new System.Drawing.Point(15, 41);
            this.input_name_vegetal.Name = "input_name_vegetal";
            this.input_name_vegetal.Size = new System.Drawing.Size(526, 22);
            this.input_name_vegetal.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(211, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Nom latin : Genre espèce \'Cultivar\'";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Type de végétation :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Largeur :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(286, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Hauteur :";
            // 
            // input_largeur_vegetal
            // 
            this.input_largeur_vegetal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.input_largeur_vegetal.ContextMenuStrip = this.contextMenu;
            this.input_largeur_vegetal.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.input_largeur_vegetal.Location = new System.Drawing.Point(15, 157);
            this.input_largeur_vegetal.Name = "input_largeur_vegetal";
            this.input_largeur_vegetal.Size = new System.Drawing.Size(252, 22);
            this.input_largeur_vegetal.TabIndex = 2;
            // 
            // input_hauteur_vegetal
            // 
            this.input_hauteur_vegetal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.input_hauteur_vegetal.ContextMenuStrip = this.contextMenu;
            this.input_hauteur_vegetal.Location = new System.Drawing.Point(289, 157);
            this.input_hauteur_vegetal.Name = "input_hauteur_vegetal";
            this.input_hauteur_vegetal.Size = new System.Drawing.Size(252, 22);
            this.input_hauteur_vegetal.TabIndex = 3;
            // 
            // input_vegetation_type
            // 
            this.input_vegetation_type.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.input_vegetation_type.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.input_vegetation_type.ContextMenuStrip = this.contextMenu;
            this.input_vegetation_type.FormattingEnabled = true;
            this.input_vegetation_type.Items.AddRange(new object[] {
            "ARBRE",
            "ARBUSTE",
            "VIVACE",
            "GRAMINEE",
            "GRIMPANTE"});
            this.input_vegetation_type.Location = new System.Drawing.Point(15, 97);
            this.input_vegetation_type.Name = "input_vegetation_type";
            this.input_vegetation_type.Size = new System.Drawing.Size(526, 24);
            this.input_vegetation_type.TabIndex = 1;
            this.input_vegetation_type.Text = "ARBUSTE";
            this.input_vegetation_type.SelectedIndexChanged += new System.EventHandler(this.input_vegetation_type_SelectedIndexChanged);
            this.input_vegetation_type.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.vegetation_type_KeyPress);
            // 
            // input_create_legende_vegetal
            // 
            this.input_create_legende_vegetal.AutoSize = true;
            this.input_create_legende_vegetal.Checked = true;
            this.input_create_legende_vegetal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.input_create_legende_vegetal.Location = new System.Drawing.Point(15, 204);
            this.input_create_legende_vegetal.Name = "input_create_legende_vegetal";
            this.input_create_legende_vegetal.Size = new System.Drawing.Size(145, 20);
            this.input_create_legende_vegetal.TabIndex = 6;
            this.input_create_legende_vegetal.Text = "Generer la légende";
            this.input_create_legende_vegetal.UseVisualStyleBackColor = true;
            this.input_create_legende_vegetal.CheckedChanged += new System.EventHandler(this.input_create_legende_vegetal_CheckedChanged);
            // 
            // contextMenu
            // 
            this.contextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chargerDepuisUneFeuilleExcelToolStripMenuItem});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(235, 28);
            // 
            // chargerDepuisUneFeuilleExcelToolStripMenuItem
            // 
            this.chargerDepuisUneFeuilleExcelToolStripMenuItem.Name = "chargerDepuisUneFeuilleExcelToolStripMenuItem";
            this.chargerDepuisUneFeuilleExcelToolStripMenuItem.Size = new System.Drawing.Size(234, 24);
            this.chargerDepuisUneFeuilleExcelToolStripMenuItem.Text = "Ajouter depuis une liste";
            this.chargerDepuisUneFeuilleExcelToolStripMenuItem.Click += new System.EventHandler(this.chargerDepuisUneFeuilleExcelToolStripMenuItem_Click);
            // 
            // Internal_modal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 240);
            this.ContextMenuStrip = this.contextMenu;
            this.Controls.Add(this.input_create_legende_vegetal);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.input_hauteur_vegetal);
            this.Controls.Add(this.input_largeur_vegetal);
            this.Controls.Add(this.input_name_vegetal);
            this.Controls.Add(this.input_vegetation_type);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_confirm);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximumSize = new System.Drawing.Size(565, 287);
            this.MinimumSize = new System.Drawing.Size(565, 287);
            this.Name = "Internal_modal";
            this.Text = "VEGBLOC - Créer un bloc";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Internal_modal_FormClosing);
            this.Load += new System.EventHandler(this.Internal_modal_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Internal_modal_KeyDown);
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_confirm;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.TextBox input_name_vegetal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox input_largeur_vegetal;
        private System.Windows.Forms.TextBox input_hauteur_vegetal;
        private System.Windows.Forms.ComboBox input_vegetation_type;
        private System.Windows.Forms.CheckBox input_create_legende_vegetal;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem chargerDepuisUneFeuilleExcelToolStripMenuItem;
    }
}