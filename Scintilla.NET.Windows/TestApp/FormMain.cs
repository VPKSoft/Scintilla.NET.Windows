using ScintillaNet.WinForms;

namespace TestApp;


public partial class FormMain : Form
{
    public FormMain()
    {
        InitializeComponent();
        var scintilla = new Scintilla { Dock = DockStyle.Fill, };

        Controls.Add(scintilla);
    }
}
