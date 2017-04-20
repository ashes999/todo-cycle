using System;
using System.Configuration;
using System.Reflection;
using System.Windows.Forms;

namespace TodoCycle.DataAccess.Migrations.Runner
{
    public partial class MainForm : Form
    {
        private const string ConnectionStringName = "DefaultConnection";
        private const string MigrationsAssemblyName = "TodoCycle.DataAccess.Migrations";

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnMigrate_Click(object sender, EventArgs e)
        {
            this.txtStatus.Clear();

            try
            {
                var assembly = Assembly.Load(MigrationsAssemblyName);
                var connectionString = ConfigurationManager.ConnectionStrings[ConnectionStringName];

                var migrator = new MigrationsWrapper(assembly, connectionString);
                migrator.MigrateToLatestVersion();

                this.txtStatus.Text = migrator.Output;
            }
            catch (Exception ex)
            {
                this.txtStatus.Text = ex.ToString();
            }
        }
    }
}
