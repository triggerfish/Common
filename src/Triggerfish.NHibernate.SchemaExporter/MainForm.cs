using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace Triggerfish.NHibernate.SchemaExporter
{
	public partial class MainForm : Form
	{
		private List<ISchemaExporter> m_exporters = new List<ISchemaExporter>();
		private Dictionary<int, IList<string>> m_parameters = new Dictionary<int, IList<string>>();

		protected int SelectedExporter 
		{ 
			get { return cboConfigurations.SelectedIndex; }
			set { cboConfigurations.SelectedIndex = value; }
		}
		protected int SelectedParameter
		{
			get { return cboParameters.SelectedIndex; }
			set { cboParameters.SelectedIndex = value; }
		}

		public MainForm()
		{
			InitializeComponent();
		}

		private void cmdBrowse_Click(object sender, EventArgs e)
		{
			if (DialogResult.OK != dlgFileOpen.ShowDialog())
			{
				return;
			}

			txtAssembly.Text = Path.GetFileName(dlgFileOpen.FileName);

			try
			{
				Assembly asm = Assembly.LoadFile(dlgFileOpen.FileName);

				Type[] types = null;

				try
				{
					types = asm.GetTypes();
				}
				catch (ReflectionTypeLoadException rex)
				{
					types = rex.Types;
				}

				IEnumerable<Type> exporters =
					from t in types
					where t != null && t.IsClass && null != t.GetInterface("Triggerfish.NHibernate.ISchemaExporter")
					select t;

				if (exporters.Any())
				{
					cboConfigurations.Items.Clear();
					txtScript.Text = "";
				}

				foreach (Type t in exporters)
				{
					ISchemaExporter exporter = (ISchemaExporter)asm.CreateInstance(t.FullName);

					if (exporter != null)
					{
						m_exporters.Add(exporter);

						if (exporter.ParameterNames.Any())
						{
							List<string> parameters = new List<string>();
							for (int i = 0; i < exporter.ParameterNames.Count; i++)
							{
								parameters.Add("");
							}
							m_parameters.Add(m_exporters.Count - 1, parameters);
						}
					}
				}

				cboConfigurations.Items.AddRange(m_exporters.Select(x => x.Name).ToArray());

				if (m_exporters.Count > 0)
				{
					SelectedExporter = 0;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Exception thrown: \n\n" + ex.ToString(), "Exception!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
		}

		private void cmdExport_Click(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;

			if (SelectedExporter < 0 || m_exporters.Count <= SelectedExporter)
			{
				MessageBox.Show("Select something!", "Idiot!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			try
			{
				ISchemaExporter exporter = m_exporters[SelectedExporter];
				StringWriter sw = new StringWriter();
				exporter.GenerateScript(sw.WriteLine, m_parameters[SelectedExporter]);
				txtScript.Text = sw.ToString();
			}
			catch (FileNotFoundException fex)
			{
				MessageBox.Show("Cannot load find dependent assemblies\n\n" + fex.Message, "Exception!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Exception thrown: \n\n" + ex.ToString(), "Exception!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			finally
			{
				Cursor = Cursors.Default;
			}
		}

		private void cboConfigurations_SelectedIndexChanged(object sender, EventArgs e)
		{
			cboParameters.Items.Clear();
			txtParameter.Text = "";

			ISchemaExporter exporter = m_exporters[SelectedExporter];

			bool any = exporter.ParameterNames.Any();
			if (any)
			{
				cboParameters.Items.AddRange(exporter.ParameterNames.ToArray());
				SelectedParameter = 0;
			}

			lblParameters.Visible = any;
			cboParameters.Visible = any;
			txtParameter.Visible = any;
		}

		private void cboParameters_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtParameter.Text = m_parameters[SelectedExporter][SelectedParameter];
		}

		private void txtParamater_Leave(object sender, EventArgs e)
		{
			if (txtParameter.Visible)
			{
				m_parameters[SelectedExporter][SelectedParameter] = txtParameter.Text;
			}
		}
	}
}
