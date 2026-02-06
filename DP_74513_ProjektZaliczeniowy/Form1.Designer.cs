namespace DP_74513_ProjektZaliczeniowy
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel WorkPanel;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.WorkPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            this.WorkPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WorkPanel.Name = "WorkPanel";
            this.WorkPanel.TabIndex = 0;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 750);
            this.Controls.Add(this.WorkPanel);
            this.Name = "Form1";
            this.Text = "74513-G1-ProjektZaliczeniowy";
            this.ResumeLayout(false);
        }

        #endregion
    }
}

